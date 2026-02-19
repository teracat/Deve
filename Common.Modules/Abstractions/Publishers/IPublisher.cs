using Deve.Abstractions.Publishers;

namespace Deve.Publishers;

public interface IPublisher
{
    Task PublishAsync(INotification notification);
}
