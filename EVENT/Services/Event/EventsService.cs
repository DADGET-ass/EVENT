using Microsoft.EntityFrameworkCore;

public class EventsService : IEventsService
{
    private readonly DataBase _context;

    public EventsService(DataBase context)
    {
        _context = context;
    }

    public async Task<bool> CreateEvent(CreateEventModel body)
    {
        var calendar = await _context.Calendars.FirstOrDefaultAsync(u => u.Id == body.CalendarId);


        var _event = new Event
        {
            Name = body.Name,
            Description = string.IsNullOrEmpty(body.Description) ? "Default description" : body.Description,
            Date = body.Date,
        };
        if (calendar != null)
        {
            _event.CalendarId = calendar.Id;
        }
        await _context.Events.AddAsync(_event);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Event>> GetAll(int CalendarId)
    {
        var calendar = await _context.Calendars.FirstOrDefaultAsync(u => u.Id == CalendarId);
        if (calendar == null)
        {
            throw new NotCalendarException();
        }

        var _event = await _context.Events.Where(u => u.CalendarId == CalendarId).ToListAsync();
        return _event;
    }

    public async Task<bool> EventsInCalendar(EventsInCalendarModel body)
    {
        var calendar = await _context.Calendars.FirstOrDefaultAsync(u => u.Id == body.CalendarId);
        if (calendar == null)
        {
            throw new NotCalendarException();
        }
        var _event = await _context.Events.FirstOrDefaultAsync(u => u.Id == body.EventId && u.CalendarId != body.CalendarId);
        if (_event == null)
        {
            throw new NotEventException($"Event not found or already in calendar: {_event.CalendarId}");
        }

        _event.CalendarId = calendar.Id;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RefreshEvent(RefreshEventModel body)
    {
        var _event = await _context.Events.FirstOrDefaultAsync(u => u.Id == body.Id);
        if (_event == null)
        {
            throw new NotEventException();
        }

        _event.Name = body.Name ?? _event.Name;
        _event.Date = body.Date ?? _event.Date;
        _event.Description = body.Description ?? _event.Description;
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeleteEvent(int EventId)
    {
        var _event = await _context.Events.FirstOrDefaultAsync(u => u.Id == EventId);
        if (_event == null)
        {
            throw new NotEventException();
        }
        _context.Events.Remove(_event);
        _context.SaveChanges(); 
        return true;
    }

    public async Task<bool> DeleteAllEvents(int CalendarId)
    {
        var EventsToDelete = await _context.Events.Where(e => e.CalendarId == CalendarId).ToListAsync();
        if (EventsToDelete == null || EventsToDelete.Count == 0)
        {
            throw new NotEventException($"No events with calendarId: {CalendarId}");
        }
        
        _context.Events.RemoveRange(EventsToDelete);
        await _context.SaveChangesAsync();
        return true;
    }
}

