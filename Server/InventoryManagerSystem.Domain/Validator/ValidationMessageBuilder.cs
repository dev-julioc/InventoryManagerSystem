namespace InventoryManagerSystem.Domain.Validator;

public static class ValidationMessageBuilder
{
    public static string RequiredField(string fieldName)
        => $"O campo {fieldName} é obrigatório.";
 
    public static string MaxLengthField(string fieldName, int maxLength)
        => $"O campo {fieldName} deve ter no máximo {maxLength} caracteres.";
 
    public static string MinLengthField(string fieldName, int minLength)
        => $"O campo {fieldName} deve ter no mínimo {minLength} caracteres.";
    
    public static string LengthField(string fieldName, int length)
        => $"O campo {fieldName} deve ter {length} caracteres.";
 
    public static string RangeField(string fieldName, int minValue, int maxValue)
        => $"O campo {fieldName} deve estar entre {minValue} e {maxValue} caracteres.";
 
    public static string InvalidField(string fieldName)
        => $"{fieldName} inválido.";
 
    public static string GreaterThanZero(string fieldName)
        => $"O campo {fieldName} deve ser maior que zero.";
}