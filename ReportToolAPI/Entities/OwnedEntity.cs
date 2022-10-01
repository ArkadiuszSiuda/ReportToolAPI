namespace ReportToolAPI.Entities;

public abstract class OwnedEntity : BaseEntity
{
    public Guid OwnerId { get; set; }
}