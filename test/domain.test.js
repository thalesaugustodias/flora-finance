import test from 'node:test';
import assert from 'node:assert/strict';
import { Workspace, FinancialAccount, Income, financialHealthScore, rankDebts, forecastCashflow } from '../src/index.js';

test('cria workspace inicial com defaults do volume 7', () => {
  const result = Workspace.createForUser('user-1');
  assert.equal(result.ok, true);
  assert.equal(result.value.name, 'Meu Financeiro');
  assert.equal(result.value.currency, 'BRL');
  assert.equal(result.value.language, 'pt-BR');
  assert.equal(result.value.timezone, 'America/Sao_Paulo');
});

test('cria conta financeira com saldo inicial negativo permitido', () => {
  const result = FinancialAccount.create({ workspaceId: 'w1', name: 'Conta Principal', type: 'Checking', initialBalance: -100, currency: 'BRL' });
  assert.equal(result.ok, true);
  assert.equal(result.value.currentBalance, -100);
});

test('impede receita com valor zero', () => {
  const result = Income.create({ workspaceId: 'w1', accountId: 'a1', categoryId: 'c1', description: 'Salário', amount: 0, receivedDate: '2026-06-28' });
  assert.equal(result.ok, false);
  assert.match(result.errors.join(' '), /different from zero/);
});

test('calcula inteligência financeira sem persistir alterações', () => {
  assert.equal(financialHealthScore({ balance: 3000, monthlyIncome: 5000, monthlyExpenses: 3500 }), 30);
  assert.deepEqual(rankDebts([{id:'b',interestRate:2,monthlyImpact:10,dueDate:'2026-07-01'},{id:'a',interestRate:5,monthlyImpact:1,dueDate:'2026-08-01'}]).map(d=>d.id), ['a','b']);
  assert.deepEqual(forecastCashflow({ openingBalance: 100, entries: [{type:'expense', amount: 40, date:'2026-06-29'}, {type:'income', amount: 10, date:'2026-06-28'}]}).map(e=>e.projectedBalance), [110,70]);
});
