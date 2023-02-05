using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Features.Articles.Models;

[Table("article_authors")]
public class ArticleAuthor
{
    [Column("id", TypeName = "integer")]
    [Required]
    public int Id { get; set; }

    [Column("article_id")]
    [Required]
    [ForeignKey("Article")]
    public int ArticleId { get; set; }

    [Column("fio", TypeName = "varchar(200)")]
    public string FIO { get; set; }

    public Article Article { get; set; }
}