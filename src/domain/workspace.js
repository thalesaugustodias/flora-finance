import { Result, id, maxLength, now, required } from '../shared/result.js';
const defaults = { name: 'Meu Financeiro', currency: 'BRL', language: 'pt-BR', timezone: 'America/Sao_Paulo' };
const slugify = (s) => s.normalize('NFD').replace(/[\u0300-\u036f]/g,'').toLowerCase().replace(/[^a-z0-9]+/g,'-').replace(/(^-|-$)/g,'');
export class Workspace {
  static createForUser(userId, input = {}) {
    const data = { ...defaults, ...input };
    const errors = [required(userId,'userId'), required(data.name,'name'), maxLength(data.name,120,'name'), required(data.currency,'currency'), required(data.language,'language'), required(data.timezone,'timezone')].filter(Boolean);
    if (errors.length) return Result.fail(errors);
    const at = now();
    return Result.ok({ id: id('wks'), userId, name: data.name, slug: slugify(`${data.name}-${userId}`), currency: data.currency, language: data.language, timezone: data.timezone, createdAt: at, updatedAt: at, deletedAt: null, events: ['WorkspaceCreated'] });
  }
  static archive(workspace) { return Result.ok({ ...workspace, deletedAt: now(), events: [...(workspace.events ?? []),'WorkspaceArchived'] }); }
}
