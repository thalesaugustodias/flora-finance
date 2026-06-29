import { Result, id, maxLength, now, required } from '../shared/result.js';
import { Money } from './money.js';
export class Income {
  static create(input) {
    const errors = [required(input?.workspaceId,'workspaceId'), required(input?.accountId,'accountId'), required(input?.categoryId,'categoryId'), required(input?.description,'description'), maxLength(input?.description,160,'description'), required(input?.receivedDate,'receivedDate')].filter(Boolean);
    const money = Money.create(input?.amount, input?.currency ?? 'BRL');
    if (!money.ok) errors.push(...money.errors);
    if (Number.isNaN(Date.parse(input?.receivedDate))) errors.push('receivedDate is invalid');
    if (errors.length) return Result.fail(errors);
    const at = now();
    return Result.ok({ id: id('inc'), workspaceId: input.workspaceId, accountId: input.accountId, categoryId: input.categoryId, description: input.description, amount: money.value.amount, currency: money.value.currency, receivedDate: new Date(input.receivedDate).toISOString(), observation: input.observation ?? null, createdAt: at, updatedAt: at, deletedAt: null, events: ['IncomeCreated','DashboardRecalculationRequested','CashFlowRecalculationRequested'] });
  }
}
export class Expense {
  static create(input) { return createOutflow(input, 'ExpenseCreated'); }
}
function createOutflow(input, event) {
  const errors = [required(input?.workspaceId,'workspaceId'), required(input?.accountId,'accountId'), required(input?.categoryId,'categoryId'), required(input?.description,'description'), required(input?.dueDate,'dueDate')].filter(Boolean);
  const money = Money.create(input?.amount, input?.currency ?? 'BRL');
  if (!money.ok) errors.push(...money.errors);
  if (Number.isNaN(Date.parse(input?.dueDate))) errors.push('dueDate is invalid');
  if (errors.length) return Result.fail(errors);
  const at = now(); return Result.ok({ id: id('exp'), ...input, amount: money.value.amount, currency: money.value.currency, createdAt: at, updatedAt: at, deletedAt: null, events: [event,'DashboardRecalculationRequested'] });
}
