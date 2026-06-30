export class Result {
  constructor(ok, value, errors = []) { this.ok = ok; this.value = value; this.errors = errors; }
  static ok(value) { return new Result(true, value, []); }
  static fail(...errors) { return new Result(false, null, errors.flat()); }
}
export const required = (value, field) => value === undefined || value === null || value === '' ? `${field} is required` : null;
export const maxLength = (value, max, field) => typeof value === 'string' && value.length > max ? `${field} must have at most ${max} chars` : null;
export const id = (prefix) => `${prefix}_${crypto.randomUUID()}`;
export const now = () => new Date().toISOString();
