namespace FileShare.Domain.Entities;

public class SharedFile
{
    public int Id { get; set; }
    public int TransferId { get; set; }
    public required string OriginalFileName { get; set; }
    public required string StorageKey { get; set; }
    public long FileSizeBytes { get; set; }
    public required string ContentType { get; set; }
    public DateTime CreatedAt { get; set; }
    public Transfer Transfer { get; set; } = null!;
}
