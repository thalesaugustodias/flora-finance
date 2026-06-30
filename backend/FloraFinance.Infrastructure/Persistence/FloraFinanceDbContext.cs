using FloraFinance.Application.Abstractions;
using FloraFinance.Domain.Accounts;
using FloraFinance.Domain.Categories;
using FloraFinance.Domain.Common;
using FloraFinance.Domain.Incomes;
using FloraFinance.Domain.Expenses;
using FloraFinance.Domain.Transfers;
using FloraFinance.Domain.Workspaces;
using Microsoft.EntityFrameworkCore;

namespace FloraFinance.Infrastructure.Persistence;

public sealed class FloraFinanceDbContext(DbContextOptions<FloraFinanceDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Workspace> Workspaces => Set<Workspace>();
    public DbSet<FinancialAccount> Accounts => Set<FinancialAccount>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Income> Incomes => Set<Income>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<Transfer> Transfers => Set<Transfer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Workspace>(b =>
        {
            b.ToTable("workspaces"); b.HasKey(x => x.Id); b.HasIndex(x => x.UserId); b.HasIndex(x => x.Slug).IsUnique();
            b.OwnsOne(x => x.Name, nb => nb.Property(p => p.Value).HasColumnName("name").HasMaxLength(120));
            b.OwnsOne(x => x.Slug, sb => sb.Property(p => p.Value).HasColumnName("slug").HasMaxLength(120));
            b.OwnsOne(x => x.Currency, cb => cb.Property(p => p.Code).HasColumnName("currency").HasMaxLength(3));
            b.Ignore(x => x.DomainEvents);
        });
        modelBuilder.Entity<FinancialAccount>(b =>
        {
            b.ToTable("financial_accounts"); b.HasKey(x => x.Id); b.HasIndex(x => x.WorkspaceId);
            b.OwnsOne(x => x.Name, nb => nb.Property(p => p.Value).HasColumnName("name").HasMaxLength(120));
            b.OwnsOne(x => x.Currency, cb => cb.Property(p => p.Code).HasColumnName("currency").HasMaxLength(3));
            b.Ignore(x => x.DomainEvents);
        });
        modelBuilder.Entity<Category>(b => { b.ToTable("categories"); b.HasKey(x => x.Id); b.HasIndex(x => x.WorkspaceId); b.Ignore(x => x.DomainEvents); });
        modelBuilder.Entity<Income>(b =>
        {
            b.ToTable("incomes"); b.HasKey(x => x.Id); b.HasIndex(x => new { x.WorkspaceId, x.ReceivedDate });
            b.OwnsOne(x => x.Currency, cb => cb.Property(p => p.Code).HasColumnName("currency").HasMaxLength(3));
            b.Ignore(x => x.DomainEvents);
        });
        modelBuilder.Entity<Expense>(b =>
        {
            b.ToTable("expenses"); b.HasKey(x => x.Id); b.HasIndex(x => new { x.WorkspaceId, x.DueDate });
            b.OwnsOne(x => x.Currency, cb => cb.Property(p => p.Code).HasColumnName("currency").HasMaxLength(3));
            b.Ignore(x => x.DomainEvents);
        });
        modelBuilder.Entity<Transfer>(b =>
        {
            b.ToTable("transfers"); b.HasKey(x => x.Id); b.HasIndex(x => new { x.WorkspaceId, x.TransferDate });
            b.OwnsOne(x => x.Currency, cb => cb.Property(p => p.Code).HasColumnName("currency").HasMaxLength(3));
            b.Ignore(x => x.DomainEvents);
        });
    }

    public async Task<Result> SaveChangesAsync(CancellationToken cancellationToken)
    {
        await base.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
