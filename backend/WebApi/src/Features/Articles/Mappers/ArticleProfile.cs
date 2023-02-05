using AutoMapper;
using WebApi.Features.Articles.Models;
using ArticleElibrary = Elibrary.Contracts.Article;
using ArticleModel = WebApi.Features.Articles.Models.Article;

namespace WebApi.Features.Articles.Mappers;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<ArticleElibrary, ArticleModel>()
            .ForMember(x => x.Id, 
                y => y.MapFrom(z => z.Id))
            .ForMember(x => x.EDN, 
                y => y.MapFrom(z => z.EDN))
            .ForMember(x => x.Title, 
                y => y.MapFrom(z => z.Title))
            .ForMember(x => x.Type, 
                y => y.MapFrom(z => z.Type))
            .ForMember(x => x.Language, 
                y => y.MapFrom(z => z.Language))
            .ForMember(x => x.Year, 
                y => y.MapFrom(z => z.Year))
            .ForMember(x => x.Annotation, 
                y => y.MapFrom(z => z.Annotation))
            .ForMember(x => x.RubricGRNTI, 
                y => y.MapFrom(z => z.BibliometricIndicators.RubricGRNTI))
            .ForMember(x => x.ThematicArea, 
                y => y.MapFrom(z => z.BibliometricIndicators.ThematicArea))
            .ForMember(x => x.InRSCI, 
                y => y.MapFrom(z => z.BibliometricIndicators.InRSCI))
            .ForMember(x => x.NumberOfCitationsInRSCI,
                y => y.MapFrom(z => z.BibliometricIndicators.NumberOfCitationsInRSCI))
            .ForMember(x => x.InCoreRSCI, 
                y => y.MapFrom(z => z.BibliometricIndicators.InCoreRSCI))
            .ForMember(x => x.NumberOfCitationsInCoreRSCI,
                y => y.MapFrom(z => z.BibliometricIndicators.NumberOfCitationsInCoreRSCI))
            .ForMember(x => x.Views, 
                y => y.MapFrom(z => z.Altmetrics.Views))
            .ForMember(x => x.NumberUploads, 
                y => y.MapFrom(z => z.Altmetrics.NumberUploads));
    }
}