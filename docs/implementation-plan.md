# Flora Finance — Implementation Plan

## Decisão técnica

A partir desta etapa o repositório passa a conter um scaffold backend alinhado ao backlog: .NET 10, Vertical Slice, CQRS, Result Pattern, EF Core, PostgreSQL e Minimal APIs.

> Observação: o ambiente atual não possui `dotnet` instalado, então a validação executável desta entrega cobre a presença e coerência dos arquivos. A compilação deve ser executada em ambiente com .NET 10 SDK.

## Implementado até aqui

1. **Workspace Management**
   - Aggregate `Workspace`.
   - Defaults do cadastro: `Meu Financeiro`, `BRL`, `pt-BR`, `America/Sao_Paulo`.
   - Eventos `WorkspaceCreated`, `WorkspaceUpdated`, `WorkspaceArchived`.
   - Bootstrap transacional após `UserRegistered`.
   - Endpoints para criar bootstrap, listar, obter, editar e arquivar.

2. **Financial Accounts**
   - Aggregate `FinancialAccount`.
   - `AccountType` conforme backlog.
   - Saldo inicial negativo permitido.
   - Soft delete por arquivamento.
   - Endpoints para criar, listar, obter e arquivar.

3. **Categories**
   - Módulo mínimo para desbloquear receitas/despesas.
   - Categorias iniciais criadas no bootstrap do workspace.

4. **Income Management**
   - Aggregate `Income`.
   - Vínculo obrigatório com Workspace, Account e Category.
   - Eventos de recálculo de dashboard e fluxo de caixa.
   - Endpoints para criar e listar por período.

5. **Expense Management**
   - Aggregate `Expense`.
   - Valor positivo, recorrência opcional e parcelamento opcional.
   - Vínculo obrigatório com Workspace, Account e Category.
   - Eventos de recálculo de dashboard e fluxo de caixa.
   - Endpoints para criar e listar por período.

6. **Persistência inicial**
   - `FloraFinanceDbContext` com mapeamentos EF Core.
   - SQL inicial em `backend/FloraFinance.Infrastructure/Persistence/Migrations/0001_initial.sql`.

## Próximo incremento recomendado

Com `dotnet` disponível, executar:

```bash
dotnet restore backend/FloraFinance.sln
dotnet build backend/FloraFinance.sln
dotnet test backend/FloraFinance.sln
```

Depois disso, ajustar qualquer incompatibilidade de compilação, gerar migrations EF Core oficiais, criar testes de integração com PostgreSQL e avançar para transferências, dashboard e fluxo de caixa mensal.
