using Moq;
using StackExchange.Redis;

namespace Deve.Tests.Cache.Mocks;

internal sealed class RedisDatabaseMock : Mock<IDatabase>
{
    private readonly Dictionary<string, RedisValue> _dictionary = new(StringComparer.OrdinalIgnoreCase);

    public RedisDatabaseMock()
    {
        _ = Setup(d => d.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
            .Returns((RedisKey key, CommandFlags _) =>
            {
                var keyString = key.ToString();
                if (keyString != null && _dictionary.TryGetValue(keyString, out var value))
                {
                    return value;
                }
                return RedisValue.Null;
            });

        _ = Setup(d => d.StringSet(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<TimeSpan?>(), It.IsAny<bool>(), It.IsAny<When>(), It.IsAny<CommandFlags>()))
            .Callback<RedisKey, RedisValue, TimeSpan?, bool, When, CommandFlags>((key, value, _, _, _, _) =>
            {
                var keyString = key.ToString();
                if (keyString != null)
                {
                    _dictionary[keyString] = value;
                }
            })
            .Returns(true);
    }
}
