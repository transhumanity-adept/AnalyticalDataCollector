using System.Text.Json.Serialization;
using AutoMapper;
using MediatR;
using WebApi.Features.Shared;
using WebApi.Features.Tasks.Contracts;
using WebApi.Features.Tasks.Models;
using WebApi.Mapping;

namespace WebApi.Features.Tasks.Commands;

public class CreateIntervalDataCollectionTaskCommand : 
    IRequest<RequestExecutionResult<IntervalDataCollectionTaskDto>>,
    IMapTo<DataCollectionTaskGeneral>,
    IMapTo<IntervalDataCollectionTask>
{
    [JsonPropertyName("fromArticleId")]
    public int FromArticleId { get; set; }
    
    [JsonPropertyName("toArticleId")]
    public int ToArticleId { get; set; }

    #region Mappings
    void IMapTo<DataCollectionTaskGeneral>.Map(Profile profile)
    {
        profile.CreateMap<CreateIntervalDataCollectionTaskCommand, DataCollectionTaskGeneral>()
            .ForMember(x => x.Id,
                y =>
                    y.MapFrom(z => Guid.Empty))
            .ForMember(x => x.CreateDate,
                y =>
                    y.MapFrom(z => DateTime.Now));
    }

    void IMapTo<IntervalDataCollectionTask>.Map(Profile profile)
    {
        profile.CreateMap<CreateIntervalDataCollectionTaskCommand, IntervalDataCollectionTask>()
            .ForMember(x => x.Id,
                y =>
                    y.MapFrom(z => Guid.Empty))
            .ForMember(x => x.FromArticleId,
                y =>
                    y.MapFrom(z => z.FromArticleId))
            .ForMember(x => x.ToArticleId,
                y =>
                    y.MapFrom(z => z.ToArticleId));
    }
    #endregion
}