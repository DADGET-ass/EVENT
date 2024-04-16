public interface ICalendarsService
{
    public Task<bool> Create();
    public Task<bool> Delete(int CalendarId);
}