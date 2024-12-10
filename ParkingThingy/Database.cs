namespace ParkingThingy;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using MongoDB.Driver;

using Users;

public class UserDataAccess
{
    private MongoClient? _client;

    private IMongoCollection<User>? _userCollection;
    private IMongoDatabase? _db;

    private bool SuccessfullyConnected { get; set; }
    
    public UserDataAccess()
    {

        try
        {
            Connect();
        }
        catch (Exception e)
        {

            Console.WriteLine($"Error while connecting: {e}");

        }
    }

    private void Connect()
    {
        _client = new MongoClient("mongodb://localhost:27017");
        _db = _client.GetDatabase("leklotz");
        _userCollection = _db.GetCollection<User>("users");
        SuccessfullyConnected = true;
        Console.WriteLine("MongoDb has been connected!");
    }
    

    public void CreateUser(User user)
    {
        if (!SuccessfullyConnected)
            return;
        _userCollection?.InsertOne(user);
    }

    public List<User> FindAll()
    {
        if (!SuccessfullyConnected)
            return new List<User>();
        var result = _userCollection.Find(_ => true);
        return result.ToList();
    }

    public User? FindByUsername(string username)
    {
        var result = _userCollection.Find(user => user.Nickname == username);
        if (result.Any())
        {
            return result.First();
        }
        else
        {
            Console.WriteLine($"User {username} not found!");
        }
        return null;

    }
    private bool Exists(Expression<Func<User, bool>> filterExpr)
    {
        if (!SuccessfullyConnected)
            return false;

        return _userCollection.Find(filterExpr).CountDocuments() > 0;
    }

    public bool Exists(Guid guid)
    {
        if (!SuccessfullyConnected)
            return false;

        Expression<Func<User, bool>> filterBySteamId = x => x.Id == guid;
        return Exists(filterBySteamId);
    }


    public void UpdatePlayer(User user)
    {
        if (!SuccessfullyConnected)
        {
            return;
        }
        _userCollection.ReplaceOne(x => x.Id == user.Id, user,
            new ReplaceOptions{
                IsUpsert = true
            });
    }
}