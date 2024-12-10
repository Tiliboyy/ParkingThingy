

**Leklotz**
================

A .NET Core project that utilizes MongoDB as its database.

**Overview**
------------

This project provides a basic structure for building a .NET Core application with MongoDB as its database. It includes a `User` class with a `Guid` primary key and uses the MongoDB .NET driver to interact with the database.

**Features**
------------

* Uses MongoDB as its database
* Includes a `User` class with a `Guid` primary key
* Utilizes the MongoDB .NET driver for database interactions

**Getting Started**
-------------------

1. Clone the repository: `git clone https://github.com/your-username/leklotz.git`
2. Install the required NuGet packages: `dotnet restore`
3. Update the `appsettings.json` file with your MongoDB connection string
4. Run the application: `dotnet run`

**Database Configuration**
-------------------------

To configure the database, update the `appsettings.json` file with your MongoDB connection string:
```json
{
  "MongoDb": {
    "ConnectionString": "mongodb://localhost:27017"
  }
}
```
**User Class**
-------------

The `User` class is defined in the `Models` folder:
```csharp
public class User
{
    [BsonId]
    public Guid Id { get; set; }
    // ...
}
```
**Contributing**
------------

Contributions are welcome! If you'd like to contribute to this project, please fork the repository and submit a pull request.

**License**
-------

This project is licensed under the MIT License.

**Acknowledgments**
---------------

* MongoDB .NET driver: https://github.com/mongodb/mongo-csharp-driver
* .NET Core: https://github.com/dotnet/core
