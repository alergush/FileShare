namespace FileShare.Domain.Entities;

public class SubscriptionTier
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int MaxFilesPerUpload { get; set; }
    public required int MaxFileSizeMb { get; set; }
    public required int MaxDownloads { get; set; }
    public required int MaxExpirationMinutes { get; set; }
    public required int MaxTransfersPerWindow { get; set; }
    public required int RateLimitWindowMinutes { get; set; }
    public required long PriceCents { get; set; }
}
