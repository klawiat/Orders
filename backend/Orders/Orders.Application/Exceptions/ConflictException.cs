namespace Orders.Application.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string obj, string message) : base($"{obj} {message}")
        { }
    }
}
