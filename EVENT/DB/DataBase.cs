using Microsoft.EntityFrameworkCore;

public class DataBase : DbContext, IDataBase
{
    public DbSet<Calendar> Calendars { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Event> Events { get; set; }

    public DbSet<Invite> Invites { get; set; }

    public DataBase(DbContextOptions<DataBase> options) : base(options)
    {

    Database.EnsureCreated();
    }

    public Task SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }
}
