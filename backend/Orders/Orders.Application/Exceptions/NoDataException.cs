namespace Orders.Application.Exceptions
{
    public class NoDataException : Exception
    {
        public NoDataException(string message) : base(message)
        {
        }
    }
}
