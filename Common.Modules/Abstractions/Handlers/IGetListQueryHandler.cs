namespace Deve.Handlers;

public interface IGetListQueryHandler<TRequest, TResponseItem> : IRequestHandler<TRequest, ResultGetList<TResponseItem>>;
