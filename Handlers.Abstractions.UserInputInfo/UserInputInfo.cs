namespace Handlers.Abstractions.UserInputInfo;

public class InputInfo(int LastInputTicks)
{
    public DateTime LastInput
    {
        get
        {
            var bootTime = DateTime.UtcNow.AddMilliseconds(-Environment.TickCount);
            var lastInput = bootTime.AddMilliseconds(LastInputTicks);
            return lastInput;
        }
    }

    public TimeSpan IdleTime
    {
        get
        {
            return DateTime.UtcNow.Subtract(LastInput);
        }
    }
}
