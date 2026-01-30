namespace Deve.Handlers;

public interface ICommandDeleteHandler<TCommand> : IRequestHandler<TCommand, Result>;
