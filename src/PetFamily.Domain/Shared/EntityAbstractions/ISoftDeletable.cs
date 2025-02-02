namespace PetFamily.Domain.Shared.EntityAbstractions;

public interface ISoftDeletable
{
    public DateOnly? DeletedOn { get; }
    public bool IsDeleted { get; }
    public void Delete();
    public void Restore();
}
