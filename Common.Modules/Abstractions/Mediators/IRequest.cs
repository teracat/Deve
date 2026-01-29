namespace Deve.Mediators;

public interface IRequest;

public interface IRequest<out TResponse> : IRequest where TResponse : IResult;
