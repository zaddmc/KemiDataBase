using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KemiDataBase;
internal class DataMaster {

}
public class Data {
    public int Index { get; set; }
    public string Navn { get; set; }
    public string Formel { get; set; }
    public double MolarMass { get; set; }
    public string TilstandsForm { get; set; }
    public double Enthalpi { get; set; }
    public double Entropi { get; set; }
    public double VarmeKapacitet { get; set; }
    public double Gibbs { get; set; }
    public double Densitet { get; set; }
    public double Smeltepunkt { get; set; }
    public double Kogepunkt { get; set; }
}
public interface IDataCreate {
    Task Create(Data data);
}
public class DataCreate : IDataCreate {
    private readonly DatabaseConfig databaseConfig;

    public DataCreate(DatabaseConfig databaseConfig) {
        this.databaseConfig = databaseConfig;
    }

    public async Task Create(Data data) {
        using var connection = new SqliteConnection(databaseConfig.Name);

        await connection.ExecuteAsync(
            "INSERT INTO Data (Navn, Formel, MolarMass, TilstandsForm, Enthalpi, Entropi, VarmeKapacitet, Gibbs, Densitet, Smeltepunkt, Kogepunkt)" +
            "VALUES (@Navn, @Formel, @MolarMass, @TilstandsForm, @Enthalpi, @Entropi, @VarmeKapacitet, @Gibbs, @Densitet, @Smeltepunkt, @Kogepunkt);", data);
    }
}

public interface IDataRead {
    Task<IEnumerable<Data>> Read();
    Task<Data> ReadById(int dataIndex);
    Task<Data> ReadByName(string name);
}

public class DataRead : IDataRead {
    private readonly DatabaseConfig databaseConfig;

    public DataRead(DatabaseConfig databaseConfig) {
        this.databaseConfig = databaseConfig;
    }

    public async Task<IEnumerable<Data>> Read() {
        using var connection = new SqliteConnection(databaseConfig.Name);

        return await connection.QueryAsync<Data>("SELECT Index, Navn, Formel, MolarMass, TilstandsForm, Enthalpi, Entropi, VarmeKapacitet, Gibbs, Densitet, Smeltepunkt, Kogepunkt " +
            "FROM Data;");
    }

    public async Task<Data> ReadById(int index) {
        using var connection = new SqliteConnection(databaseConfig.Name);

        var result = await connection.QueryFirstOrDefaultAsync<Data>("SELECT Index, Navn, Formel, MolarMass, TilstandsForm, Enthalpi, Entropi, VarmeKapacitet, Gibbs, Densitet, Smeltepunkt, Kogepunkt " +
            $"FROM Data WHERE Index = {index};");
        if (result == null) return null;
        return result;
    }
    public async Task<Data> ReadByName(string name) {
        using var connection = new SqliteConnection(databaseConfig.Name);

        var result = await connection.QueryFirstOrDefaultAsync<Data>("SELECT Index, Navn, Formel, MolarMass, TilstandsForm, Enthalpi, Entropi, VarmeKapacitet, Gibbs, Densitet, Smeltepunkt, Kogepunkt " +
            $"FROM Data WHERE Navn = {name};");
        if (result == null) return null;
        return result;
    }

}
public interface IDataUpdate {
    Task UpdateByIndex(Data data);
}
public class DataUpdate : IDataUpdate {
    private readonly DatabaseConfig databaseConfig;

    public DataUpdate(DatabaseConfig databaseConfig) {
        this.databaseConfig = databaseConfig;
    }

    public async Task UpdateByIndex(Data data) {
        using var connection = new SqliteConnection(databaseConfig.Name);

        string executeable = "UPDATE Data SET "; // this might be ugly as shit, but hopefully works as intended
        if (data.Navn != null && data.Navn != "") executeable += $"Navn = {data.Navn}, ";
        if (data.Formel != null && data.Formel != "") executeable += $"Formel = {data.Formel}, ";
        if (data.MolarMass != 0) executeable += $"MolarMass = {data.MolarMass}, ";
        if (data.TilstandsForm != null && data.TilstandsForm != "") executeable += $"TilstandsForm = {data.TilstandsForm}, ";
        if (data.Enthalpi != 0) executeable += $"Enthalpi = {data.Enthalpi}, ";
        if (data.Entropi != 0) executeable += $"Entropi = {data.Entropi}, ";
        if (data.VarmeKapacitet != 0) executeable += $"VarmeKapacitet = {data.VarmeKapacitet}, ";
        if (data.Gibbs != 0) executeable += $"Gibbs = {data.Gibbs}, ";
        if (data.Densitet != 0) executeable += $"Densitet = {data.Densitet}, ";
        if (data.Smeltepunkt != 0) executeable += $"Smeltepunkt = {data.Smeltepunkt}, ";
        if (data.Kogepunkt != 0) executeable += $"Kogepunkt = {data.Kogepunkt}, ";
        executeable += $"WHERE Index = {data.Index};";

        await connection.ExecuteAsync(executeable);
        return;
    }
}

internal interface IDataDelete {
    Task DeleteByIndex(int dataIndex);
}
internal class DataDelete : IDataDelete {
    private readonly DatabaseConfig databaseConfig;

    public DataDelete(DatabaseConfig databaseConfig) {
        this.databaseConfig = databaseConfig;
    }
    public async Task DeleteByIndex(int dataIndex) {
        using var connection = new SqliteConnection(databaseConfig.Name);

        await connection.ExecuteAsync($"DELETE FROM Data WHERE Index = {dataIndex};");
    }
}