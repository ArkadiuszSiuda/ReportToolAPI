namespace ReportToolAPI.Exceptions;

public class NullEntityException : Exception
{
    public NullEntityException() : base()
    {
    }

    public NullEntityException(string message) : base(message)
    {
    }
}