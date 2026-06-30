import { existsSync, readFileSync } from 'node:fs';

const requiredFiles = [
  'backend/FloraFinance.sln',
  'backend/FloraFinance.Domain/Workspaces/Workspace.cs',
  'backend/FloraFinance.Domain/Accounts/FinancialAccount.cs',
  'backend/FloraFinance.Domain/Categories/Category.cs',
  'backend/FloraFinance.Domain/Incomes/Income.cs',
  'backend/FloraFinance.Domain/Expenses/Expense.cs',
  'backend/FloraFinance.Domain/Transfers/Transfer.cs',
  'backend/FloraFinance.Application/Workspaces/RegisterUserWorkspaceBootstrap.cs',
  'backend/FloraFinance.Application/Workspaces/WorkspaceQueries.cs',
  'backend/FloraFinance.Application/Workspaces/UpdateWorkspace.cs',
  'backend/FloraFinance.Application/Accounts/AccountQueries.cs',
  'backend/FloraFinance.Application/Incomes/IncomeQueries.cs',
  'backend/FloraFinance.Application/Expenses/CreateExpense.cs',
  'backend/FloraFinance.Application/Expenses/ExpenseQueries.cs',
  'backend/FloraFinance.Application/Transfers/CreateTransfer.cs',
  'backend/FloraFinance.Application/Transfers/TransferQueries.cs',
  'backend/FloraFinance.Api/Endpoints/WorkspaceEndpoints.cs',
  'backend/FloraFinance.Api/Endpoints/ExpenseEndpoints.cs',
  'backend/FloraFinance.Api/Endpoints/TransferEndpoints.cs',
  'backend/FloraFinance.Infrastructure/Persistence/FloraFinanceDbContext.cs',
  'backend/FloraFinance.Infrastructure/Persistence/Migrations/0001_initial.sql'
];
for (const file of requiredFiles) {
  if (!existsSync(file)) throw new Error(`Missing required scaffold file: ${file}`);
}
const workspace = readFileSync('backend/FloraFinance.Domain/Workspaces/Workspace.cs', 'utf8');
for (const expected of ['Meu Financeiro', 'BRL', 'pt-BR', 'America/Sao_Paulo', 'WorkspaceCreated', 'WorkspaceArchived']) {
  if (!workspace.includes(expected)) throw new Error(`Workspace does not include ${expected}`);
}
const bootstrap = readFileSync('backend/FloraFinance.Application/Workspaces/RegisterUserWorkspaceBootstrap.cs', 'utf8');
for (const expected of ['Conta Principal', 'Salário', 'Alimentação', 'SaveChangesAsync']) {
  if (!bootstrap.includes(expected)) throw new Error(`Bootstrap does not include ${expected}`);
}
const workspaceEndpoints = readFileSync('backend/FloraFinance.Api/Endpoints/WorkspaceEndpoints.cs', 'utf8');
for (const expected of ['MapGet(""', 'MapGet("{id:guid}"', 'MapPut("{id:guid}"', 'MapDelete("{id:guid}"']) {
  if (!workspaceEndpoints.includes(expected)) throw new Error(`Workspace endpoints missing ${expected}`);
}
const expenseEndpoints = readFileSync('backend/FloraFinance.Api/Endpoints/ExpenseEndpoints.cs', 'utf8');
for (const expected of ['MapPost(""', 'MapGet(""']) {
  if (!expenseEndpoints.includes(expected)) throw new Error(`Expense endpoints missing ${expected}`);
}
const transferEndpoints = readFileSync('backend/FloraFinance.Api/Endpoints/TransferEndpoints.cs', 'utf8');
for (const expected of ['MapPost(""', 'MapGet(""']) {
  if (!transferEndpoints.includes(expected)) throw new Error(`Transfer endpoints missing ${expected}`);
}
const migration = readFileSync('backend/FloraFinance.Infrastructure/Persistence/Migrations/0001_initial.sql', 'utf8');
for (const expected of ['CREATE TABLE IF NOT EXISTS workspaces', 'financial_accounts', 'categories', 'incomes', 'expenses', 'transfers']) {
  if (!migration.includes(expected)) throw new Error(`Migration missing ${expected}`);
}
console.log('Scaffold validation passed');
