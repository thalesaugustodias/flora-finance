using FloraFinance.Application.Abstractions;
using FloraFinance.Application.Accounts;
using FloraFinance.Application.Incomes;
using FloraFinance.Application.Expenses;
using FloraFinance.Application.Transfers;
using FloraFinance.Application.Dashboard;
using FloraFinance.Application.CashFlow;
using FloraFinance.Application.Workspaces;
using FloraFinance.Domain.Accounts;
using FloraFinance.Domain.Categories;
using FloraFinance.Domain.Incomes;
using FloraFinance.Domain.Expenses;
using FloraFinance.Domain.Transfers;
using FloraFinance.Domain.Workspaces;
using Microsoft.EntityFrameworkCore;

namespace FloraFinance.Infrastructure.Persistence;

public sealed class WorkspaceRepository(FloraFinanceDbContext db) : IWorkspaceRepository
{
    public Task<bool> ExistsForUserAsync(Guid userId, CancellationToken cancellationToken) => db.Workspaces.AnyAsync(x => x.UserId == userId && x.DeletedAt == null, cancellationToken);
    public async Task AddAsync(Workspace workspace, CancellationToken cancellationToken) => await db.Workspaces.AddAsync(workspace, cancellationToken);
    public Task<Workspace?> GetTrackedAsync(Guid id, CancellationToken cancellationToken) => db.Workspaces.FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null, cancellationToken);
    public Task<WorkspaceResponse?> GetAsync(Guid id, CancellationToken cancellationToken) => db.Workspaces.AsNoTracking().Where(x => x.Id == id && x.DeletedAt == null).Select(x => new WorkspaceResponse(x.Id, x.UserId, x.Name.Value, x.Slug.Value, x.Currency.Code, x.Language, x.Timezone, x.CreatedAt, x.UpdatedAt, x.DeletedAt)).FirstOrDefaultAsync(cancellationToken);
    public async Task<IReadOnlyList<WorkspaceResponse>> ListAsync(Guid userId, CancellationToken cancellationToken) => await db.Workspaces.AsNoTracking().Where(x => x.UserId == userId && x.DeletedAt == null).Select(x => new WorkspaceResponse(x.Id, x.UserId, x.Name.Value, x.Slug.Value, x.Currency.Code, x.Language, x.Timezone, x.CreatedAt, x.UpdatedAt, x.DeletedAt)).ToListAsync(cancellationToken);
}

public sealed class AccountRepository(FloraFinanceDbContext db) : IAccountRepository
{
    public async Task AddAsync(FinancialAccount account, CancellationToken cancellationToken) => await db.Accounts.AddAsync(account, cancellationToken);
    public Task<FinancialAccount?> GetTrackedAsync(Guid id, CancellationToken cancellationToken) => db.Accounts.FirstOrDefaultAsync(x => x.Id == id && !x.IsArchived, cancellationToken);
    public Task<AccountResponse?> GetAsync(Guid id, CancellationToken cancellationToken) => db.Accounts.AsNoTracking().Where(x => x.Id == id && !x.IsArchived).Select(x => new AccountResponse(x.Id, x.WorkspaceId, x.Name.Value, x.Bank, x.Type.ToString(), x.Currency.Code, x.InitialBalance, x.CurrentBalance, x.Color, x.Icon, x.IsArchived, x.CreatedAt, x.DeletedAt)).FirstOrDefaultAsync(cancellationToken);
    public async Task<IReadOnlyList<AccountResponse>> ListAsync(Guid workspaceId, CancellationToken cancellationToken) => await db.Accounts.AsNoTracking().Where(x => x.WorkspaceId == workspaceId && !x.IsArchived).Select(x => new AccountResponse(x.Id, x.WorkspaceId, x.Name.Value, x.Bank, x.Type.ToString(), x.Currency.Code, x.InitialBalance, x.CurrentBalance, x.Color, x.Icon, x.IsArchived, x.CreatedAt, x.DeletedAt)).ToListAsync(cancellationToken);
}

public sealed class CategoryRepository(FloraFinanceDbContext db) : ICategoryRepository { public async Task AddAsync(Category category, CancellationToken cancellationToken) => await db.Categories.AddAsync(category, cancellationToken); }

public sealed class IncomeRepository(FloraFinanceDbContext db) : IIncomeRepository
{
    public async Task AddAsync(Income income, CancellationToken cancellationToken) => await db.Incomes.AddAsync(income, cancellationToken);
    public async Task<IReadOnlyList<IncomeResponse>> ListAsync(Guid workspaceId, DateOnly? from, DateOnly? to, CancellationToken cancellationToken)
    {
        var query = db.Incomes.AsNoTracking().Where(x => x.WorkspaceId == workspaceId && x.DeletedAt == null);
        if (from.HasValue) query = query.Where(x => x.ReceivedDate >= from.Value);
        if (to.HasValue) query = query.Where(x => x.ReceivedDate <= to.Value);
        return await query.OrderByDescending(x => x.ReceivedDate).Select(x => new IncomeResponse(x.Id, x.WorkspaceId, x.AccountId, x.CategoryId, x.Description, x.Amount, x.Currency.Code, x.ReceivedDate, x.Observation, x.CreatedAt)).ToListAsync(cancellationToken);
    }
}

public sealed class ExpenseRepository(FloraFinanceDbContext db) : IExpenseRepository
{
    public async Task AddAsync(Expense expense, CancellationToken cancellationToken) => await db.Expenses.AddAsync(expense, cancellationToken);
    public async Task<IReadOnlyList<ExpenseResponse>> ListAsync(Guid workspaceId, DateOnly? from, DateOnly? to, CancellationToken cancellationToken)
    {
        var query = db.Expenses.AsNoTracking().Where(x => x.WorkspaceId == workspaceId && x.DeletedAt == null);
        if (from.HasValue) query = query.Where(x => x.DueDate >= from.Value);
        if (to.HasValue) query = query.Where(x => x.DueDate <= to.Value);
        return await query.OrderBy(x => x.DueDate).Select(x => new ExpenseResponse(x.Id, x.WorkspaceId, x.AccountId, x.CategoryId, x.Description, x.Amount, x.Currency.Code, x.DueDate, x.PaidDate, x.Observation, x.IsRecurring, x.InstallmentNumber, x.InstallmentTotal, x.CreatedAt)).ToListAsync(cancellationToken);
    }
}

public sealed class TransferRepository(FloraFinanceDbContext db) : ITransferRepository
{
    public async Task AddAsync(Transfer transfer, CancellationToken cancellationToken) => await db.Transfers.AddAsync(transfer, cancellationToken);
    public async Task<IReadOnlyList<TransferResponse>> ListAsync(Guid workspaceId, DateOnly? from, DateOnly? to, CancellationToken cancellationToken)
    {
        var query = db.Transfers.AsNoTracking().Where(x => x.WorkspaceId == workspaceId && x.DeletedAt == null);
        if (from.HasValue) query = query.Where(x => x.TransferDate >= from.Value);
        if (to.HasValue) query = query.Where(x => x.TransferDate <= to.Value);
        return await query.OrderByDescending(x => x.TransferDate).Select(x => new TransferResponse(x.Id, x.WorkspaceId, x.SourceAccountId, x.DestinationAccountId, x.Amount, x.Currency.Code, x.TransferDate, x.Description, x.CreatedAt)).ToListAsync(cancellationToken);
    }
}

public sealed class DashboardReadRepository(FloraFinanceDbContext db) : IDashboardReadRepository
{
    public async Task<DashboardExecutiveResponse> GetExecutiveAsync(Guid workspaceId, DateOnly referenceDate, CancellationToken cancellationToken)
    {
        var firstDay = new DateOnly(referenceDate.Year, referenceDate.Month, 1);
        var nextMonth = firstDay.AddMonths(1);
        var upcomingLimit = referenceDate.AddDays(15);
        var balance = await db.Accounts.AsNoTracking().Where(x => x.WorkspaceId == workspaceId && !x.IsArchived).SumAsync(x => x.CurrentBalance, cancellationToken);
        var income = await db.Incomes.AsNoTracking().Where(x => x.WorkspaceId == workspaceId && x.DeletedAt == null && x.ReceivedDate >= firstDay && x.ReceivedDate < nextMonth).SumAsync(x => x.Amount, cancellationToken);
        var expenses = await db.Expenses.AsNoTracking().Where(x => x.WorkspaceId == workspaceId && x.DeletedAt == null && x.DueDate >= firstDay && x.DueDate < nextMonth).SumAsync(x => x.Amount, cancellationToken);
        var upcoming = db.Expenses.AsNoTracking().Where(x => x.WorkspaceId == workspaceId && x.DeletedAt == null && x.PaidDate == null && x.DueDate >= referenceDate && x.DueDate <= upcomingLimit);
        var upcomingAmount = await upcoming.SumAsync(x => x.Amount, cancellationToken);
        var upcomingCount = await upcoming.CountAsync(cancellationToken);
        var score = CalculateHealthScore(balance, income, expenses, upcomingAmount);
        return new DashboardExecutiveResponse(workspaceId, balance, income, expenses, income - expenses, upcomingAmount, upcomingCount, score, referenceDate);
    }

    private static int CalculateHealthScore(decimal balance, decimal income, decimal expenses, decimal upcoming)
    {
        var savingsRate = income > 0 ? Math.Max(0m, (income - expenses) / income) : 0m;
        var runway = expenses > 0 ? Math.Min(balance / expenses, 6m) / 6m : 1m;
        var upcomingPenalty = income > 0 ? Math.Min(upcoming / income, 1m) : upcoming > 0 ? 1m : 0m;
        return (int)Math.Round(Math.Max(0m, Math.Min(100m, 45m * savingsRate + 45m * runway + 10m * (1m - upcomingPenalty))));
    }
}

public sealed class CashFlowReadRepository(FloraFinanceDbContext db) : ICashFlowReadRepository
{
    public async Task<MonthlyCashFlowResponse> GetMonthlyAsync(Guid workspaceId, int year, int month, CancellationToken cancellationToken)
    {
        var firstDay = new DateOnly(year, month, 1);
        var nextMonth = firstDay.AddMonths(1);
        var openingBalance = await db.Accounts.AsNoTracking().Where(x => x.WorkspaceId == workspaceId && !x.IsArchived).SumAsync(x => x.CurrentBalance, cancellationToken);
        var incomes = await db.Incomes.AsNoTracking().Where(x => x.WorkspaceId == workspaceId && x.DeletedAt == null && x.ReceivedDate >= firstDay && x.ReceivedDate < nextMonth).GroupBy(x => x.ReceivedDate).Select(g => new { Date = g.Key, Amount = g.Sum(x => x.Amount) }).ToListAsync(cancellationToken);
        var expenses = await db.Expenses.AsNoTracking().Where(x => x.WorkspaceId == workspaceId && x.DeletedAt == null && x.DueDate >= firstDay && x.DueDate < nextMonth).GroupBy(x => x.DueDate).Select(g => new { Date = g.Key, Amount = g.Sum(x => x.Amount) }).ToListAsync(cancellationToken);
        var incomeByDay = incomes.ToDictionary(x => x.Date, x => x.Amount);
        var expenseByDay = expenses.ToDictionary(x => x.Date, x => x.Amount);
        var projected = openingBalance;
        List<CashFlowDayResponse> days = [];
        for (var day = firstDay; day < nextMonth; day = day.AddDays(1))
        {
            var income = incomeByDay.GetValueOrDefault(day);
            var expense = expenseByDay.GetValueOrDefault(day);
            var net = income - expense;
            projected += net;
            days.Add(new CashFlowDayResponse(day, income, expense, net, projected));
        }
        return new MonthlyCashFlowResponse(workspaceId, year, month, openingBalance, days);
    }
}
