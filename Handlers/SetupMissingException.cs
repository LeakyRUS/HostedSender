namespace Handlers;

public class SetupMissingException : Exception
{
    public SetupMissingException()
    {
    }

    public SetupMissingException(string message)
        : base(message)
    {
    }

    public SetupMissingException(string message, Exception inner)
        : base(message, inner)
    {
    }

    private static void Throw(string message)
    {
        throw new SetupMissingException(message);
    }

    public static void ThrowIfNull(object? value, Type type)
    {
        if (value == null)
        {
            Throw($"Missed {type.Name} setting!");
        }
    }
}
