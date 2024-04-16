using Microsoft.EntityFrameworkCore;

public interface IDataBase
{
    public DbSet<Calendar> Calendars { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Event> Events { get; set; }

    public DbSet<Invite> Invites { get; set; }

    Task SaveChangesAsync();
}