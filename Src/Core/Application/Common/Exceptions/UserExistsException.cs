using System.Runtime.Serialization;

namespace JustAnotherToDo.Application.Common.Exceptions;

public class UserExistsException : Exception
{
    public UserExistsException(string name, object key) : base($"Entity \"{name}\" ({key}) exists!")
    {
    }

    protected UserExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UserExistsException(string? message) : base(message)
    {
    }

    public UserExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}