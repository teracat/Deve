namespace Deve.Validators;

public interface IValidator;

public interface IValidator<TCommand> : IValidator
{
    Task<Result> ValidateAsync(TCommand command) => ValidateAsync(command, CancellationToken.None);
    Task<Result> ValidateAsync(TCommand command, CancellationToken cancellationToken);
}
