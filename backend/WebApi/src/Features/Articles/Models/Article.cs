using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Features.Tasks.Models;

namespace WebApi.Features.Articles.Models;

[Table("articles")]
public class Article
{
    [Column("id", TypeName = "integer")]
    [Required]
    public int Id { get; set; }
    
    [Column("edn", TypeName = "varchar(100)")]
    public string? EDN { get; set; }
    
    [Column("title", TypeName = "text")]
    public string? Title { get; set; }
    
    [Column("type", TypeName = "text")]
    public string? Type { get; set; }
    
    [Column("language", TypeName = "text")]
    public string? Language { get; set; }
    
    [Column("year", TypeName = "text")]
    public string? Year { get; set; }
    
    [Column("annotation", TypeName = "text")]
    public string? Annotation { get; set; }
    
    [Column("rubric_grnti", TypeName = "text")]
    public string? RubricGRNTI { get; set; }
    
    [Column("thematic_area", TypeName = "text")]
    public string? ThematicArea { get; set; }
    
    [Column("in_rsci", TypeName = "boolean")]
    public bool? InRSCI { get; set; }
    
    [Column("number_of_citations_in_rsci", TypeName = "integer")]
    public int? NumberOfCitationsInRSCI { get; set; }
    
    [Column("in_core_rsci", TypeName = "boolean")]
    public bool? InCoreRSCI { get; set; }
    
    [Column("number_of_citations_in_core_rsci", TypeName = "integer")]
    public int? NumberOfCitationsInCoreRSCI { get; set; }
    
    [Column("views", TypeName = "integer")]
    public int? Views { get; set; }
    
    [Column("number_uploads", TypeName = "integer")]
    public int? NumberUploads { get; set; }

    public ICollection<ArticleAuthor> Authors { get; set; }
    
    public ICollection<ArticleOrganization> Organizations { get; set; }

    public ICollection<ArticleKeyWord> KeyWords { get; set; }
    
    public ICollection<DataCollectionTaskGeneral> DataCollectionTasks { get; set; }
}