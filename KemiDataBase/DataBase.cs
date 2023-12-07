using Dapper;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using System.Linq;

namespace KemiDataBase;
public class DatabaseConfig {
    public string Name { get; set; }
    public DatabaseConfig(string name) {
        Name = name;
    }
}
public class DatabaseBootstrap {
    private readonly DatabaseConfig databaseConfig;

    public DatabaseBootstrap(DatabaseConfig databaseConfig) {
        this.databaseConfig = databaseConfig;
    }

    public void Setup() {
        using var connection = new SqliteConnection(databaseConfig.Name);

        //connection.createFile
        //connection.Open();

        var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Data';");
        var tableName = table.FirstOrDefault();
        if (!string.IsNullOrEmpty(tableName) && tableName == "Data")
            return;

        connection.Execute(
        "CREATE TABLE Data (" +
        "Index   INTEGER UNIQUE, " +
        "Navn    TEXT UNIQUE," +
        "Formel  TEXT," +
        "MolarMass   REAL," +
        "TilstandsForm   TEXT," +
        "Enthalpi    REAL," +
        "Entropi REAL," +
        "VarmeKapacitet  REAL," +
        "Gibbs   REAL," +
        "Densitet    REAL," +
        "Smeltepunkt REAL," +
        "Kogepunkt   REAL," +
        "PRIMARY KEY(\"Index\" AUTOINCREMENT));");
    }
}

