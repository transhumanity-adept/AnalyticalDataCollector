using Elibrary.Contracts;
using HtmlAgilityPack;

namespace Elibrary;

public class ElibraryParser : IElibraryParser
{
    private const string REQUEST_ADDRESS = "https://elibrary.ru/item.asp?id={0}";
    public async Task<ParseResponse<Article>> GetArticleByIdAsync(int articleId)
    {
        try
        {
            using var client = new HttpClient();
            var articleContent = await (await client.GetAsync(string.Format(REQUEST_ADDRESS, articleId))).Content.ReadAsStringAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(articleContent);
            var scripts = doc.DocumentNode.SelectNodes("//script");
            if (scripts is not null)
            {
                foreach (var script in scripts)
                {
                    script.RemoveAll();
                    script.Remove();
                }
            }

            var endNode = doc.DocumentNode.SelectNodes("//td").FirstOrDefault(x => x.FirstChild?.InnerText.Contains("EDN:") ?? false);
            var end = endNode?.SelectSingleNode(".//a").InnerText;

            var title = doc.DocumentNode
                .SelectSingleNode("//p[@class='bigtext']")
                .InnerText;

            var div = doc.DocumentNode.SelectSingleNode("//div[@style[contains(.,'width:580px; margin:0; border:0; padding:0;')]]");
            var a = div.ChildNodes[2].SelectNodes(".//div[@style[contains(.,'display: inline-block; white-space: nowrap')]]//font[@color='#00008f']");

            var authors = doc.DocumentNode
                .SelectNodes("//span[@class='help pointer']//font")
                .Select(x => x.InnerText);

            var organizations = doc.DocumentNode
                .SelectNodes("//span[@class='help1 pointer']//font")
                .Select(x => x.InnerText);

            var typeNode = doc.DocumentNode.SelectSingleNode("//text()[contains(.,'Тип:')]");
            var typeNodeIndex = typeNode.ParentNode.ChildNodes.IndexOf(typeNode);
            var type = typeNode.ParentNode.ChildNodes[typeNodeIndex + 1].InnerText;

            var languageNode = doc.DocumentNode.SelectSingleNode("//text()[contains(.,'Язык:')]");
            var languageNodeIndex = languageNode.ParentNode.ChildNodes.IndexOf(languageNode);
            var language = languageNode.ParentNode.ChildNodes[languageNodeIndex + 1].InnerText;

            var yearNode = doc.DocumentNode.SelectSingleNode("//text()[contains(.,'Год:') or contains(.,'Год издания:') or contains(.,'Год публикации:')]");
            var yearNodeIndex = yearNode.ParentNode.ChildNodes.IndexOf(yearNode);
            var year = yearNode.ParentNode.ChildNodes[yearNodeIndex + 1].InnerText;

            var keyWords = doc.DocumentNode.SelectNodes("//a[@href[contains(.,'keyword_items')]]").Select(x => x.InnerText);

            var annotationNode = doc.DocumentNode.SelectSingleNode("//div[@id='abstract2']");
            annotationNode ??= doc.DocumentNode.SelectSingleNode("//div[@id='abstract1']");
            var annonation = annotationNode.FirstChild.InnerText;

            var grntiRubric = doc.DocumentNode.SelectSingleNode("//span[@id='rubric_grnti']").FirstChild.InnerText;
            var thematicArea = doc.DocumentNode.SelectSingleNode("//span[@id='rubric_oecd']").FirstChild.InnerText;

            var inRSCI = doc.DocumentNode.SelectSingleNode("//td[text()[contains(.,'Входит в РИНЦ')]]//font").InnerText.Trim() == "да";
            var numberOfCitationsInRSCI = int.Parse(doc.DocumentNode.SelectSingleNode("//td[text()[contains(.,'Цитирований в РИНЦ')]]//font").InnerText.Trim());
            var inCoreRSCI = doc.DocumentNode.SelectSingleNode("//td[text()[contains(.,'Входит в ядро РИНЦ')]]//font").InnerText.Trim() == "да";
            var numberOfCitationsInCoreRSCI = int.Parse(doc.DocumentNode.SelectSingleNode("//td[text()[contains(.,'Цитирований из ядра РИНЦ')]]//font").InnerText.Trim());

            var views = int.Parse(doc.DocumentNode.SelectSingleNode("//td[text()[contains(.,'Просмотров:')]]//font").InnerText.Split(' ')[0].Trim());
            var uploads = int.Parse(doc.DocumentNode.SelectSingleNode("//td[text()[contains(.,'Загрузок:')]]//font").InnerText.Split(' ')[0].Trim());

            var article = new Article()
            {
                Id = articleId,
                EDN = end,
                Title = title,
                Authors = authors,
                OrgNames = organizations,
                Type = type,
                Language = language,
                Year = year,
                KeyWords = keyWords,
                Annotation = annonation,
                BibliometricIndicators = new()
                {
                    RubricGRNTI = grntiRubric,
                    ThematicArea = thematicArea,
                    InRSCI = inRSCI,
                    NumberOfCitationsInRSCI = numberOfCitationsInRSCI,
                    InCoreRSCI = inCoreRSCI,
                    NumberOfCitationsInCoreRSCI = numberOfCitationsInCoreRSCI
                },
                Altmetrics = new()
                {
                    Views = views,
                    NumberUploads = uploads
                }
            };
            return ParseResponse<Article>.CreateOkResult(article);
        }
        catch (Exception e)
        {
            return ParseResponse<Article>.CreateBadResult(e.Message);
        }
    }
}