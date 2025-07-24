namespace InventoryManagerSystem.Domain.Constants;

public static class OrderStates
{
    public const string Cancelled = "Cancelled";
    public const string Processing = "Processing";
    public const string Delivering = "Delivering";
    public const string Delivered = "Delivered";
}

public static class DefaultClaims
{
    public const string ManageUser = "ManageUser";
    public const string Add = "Add";
    public const string Delete = "Delete";
    public const string Update = "Update";
    public const string Read = "Read";
}