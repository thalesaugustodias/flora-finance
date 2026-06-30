using FluentValidation;

namespace FloraFinance.Application.Workspaces;

public sealed class RegisterUserWorkspaceBootstrapCommandValidator : AbstractValidator<RegisterUserWorkspaceBootstrapCommand>
{
    public RegisterUserWorkspaceBootstrapCommandValidator() => RuleFor(x => x.UserId).NotEmpty();
}

public sealed class UpdateWorkspaceCommandValidator : AbstractValidator<UpdateWorkspaceCommand>
{
    public UpdateWorkspaceCommandValidator() => RuleFor(x => x.Name).NotEmpty().MaximumLength(120);
}
