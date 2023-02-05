namespace Elibrary.Contracts;

public class Article
{
    public int Id { get; set; }
    public string? EDN { get; set; }
    public string? Title { get; set; }
    public IEnumerable<string> Authors { get; set; } = Array.Empty<string>();
    public IEnumerable<string> OrgNames { get; set; } = Array.Empty<string>();
    public string? Type { get; set; }
    public string? Language { get; set; }
    public string? Year { get; set; }
    public IEnumerable<string> KeyWords { get; set; } = Array.Empty<string>();
    public string? Annotation { get; set; }
    public BibliometricIndicators BibliometricIndicators { get; set; } = new BibliometricIndicators();
    public Altmetrics Altmetrics { get; set; } = new Altmetrics();
}