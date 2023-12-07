using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace KemiDatabog3;

internal class Program {
    public static List<Data> datas { get; set; }
    static void Main(string[] args) {
        initDataSet();


    }
    public static void GetConsoleInput() {
        Console.WriteLine("Pls write the line to do something to");
        string reaction = Console.ReadLine();

        Console.WriteLine("next, pls write what to do with this: Energi/E");
        switch (Console.ReadLine().ToLower()) {
            case "energi" or "e":
                Energy(reaction);
                break;
        }

    }
    public static void Energy(string reaction) {
        string[] str = reaction.Split("->");
        string[] lhs = str[0].Split("+"), rhs = str[1].Split("+");

        for (int i = 1; i < lhs.Length; i++) {
            if (lhs[i] == "" || lhs[i].Contains("(")) lhs[i - 1] += "+";
            if (rhs[i] == "") rhs[i - 1] += "+";
        }    



    }
    public static void initDataSet() {
        datas = new List<Data>();
        StreamReader sr = new("DataSet.csv");
        while (!sr.EndOfStream) {
            datas.Add(new(sr.ReadLine()));
        }
    }
    public static Data FindByName(string name) {
        var result =
            from data in datas
            where data.Navn == name
            select data;

        if (result.Count() == 0) throw new ArgumentNullException("findbyname is missing something, likely due to missing dataset");
        else if (result.Count() == 1) return result.First();
        else return Resolve(result);
    }
    public static Data FindByFormula(string formula, string state = "none") {
        var result =
            from data in datas
            where data.Formel == formula
            select data;

        if (result.Count() == 0) throw new ArgumentNullException("findbyformula is missing something, likely due to missing dataset");
        else if (result.Count() == 1) return result.First();
        else return Resolve(result);
    }
    public static Data Resolve(IEnumerable<Data> data) {

        Console.WriteLine($"Which of the following {data.Count()} is the correct one");

        int i = 0;
        foreach (var item in data) {
            Console.WriteLine($"item nr {++i}; Name: {item.Navn}, Formula: {item.Formel}, State of Matter: {item.TilstandsForm}");
        }

        int answer;
        while (true) {
            if (int.TryParse(Console.ReadLine(), out answer) && answer <= data.Count()) break;
            else Console.WriteLine("NaN, try again");
        }
            

        return data.ToArray()[answer - 1];
    }

}
public class Data {
    public int Id { get; set; }
    public string Navn { get; set; }
    public string Formel { get; set; }
    public string MolarMass { get; set; }
    public string TilstandsForm { get; set; }
    public string Enthalpi { get; set; }
    public string Entropi { get; set; }
    public string VarmeKapacitet { get; set; }
    public string Gibbs { get; set; }
    public string Densitet { get; set; }
    public string Smeltepunkt { get; set; }
    public string Kogepunkt { get; set; }

    private static int index = 0;
    public Data(string input) {
        string[] strarr = input.Split(';');
        if (strarr.Length != 11) throw new ArgumentNullException("fish man");

        Id = index++;
        Navn = strarr[0];
        Formel = strarr[1];
        MolarMass = strarr[2];
        TilstandsForm = strarr[3];
        Enthalpi = strarr[4];
        Entropi = strarr[5];
        VarmeKapacitet = strarr[6];
        Gibbs = strarr[7];
        Densitet = strarr[8];
        Smeltepunkt = strarr[9];
        Kogepunkt = strarr[10];
    }
}

