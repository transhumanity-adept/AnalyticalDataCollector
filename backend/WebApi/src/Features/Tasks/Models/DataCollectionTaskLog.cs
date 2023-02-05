using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Features.Tasks.Contracts;
using WebApi.Mapping;

namespace WebApi.Features.Tasks.Models;

[Table("data_collection_task_logs")]
public class DataCollectionTaskLog : IMapTo<DataCollectionTaskLogDto>
{
    [Column("id", TypeName = "integer")]
    [Required]
    public int Id { get; set; }

    [Column("log_type", TypeName = "varchar(50)")]
    [Required]
    public string LogType { get; set; }

    [Column("message", TypeName = "text")]
    [Required]
    public string Message { get; set; }
}