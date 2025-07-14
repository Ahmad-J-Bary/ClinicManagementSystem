namespace Clinic.Application.Exceptions
{
    /// <summary>
    /// Exception thrown when a requested resource is not found.
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) 
            : base($"{name} ({key}) was not found")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

