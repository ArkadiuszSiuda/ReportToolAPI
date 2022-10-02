namespace ReportToolAPI.Exceptions;

public class EntityExistException : Exception
{
    public EntityExistException() : base()
    {
    }

    public EntityExistException(string message) : base(message)
    {
    }
}