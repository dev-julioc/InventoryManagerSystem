namespace InventoryManagerSystem.Domain.Validator;

public class DomainValidationException(string error) : Exception(error)
{
    public static void When(bool hasError, string message)
    {
        if(hasError)
            throw new DomainValidationException(message);
    }
}
