using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Tests.SharedTests;

[TestFixture(Category = "PersonNameVOTests")]
public class PersonNameVOTests
{
    [Test, Order(1)]
    public void Create_PersonName_With_Empty_Attributes()
    {
        Result<PersonName> name = PersonName.Create("", "", "");
        Error expected = PersonNameErrors.NameWasEmpty;
        Assert.That(expected, Is.EqualTo(name.Error));
    }

    [Test, Order(2)]
    public void Create_PersonName_With_Only_Name_Null()
    {
        Result<PersonName> name = PersonName.Create("", "Jack", "Marston");
        Error expected = PersonNameErrors.NameWasEmpty;
        Assert.That(expected, Is.EqualTo(name.Error));
    }
    
    [Test, Order(3)]
    public void Create_PersonName_With_Only_Surname_Empty()
    {
        Result<PersonName> name = PersonName.Create("Jack", "", "Marston");
        Error expected = PersonNameErrors.SurnameWasEmpty;
        Assert.That(expected, Is.EqualTo(name.Error));
    }
    
    [Test, Order(4)]
    public void Create_PersonName_With_Only_Patronymic()
    {
        Result<PersonName> name = PersonName.Create("", "", "Marston");
        Error expected = PersonNameErrors.NameWasEmpty;
        Assert.That(expected, Is.EqualTo(name.Error));
    }
}