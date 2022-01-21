using System.Runtime.Serialization;

namespace JustAnotherToDo.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found!")
    {
    }

    protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NotFoundException(string? message) : base(message)
    {
    }

    public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}