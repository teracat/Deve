namespace Deve.Handlers;

public interface IGetQueryHandler<TRequest, TResponseItem> : IRequestHandler<TRequest, ResultGet<TResponseItem>>;
