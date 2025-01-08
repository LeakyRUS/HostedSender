namespace Handlers.Abstractions.Box;

public interface IBoxHandlerBuilder
{
    IEnumerable<IBoxHandler> BuildCollection(GeneralSetting generalSetting);
}