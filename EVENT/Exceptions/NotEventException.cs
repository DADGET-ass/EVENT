public class NotEventException : Exception
{
    public NotEventException() : base("Event not found") { }

    public NotEventException(string message) : base(message) { }

    public NotEventException(string message, Exception innerException) : base(message, innerException) { }
}
