#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models;

[Table("data_collection_task")]
public class DataCollectionTask
{
    [Column("id", TypeName = "text")]
    public Guid Id { get; set; }

    [Column("file_path", TypeName = "text")]
    public string FilePath { get; set; }

    [Column("start_id", TypeName = "integer")]
    public int StartId { get; set; }

    [Column("end_id", TypeName = "integer")]
    public int EndId { get; set; }
}