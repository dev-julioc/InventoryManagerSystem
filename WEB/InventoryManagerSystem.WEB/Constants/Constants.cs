namespace InventoryManagerSystem.WEB.Constants;

public static class DefaultClaims
{
    public const string ManageUser = "ManageUser";
    public const string Add = "Add";
    public const string Delete = "Delete";
    public const string Update = "Update";
    public const string Read = "Read";
}

public static class Policy
{
    public const string AdminPolicy = "AdminPolicy";
    public const string ManagerPolicy = "ManagerPolicy";
    public const string UserPolicy = "UserPolicy";

    public static class RoleClaim
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string User = "User";
    }
}