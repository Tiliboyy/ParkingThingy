namespace ParkingThingy.Users;

public class Admin : User
{
    public Admin(string nickname, string password) : base(nickname, password)
    {
        IsAdmin = true;
    }

    public void BlockSlot(ParkingSlot parkingSlot)
    {
        parkingSlot.IsBlocked = true;
    }

    private UserPermissions Permissions { get; set; }
    public void SetPermissions(UserPermissions permissions)
    {
        Permissions = permissions;
    }

    public void SetPermissions(params UserPermissions[] permissions)
    {
        Permissions = 0;
        foreach (var permission in permissions)
        {
            Permissions |=  permission;
        }
    }

    public bool HasPermissions(UserPermissions permission)
    {
        return (Permissions & permission) == permission;
    }
    
    public void AddPermissions(params UserPermissions[] permissions)
    {
        foreach (var permission in permissions)
        {
            Permissions |= permission;
        }
    }
}

[Flags]
public enum UserPermissions
{
    None = 0,
    Block = 2,
    ManageUsers = 4,
    ManageSlots = 8,
    ManageAdmins = 16
}