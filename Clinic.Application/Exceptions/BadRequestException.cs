namespace Clinic.Application.Exceptions
{
    /// <summary>
    /// Exception thrown when a request is malformed or contains invalid data.
    /// </summary>
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

