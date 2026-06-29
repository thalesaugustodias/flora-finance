import { Result, id, maxLength, now, required } from '../shared/result.js';
import { Money } from './money.js';
export const accountTypes = new Set(['Checking','Savings','Cash','Investment','Digital','Credit','Wallet','Other']);
export class FinancialAccount {
  static create(input) {
    const errors = [required(input?.workspaceId,'workspaceId'), required(input?.name,'name'), maxLength(input?.name,120,'name')].filter(Boolean);
    if (!accountTypes.has(input?.type)) errors.push('type is invalid');
    const money = Money.create(input?.initialBalance ?? 0, input?.currency ?? 'BRL', { allowZero: true, allowNegative: true });
    if (!money.ok) errors.push(...money.errors);
    if (errors.length) return Result.fail(errors);
    const at = now();
    return Result.ok({ id: id('acc'), workspaceId: input.workspaceId, name: input.name, bank: input.bank ?? null, type: input.type, currency: money.value.currency, initialBalance: money.value.amount, currentBalance: money.value.amount, color: input.color ?? '#2f855a', icon: input.icon ?? 'wallet', isArchived: false, createdAt: at, updatedAt: at, deletedAt: null, events: ['AccountCreated'] });
  }
  static archive(account) { return Result.ok({ ...account, isArchived: true, deletedAt: now(), events: [...(account.events ?? []),'AccountArchived'] }); }
}
