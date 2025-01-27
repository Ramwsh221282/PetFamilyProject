namespace PetFamily.Scratches.FluentValidationScratches;

public class Person(PersonName name, Contacts contacts)
{
    public Guid Id { get; } = Guid.NewGuid();
    public PersonName Name { get; private set; } = name;
    public Contacts Contacts { get; private set; } = contacts;
}