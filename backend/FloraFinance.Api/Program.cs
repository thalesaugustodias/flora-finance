using FloraFinance.Api.Endpoints;
using FloraFinance.Application.Abstractions;
using FloraFinance.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FloraFinanceDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<FloraFinanceDbContext>());
builder.Services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
builder.Services.AddScoped<FloraFinance.Application.Workspaces.RegisterUserWorkspaceBootstrapHandler>();
builder.Services.AddScoped<FloraFinance.Application.Workspaces.WorkspaceQueryHandler>();
builder.Services.AddScoped<FloraFinance.Application.Workspaces.UpdateWorkspaceHandler>();
builder.Services.AddScoped<FloraFinance.Application.Workspaces.ArchiveWorkspaceHandler>();
builder.Services.AddScoped<FloraFinance.Application.Accounts.CreateAccountHandler>();
builder.Services.AddScoped<FloraFinance.Application.Accounts.AccountQueryHandler>();
builder.Services.AddScoped<FloraFinance.Application.Accounts.ArchiveAccountHandler>();
builder.Services.AddScoped<FloraFinance.Application.Incomes.CreateIncomeHandler>();
builder.Services.AddScoped<FloraFinance.Application.Incomes.IncomeQueryHandler>();
builder.Services.AddOpenApi();

var app = builder.Build();
app.MapOpenApi();
app.MapWorkspaceEndpoints();
app.MapAccountEndpoints();
app.MapIncomeEndpoints();
app.Run();
