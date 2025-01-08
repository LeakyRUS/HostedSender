using System.Text.Json;

namespace Handlers.Abstractions.Json;

public interface ISerializerOptionProvider
{
    JsonSerializerOptions Option { get; }
}
