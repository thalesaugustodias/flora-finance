import { Result } from '../shared/result.js';
export const currencies = new Set(['BRL','USD','EUR']);
export class Money {
  constructor(amount, currency = 'BRL') { this.amount = Number(amount); this.currency = currency; Object.freeze(this); }
  static create(amount, currency = 'BRL', { allowZero = false, allowNegative = false } = {}) {
    const errors = [];
    if (!Number.isFinite(Number(amount))) errors.push('amount must be numeric');
    if (!currencies.has(currency)) errors.push('currency is invalid');
    if (!allowZero && Number(amount) === 0) errors.push('amount must be different from zero');
    if (!allowNegative && Number(amount) < 0) errors.push('amount cannot be negative');
    return errors.length ? Result.fail(errors) : Result.ok(new Money(amount, currency));
  }
  add(other) { if (this.currency !== other.currency) throw new Error('currency mismatch'); return new Money(this.amount + other.amount, this.currency); }
}
