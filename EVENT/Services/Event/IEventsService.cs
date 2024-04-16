public interface IEventsService
{
    public Task<bool> CreateEvent(CreateEventModel body);

    public Task<List<Event>> GetAll(int CalendarId);

    public Task<bool> EventsInCalendar(EventsInCalendarModel body);

    public Task<bool> RefreshEvent(RefreshEventModel body);

    public Task<bool> DeleteEvent(int EventId);

    public Task<bool> DeleteAllEvents(int CalendarId);
}