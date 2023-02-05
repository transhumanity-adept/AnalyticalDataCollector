using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Features.Tasks.Contracts;
using WebApi.Mapping;

namespace WebApi.Features.Tasks.Models;

[Table("interval_data_collection_task")]
public class IntervalDataCollectionTask : 
    IMapTo<>
{
    [ForeignKey("General")]
    public Guid Id { get; set; }

    public int FromArticleId { get; set; }

    public int ToArticleId { get; set; }
    
    public DataCollectionTaskGeneralDto General { get; set; }
}