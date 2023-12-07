using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KemiDataBase2;

internal class Program {
    public static DataContext DB { get; private set; }
    static async Task Main(string[] args) {

        await using var db = new DataContext("KemiDatabase.db");
        DB = db;

        Console.WriteLine($"Database path: {db.DbPath}");

        AddItAll();
        await DB.SaveChangesAsync();

    }
    public static Data Resolve(IEnumerable<Data> data) {

        Console.WriteLine($"Which of the following {data.Count()} is the correct one");

        foreach (var item in data) {
            Console.WriteLine($"item nr 1; Name: {item.Navn}, Formula: {item.Formel}, State of Matter: {item.TilstandsForm}");
        }

        int answer;
        while (true) {
            if (int.TryParse(Console.ReadLine(), out answer)) break;
            else Console.WriteLine("NaN, try again");
        }

        return data.ToArray()[answer];
    }
    public static async void AddItAll() {
        StreamReader sr = new("C:\\Users\\Zadd\\source\\repos\\CSharp\\KemiDataBase\\KemiDataBase2\\thingy.csv");

        int i = 0;
        while (true) {
            Data data = new Data() { Navn = sr.ReadLine() };
            DB.DataSet.Add(data);
            i++;
            if (sr.EndOfStream) break;
        }
            await DB.SaveChangesAsync(false);
        
    }
    public static IAsyncEnumerable<Data> ReadById(int id) {

        var results =
     from data in DB.DataSet
     where data.Id == 1
     select data;

        return results.AsAsyncEnumerable();
    }
    public static IAsyncEnumerable<Data> ReadByName(string name) {

        var results =
     from data in DB.DataSet
     where data.Navn == name
     select data;

        return results.AsAsyncEnumerable();
    }
    public static Data ReadByFormula(string formula, string stateOfMatter) {

        var results =
     from data in DB.DataSet
     where data.Formel == formula
     && data.TilstandsForm == stateOfMatter
     select data;


        if (results.Count() == 0) throw new Exception("Nothing to read moron");
        else if (results.Count() == 1) return results.ToArray()[0];
        else return Resolve(results.ToArray());

    }
}