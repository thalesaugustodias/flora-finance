using FluentValidation;

namespace FloraFinance.Application.Expenses;

public sealed class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
{
    public CreateExpenseCommandValidator()
    {
        RuleFor(x => x.WorkspaceId).NotEmpty();
        RuleFor(x => x.AccountId).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.Description).NotEmpty().MaximumLength(160);
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.Currency).NotEmpty().Length(3);
        RuleFor(x => x.InstallmentTotal).GreaterThan(0).When(x => x.InstallmentTotal.HasValue);
        RuleFor(x => x.InstallmentNumber).GreaterThan(0).LessThanOrEqualTo(x => x.InstallmentTotal ?? int.MaxValue).When(x => x.InstallmentNumber.HasValue);
    }
}
