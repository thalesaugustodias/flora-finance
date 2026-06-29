import { existsSync, readFileSync } from 'node:fs';

const requiredFiles = [
  'backend/FloraFinance.sln',
  'backend/FloraFinance.Domain/Workspaces/Workspace.cs',
  'backend/FloraFinance.Domain/Accounts/FinancialAccount.cs',
  'backend/FloraFinance.Domain/Categories/Category.cs',
  'backend/FloraFinance.Domain/Incomes/Income.cs',
  'backend/FloraFinance.Application/Workspaces/RegisterUserWorkspaceBootstrap.cs',
  'backend/FloraFinance.Api/Endpoints/WorkspaceEndpoints.cs',
  'backend/FloraFinance.Infrastructure/Persistence/FloraFinanceDbContext.cs'
];
for (const file of requiredFiles) {
  if (!existsSync(file)) throw new Error(`Missing required scaffold file: ${file}`);
}
const workspace = readFileSync('backend/FloraFinance.Domain/Workspaces/Workspace.cs', 'utf8');
for (const expected of ['Meu Financeiro', 'BRL', 'pt-BR', 'America/Sao_Paulo', 'WorkspaceCreated']) {
  if (!workspace.includes(expected)) throw new Error(`Workspace does not include ${expected}`);
}
const bootstrap = readFileSync('backend/FloraFinance.Application/Workspaces/RegisterUserWorkspaceBootstrap.cs', 'utf8');
for (const expected of ['Conta Principal', 'Salário', 'Alimentação', 'SaveChangesAsync']) {
  if (!bootstrap.includes(expected)) throw new Error(`Bootstrap does not include ${expected}`);
}
console.log('Scaffold validation passed');
