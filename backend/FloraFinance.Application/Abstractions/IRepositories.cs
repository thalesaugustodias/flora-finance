using FloraFinance.Domain.Accounts;
using FloraFinance.Domain.Categories;
using FloraFinance.Domain.Common;
using FloraFinance.Domain.Incomes;
using FloraFinance.Domain.Workspaces;

namespace FloraFinance.Application.Abstractions;

public interface IWorkspaceRepository
{
    Task<bool> ExistsForUserAsync(Guid userId, CancellationToken cancellationToken);
    Task AddAsync(Workspace workspace, CancellationToken cancellationToken);
}

public interface IAccountRepository { Task AddAsync(FinancialAccount account, CancellationToken cancellationToken); }
public interface ICategoryRepository { Task AddAsync(Category category, CancellationToken cancellationToken); }
public interface IIncomeRepository { Task AddAsync(Income income, CancellationToken cancellationToken); }
public interface IUnitOfWork { Task<Result> SaveChangesAsync(CancellationToken cancellationToken); }
