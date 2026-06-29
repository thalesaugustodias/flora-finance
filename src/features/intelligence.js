export function financialHealthScore({ balance = 0, monthlyIncome = 0, monthlyExpenses = 0, overdueDebt = 0 }) {
  const savingsRate = monthlyIncome > 0 ? Math.max(0, (monthlyIncome - monthlyExpenses) / monthlyIncome) : 0;
  const runway = monthlyExpenses > 0 ? Math.min(balance / monthlyExpenses, 6) / 6 : 1;
  const debtPenalty = monthlyIncome > 0 ? Math.min(overdueDebt / monthlyIncome, 1) : overdueDebt > 0 ? 1 : 0;
  return Math.round(Math.max(0, Math.min(100, 45 * savingsRate + 45 * runway + 10 * (1 - debtPenalty))));
}
export function rankDebts(debts) {
  return [...debts].sort((a,b) => (b.interestRate - a.interestRate) || (b.monthlyImpact - a.monthlyImpact) || new Date(a.dueDate) - new Date(b.dueDate));
}
export function forecastCashflow({ openingBalance = 0, entries = [] }) {
  let balance = openingBalance;
  return [...entries].sort((a,b)=>new Date(a.date)-new Date(b.date)).map(e => ({ ...e, projectedBalance: balance += e.type === 'income' ? e.amount : -e.amount }));
}
