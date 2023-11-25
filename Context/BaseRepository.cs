using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;

public class BaseRepository : DbContext
{
    public BaseRepository(DbContextOptions options)
        : base(options) { }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Invoice> Invoices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(client =>
        {
          client.Property(p => p.Id).HasValueGenerator<OrderIdValueGenerator>();
        });
        modelBuilder.Entity<Invoice>(invoice =>
        {
          invoice.Property(p => p.Id).HasValueGenerator<OrderIdValueGenerator>();
        });
    }
    public async Task<T> Insertar<T>(T elemento) where T : class
    {
        await AddAsync<T>(elemento);
        return elemento;
    }

    public async void SalvarCambios()
    {
        await SaveChangesAsync();
    }

    public void Eliminar<T>(T elemento) where T : class
    {
        Remove<T>(elemento);
    }

    public IQueryable<T> Queryable<T>(Expression<Func<T, bool>>? expression = null) where T : class
    {
        if (expression == null)
            return Set<T>();
        return Set<T>().Where(expression);
    }
}
public class OrderIdValueGenerator : ValueGenerator<int>
{
    private int _current;

    public override bool GeneratesTemporaryValues => false;

    public override int Next(EntityEntry entry)
    {
        var a = Interlocked.Increment(ref _current);
        return a;
    }
}