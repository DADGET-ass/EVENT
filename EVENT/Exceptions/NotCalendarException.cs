public class NotCalendarException : Exception
{
    public NotCalendarException() : base("Calendar not found") { }

    public NotCalendarException(string message) : base(message) { }

    public NotCalendarException(string message, Exception innerException) : base(message, innerException) { }
}
