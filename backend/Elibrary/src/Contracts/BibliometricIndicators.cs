namespace Elibrary.Contracts;

public class BibliometricIndicators
{
    public string? RubricGRNTI { get; set; }
    public string? ThematicArea { get; set; }
    public bool? InRSCI { get; set; }
    public int? NumberOfCitationsInRSCI { get; set; }
    public bool? InCoreRSCI { get; set; }
    public int? NumberOfCitationsInCoreRSCI { get; set; }
}