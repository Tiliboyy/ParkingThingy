namespace ParkingThingy;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

using Users;

public class Programm
{
    private static UserDataAccess? _userDataAccess;
    public static void Main()
    {
        BsonClassMap.RegisterClassMap<User>(m =>
        {
            m.AutoMap();
            m.MapIdMember(d => d.Id).SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
        });
        _userDataAccess = new UserDataAccess();
        
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter 1 to login, 2 to create an account, or any other key to exit");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    {
                        Login();
                        break;
                    }
                case "2":
                    {
                        CreateAccount();
                        break;
                    }
            }
        }
    }

    private static void CreateAccount()
    {

        Console.WriteLine("Enter username:");
        var username = Console.ReadLine();
        Console.WriteLine("Enter password:");
        var password = Console.ReadLine();
        if (username != null)
        {
            if (password != null)
            {
                var user = new User(username, password);
                _userDataAccess?.CreateUser(user);
            }
        }
        Console.WriteLine("Account created!");
    }

    private static void Login()
    {

        Console.WriteLine("Enter username:");
        string? username = Console.ReadLine();
        Console.WriteLine("Enter password:");
        string? password = Console.ReadLine();
        if (username == null)
        {
            Console.WriteLine("Username cannot be empty!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }
        var user = _userDataAccess?.FindByUsername(username);
        if (user != null && password != null && user.IsPasswordValid(password))
        {
            Console.WriteLine("Login successful!");
        }
        else
        {
            Console.WriteLine("Login failed!");
        }
        return;
    }
}