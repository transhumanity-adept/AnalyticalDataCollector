using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Features.Articles.Models;

[Table("article_organizations")]
public class ArticleOrganization
{
    [Column("id", TypeName = "integer")]
    [Required]
    public int Id { get; set; }

    [Column("article_id")]
    [Required]
    [ForeignKey("Article")]
    public int ArticleId { get; set; }

    [Column("name", TypeName = "text")]
    public string Name { get; set; }

    public Article Article { get; set; }
}