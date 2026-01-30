namespace Deve.Handlers;

public interface ICommandAddHandler<TCommand> : IRequestHandler<TCommand, ResultGet<ResponseId>>;
