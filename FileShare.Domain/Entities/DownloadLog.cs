namespace FileShare.Domain.Entities;

public class DownloadLog
{
    public int Id { get; set; }
    public int TransferId { get; set; }
    public int? SharedFileId { get; set; }
    public DateTime DownloadedAt { get; set; }
    public required string IpHash { get; set; }
    public required string UserAgent { get; set; }
    public Transfer Transfer { get; set; } = null!;
    public SharedFile? SharedFile { get; set; }
}
