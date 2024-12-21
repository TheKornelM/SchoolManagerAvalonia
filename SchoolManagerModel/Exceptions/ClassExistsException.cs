namespace SchoolManagerModel.Exceptions;

[Serializable]
public class ClassExistsException : Exception
{
    public ClassExistsException()
    {
    }

    public ClassExistsException(string? message) : base(message)
    {
    }

    public ClassExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}