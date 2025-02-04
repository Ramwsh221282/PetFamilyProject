using PetFamily.Domain.Pet.ValueObjects;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Species;
using PetFamily.Domain.Species.ValueObjects;
using PetFamily.Domain.Utils.ResultPattern;

namespace PetFamily.Domain.Tests.Volunteers;

public class VolunteerTests
{
    private static Result<Pet.Pet> Create_Mok_Pet(string name, Volunteer.Volunteer volunteer)
    {
        Contacts contacts = Contacts.Create("7123456789", "owner@mail.com");
        SpecieType type = SpecieType.Create("Specie type");
        Specie specie = new Specie(type);
        BreedName breedName = BreedName.Create("Breed");
        Breed breed = new Breed(specie.Id, breedName);
        PetColor color = PetColor.Create("Color");
        PetBirthday birthday = PetBirthday.Create(
            DateOnly.FromDateTime(DateTime.Now.AddMonths(-1))
        );
        PetBodyMetrics petBody = PetBodyMetrics.Create(22.2, 22.2);
        PetAddress petAddress = PetAddress.Create("Address");
        PetHelpStatus status = PetHelpStatus.Create((int)PetHelpStatuses.RequiresHelp);
        Result<Pet.Pet> pet = volunteer.CarryPet(
            PetName.Create(name),
            specie.Id,
            breed.Id,
            petBody,
            color,
            birthday,
            petAddress,
            contacts,
            status
        );
        return pet;
    }

    [Fact]
    public void Volunteer_ChangePetPriority_Success_Case_Scenario_FromHigher_To_Lower()
    {
        Contacts contacts = Contacts.Create("71234567890", "volunteer@mail.com");
        PersonName name = PersonName.Create("Volunteer", "Volunteer", "Volunteer");
        Volunteer.Volunteer volunteer = new Volunteer.Volunteer(contacts, name);

        string[] petNames = ["pet_1", "pet_2", "pet_3", "pet_4", "pet_5"];
        Result<Pet.Pet>[] pets = petNames.Select(n => Create_Mok_Pet(n, volunteer)).ToArray();
        Assert.Equal(petNames.Length, pets.Length);
        Assert.True(pets.All(p => p.IsSuccess));

        Pet.Pet wantToMove = pets.FirstOrDefault(p => p.Value.Name.Value == "Pet_5");
        Pet.Pet willBeMoved = pets.FirstOrDefault(p => p.Value.Name.Value == "Pet_2");
        Pet.Pet someMoved1 = pets.FirstOrDefault(p => p.Value.Name.Value == "Pet_3");
        Pet.Pet someMoved2 = pets.FirstOrDefault(p => p.Value.Name.Value == "Pet_4");
        Assert.NotNull(wantToMove);
        Assert.NotNull(willBeMoved);

        Result operation = volunteer.Move(wantToMove, 2);
        Assert.True(operation.IsSuccess);

        Assert.Equal(2, wantToMove.Position.Value);
        Assert.Equal(3, willBeMoved.Position.Value);
        Assert.Equal(4, someMoved1.Position.Value);
        Assert.Equal(5, someMoved2.Position.Value);
    }

    [Fact]
    public void Volunteer_ChangePetPriority_Success_Case_Scenario_FromLower_To_Higher()
    {
        Contacts contacts = Contacts.Create("71234567890", "volunteer@mail.com");
        PersonName name = PersonName.Create("Volunteer", "Volunteer", "Volunteer");
        Volunteer.Volunteer volunteer = new Volunteer.Volunteer(contacts, name);

        string[] petNames = ["pet_1", "pet_2", "pet_3", "pet_4", "pet_5"];
        Result<Pet.Pet>[] pets = petNames.Select(n => Create_Mok_Pet(n, volunteer)).ToArray();
        Assert.Equal(petNames.Length, pets.Length);
        Assert.True(pets.All(p => p.IsSuccess));

        Pet.Pet wantToMove = pets.FirstOrDefault(p => p.Value.Name.Value == "Pet_2");
        Pet.Pet willBeMoved = pets.FirstOrDefault(p => p.Value.Name.Value == "Pet_4");
        Pet.Pet someMoved1 = pets.FirstOrDefault(p => p.Value.Name.Value == "Pet_3");
        Assert.NotNull(wantToMove);
        Assert.NotNull(willBeMoved);

        Result operation = volunteer.Move(wantToMove, 4);
        Assert.True(operation.IsSuccess);

        Assert.Equal(4, wantToMove.Position.Value);
        Assert.Equal(3, willBeMoved.Position.Value);
        Assert.Equal(2, someMoved1.Position.Value);
    }

    [Fact]
    public void Volunteer_ChangePetPriority_Failure_Out_Of_Allowed_Range_Position()
    {
        Contacts contacts = Contacts.Create("71234567890", "volunteer@mail.com");
        PersonName name = PersonName.Create("Volunteer", "Volunteer", "Volunteer");
        Volunteer.Volunteer volunteer = new Volunteer.Volunteer(contacts, name);

        string[] petNames = ["pet_1", "pet_2", "pet_3", "pet_4", "pet_5"];
        Result<Pet.Pet>[] pets = petNames.Select(n => Create_Mok_Pet(n, volunteer)).ToArray();
        Assert.Equal(petNames.Length, pets.Length);
        Assert.True(pets.All(p => p.IsSuccess));

        Pet.Pet wantToMove = pets.FirstOrDefault(p => p.Value.Name.Value == "Pet_2");
        Pet.Pet willBeMoved = pets.FirstOrDefault(p => p.Value.Name.Value == "Pet_4");
        Assert.NotNull(wantToMove);
        Assert.NotNull(willBeMoved);

        Result operation1 = volunteer.Move(wantToMove, 7);
        Result operation2 = volunteer.Move(wantToMove, 0);
        Assert.True(operation1.IsFailure);
        Assert.True(operation2.IsFailure);
    }
}
