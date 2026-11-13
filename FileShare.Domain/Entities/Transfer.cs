using FileShare.Domain.Enums;

namespace FileShare.Domain.Entities;

public class Transfer
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public required string CreatorIpHash { get; set; }
    public required string Code { get; set; }
    public required int MaxDownloads { get; set; }
    public int CurrentDownloads { get; set; }
    public required DateTime ExpiresAt { get; set; }
    public required TransferStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public User? User { get; set; }
    public ICollection<SharedFile> SharedFiles { get; set; } = new List<SharedFile>();
    public ICollection<DownloadLog> DownloadLogs { get; set; } = new List<DownloadLog>();
}
