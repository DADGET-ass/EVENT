using Microsoft.EntityFrameworkCore;

public class CalendarsService: ICalendarsService
{
    private DataBase _context;

    public CalendarsService(DataBase context)
    {
        _context = context;
    }

    public async Task<bool> Create()
    {
        var calendar = new Calendar { };
        await _context.Calendars.AddAsync(calendar);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int CalendarId)
    {
        var calendar = await _context.Calendars.FirstOrDefaultAsync(u => u.Id == CalendarId);

        if (calendar == null)
        {
            throw new NotCalendarException();
        }

        var events = await _context.Events.Where(e => e.CalendarId == CalendarId).ToListAsync();

        foreach (var e in events)
        {
            e.CalendarId = null;
        }

        _context.Calendars.Remove(calendar);

        await _context.SaveChangesAsync();
        return true;
    }
}