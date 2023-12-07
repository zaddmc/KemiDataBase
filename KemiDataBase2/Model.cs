using Microsoft.EntityFrameworkCore;

namespace KemiDataBase2;
public class DataContext : DbContext {
    public DbSet<Data> DataSet { get; set; }
    public string DbPath { get; }
    public DataContext(string path) {
        DbPath = path;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlServer($"Data Source={DbPath}");
        base.OnConfiguring(optionsBuilder);
    }

}
public class Data {
    public int Id { get; set; }
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
