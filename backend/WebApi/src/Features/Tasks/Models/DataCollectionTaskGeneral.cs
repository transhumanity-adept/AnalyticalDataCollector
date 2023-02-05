using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using WebApi.Features.Articles.Models;
using WebApi.Features.Tasks.Contracts;
using WebApi.Mapping;

namespace WebApi.Features.Tasks.Models;

[Table("data_collection_task_general")]
public class DataCollectionTaskGeneral : 
    IMapTo<DataCollectionTaskGeneralDto>,
    IMapTo<IntervalDataCollectionTaskDto>
{
    [Column("id", TypeName = "text")] 
    public Guid Id { get; set; }

    [Column("create_date", TypeName = "timestamp")]
    public DateTime CreateDate { get; set; }

    public ICollection<Article> Articles { get; set; }

    #region Mappings

    void IMapTo<DataCollectionTaskGeneralDto>.Map(Profile profile)
    {
        profile.CreateMap<DataCollectionTaskGeneral, DataCollectionTaskGeneralDto>()
            .ForMember(x => x.Id,
                y =>
                    y.MapFrom(z => z.Id))
            .ForMember(x => x.CreateDate,
                y =>
                    y.MapFrom(z => z.CreateDate))
            .ForAllMembers(x => x.Ignore());
    }
    
    void IMapTo<IntervalDataCollectionTaskDto>.Map(Profile profile)
    {
        profile.CreateMap<DataCollectionTaskGeneral, IntervalDataCollectionTaskDto>()
            .ForMember(x => x.General,
                y =>
                    y.MapFrom((
                        src,
                        _,
                        _,
                        context
                        ) => context.Mapper.Map<DataCollectionTaskGeneralDto>(src)))
            .ForAllMembers(x => x.Ignore());
    }

    #endregion
}