using System;

namespace KemiDataBase;

internal class Program {
    static void Main(string[] args) {
        Console.WriteLine("Hello, World!");

        var databaseConfig = new DatabaseConfig("Data Source=DataBase.sqlite");
        var dbBootStrap = new DatabaseBootstrap(databaseConfig);
        dbBootStrap.Setup();




    }
}
