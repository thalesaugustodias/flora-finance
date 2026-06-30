using FloraFinance.Application.Abstractions;
using FloraFinance.Domain.Accounts;
using FloraFinance.Domain.Categories;
using FloraFinance.Domain.Common;
using FloraFinance.Domain.Workspaces;

namespace FloraFinance.Application.Workspaces;

public sealed record RegisterUserWorkspaceBootstrapCommand(Guid UserId);

public sealed class RegisterUserWorkspaceBootstrapHandler(IWorkspaceRepository workspaces, IAccountRepository accounts, ICategoryRepository categories, IUnitOfWork unitOfWork)
{
    public async Task<Result<Guid>> Handle(RegisterUserWorkspaceBootstrapCommand command, CancellationToken cancellationToken)
    {
        if (await workspaces.ExistsForUserAsync(command.UserId, cancellationToken))
            return Result<Guid>.Failure(new Error("Workspace.AlreadyExists", "Workspace inicial já existe para o usuário."));

        var workspace = Workspace.CreateInitial(command.UserId);
        if (workspace.IsFailure) return Result<Guid>.Failure([.. workspace.Errors]);

        var account = FinancialAccount.Create(workspace.Value!.Id, "Conta Principal", null, AccountType.Checking, 0, "BRL", "#2f855a", "wallet");
        if (account.IsFailure) return Result<Guid>.Failure([.. account.Errors]);

        foreach (var categoryName in new[] { "Salário", "Alimentação", "Moradia", "Transporte", "Saúde" })
        {
            var kind = categoryName == "Salário" ? CategoryKind.Income : CategoryKind.Expense;
            var category = Category.Create(workspace.Value.Id, categoryName, kind, isSystem: true);
            if (category.IsFailure) return Result<Guid>.Failure([.. category.Errors]);
            await categories.AddAsync(category.Value!, cancellationToken);
        }

        await workspaces.AddAsync(workspace.Value, cancellationToken);
        await accounts.AddAsync(account.Value!, cancellationToken);
        var saved = await unitOfWork.SaveChangesAsync(cancellationToken);
        return saved.IsFailure ? Result<Guid>.Failure([.. saved.Errors]) : Result<Guid>.Success(workspace.Value.Id);
    }
}
