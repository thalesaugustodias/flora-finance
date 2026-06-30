using FloraFinance.Domain.Accounts;
using FloraFinance.Domain.Categories;
using FloraFinance.Domain.Common;
using FloraFinance.Domain.Incomes;
using FloraFinance.Domain.Expenses;
using FloraFinance.Domain.Workspaces;
using FloraFinance.Application.Accounts;
using FloraFinance.Application.Incomes;
using FloraFinance.Application.Expenses;
using FloraFinance.Application.Workspaces;

namespace FloraFinance.Application.Abstractions;

public interface IWorkspaceRepository
{
    Task<bool> ExistsForUserAsync(Guid userId, CancellationToken cancellationToken);
    Task AddAsync(Workspace workspace, CancellationToken cancellationToken);
    Task<Workspace?> GetTrackedAsync(Guid id, CancellationToken cancellationToken);
    Task<WorkspaceResponse?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkspaceResponse>> ListAsync(Guid userId, CancellationToken cancellationToken);
}

public interface IAccountRepository
{
    Task AddAsync(FinancialAccount account, CancellationToken cancellationToken);
    Task<FinancialAccount?> GetTrackedAsync(Guid id, CancellationToken cancellationToken);
    Task<AccountResponse?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AccountResponse>> ListAsync(Guid workspaceId, CancellationToken cancellationToken);
}

public interface ICategoryRepository { Task AddAsync(Category category, CancellationToken cancellationToken); }

public interface IIncomeRepository
{
    Task AddAsync(Income income, CancellationToken cancellationToken);
    Task<IReadOnlyList<IncomeResponse>> ListAsync(Guid workspaceId, DateOnly? from, DateOnly? to, CancellationToken cancellationToken);
}

public interface IExpenseRepository
{
    Task AddAsync(Expense expense, CancellationToken cancellationToken);
    Task<IReadOnlyList<ExpenseResponse>> ListAsync(Guid workspaceId, DateOnly? from, DateOnly? to, CancellationToken cancellationToken);
}

public interface IUnitOfWork { Task<Result> SaveChangesAsync(CancellationToken cancellationToken); }
