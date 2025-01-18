namespace PetFamily.Domain.Utils.ResultPattern;

public sealed class ResultException : Exception
{
    public override string Message => "Cannot access value of failed result";
}
