using FloraFinance.Application.Abstractions;
using FloraFinance.Domain.Accounts;
using FloraFinance.Domain.Categories;
using FloraFinance.Domain.Incomes;
using FloraFinance.Domain.Workspaces;
using Microsoft.EntityFrameworkCore;

namespace FloraFinance.Infrastructure.Persistence;

public sealed class WorkspaceRepository(FloraFinanceDbContext db) : IWorkspaceRepository
{
    public Task<bool> ExistsForUserAsync(Guid userId, CancellationToken cancellationToken) => db.Workspaces.AnyAsync(x => x.UserId == userId && x.DeletedAt == null, cancellationToken);
    public async Task AddAsync(Workspace workspace, CancellationToken cancellationToken) => await db.Workspaces.AddAsync(workspace, cancellationToken);
}
public sealed class AccountRepository(FloraFinanceDbContext db) : IAccountRepository { public async Task AddAsync(FinancialAccount account, CancellationToken cancellationToken) => await db.Accounts.AddAsync(account, cancellationToken); }
public sealed class CategoryRepository(FloraFinanceDbContext db) : ICategoryRepository { public async Task AddAsync(Category category, CancellationToken cancellationToken) => await db.Categories.AddAsync(category, cancellationToken); }
public sealed class IncomeRepository(FloraFinanceDbContext db) : IIncomeRepository { public async Task AddAsync(Income income, CancellationToken cancellationToken) => await db.Incomes.AddAsync(income, cancellationToken); }
