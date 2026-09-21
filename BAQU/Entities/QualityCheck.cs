namespace BAQU.Entities;

public class QualityCheck
{
    public QualityCheck()
    {
        CheckTypeId = 3;
    }
    public int CheckId { get; set; } = default!;
    public string PersonName { get; set; } = default!;
    public bool Archived { get; set; } = default!;
    public QualityCheckDetails QualityCheckDetails { get; set; } = default!;
    public int CheckTypeId { get; set; } = default!;
}

public class QualityCheckDetails : QualityCheck
{
    // public int CheckId { get; set; } = default!;
    public int FRN { get; set; } = default!;
    public string BusinessName { get; set; } = default!;
    public string ManagerName { get; set; } = default!;
    public DateTime DateQCCreated { get; set; } = default!;
}
