namespace WebApi.Features.Tasks.Contracts;

public class IntervalDataCollectionTaskDto
{
    public DataCollectionTaskGeneralDto General { get; set; }

    public int FromArticleId { get; set; }

    public int ToArticleId { get; set; }
}