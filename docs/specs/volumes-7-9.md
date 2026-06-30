# Flora Finance

# Volume 07

## EPIC 07

Workspace Management

Version

1.0

Status

Approved

Author

Product Owner

---

# Objetivo

Este épico é responsável pelo gerenciamento do ambiente do usuário.

Todo dado financeiro pertence obrigatoriamente a um Workspace.

O sistema foi concebido para suportar futuramente múltiplos Workspaces por usuário, embora inicialmente apenas um Workspace seja criado automaticamente durante o cadastro.

Nenhum registro financeiro poderá existir fora de um Workspace.

---

# Objetivos de Negócio

Permitir isolamento de dados.

Preparar arquitetura para Multi Tenant.

Permitir compartilhamento futuro.

Permitir empresas.

Permitir famílias.

Permitir consultorias.

---

# Escopo

Inclui

Criação

Edição

Configuração

Arquivamento

Não inclui

Convites

Permissões

Múltiplos administradores

Sincronização

---

# User Story 061

## Criar Workspace

ID

US-061

Prioridade

Highest

Story Points

13

Epic

Workspace

---

## História

Como um usuário recém cadastrado

Quero possuir um Workspace automaticamente criado

Para que eu possa começar a utilizar o sistema sem qualquer configuração manual.

---

## Contexto

Durante o cadastro o usuário não deverá ser perguntado sobre Workspaces.

Todo Workspace inicial será criado automaticamente.

Nome inicial

Meu Financeiro

Moeda

BRL

Idioma

pt-BR

Timezone

America/Sao_Paulo

---

## Fluxo Principal

Cadastro concluído

↓

Evento UserRegistered

↓

Criar Workspace

↓

Criar Conta Principal

↓

Criar Categorias

↓

Criar Dashboard

↓

Retornar Login

---

# Fluxos Alternativos

Workspace já existe

Não executar novamente.

Erro de persistência

Rollback completo.

---

# Regras de Negócio

BR-061-001

Workspace pertence a exatamente um usuário.

BR-061-002

Nome obrigatório.

BR-061-003

Nome máximo 120 caracteres.

BR-061-004

Criado automaticamente.

BR-061-005

Soft Delete obrigatório.

BR-061-006

Auditável.

BR-061-007

Slug único.

BR-061-008

Timezone obrigatório.

BR-061-009

Moeda obrigatória.

BR-061-010

Idioma obrigatório.

---

# Entidade

Workspace

Id

UserId

Name

Slug

Currency

Timezone

Language

CreatedAt

UpdatedAt

DeletedAt

---

# Value Objects

WorkspaceName

WorkspaceSlug

Currency

Language

Timezone

---

# API

POST

/api/v1/workspaces

GET

/api/v1/workspaces

GET

/api/v1/workspaces/{id}

PUT

/api/v1/workspaces/{id}

DELETE

/api/v1/workspaces/{id}

---

# DTO

CreateWorkspaceRequest

UpdateWorkspaceRequest

WorkspaceResponse

WorkspaceSummary

---

# Eventos

WorkspaceCreated

WorkspaceUpdated

WorkspaceArchived

---

# Permissões

Workspace.Read

Workspace.Write

Workspace.Delete

Workspace.Admin

---

# Critérios de Aceitação

Dado um usuário recém criado

Quando finalizar cadastro

Então deverá existir exatamente um Workspace.

---

Dado um Workspace

Quando alterar nome

Então Dashboard permanece intacto.

---

Dado um Workspace arquivado

Então nenhum dado poderá ser perdido.

---

# Testes

Unitários

Integração

Arquitetura

Endpoints

Autorização

Concorrência

---

# Tasks Backend

Criar Aggregate

Criar Repository

Criar Command

Criar Query

Criar Validators

Criar Endpoints

Criar Migration

Criar Eventos

Criar Testes

---

# Tasks Frontend

Tela

Lista

Formulário

Editar

Arquivar

Confirmação

---

# Tasks QA

Testar criação.

Testar edição.

Testar exclusão lógica.

Testar auditoria.

Testar autorização.

---

# Definition of Done

Todos testes aprovados.

Cobertura mínima 90%.

Sem warnings.

OpenAPI atualizado.

Migration revisada.

Logs implementados.

Observabilidade implementada.
--------
# Flora Finance

# Volume 08

## EPIC 08 — Financial Accounts

Version: 1.0

Status: Approved

Owner: Product Owner

---

# Objetivo do Épico

O módulo **Financial Accounts** é responsável por representar todas as contas financeiras pertencentes ao usuário.

Uma Conta Financeira representa um local onde dinheiro pode existir.

Exemplos:

* Conta Corrente
* Conta Poupança
* Carteira
* Conta Digital
* Conta Internacional
* Conta PJ
* Conta de Investimentos
* Conta Caixa
* Conta de Pagamento

Toda movimentação financeira obrigatoriamente pertence a uma Conta Financeira.

---

# Objetivos de Negócio

* Centralizar patrimônio.
* Permitir múltiplas contas.
* Suportar fluxo de caixa.
* Permitir consolidação.
* Permitir dashboards.
* Preparar integração futura com Open Finance.

---

# Fora do Escopo

Não faz parte deste módulo:

* Cartões
* Investimentos
* PIX
* Open Finance
* Conciliação Bancária

Esses módulos serão implementados posteriormente.

---

# Aggregate Root

FinancialAccount

---

# User Story 065

## Criar Conta Financeira

ID

US-065

Prioridade

Highest

Story Points

8

---

## História

Como usuário

Quero cadastrar uma conta financeira

Para organizar corretamente meu patrimônio.

---

## Persona

Usuário autenticado.

---

## Fluxo Principal

1. Acessar Contas.

2. Selecionar Nova Conta.

3. Informar:

* Nome
* Banco
* Tipo
* Saldo Inicial
* Cor
* Ícone

4. Confirmar.

5. Sistema valida.

6. Sistema salva.

7. Dashboard recalcula.

---

# Fluxos Alternativos

Saldo inicial negativo.

Permitido.

Conta duplicada.

Permitido.

Nome vazio.

Erro.

Workspace inexistente.

Erro.

---

# Regras de Negócio

BR-065-001

Conta pertence exatamente a um Workspace.

BR-065-002

Nome obrigatório.

BR-065-003

Nome máximo 120 caracteres.

BR-065-004

Saldo inicial pode ser negativo.

BR-065-005

Moeda obrigatória.

BR-065-006

Conta pode ser arquivada.

BR-065-007

Nunca excluir fisicamente.

BR-065-008

Criar evento AccountCreated.

BR-065-009

Atualizar Dashboard.

---

# Campos

Id

WorkspaceId

Name

Bank

Type

Currency

InitialBalance

CurrentBalance

Color

Icon

IsArchived

CreatedAt

UpdatedAt

DeletedAt

---

# Value Objects

Money

Currency

AccountName

AccountColor

---

# Enum

AccountType

Checking

Savings

Cash

Investment

Digital

Credit

Wallet

Other

---

# Endpoints

POST

/api/v1/accounts

GET

/api/v1/accounts

GET

/api/v1/accounts/{id}

PUT

/api/v1/accounts/{id}

DELETE

/api/v1/accounts/{id}

---

# DTO

CreateAccountRequest

UpdateAccountRequest

AccountResponse

AccountSummaryResponse

---

# Eventos

AccountCreated

AccountUpdated

AccountArchived

---

# Permissões

Accounts.Read

Accounts.Write

Accounts.Delete

---

# Critérios de Aceite

Dado um Workspace válido

Quando cadastrar uma conta

Então ela deverá ficar imediatamente disponível para lançamentos.

---

Dado uma nova conta

Quando possuir saldo inicial

Então o Dashboard deverá refletir o patrimônio atualizado.

---

# Casos Negativos

Workspace inexistente.

Nome vazio.

Tipo inválido.

Moeda inválida.

---

# Observabilidade

Registrar:

AccountId

WorkspaceId

ExecutionTime

CorrelationId

UserId

---

# Auditoria

Registrar:

Quem criou.

Quando criou.

Valores antigos.

Valores novos.

---

# Testes Unitários

Criar conta.

Nome inválido.

Workspace inválido.

Saldo negativo.

Arquivamento.

---

# Testes Integração

Endpoint.

Persistência.

Eventos.

Dashboard.

---

# Testes Arquitetura

Command separado.

Query separada.

Sem acesso direto ao DbContext fora da camada de infraestrutura.

---

# Definition of Done

* Testes > 90%
* OpenAPI atualizado
* Migration criada
* Observabilidade implementada
* Logs estruturados
* Auditoria funcionando

---

# AI Context

Este módulo é dependência obrigatória de:

* Receitas
* Despesas
* Transferências
* Cartões
* Investimentos
* Planejamento Financeiro

Nenhum lançamento financeiro poderá existir sem uma FinancialAccount válida.

O agente de IA deverá utilizar:

* Vertical Slice Architecture
* CQRS
* FluentValidation
* EF Core
* PostgreSQL
* Minimal APIs
* Result Pattern
* MediatR

Nunca acessar DbContext diretamente na camada de apresentação.

Nunca utilizar Exceptions para fluxo de negócio.

Todos os erros deverão utilizar Result<T>.
--------
# Flora Finance

# Volume 09

## EPIC 09 — Income Management

Version: 1.0

Status: Approved

Owner: Product Owner

---

# Objetivo

O módulo **Income Management** é responsável por representar toda entrada financeira do usuário.

Uma receita representa qualquer aumento patrimonial.

Exemplos:

* Salário
* Décimo Terceiro
* Férias
* PLR
* Cashback
* Dividendos
* Juros
* Venda
* Reembolso
* Pix Recebido

Toda receita obrigatoriamente pertence a uma Conta Financeira.

---

# Objetivos de Negócio

* Consolidar entradas financeiras.
* Alimentar fluxo de caixa.
* Atualizar patrimônio.
* Servir como base para projeções.
* Possibilitar análises futuras.

---

# Aggregate Root

Income

---

# Dependências

Workspace

FinancialAccount

Category

Currency

---

# User Story

## US-066

Cadastrar Receita

Prioridade

Highest

Story Points

8

---

## História

Como usuário

Quero registrar uma receita

Para acompanhar corretamente minhas entradas financeiras.

---

# Persona

Usuário autenticado.

---

# Fluxo Principal

1.

Abrir menu Receitas

↓

2.

Nova Receita

↓

3.

Selecionar Conta

↓

4.

Selecionar Categoria

↓

5.

Informar

Descrição

Valor

Data

Observação

↓

6.

Salvar

↓

7.

Sistema valida

↓

8.

Sistema persiste

↓

9.

Dashboard recalcula

↓

10.

Fluxo de caixa recalculado

↓

11.

Metas atualizadas

---

# Fluxos Alternativos

Conta inexistente

Erro 404

Categoria inexistente

Erro

Valor igual zero

Erro

Data inválida

Erro

Workspace inválido

Erro

---

# Campos

Id

WorkspaceId

AccountId

CategoryId

Description

Amount

Currency

ReceivedDate

Observation

CreatedAt

UpdatedAt

DeletedAt

---

# Value Objects

Money

Description

Currency

IncomeDate

---

# Enum

IncomeType

Salary

Bonus

Vacation

PLR

Dividend

Cashback

Interest

Pix

Sale

Refund

Other

---

# Regras de Negócio

BR-066-001

Valor obrigatório.

BR-066-002

Valor maior que zero.

BR-066-003

Conta obrigatória.

BR-066-004

Categoria obrigatória.

BR-066-005

Descrição obrigatória.

BR-066-006

Receita arquivada nunca poderá ser alterada.

BR-066-007

Atualizar Dashboard.

BR-066-008

Atualizar Fluxo de Caixa.

BR-066-009

Publicar evento IncomeCreated.

BR-066-010

Registrar Auditoria.

---

# Eventos de Domínio

IncomeCreated

IncomeUpdated

IncomeDeleted

IncomeArchived

---

# Eventos de Integração

DashboardProjectionUpdated

CashflowUpdated

GoalsProjectionUpdated

---

# API

POST

/api/v1/incomes

GET

/api/v1/incomes

GET

/api/v1/incomes/{id}

PUT

/api/v1/incomes/{id}

DELETE

/api/v1/incomes/{id}

---

# DTO

CreateIncomeRequest

UpdateIncomeRequest

IncomeResponse

IncomeSummaryResponse

---

# Banco

Tabela

Incomes

---

# Índices

WorkspaceId

AccountId

ReceivedDate

CategoryId

---

# Constraints

FK Workspace

FK Account

FK Category

---

# Auditoria

Registrar

UserId

Data

IP

CorrelationId

Valores antigos

Valores novos

---

# Observabilidade

OpenTelemetry

Serilog

CorrelationId

ExecutionTime

TraceId

---

# Segurança

JWT obrigatório.

Workspace obrigatório.

Somente proprietário pode alterar.

Soft Delete obrigatório.

---

# Critérios de Aceitação (BDD)

### Cenário 1

Dado um usuário autenticado

Quando cadastrar uma receita válida

Então ela deverá ser persistida com sucesso

E atualizar o Dashboard

E atualizar o Fluxo de Caixa

E registrar Auditoria.

---

### Cenário 2

Dado uma conta inexistente

Quando tentar cadastrar

Então retornar HTTP 404.

---

### Cenário 3

Dado valor igual zero

Quando salvar

Então retornar HTTP 400.

---

# Testes Unitários

Cadastro

Validação

Categoria

Conta

Valor

Workspace

---

# Testes Integração

Endpoint

Migration

Persistência

Eventos

---

# Testes Arquiteturais

Command separado

Query separada

Sem dependência circular

Sem acesso direto ao DbContext

---

# Tarefas Backend

* CreateIncomeCommand
* CreateIncomeHandler
* CreateIncomeValidator
* GetIncomeQuery
* UpdateIncomeCommand
* DeleteIncomeCommand
* Endpoint
* Migration
* Testes

---

# Tarefas Frontend

Tela de Listagem

Tela de Cadastro

Tela de Edição

Filtros

Paginação

Ordenação

Resumo

---

# Tarefas QA

Cadastro

Alteração

Exclusão

Paginação

Filtros

Segurança

Performance

---

# Definition of Done

✔ Testes > 90%

✔ Sem warnings

✔ OpenAPI atualizado

✔ Migration criada

✔ Observabilidade implementada

✔ Auditoria implementada

✔ Logs estruturados

---

# AI Context

Este Aggregate Root é utilizado por:

* Dashboard
* Fluxo de Caixa
* Planejamento Financeiro
* Metas
* IA Financeira
* Relatórios

O agente deve implementar seguindo obrigatoriamente:

* Vertical Slice Architecture
* CQRS
* MediatR
* FluentValidation
* EF Core
* PostgreSQL
* Result Pattern
* Minimal APIs

É proibido:

* utilizar Exceptions como fluxo
* acessar DbContext na camada de apresentação
* misturar Query com Command
* utilizar lógica de negócio em Controllers
------

# Flora Finance

# Volume 09

## EPIC 09 — Income Management

Version: 1.0

Status: Approved

Owner: Product Owner

---

# Objetivo

O módulo **Income Management** é responsável por representar toda entrada financeira do usuário.

Uma receita representa qualquer aumento patrimonial.

Exemplos:

* Salário
* Décimo Terceiro
* Férias
* PLR
* Cashback
* Dividendos
* Juros
* Venda
* Reembolso
* Pix Recebido

Toda receita obrigatoriamente pertence a uma Conta Financeira.

---

# Objetivos de Negócio

* Consolidar entradas financeiras.
* Alimentar fluxo de caixa.
* Atualizar patrimônio.
* Servir como base para projeções.
* Possibilitar análises futuras.

---

# Aggregate Root

Income

---

# Dependências

Workspace

FinancialAccount

Category

Currency

---

# User Story

## US-066

Cadastrar Receita

Prioridade

Highest

Story Points

8

---

## História

Como usuário

Quero registrar uma receita

Para acompanhar corretamente minhas entradas financeiras.

---

# Persona

Usuário autenticado.

---

# Fluxo Principal

1.

Abrir menu Receitas

↓

2.

Nova Receita

↓

3.

Selecionar Conta

↓

4.

Selecionar Categoria

↓

5.

Informar

Descrição

Valor

Data

Observação

↓

6.

Salvar

↓

7.

Sistema valida

↓

8.

Sistema persiste

↓

9.

Dashboard recalcula

↓

10.

Fluxo de caixa recalculado

↓

11.

Metas atualizadas

---

# Fluxos Alternativos

Conta inexistente

Erro 404

Categoria inexistente

Erro

Valor igual zero

Erro

Data inválida

Erro

Workspace inválido

Erro

---

# Campos

Id

WorkspaceId

AccountId

CategoryId

Description

Amount

Currency

ReceivedDate

Observation

CreatedAt

UpdatedAt

DeletedAt

---

# Value Objects

Money

Description

Currency

IncomeDate

---

# Enum

IncomeType

Salary

Bonus

Vacation

PLR

Dividend

Cashback

Interest

Pix

Sale

Refund

Other

---

# Regras de Negócio

BR-066-001

Valor obrigatório.

BR-066-002

Valor maior que zero.

BR-066-003

Conta obrigatória.

BR-066-004

Categoria obrigatória.

BR-066-005

Descrição obrigatória.

BR-066-006

Receita arquivada nunca poderá ser alterada.

BR-066-007

Atualizar Dashboard.

BR-066-008

Atualizar Fluxo de Caixa.

BR-066-009

Publicar evento IncomeCreated.

BR-066-010

Registrar Auditoria.

---

# Eventos de Domínio

IncomeCreated

IncomeUpdated

IncomeDeleted

IncomeArchived

---

# Eventos de Integração

DashboardProjectionUpdated

CashflowUpdated

GoalsProjectionUpdated

---

# API

POST

/api/v1/incomes

GET

/api/v1/incomes

GET

/api/v1/incomes/{id}

PUT

/api/v1/incomes/{id}

DELETE

/api/v1/incomes/{id}

---

# DTO

CreateIncomeRequest

UpdateIncomeRequest

IncomeResponse

IncomeSummaryResponse

---

# Banco

Tabela

Incomes

---

# Índices

WorkspaceId

AccountId

ReceivedDate

CategoryId

---

# Constraints

FK Workspace

FK Account

FK Category

---

# Auditoria

Registrar

UserId

Data

IP

CorrelationId

Valores antigos

Valores novos

---

# Observabilidade

OpenTelemetry

Serilog

CorrelationId

ExecutionTime

TraceId

---

# Segurança

JWT obrigatório.

Workspace obrigatório.

Somente proprietário pode alterar.

Soft Delete obrigatório.

---

# Critérios de Aceitação (BDD)

### Cenário 1

Dado um usuário autenticado

Quando cadastrar uma receita válida

Então ela deverá ser persistida com sucesso

E atualizar o Dashboard

E atualizar o Fluxo de Caixa

E registrar Auditoria.

---

### Cenário 2

Dado uma conta inexistente

Quando tentar cadastrar

Então retornar HTTP 404.

---

### Cenário 3

Dado valor igual zero

Quando salvar

Então retornar HTTP 400.

---

# Testes Unitários

Cadastro

Validação

Categoria

Conta

Valor

Workspace

---

# Testes Integração

Endpoint

Migration

Persistência

Eventos

---

# Testes Arquiteturais

Command separado

Query separada

Sem dependência circular

Sem acesso direto ao DbContext

---

# Tarefas Backend

* CreateIncomeCommand
* CreateIncomeHandler
* CreateIncomeValidator
* GetIncomeQuery
* UpdateIncomeCommand
* DeleteIncomeCommand
* Endpoint
* Migration
* Testes

---

# Tarefas Frontend

Tela de Listagem

Tela de Cadastro

Tela de Edição

Filtros

Paginação

Ordenação

Resumo

---

# Tarefas QA

Cadastro

Alteração

Exclusão

Paginação

Filtros

Segurança

Performance

---

# Definition of Done

✔ Testes > 90%

✔ Sem warnings

✔ OpenAPI atualizado

✔ Migration criada

✔ Observabilidade implementada

✔ Auditoria implementada

✔ Logs estruturados

---

# AI Context

Este Aggregate Root é utilizado por:

* Dashboard
* Fluxo de Caixa
* Planejamento Financeiro
* Metas
* IA Financeira
* Relatórios

O agente deve implementar seguindo obrigatoriamente:

* Vertical Slice Architecture
* CQRS
* MediatR
* FluentValidation
* EF Core
* PostgreSQL
* Result Pattern
* Minimal APIs

É proibido:

* utilizar Exceptions como fluxo
* acessar DbContext na camada de apresentação
* misturar Query com Command
* utilizar lógica de negócio em Controllers

-----

# Flora Finance

# Volume 11

## EPIC 11 — Debt Management Engine

Version: 1.0

Status: Approved

Owner: Product Owner

Domain: Financial Intelligence

Aggregate Root: Debt

---

# Visão

O módulo **Debt Management Engine** é o núcleo estratégico do Flora Finance.

Enquanto a maioria dos aplicativos financeiros apenas registra dívidas, este módulo possui como objetivo **reduzir o custo financeiro do usuário**, priorizando pagamentos e simulando cenários.

O sistema deverá ser capaz de responder perguntas como:

* Qual dívida devo pagar primeiro?
* Quanto economizarei se quitar esta dívida hoje?
* Qual o impacto de renegociar?
* Em qual mês ficarei inadimplente?
* Quanto de juros pagarei até o fim?
* Vale mais quitar cartão ou financiamento?

---

# Objetivos de Negócio

* Centralizar todas as obrigações financeiras.
* Simular cenários.
* Priorizar pagamentos.
* Calcular juros.
* Calcular economia.
* Reduzir risco financeiro.
* Alimentar IA Financeira.

---

# Escopo

Inclui

* Cartões
* Empréstimos
* Financiamentos
* Parcelamentos
* Dívidas pessoais
* Acordos
* Renegociações

Não inclui

* Investimentos
* Impostos
* Open Finance

---

# Aggregate Root

Debt

---

# User Story

## US-068

Cadastrar Dívida

Priority

Highest

Story Points

13

---

# História

Como usuário

Quero registrar uma dívida

Para acompanhar exatamente minhas obrigações financeiras.

---

# Campos

Id

WorkspaceId

Name

Creditor

DebtType

OriginalAmount

CurrentBalance

InterestRateMonthly

InterestRateYearly

Installments

RemainingInstallments

NextDueDate

MinimumPayment

Status

Priority

CreatedAt

UpdatedAt

DeletedAt

---

# DebtType

CreditCard

Loan

Mortgage

VehicleFinance

PersonalDebt

TaxDebt

Installment

Other

---

# DebtStatus

Open

Negotiating

Renegotiated

Paid

Cancelled

---

# Priority

Critical

High

Medium

Low

---

# Regras de Negócio

BR-068-001

Toda dívida pertence a um Workspace.

BR-068-002

Saldo nunca pode ser negativo.

BR-068-003

Taxa de juros mensal obrigatória.

BR-068-004

Permitir informar CET.

BR-068-005

Registrar credor.

BR-068-006

Registrar parcelas restantes.

BR-068-007

Permitir renegociação.

BR-068-008

Nunca excluir histórico.

BR-068-009

Atualizar Dashboard.

BR-068-010

Atualizar Fluxo de Caixa.

BR-068-011

Publicar DebtCreated.

---

# User Story

## US-069

Simular Quitação

---

# História

Como usuário

Quero simular a quitação de uma dívida

Para descobrir quanto economizarei em juros.

---

# Fluxo

Selecionar dívida

↓

Selecionar data de pagamento

↓

Informar valor disponível

↓

Sistema calcula

↓

Economia

↓

Tempo economizado

↓

Fluxo de caixa atualizado (simulação)

↓

Nenhum dado persistido

---

# Resultado Esperado

Economia em juros

Meses economizados

Novo fluxo de caixa

Novo patrimônio

Novo Debt Score

---

# User Story

## US-070

Priorização Inteligente

---

# Objetivo

O sistema deverá gerar automaticamente uma lista priorizada de dívidas.

---

# Estratégias Suportadas

## Avalanche

Maior taxa de juros primeiro.

---

## Snowball

Menor saldo primeiro.

---

## Personalizada

Motor baseado em Score.

---

# Debt Score

Cada dívida receberá um Score.

Exemplo

Score =

Peso Juros

*

Peso Valor

*

Peso Vencimento

*

Peso Impacto Mensal

*

Peso Tipo

---

# Pesos (configuráveis)

Juros

40%

Saldo

20%

Vencimento

20%

Impacto Mensal

10%

Tipo

10%

---

# Resultado

Ranking

1°

Amex

2°

Santander

3°

Mercado Pago

...

---

# User Story

## US-071

Renegociação

---

# Objetivo

Registrar acordos.

---

# Campos

Valor Original

Novo Valor

Novo CET

Parcelas

Entrada

Primeiro Vencimento

Observações

---

# Regras

Nunca apagar dívida original.

Criar histórico.

Permitir comparação.

Mostrar economia.

---

# Eventos

DebtRenegotiated

DebtSimulationExecuted

DebtPrioritized

---

# APIs

POST

/api/v1/debts

GET

/api/v1/debts

GET

/api/v1/debts/{id}

PUT

/api/v1/debts/{id}

DELETE

/api/v1/debts/{id}

POST

/api/v1/debts/simulations

POST

/api/v1/debts/prioritize

POST

/api/v1/debts/renegotiations

---

# Eventos de Domínio

DebtCreated

DebtUpdated

DebtPaid

DebtRenegotiated

DebtDeleted

DebtPrioritized

SimulationExecuted

---

# Critérios BDD

### Cenário 1

Dado uma dívida cadastrada

Quando executar priorização

Então o sistema deverá retornar ranking ordenado.

---

### Cenário 2

Dado uma renegociação

Quando salvar

Então manter histórico completo.

---

### Cenário 3

Dado uma simulação

Quando finalizar

Então nenhuma informação deverá ser persistida.

---

# Observabilidade

OpenTelemetry

Serilog

CorrelationId

ExecutionTime

TraceId

DebtId

WorkspaceId

---

# Segurança

JWT obrigatório.

Workspace obrigatório.

Todas operações auditáveis.

Soft Delete obrigatório.

---

# Testes

Unitários

Integração

Arquitetura

Performance

Concorrência

Segurança

---

# AI Context

Este módulo é um dos pilares do sistema.

É proibido implementar regras financeiras dentro dos Endpoints.

Todos os cálculos deverão ficar em Domain Services.

A estratégia de priorização deverá utilizar o padrão Strategy, permitindo adicionar novos algoritmos sem modificar os existentes.

Os cálculos financeiros deverão ser determinísticos e testáveis.

Toda simulação deverá executar em memória, sem persistência.

Nenhuma regra poderá depender de componentes de infraestrutura.

----------
# Flora Finance

# Volume 11

## EPIC 11 — Debt Management Engine

Version: 1.0

Status: Approved

Owner: Product Owner

Domain: Financial Intelligence

Aggregate Root: Debt

---

# Visão

O módulo **Debt Management Engine** é o núcleo estratégico do Flora Finance.

Enquanto a maioria dos aplicativos financeiros apenas registra dívidas, este módulo possui como objetivo **reduzir o custo financeiro do usuário**, priorizando pagamentos e simulando cenários.

O sistema deverá ser capaz de responder perguntas como:

* Qual dívida devo pagar primeiro?
* Quanto economizarei se quitar esta dívida hoje?
* Qual o impacto de renegociar?
* Em qual mês ficarei inadimplente?
* Quanto de juros pagarei até o fim?
* Vale mais quitar cartão ou financiamento?

---

# Objetivos de Negócio

* Centralizar todas as obrigações financeiras.
* Simular cenários.
* Priorizar pagamentos.
* Calcular juros.
* Calcular economia.
* Reduzir risco financeiro.
* Alimentar IA Financeira.

---

# Escopo

Inclui

* Cartões
* Empréstimos
* Financiamentos
* Parcelamentos
* Dívidas pessoais
* Acordos
* Renegociações

Não inclui

* Investimentos
* Impostos
* Open Finance

---

# Aggregate Root

Debt

---

# User Story

## US-068

Cadastrar Dívida

Priority

Highest

Story Points

13

---

# História

Como usuário

Quero registrar uma dívida

Para acompanhar exatamente minhas obrigações financeiras.

---

# Campos

Id

WorkspaceId

Name

Creditor

DebtType

OriginalAmount

CurrentBalance

InterestRateMonthly

InterestRateYearly

Installments

RemainingInstallments

NextDueDate

MinimumPayment

Status

Priority

CreatedAt

UpdatedAt

DeletedAt

---

# DebtType

CreditCard

Loan

Mortgage

VehicleFinance

PersonalDebt

TaxDebt

Installment

Other

---

# DebtStatus

Open

Negotiating

Renegotiated

Paid

Cancelled

---

# Priority

Critical

High

Medium

Low

---

# Regras de Negócio

BR-068-001

Toda dívida pertence a um Workspace.

BR-068-002

Saldo nunca pode ser negativo.

BR-068-003

Taxa de juros mensal obrigatória.

BR-068-004

Permitir informar CET.

BR-068-005

Registrar credor.

BR-068-006

Registrar parcelas restantes.

BR-068-007

Permitir renegociação.

BR-068-008

Nunca excluir histórico.

BR-068-009

Atualizar Dashboard.

BR-068-010

Atualizar Fluxo de Caixa.

BR-068-011

Publicar DebtCreated.

---

# User Story

## US-069

Simular Quitação

---

# História

Como usuário

Quero simular a quitação de uma dívida

Para descobrir quanto economizarei em juros.

---

# Fluxo

Selecionar dívida

↓

Selecionar data de pagamento

↓

Informar valor disponível

↓

Sistema calcula

↓

Economia

↓

Tempo economizado

↓

Fluxo de caixa atualizado (simulação)

↓

Nenhum dado persistido

---

# Resultado Esperado

Economia em juros

Meses economizados

Novo fluxo de caixa

Novo patrimônio

Novo Debt Score

---

# User Story

## US-070

Priorização Inteligente

---

# Objetivo

O sistema deverá gerar automaticamente uma lista priorizada de dívidas.

---

# Estratégias Suportadas

## Avalanche

Maior taxa de juros primeiro.

---

## Snowball

Menor saldo primeiro.

---

## Personalizada

Motor baseado em Score.

---

# Debt Score

Cada dívida receberá um Score.

Exemplo

Score =

Peso Juros

*

Peso Valor

*

Peso Vencimento

*

Peso Impacto Mensal

*

Peso Tipo

---

# Pesos (configuráveis)

Juros

40%

Saldo

20%

Vencimento

20%

Impacto Mensal

10%

Tipo

10%

---

# Resultado

Ranking

1°

Amex

2°

Santander

3°

Mercado Pago

...

---

# User Story

## US-071

Renegociação

---

# Objetivo

Registrar acordos.

---

# Campos

Valor Original

Novo Valor

Novo CET

Parcelas

Entrada

Primeiro Vencimento

Observações

---

# Regras

Nunca apagar dívida original.

Criar histórico.

Permitir comparação.

Mostrar economia.

---

# Eventos

DebtRenegotiated

DebtSimulationExecuted

DebtPrioritized

---

# APIs

POST

/api/v1/debts

GET

/api/v1/debts

GET

/api/v1/debts/{id}

PUT

/api/v1/debts/{id}

DELETE

/api/v1/debts/{id}

POST

/api/v1/debts/simulations

POST

/api/v1/debts/prioritize

POST

/api/v1/debts/renegotiations

---

# Eventos de Domínio

DebtCreated

DebtUpdated

DebtPaid

DebtRenegotiated

DebtDeleted

DebtPrioritized

SimulationExecuted

---

# Critérios BDD

### Cenário 1

Dado uma dívida cadastrada

Quando executar priorização

Então o sistema deverá retornar ranking ordenado.

---

### Cenário 2

Dado uma renegociação

Quando salvar

Então manter histórico completo.

---

### Cenário 3

Dado uma simulação

Quando finalizar

Então nenhuma informação deverá ser persistida.

---

# Observabilidade

OpenTelemetry

Serilog

CorrelationId

ExecutionTime

TraceId

DebtId

WorkspaceId

---

# Segurança

JWT obrigatório.

Workspace obrigatório.

Todas operações auditáveis.

Soft Delete obrigatório.

---

# Testes

Unitários

Integração

Arquitetura

Performance

Concorrência

Segurança

---

# AI Context

Este módulo é um dos pilares do sistema.

É proibido implementar regras financeiras dentro dos Endpoints.

Todos os cálculos deverão ficar em Domain Services.

A estratégia de priorização deverá utilizar o padrão Strategy, permitindo adicionar novos algoritmos sem modificar os existentes.

Os cálculos financeiros deverão ser determinísticos e testáveis.

Toda simulação deverá executar em memória, sem persistência.

Nenhuma regra poderá depender de componentes de infraestrutura.

--------
# Flora Finance

# Volume 13

## EPIC 13 — Budget Management Engine

Version: 1.0

Status: Approved

Owner: Product Owner

Domain: Financial Planning

Bounded Context: Budget

Aggregate Root: Budget

---

# Visão

O módulo Budget Management é responsável por transformar objetivos financeiros em limites operacionais.

Enquanto o módulo Expense registra gastos realizados, o Budget representa quanto o usuário deveria gastar.

Todo orçamento pertence a um Workspace e poderá ser distribuído por:

* Categoria
* Conta Financeira
* Centro de Custo
* Projeto
* Pessoa
* Objetivo Financeiro

O sistema deverá funcionar tanto para usuários domésticos quanto para pequenos negócios.

---

# Objetivos Estratégicos

* Planejamento financeiro.
* Controle de gastos.
* Prevenção de excesso.
* Apoio às decisões.
* Alimentar o Financial Intelligence Engine.

---

# Aggregate Root

Budget

---

# Dependências

Workspace

FinancialAccount

Category

Expense

Income

Goal

CashFlow

---

# User Story

## US-077

Criar Orçamento

Priority

Highest

Story Points

13

---

# História

Como usuário

Quero criar um orçamento

Para limitar meus gastos.

---

# Campos

Id

WorkspaceId

Name

Description

BudgetType

Amount

Currency

StartDate

EndDate

Status

CreatedAt

UpdatedAt

DeletedAt

---

# BudgetType

Monthly

Quarterly

Semester

Annual

Custom

---

# BudgetStatus

Draft

Active

Paused

Closed

Archived

---

# Regras de Negócio

BR-077-001

Todo orçamento pertence a um Workspace.

BR-077-002

Valor obrigatório.

BR-077-003

Valor maior que zero.

BR-077-004

Data inicial obrigatória.

BR-077-005

Data final obrigatória.

BR-077-006

Data final deve ser maior que inicial.

BR-077-007

Nunca excluir histórico.

---

# User Story

## US-078

Associar Categorias

Objetivo

Permitir distribuir orçamento por categorias.

---

Exemplo

Alimentação

R$ 1.200

Transporte

R$ 650

Saúde

R$ 450

Educação

R$ 600

Lazer

R$ 400

---

# Regras

A soma poderá ser:

Exatamente igual

Menor

Maior

Conforme configuração do usuário.

---

# User Story

## US-079

Monitoramento

Objetivo

Acompanhar consumo do orçamento.

---

Indicadores

Consumido

Disponível

Comprometido

Projetado

Percentual

Dias Restantes

---

# Faixas

0–50%

Excelente

51–70%

Atenção

71–90%

Risco

91–100%

Crítico

> 100%

Estourado

---

# Dashboard

Cada orçamento deverá possuir:

Barra de progresso

Percentual

Valor utilizado

Valor restante

Projeção até o final do período

Velocidade média de consumo

---

# User Story

## US-080

Recomendações

Objetivo

Gerar sugestões automáticas.

---

Exemplos

Reduza gastos em restaurantes.

Seu orçamento de combustível será insuficiente.

Você pode aumentar investimento.

Sua categoria lazer está acima da média histórica.

Você economizará R$ 420 reduzindo delivery.

---

# Eventos

BudgetCreated

BudgetUpdated

BudgetClosed

BudgetExceeded

BudgetProjectionUpdated

---

# APIs

POST

/api/v1/budgets

GET

/api/v1/budgets

PUT

/api/v1/budgets/{id}

DELETE

/api/v1/budgets/{id}

GET

/api/v1/budgets/progress

GET

/api/v1/budgets/projections

---

# Modelo de Domínio

Budget
├── BudgetItem
├── BudgetCategory
├── BudgetProjection
├── BudgetAlert
└── BudgetRecommendation

---

# Observabilidade

Registrar:

ExecutionTime

WorkspaceId

BudgetId

ProjectionTime

CorrelationId

---

# Auditoria

Toda alteração deverá registrar:

Usuário

Timestamp

Valores antigos

Valores novos

Motivo

---

# Critérios BDD

### Cenário

Dado um orçamento mensal

Quando atingir 90%

Então gerar alerta crítico.

---

### Cenário

Dado um orçamento encerrado

Quando consultar histórico

Então exibir comparação com períodos anteriores.

---

### Cenário

Dado uma despesa registrada

Quando pertencer a uma categoria orçada

Então atualizar imediatamente o percentual consumido.

---

# Requisitos Não Funcionais

* Recalcular orçamento em menos de 500 ms após nova despesa.
* Permitir até 500 categorias por Workspace.
* Todas as projeções devem ser recalculáveis.

---

# AI Context

Este módulo depende diretamente de Expense, Income e CashFlow.

A implementação deverá utilizar um serviço de projeção separado do Aggregate.

Sugestão de componentes:

BudgetCalculationService

BudgetProjectionService

BudgetRecommendationService

BudgetAlertService

BudgetDashboardService

Cada serviço deverá possuir responsabilidade única.

É proibido implementar cálculos diretamente no Aggregate.

Toda projeção deverá ser baseada em snapshots imutáveis para permitir reprocessamento histórico.

O RecommendationService deverá fornecer explicações para cada sugestão, permitindo futura integração com modelos de IA generativa.

---------

Flora Finance
Volume 14
EPIC 14 — Goal Management Engine

Version

1.0

Status

Approved

Owner

Product Owner

Domain

Financial Planning

Bounded Context

Goals

Aggregate Root

FinancialGoal

Visão

Todo usuário possui objetivos.

Normalmente os aplicativos financeiros apenas armazenam despesas.

O Flora deverá responder continuamente:

Quanto falta para eu comprar meu apartamento?

Em quanto tempo consigo quitar meu carro?

Se eu guardar R$ 1.000 por mês, quando consigo viajar?

Se eu utilizar o décimo terceiro para quitar o Amex, quanto antecipo minha meta?

O Goal Engine será responsável por isso.

Objetivos Estratégicos

Planejamento.

Previsibilidade.

Motivação.

Gamificação.

Simulações.

Integração com IA.

Objetivo Principal

Transformar movimentações financeiras em progresso patrimonial.

Aggregate

FinancialGoal

Dependências

Workspace

Income

Expense

Budget

Debt

CashFlow

Investment

User Story
US-081

Criar Objetivo

Priority

Highest

Story Points

13

História

Como usuário

Quero criar um objetivo financeiro

Para acompanhar minha evolução.

Tipos

Emergency Reserve

Debt Payoff

Vehicle

House

Travel

Investment

Education

Retirement

Business

Electronics

Wedding

Other

Campos

Id

WorkspaceId

GoalType

Title

Description

TargetAmount

CurrentAmount

MonthlyContribution

TargetDate

Priority

Status

Color

Icon

CreatedAt

UpdatedAt

DeletedAt

Goal Status

Planning

Active

Paused

Completed

Cancelled

Archived

Priority

Critical

High

Medium

Low

Regras

BR-081-001

Todo Goal pertence a um Workspace.

BR-081-002

Valor alvo obrigatório.

BR-081-003

Valor alvo maior que zero.

BR-081-004

Meta nunca poderá possuir saldo negativo.

BR-081-005

Soft Delete obrigatório.

BR-081-006

Histórico imutável.

BR-081-007

Permitir pausas.

BR-081-008

Permitir retomada.

BR-081-009

Atualizar Dashboard.

User Story
US-082

Contribuição Automática

Objetivo

Permitir que receitas alimentem metas.

Exemplo

Todo salário

↓

10%

↓

Reserva Emergência

Décimo Terceiro

↓

50%

↓

Quitar Amex

PLR

↓

30%

↓

Investimentos

Engine

Contribution Engine

Regras

Permitir múltiplas regras.

Permitir prioridade.

Permitir percentual.

Permitir valor fixo.

Permitir limites.

User Story
US-083

Projeção

Objetivo

Calcular previsão de conclusão.

Entrada

Saldo Atual

Contribuição

Receitas

Despesas

Investimentos

Inflação

Saída

Dias

Semanas

Meses

Data prevista

Valor acumulado

Fórmula

ProjectedDate

=

TargetAmount

/

AverageContribution

Observação: esta fórmula representa apenas a estimativa inicial. A implementação deverá utilizar projeções compostas que considerem aportes variáveis, inflação configurável e rendimentos futuros quando o Goal estiver vinculado a investimentos.

User Story
US-084

Metas Inteligentes

Objetivo

IA deverá sugerir metas.

Exemplos

Criar Reserva Emergência.

Quitar Mercado Pago.

Quitar Santander.

Trocar veículo.

Comprar apartamento.

Criar reserva para IPVA.

Criar reserva para seguro.

Criar reserva para férias.

User Story
US-085

Priorização

Objetivo

Organizar metas.

Critérios

Impacto financeiro.

Economia.

Prazo.

Urgência.

Valor.

Dependências.

Ranking

Goal Score

=

Impact

Urgency

Importance

Financial Gain

Eventos

GoalCreated

GoalCompleted

GoalPaused

GoalCancelled

GoalContributionAdded

GoalProjectionUpdated

GoalRecommendationGenerated

APIs

POST

/api/v1/goals

GET

/api/v1/goals

GET

/api/v1/goals/{id}

PUT

/api/v1/goals/{id}

DELETE

/api/v1/goals/{id}

POST

/api/v1/goals/contributions

GET

/api/v1/goals/projections

GET

/api/v1/goals/recommendations

Dashboard

Cada Goal deverá apresentar

Ícone.

Cor.

Valor atual.

Valor alvo.

Percentual.

Tempo restante.

Data prevista.

Próximo aporte.

Impacto.

Prioridade.

Modelo

FinancialGoal

↓

GoalContribution

↓

GoalProjection

↓

GoalRecommendation

↓

GoalHistory

Observabilidade

Registrar

WorkspaceId

GoalId

ExecutionTime

ProjectionTime

CorrelationId

Auditoria

Registrar

Usuário

Data

Alteração

Valores antigos

Valores novos

Origem da alteração

Critérios BDD
Cenário

Dado um Goal ativo

Quando registrar nova receita

Então recalcular automaticamente a projeção.

Cenário

Dado um Goal concluído

Quando atingir o valor alvo

Então alterar status para Completed.

Cenário

Dado múltiplos Goals

Quando executar priorização

Então retornar ranking ordenado por Goal Score.

Testes

Unitários.

Integração.

Performance.

Arquitetura.

Regressão.

Concorrência.

Requisitos Não Funcionais

Todas as projeções devem ser recalculadas em menos de 2 segundos para Workspaces com até 100 mil lançamentos.

As projeções deverão ser reproduzíveis utilizando snapshots históricos.

Todo cálculo deverá ser determinístico.

AI Context

Este módulo deverá ser implementado utilizando um Goal Engine independente.

Sugestão de arquitetura:

GoalProjectionService

↓

GoalContributionService

↓

GoalRecommendationService

↓

GoalPrioritizationService

↓

GoalDashboardService

Cada componente deverá possuir responsabilidade única.

Nenhum cálculo poderá ser realizado diretamente no Aggregate.

As recomendações deverão ser justificáveis, armazenando a explicação utilizada para gerá-las.

A integração futura com IA generativa deverá utilizar essas explicações para produzir respostas em linguagem natural, sem alterar a lógica financeira.

Product Vision (Longo Prazo)

Os Goals não serão apenas "metas".

Eles funcionarão como um Roadmap Financeiro Pessoal.

Cada Goal poderá depender de outro.

Exemplo:

Quitar Mercado Pago
        │
        ▼
Quitar Santander
        │
        ▼
Criar Reserva Emergência
        │
        ▼
Comprar Apartamento

O sistema deverá ser capaz de calcular automaticamente como a conclusão de uma meta impacta todas as metas dependentes, recalculando prazos, economia de juros e patrimônio projetado.

Comentário de Arquitetura

Na minha visão, esse módulo é um dos grandes diferenciais do Flora Finance.

Hoje, quase todos os aplicativos mostram "quanto você tem".

O Flora deve mostrar "quanto falta para chegar onde você quer".

Essa mudança de perspectiva transforma o sistema de um simples controle financeiro em uma plataforma de planejamento patrimonial de longo prazo.

Próximo Volume (15)

O próximo módulo será o Cash Flow Engine.

Na minha opinião, ele será o Aggregate mais importante de todo o sistema, porque praticamente todos os outros módulos dependerão dele:

Receitas
Despesas
Dívidas
Metas
Orçamentos
Cartões
Planejamento
Inteligência Financeira

Ele será responsável por construir uma linha do tempo financeira diária, semanal, mensal e anual, permitindo que o Flora responda, com precisão, perguntas como:

"Em qual dia meu saldo ficará negativo?"
"Quanto dinheiro terei em 17 de novembro?"
"Posso antecipar este financiamento sem comprometer o caixa?"

Acredito que esse será o núcleo matemático do produto.

------

# Flora Finance

# Volume 15

## EPIC 15 — Cash Flow Engine

Version: 1.0

Status: Approved

Owner: Product Owner

Domain: Financial Core

Bounded Context: Cash Flow

Aggregate Root: CashFlowProjection

---

# Visão

O Cash Flow Engine representa o coração matemático do Flora Finance.

Todos os módulos que alteram patrimônio deverão refletir neste módulo.

Ele será responsável por construir uma linha do tempo financeira completa do usuário.

Ao contrário dos aplicativos tradicionais, que mostram apenas o saldo atual, o Cash Flow Engine responderá continuamente:

- Quanto dinheiro terei amanhã?
- Em qual dia ficarei negativo?
- Posso comprar um carro?
- Posso quitar uma dívida?
- Qual será meu patrimônio daqui a 12 meses?
- Posso antecipar parcelas?
- Quanto sobrará após pagar todas as contas?

Nenhuma projeção financeira poderá ser calculada fora deste Engine.

---

# Objetivos Estratégicos

- Projetar fluxo diário.
- Projetar fluxo semanal.
- Projetar fluxo mensal.
- Projetar fluxo anual.
- Alimentar IA.
- Alimentar Dashboard.
- Alimentar Budget.
- Alimentar Goals.
- Alimentar Debt Engine.

---

# Objetivos Técnicos

Todas as projeções deverão ser:

Determinísticas

Reproduzíveis

Versionadas

Auditáveis

Cacheáveis

Incrementais

---

# Aggregate

CashFlowProjection

---

# Dependências

Income

Expense

Debt

Loan

Mortgage

Budget

Goal

Investment

Subscriptions

FinancialAccount

---

# Conceito

O Cash Flow não representa dinheiro.

Ele representa uma sequência cronológica de eventos financeiros.

Cada evento altera uma projeção.

---

# Timeline

Exemplo

01/07

+ Salário

R$ 12.650

↓

03/07

- Aluguel

R$ 700

↓

05/07

- Santander

R$ 2.547

↓

06/07

- Seguro

R$ 387

↓

08/07

+ Cashback

R$ 130

↓

10/07

- Mercado Pago

R$ 1.069

↓

Saldo Projetado

R$ 8.076

---

# Aggregate

CashFlowProjection

Campos

Id

WorkspaceId

ProjectionDate

OpeningBalance

Credits

Debits

ClosingBalance

ReservedAmount

AvailableAmount

RiskScore

HealthScore

CreatedAt

UpdatedAt

---

# Aggregate Filho

CashFlowEntry

Campos

Id

ProjectionId

SourceModule

ReferenceId

ReferenceType

Amount

Direction

ExecutionDate

Priority

Status

CreatedAt

---

# SourceModule

Income

Expense

Debt

Investment

Loan

Mortgage

Transfer

Budget

Goal

Subscription

Adjustment

---

# Direction

Credit

Debit

---

# Status

Scheduled

Projected

Executed

Cancelled

Ignored

---

# User Story

## US-086

Gerar Fluxo Diário

Priority

Highest

Story Points

21

---

## História

Como usuário

Quero visualizar meu fluxo diário

Para prever meu saldo.

---

# Fluxo

Usuário abre Dashboard

↓

Sistema consulta Snapshot

↓

Executa Projection Engine

↓

Ordena Timeline

↓

Calcula Saldo

↓

Calcula Risco

↓

Atualiza Dashboard

---

# User Story

## US-087

Simulação

Como usuário

Quero simular alterações

Para comparar cenários.

---

Exemplo

Quitar Amex hoje

↓

Recalcular Timeline

↓

Novo Saldo

↓

Nova Economia

↓

Nenhum dado persistido

---

# User Story

## US-088

Motor de Projeção

Objetivo

Construir projeções.

---

Entrada

Receitas

Despesas

Parcelamentos

Financiamentos

Metas

Investimentos

Assinaturas

Transferências

---

Saída

Saldo Diário

Saldo Semanal

Saldo Mensal

Saldo Anual

Saldo Futuro

Dias Negativos

Maior Saldo

Menor Saldo

---

# Algoritmo

Ordenar Timeline

↓

Aplicar Créditos

↓

Aplicar Débitos

↓

Reservas

↓

Compromissos

↓

Saldo

↓

Health Score

↓

Risk Score

↓

Persistir Snapshot

---

# User Story

## US-089

Alerta de Caixa

Objetivo

Antecipar problemas.

---

Exemplos

Saldo negativo previsto em 14/08.

Reserva insuficiente.

Fluxo comprometido.

Comprometimento acima de 80%.

---

# User Story

## US-090

Snapshots

Objetivo

Versionar projeções.

---

Cada alteração financeira gera um novo Snapshot.

Nunca alterar Snapshots antigos.

---

# Modelo

CashFlowSnapshot

↓

CashFlowProjection

↓

CashFlowEntry

↓

CashFlowSummary

↓

CashFlowRisk

---

# APIs

POST

/api/v1/cashflow/recalculate

GET

/api/v1/cashflow

GET

/api/v1/cashflow/timeline

GET

/api/v1/cashflow/projections

GET

/api/v1/cashflow/calendar

POST

/api/v1/cashflow/simulate

---

# Eventos

CashFlowCalculated

ProjectionUpdated

RiskDetected

NegativeBalancePredicted

SnapshotGenerated

SimulationExecuted

---

# Dashboard

Mostrar

Saldo Atual

Saldo Projetado

Saldo Disponível

Comprometido

Reservado

Dias Negativos

Maior Saldo

Menor Saldo

Próxima Entrada

Próxima Saída

---

# Regras de Negócio

BR-086-001

Todo cálculo deverá utilizar Snapshot.

BR-086-002

Nenhuma projeção altera dados reais.

BR-086-003

Timeline sempre ordenada.

BR-086-004

Nunca modificar histórico.

BR-086-005

Permitir múltiplas moedas futuramente.

BR-086-006

Todas as projeções deverão ser reproduzíveis.

BR-086-007

Projection Engine independente.

BR-086-008

Cache permitido.

BR-086-009

Atualização incremental.

BR-086-010

Eventos idempotentes.

---

# Índices

WorkspaceId

ProjectionDate

Status

RiskScore

ExecutionDate

---

# Observabilidade

Registrar

WorkspaceId

ExecutionTime

ProjectionTime

CorrelationId

SnapshotId

TimelineSize

MemoryUsage

EngineVersion

---

# Auditoria

Registrar

Quem executou

Versão do algoritmo

Tempo

Resultado

Snapshot

---

# Testes

Unitários

Integração

Arquitetura

Stress

Carga

Performance

Precisão

Determinismo

---

# Requisitos Não Funcionais

100.000 eventos financeiros

↓

Tempo máximo

2 segundos

---

Precisão decimal

Decimal(19,4)

---

Todas as projeções deverão produzir exatamente o mesmo resultado utilizando os mesmos dados.

---

# AI Context

Este módulo deverá ser implementado utilizando uma arquitetura baseada em Pipeline.

Pipeline

↓

Timeline Builder

↓

Projection Builder

↓

Risk Analyzer

↓

Health Analyzer

↓

Snapshot Generator

↓

Dashboard Publisher

Cada componente deverá possuir responsabilidade única.

Nenhum componente poderá acessar banco diretamente.

Todo cálculo deverá ocorrer em memória.

Persistência somente ao final do Pipeline.

Todo algoritmo deverá ser determinístico.

Todo componente deverá ser facilmente substituível.

---

# Product Vision

No futuro, este módulo permitirá:

- Simulação Monte Carlo.
- Cenários econômicos.
- Inflação.
- Câmbio.
- IA Preditiva.
- Open Finance.
- Machine Learning.
- Planejamento Patrimonial.
- Planejamento Tributário.

Todo o restante do Flora Finance dependerá deste módulo.

Por isso, ele deverá possuir o maior nível de cobertura de testes do sistema.

Meta mínima:

98% de cobertura.

-----

# Flora Finance

# Volume 16

## EPIC 16 — Investment Portfolio Engine

Version: 1.0

Status: Approved

Owner: Product Owner

Domain: Wealth Management

Bounded Context: Investments

Aggregate Root: InvestmentPortfolio

---

# Visão

O módulo Investment Portfolio é responsável por representar todo patrimônio investido do usuário.

O objetivo deste módulo não é substituir uma corretora.

Seu objetivo é consolidar patrimônio.

O usuário poderá acompanhar:

- Patrimônio
- Rentabilidade
- Dividendos
- Juros
- Proventos
- Evolução
- Alocação
- Performance

Este módulo deverá conversar diretamente com:

- Dashboard
- Goals
- Cash Flow
- Financial Intelligence
- Reports

---

# Objetivos Estratégicos

Construção de patrimônio.

Controle de ativos.

Planejamento de aposentadoria.

Planejamento FIRE.

Integração futura com Open Finance.

---

# Aggregate

InvestmentPortfolio

---

# Dependências

Workspace

FinancialAccount

CashFlow

Goals

Reports

---

# User Story

## US-091

Criar Carteira

Priority

Highest

Story Points

13

---

## História

Como usuário

Quero cadastrar uma carteira

Para organizar meus investimentos.

---

# Campos

Id

WorkspaceId

Name

Description

Currency

Broker

Institution

CreatedAt

UpdatedAt

DeletedAt

---

# Regras

BR-091-001

Toda carteira pertence a um Workspace.

BR-091-002

Nome obrigatório.

BR-091-003

Nunca excluir histórico.

BR-091-004

Soft Delete obrigatório.

---

# User Story

## US-092

Cadastrar Ativo

---

Objetivo

Adicionar ativos.

---

Tipos

Ação

ETF

FII

BDR

Tesouro

CDB

LCI

LCA

Criptomoeda

Fundo

Previdência

Conta Remunerada

Outro

---

Campos

Ticker

Nome

Quantidade

Preço Médio

Preço Atual

Moeda

Data Compra

Corretora

Categoria

---

# User Story

## US-093

Movimentações

---

Objetivo

Registrar

Compra

Venda

Dividendos

JCP

Bonificação

Desdobramento

Agrupamento

Transferência

Amortização

---

Cada movimentação deverá gerar histórico imutável.

---

# User Story

## US-094

Rentabilidade

---

Objetivo

Calcular

Rentabilidade diária

Mensal

Anual

Acumulada

Real

Nominal

---

Indicadores

ROI

IRR

TWR

Dividend Yield

Yield on Cost

Patrimônio

Lucro

Prejuízo

---

# User Story

## US-095

Alocação

---

Objetivo

Visualizar distribuição.

---

Por

Classe

Instituição

Setor

Moeda

País

Corretora

---

Resultado

Percentual

Valor

Meta

Diferença

---

# User Story

## US-096

Rebalanceamento

---

Objetivo

Sugerir ajustes.

---

Exemplo

Carteira alvo

Ações

40%

Atual

52%

↓

Sugestão

Reduzir

12%

---

# Eventos

PortfolioCreated

AssetAdded

AssetRemoved

DividendReceived

PortfolioRebalanced

PortfolioUpdated

---

# APIs

POST

/api/v1/portfolio

GET

/api/v1/portfolio

POST

/api/v1/assets

PUT

/api/v1/assets/{id}

DELETE

/api/v1/assets/{id}

GET

/api/v1/portfolio/performance

GET

/api/v1/portfolio/allocation

GET

/api/v1/portfolio/recommendations

---

# Modelo

InvestmentPortfolio

↓

InvestmentAsset

↓

InvestmentTransaction

↓

Dividend

↓

Allocation

↓

Performance

↓

Recommendation

---

# Dashboard

Exibir

Patrimônio

Rentabilidade

Dividendos

Maior posição

Menor posição

Concentração

Histórico

Projeção

---

# Regras de Negócio

BR-091-005

Dividendos deverão atualizar Cash Flow.

BR-091-006

Venda deverá recalcular preço médio.

BR-091-007

Nunca alterar histórico.

BR-091-008

Toda movimentação deverá gerar auditoria.

BR-091-009

Performance deverá ser recalculada incrementalmente.

BR-091-010

Permitir múltiplas moedas.

---

# Observabilidade

Registrar

WorkspaceId

PortfolioId

ExecutionTime

CorrelationId

Assets

Transactions

PerformanceVersion

---

# Auditoria

Registrar

Usuário

Movimentação

Valores antigos

Valores novos

Timestamp

---

# Testes

Unitários

Integração

Arquitetura

Precisão

Performance

Carga

---

# Requisitos Não Funcionais

Suportar

100.000 movimentações

10.000 ativos

1.000 carteiras

Tempo máximo

1 segundo

---

# AI Context

Este módulo deverá utilizar uma arquitetura baseada em Projection Services.

Investment Engine

↓

PortfolioCalculationService

↓

AllocationService

↓

PerformanceService

↓

DividendService

↓

RebalancingService

↓

RecommendationService

Cada cálculo deverá ser independente.

Todos os indicadores deverão ser reproduzíveis.

Nenhum cálculo poderá modificar histórico.

---

# Roadmap Futuro

Open Finance

B3

Binance

Interactive Brokers

XP

BTG

Rico

Nubank

Banco Inter

Avenue

---

# Product Vision

No futuro o usuário poderá visualizar:

Patrimônio Líquido

↓

Patrimônio Financeiro

↓

Patrimônio Imobiliário

↓

Patrimônio Empresarial

↓

Patrimônio Total

↓

Aposentadoria Projetada

↓

Independência Financeira

↓

FIRE Score

↓

Expected Retirement Date

Este módulo servirá como base para transformar o Flora Finance em uma plataforma completa de gestão patrimonial.
-------------------
# Flora Finance

# Volume 16

## EPIC 16 — Investment Portfolio Engine

Version: 1.0

Status: Approved

Owner: Product Owner

Domain: Wealth Management

Bounded Context: Investments

Aggregate Root: InvestmentPortfolio

---

# Visão

O módulo Investment Portfolio é responsável por representar todo patrimônio investido do usuário.

O objetivo deste módulo não é substituir uma corretora.

Seu objetivo é consolidar patrimônio.

O usuário poderá acompanhar:

- Patrimônio
- Rentabilidade
- Dividendos
- Juros
- Proventos
- Evolução
- Alocação
- Performance

Este módulo deverá conversar diretamente com:

- Dashboard
- Goals
- Cash Flow
- Financial Intelligence
- Reports

---

# Objetivos Estratégicos

Construção de patrimônio.

Controle de ativos.

Planejamento de aposentadoria.

Planejamento FIRE.

Integração futura com Open Finance.

---

# Aggregate

InvestmentPortfolio

---

# Dependências

Workspace

FinancialAccount

CashFlow

Goals

Reports

---

# User Story

## US-091

Criar Carteira

Priority

Highest

Story Points

13

---

## História

Como usuário

Quero cadastrar uma carteira

Para organizar meus investimentos.

---

# Campos

Id

WorkspaceId

Name

Description

Currency

Broker

Institution

CreatedAt

UpdatedAt

DeletedAt

---

# Regras

BR-091-001

Toda carteira pertence a um Workspace.

BR-091-002

Nome obrigatório.

BR-091-003

Nunca excluir histórico.

BR-091-004

Soft Delete obrigatório.

---

# User Story

## US-092

Cadastrar Ativo

---

Objetivo

Adicionar ativos.

---

Tipos

Ação

ETF

FII

BDR

Tesouro

CDB

LCI

LCA

Criptomoeda

Fundo

Previdência

Conta Remunerada

Outro

---

Campos

Ticker

Nome

Quantidade

Preço Médio

Preço Atual

Moeda

Data Compra

Corretora

Categoria

---

# User Story

## US-093

Movimentações

---

Objetivo

Registrar

Compra

Venda

Dividendos

JCP

Bonificação

Desdobramento

Agrupamento

Transferência

Amortização

---

Cada movimentação deverá gerar histórico imutável.

---

# User Story

## US-094

Rentabilidade

---

Objetivo

Calcular

Rentabilidade diária

Mensal

Anual

Acumulada

Real

Nominal

---

Indicadores

ROI

IRR

TWR

Dividend Yield

Yield on Cost

Patrimônio

Lucro

Prejuízo

---

# User Story

## US-095

Alocação

---

Objetivo

Visualizar distribuição.

---

Por

Classe

Instituição

Setor

Moeda

País

Corretora

---

Resultado

Percentual

Valor

Meta

Diferença

---

# User Story

## US-096

Rebalanceamento

---

Objetivo

Sugerir ajustes.

---

Exemplo

Carteira alvo

Ações

40%

Atual

52%

↓

Sugestão

Reduzir

12%

---

# Eventos

PortfolioCreated

AssetAdded

AssetRemoved

DividendReceived

PortfolioRebalanced

PortfolioUpdated

---

# APIs

POST

/api/v1/portfolio

GET

/api/v1/portfolio

POST

/api/v1/assets

PUT

/api/v1/assets/{id}

DELETE

/api/v1/assets/{id}

GET

/api/v1/portfolio/performance

GET

/api/v1/portfolio/allocation

GET

/api/v1/portfolio/recommendations

---

# Modelo

InvestmentPortfolio

↓

InvestmentAsset

↓

InvestmentTransaction

↓

Dividend

↓

Allocation

↓

Performance

↓

Recommendation

---

# Dashboard

Exibir

Patrimônio

Rentabilidade

Dividendos

Maior posição

Menor posição

Concentração

Histórico

Projeção

---

# Regras de Negócio

BR-091-005

Dividendos deverão atualizar Cash Flow.

BR-091-006

Venda deverá recalcular preço médio.

BR-091-007

Nunca alterar histórico.

BR-091-008

Toda movimentação deverá gerar auditoria.

BR-091-009

Performance deverá ser recalculada incrementalmente.

BR-091-010

Permitir múltiplas moedas.

---

# Observabilidade

Registrar

WorkspaceId

PortfolioId

ExecutionTime

CorrelationId

Assets

Transactions

PerformanceVersion

---

# Auditoria

Registrar

Usuário

Movimentação

Valores antigos

Valores novos

Timestamp

---

# Testes

Unitários

Integração

Arquitetura

Precisão

Performance

Carga

---

# Requisitos Não Funcionais

Suportar

100.000 movimentações

10.000 ativos

1.000 carteiras

Tempo máximo

1 segundo

---

# AI Context

Este módulo deverá utilizar uma arquitetura baseada em Projection Services.

Investment Engine

↓

PortfolioCalculationService

↓

AllocationService

↓

PerformanceService

↓

DividendService

↓

RebalancingService

↓

RecommendationService

Cada cálculo deverá ser independente.

Todos os indicadores deverão ser reproduzíveis.

Nenhum cálculo poderá modificar histórico.

---

# Roadmap Futuro

Open Finance

B3

Binance

Interactive Brokers

XP

BTG

Rico

Nubank

Banco Inter

Avenue

---

# Product Vision

No futuro o usuário poderá visualizar:

Patrimônio Líquido

↓

Patrimônio Financeiro

↓

Patrimônio Imobiliário

↓

Patrimônio Empresarial

↓

Patrimônio Total

↓

Aposentadoria Projetada

↓

Independência Financeira

↓

FIRE Score

↓

Expected Retirement Date

Este módulo servirá como base para transformar o Flora Finance em uma plataforma completa de gestão patrimonial.
------

# Flora Finance

# Volume 17

## EPIC 17 — Subscription & Recurring Payments Engine

Version: 1.0

Status: Approved

Owner: Product Owner

Domain: Financial Core

Bounded Context: Subscriptions

Aggregate Root: Subscription

---

# Visão

Este módulo é responsável pelo gerenciamento de todas as cobranças recorrentes.

O objetivo não é apenas lembrar o usuário.

O objetivo é construir previsibilidade financeira.

Toda despesa recorrente deverá possuir uma Subscription.

Exemplos

Netflix

Spotify

Academia

Plano de Saúde

Seguro

Internet

Aluguel

IPTU

IPVA

Servidor VPS

Azure

AWS

OpenAI

ChatGPT

Claude

GitHub

Domínio

Energia

Água

Telefone

Internet

Escola

Pensão

Financiamentos

---

# Problema

Mais de 80% das despesas mensais do usuário serão recorrentes.

Se elas forem modeladas apenas como Expenses comuns:

- não existe previsão
- não existe reajuste
- não existe histórico
- não existe renovação
- não existe alerta

---

# Objetivos

Prever despesas.

Atualizar Cash Flow.

Atualizar Budget.

Atualizar IA.

Permitir renegociação.

Detectar aumentos.

Detectar assinaturas esquecidas.

---

# Aggregate

Subscription

---

# Dependências

Workspace

Expense

CashFlow

Budget

Financial Intelligence

Notification Center

---

# User Story

## US-097

Cadastrar Assinatura

Priority

Highest

Story Points

13

---

Como usuário

Quero cadastrar uma assinatura

Para controlar despesas recorrentes.

---

# Campos

Id

WorkspaceId

CategoryId

Description

Vendor

Amount

Currency

Frequency

NextExecution

StartDate

EndDate

AutoGenerateExpense

ReminderDays

Status

CreatedAt

UpdatedAt

DeletedAt

---

# Frequency

Daily

Weekly

BiWeekly

Monthly

Quarterly

SemiAnnual

Annual

Custom

---

# Status

Draft

Active

Paused

Cancelled

Expired

Archived

---

# Regras

BR-097-001

Toda assinatura pertence a um Workspace.

BR-097-002

Valor obrigatório.

BR-097-003

Periodicidade obrigatória.

BR-097-004

Permitir pausa.

BR-097-005

Nunca excluir histórico.

BR-097-006

Soft Delete obrigatório.

BR-097-007

Permitir renovação.

---

# User Story

## US-098

Gerador Automático

Objetivo

Criar automaticamente despesas futuras.

---

Fluxo

Scheduler

↓

Buscar Assinaturas

↓

Calcular próximas execuções

↓

Criar Expense

↓

Atualizar Cash Flow

↓

Atualizar Dashboard

↓

Enviar Evento

---

# Estratégia

Não utilizar Cron diretamente.

Criar um Scheduler Service desacoplado.

---

# User Story

## US-099

Detecção de Reajuste

Objetivo

Detectar aumentos.

---

Exemplo

Spotify

R$ 19,90

↓

R$ 24,90

↓

Sistema identifica aumento

↓

Gerar Insight

↓

Atualizar projeção

---

# User Story

## US-100

Renovação

Objetivo

Controlar contratos.

---

Exemplos

Seguro

Plano Saúde

Domínio

Azure

AWS

Netflix

ChatGPT

Academia

---

# Campos

RenewalDate

NoticePeriod

CancellationDeadline

AutoRenew

---

# User Story

## US-101

Assinaturas Esquecidas

Objetivo

Detectar serviços sem utilização.

---

Critérios

Sem login.

Sem movimentação.

Sem alteração.

Sem utilização.

Mais de X meses.

---

Resultado

Insight

Possível economia.

---

# User Story

## US-102

Calendário

Objetivo

Visualizar cobranças.

---

Resultado

Hoje

Esta semana

Este mês

Próximo mês

Próximos 12 meses

---

# APIs

POST

/api/v1/subscriptions

GET

/api/v1/subscriptions

PUT

/api/v1/subscriptions/{id}

DELETE

/api/v1/subscriptions/{id}

POST

/api/v1/subscriptions/generate

GET

/api/v1/subscriptions/calendar

GET

/api/v1/subscriptions/renewals

---

# Eventos

SubscriptionCreated

SubscriptionUpdated

SubscriptionCancelled

SubscriptionRenewed

ExpenseGenerated

RenewalDetected

PriceChanged

UnusedSubscriptionDetected

---

# Modelo

Subscription

↓

SubscriptionSchedule

↓

SubscriptionExecution

↓

SubscriptionRenewal

↓

SubscriptionHistory

↓

SubscriptionInsight

---

# Regras de Negócio

BR-097-008

Toda execução deverá ser idempotente.

BR-097-009

Uma assinatura nunca poderá gerar duas despesas para a mesma competência.

BR-097-010

Alteração de valor deverá manter histórico.

BR-097-011

Renovação automática deverá respeitar configuração.

BR-097-012

Reajustes deverão atualizar projeções.

BR-097-013

Eventos deverão ser publicados somente após persistência.

BR-097-014

Scheduler deverá suportar reprocessamento.

---

# Scheduler

Sugestão

SubscriptionScheduler

↓

SubscriptionGenerator

↓

ProjectionUpdater

↓

NotificationPublisher

↓

InsightPublisher

---

# Dashboard

Exibir

Total mensal recorrente

Total anual

Maior assinatura

Maior reajuste

Próxima cobrança

Renovações

Economia potencial

---

# Observabilidade

Registrar

WorkspaceId

SubscriptionId

ExecutionDate

ExecutionTime

SchedulerVersion

CorrelationId

---

# Auditoria

Registrar

Usuário

Valor anterior

Novo valor

Data

Motivo

Origem

---

# Testes

Unitários

Integração

Scheduler

Idempotência

Carga

Performance

Concorrência

---

# Requisitos Não Funcionais

50.000 assinaturas

↓

Execução diária

↓

Tempo máximo

30 segundos

---

Nenhuma assinatura poderá ser executada duas vezes para o mesmo período.

---

# AI Context

Este módulo deverá ser implementado utilizando Event-Driven Architecture.

Sugestão

SubscriptionCreated

↓

Background Job

↓

ExpenseGenerated

↓

CashFlowUpdated

↓

DashboardUpdated

↓

InsightGenerated

Nunca utilizar lógica de recorrência dentro do Aggregate.

Toda recorrência deverá ser calculada por um Domain Service.

---

# Product Vision

No futuro este módulo permitirá:

- integração com Open Finance;
- importação automática de assinaturas;
- detecção de aumentos por IA;
- previsão de reajustes;
- comparação de preços;
- sugestão de substituição de serviços;
- cancelamento assistido.

Este módulo deverá se tornar o principal responsável pela previsibilidade das despesas recorrentes do usuário.

------
# Flora Finance

# Volume 18

## EPIC 18 — Financial Calendar Engine

Version: 1.0

Status: Approved

Owner: Product Owner

Domain: Timeline

Bounded Context: Calendar

Aggregate Root: FinancialCalendar

---

# Visão

O Financial Calendar não representa apenas um calendário.

Ele representa todos os eventos financeiros do usuário organizados cronologicamente.

Este módulo será utilizado como fonte única da verdade para eventos temporais.

Nenhum outro módulo deverá manter calendários próprios.

Todos deverão publicar eventos para o Calendar Engine.

---

# Objetivos Estratégicos

Centralizar agenda financeira.

Antecipar riscos.

Melhorar previsibilidade.

Construir Timeline.

Aumentar produtividade.

Reduzir esquecimentos.

---

# Aggregate

FinancialCalendar

---

# Dependências

Income

Expense

Debt

Investment

Subscription

Goal

Cash Flow

Notifications

---

# Conceito

Cada item do calendário representa um Financial Event.

Um Financial Event pode representar:

Receita

Despesa

Parcela

Dividendos

Pagamento

Renovação

Meta

Aporte

Transferência

Lembrete

Vencimento

---

# Aggregate

FinancialEvent

Campos

Id

WorkspaceId

ReferenceId

ReferenceType

Title

Description

Category

ExecutionDate

StartDate

EndDate

Priority

Severity

Status

ReminderDate

CreatedAt

UpdatedAt

DeletedAt

---

# ReferenceType

Income

Expense

Debt

Investment

Subscription

Goal

Reminder

Transfer

Other

---

# Priority

Critical

High

Medium

Low

---

# Severity

Critical

Warning

Normal

Information

---

# Status

Scheduled

Completed

Cancelled

Overdue

Ignored

---

# User Story

## US-103

Visualizar Calendário

Priority

Highest

Story Points

13

---

Como usuário

Quero visualizar todos os meus compromissos financeiros em um calendário

Para planejar minhas finanças.

---

# Modos

Diário

Semanal

Mensal

Trimestral

Anual

Timeline

Agenda

---

# User Story

## US-104

Gerar Eventos

Objetivo

Todos os módulos deverão publicar eventos.

---

Exemplo

ExpenseCreated

↓

FinancialEventCreated

---

SubscriptionRenewed

↓

FinancialEventCreated

---

DividendReceived

↓

FinancialEventCreated

---

GoalContributionScheduled

↓

FinancialEventCreated

---

# User Story

## US-105

Alertas

Objetivo

Gerar lembretes.

---

Exemplos

Conta vence amanhã.

Seguro vence em 15 dias.

Renegociação termina hoje.

Meta atrasada.

Parcela vence hoje.

---

# Configurações

30 dias

15 dias

7 dias

3 dias

1 dia

No mesmo dia

Após vencimento

---

# User Story

## US-106

Conflitos

Objetivo

Detectar excesso de eventos.

---

Exemplo

Mesmo dia

↓

Santander

↓

Mercado Pago

↓

Seguro

↓

Pensão

↓

Saldo insuficiente

↓

Insight

---

# User Story

## US-107

Linha do Tempo

Objetivo

Construir Timeline completa.

---

Resultado

Hoje

↓

Amanhã

↓

Semana

↓

Mês

↓

Ano

↓

Futuro

---

# APIs

POST

/api/v1/calendar/events

GET

/api/v1/calendar

GET

/api/v1/calendar/day

GET

/api/v1/calendar/week

GET

/api/v1/calendar/month

GET

/api/v1/calendar/timeline

GET

/api/v1/calendar/upcoming

---

# Eventos

FinancialEventCreated

FinancialEventUpdated

FinancialEventCancelled

ReminderTriggered

ConflictDetected

TimelineUpdated

---

# Modelo

FinancialCalendar

↓

FinancialEvent

↓

Reminder

↓

Conflict

↓

Timeline

↓

Agenda

---

# Dashboard

Exibir

Próximos vencimentos

Hoje

Esta semana

Este mês

Eventos críticos

Maior impacto

Alertas

---

# Regras de Negócio

BR-103-001

Todo evento pertence a um Workspace.

BR-103-002

Eventos nunca poderão ser alterados diretamente.

BR-103-003

Eventos deverão ser regenerados pelo módulo de origem.

BR-103-004

Eventos recorrentes deverão possuir origem.

BR-103-005

Permitir lembretes múltiplos.

BR-103-006

Permitir filtros.

BR-103-007

Toda alteração gera auditoria.

BR-103-008

Eventos deverão possuir timezone.

BR-103-009

Eventos deverão suportar recorrência.

BR-103-010

Permitir anexos futuramente.

---

# Scheduler

Calendar Builder

↓

Timeline Generator

↓

Reminder Generator

↓

Conflict Analyzer

↓

Notification Publisher

↓

Dashboard Publisher

---

# Observabilidade

Registrar

WorkspaceId

EventId

TimelineVersion

ExecutionTime

CorrelationId

---

# Auditoria

Registrar

Origem

Usuário

Evento

Timestamp

Payload

---

# Testes

Unitários

Integração

Timeline

Scheduler

Timezone

Performance

Carga

---

# Requisitos Não Funcionais

1 milhão de eventos

↓

Pesquisa

< 500ms

---

Timeline

↓

Construção

< 2 segundos

---

Timezone

100%

Compatível

---

# AI Context

Este módulo deverá funcionar como uma Projection.

Nenhum Aggregate deverá consultar diretamente o Calendar.

Todos publicarão Domain Events.

O Calendar construirá uma Timeline própria.

Sugestão

Domain Event

↓

Outbox

↓

Background Worker

↓

Calendar Builder

↓

Timeline Projection

↓

Dashboard

↓

Notifications

---

# Roadmap Futuro

Google Calendar

Outlook

Apple Calendar

ICS Export

ICS Import

Sincronização Bidirecional

Agenda Compartilhada

Eventos Empresariais

---

# Product Vision

O usuário deverá conseguir abrir o Calendar e responder imediatamente:

"O que acontecerá financeiramente na minha vida durante os próximos 12 meses?"

Este será o principal objetivo deste módulo.

--------
# Flora Finance

# Volume 19

## EPIC 19 — Notification Center

Version: 1.0

Status: Approved

Owner: Product Owner

Domain: Communication

Bounded Context: Notification Center

Aggregate Root: Notification

---

# Visão

O Notification Center é responsável por toda comunicação entre o Flora Finance e seus usuários.

Nenhum outro módulo poderá enviar notificações diretamente.

Todos deverão publicar Domain Events.

O Notification Center decidirá:

- se envia
- quando envia
- para quem envia
- por qual canal envia

Este módulo funciona como um Orquestrador de Comunicação.

---

# Objetivos Estratégicos

Centralizar notificações.

Eliminar duplicidade.

Permitir novos canais.

Respeitar preferências.

Evitar spam.

Construir histórico.

---

# Objetivos Técnicos

Event Driven

Retry

Outbox

Inbox

Rate Limit

Observabilidade

---

# Aggregate

Notification

---

# Dependências

Financial Calendar

Goals

Debt

Cash Flow

Subscriptions

Dashboard

Financial Intelligence

Authentication

User Preferences

---

# Canais

In-App

Email

Push Notification

SMS

WhatsApp

Webhook

Microsoft Teams

Slack

Discord

Telegram

---

# NotificationType

Reminder

Warning

Critical

Recommendation

Achievement

Promotion

Security

System

Financial

Goal

Investment

Subscription

---

# NotificationPriority

Critical

High

Medium

Low

Silent

---

# NotificationStatus

Pending

Queued

Processing

Sent

Delivered

Read

Failed

Cancelled

Expired

---

# User Story

## US-108

Enviar Notificação

Priority

Highest

Story Points

13

---

Como usuário

Quero receber notificações

Para acompanhar meus compromissos financeiros.

---

# Fluxo

ExpenseCreated

↓

Domain Event

↓

Notification Rule Engine

↓

Template

↓

Channel Selection

↓

Queue

↓

Provider

↓

Delivery

↓

Tracking

↓

Dashboard

---

# User Story

## US-109

Preferências

Objetivo

Permitir personalização.

---

Exemplos

Receber somente Push

↓

Receber Email

↓

Silenciar SMS

↓

Não receber promoções

↓

Receber apenas alertas críticos

---

# User Story

## US-110

Agendamento

Objetivo

Permitir notificações futuras.

---

Exemplo

Seguro vence em

30 dias

↓

Enviar Email

---

7 dias

↓

Push

---

1 dia

↓

WhatsApp

---

No dia

↓

Push + Email

---

# User Story

## US-111

Retry

Objetivo

Garantir entrega.

---

Estratégia

Tentativa 1

↓

5 minutos

↓

Tentativa 2

↓

30 minutos

↓

Tentativa 3

↓

6 horas

↓

Falhou

↓

Dead Letter Queue

---

# User Story

## US-112

Centro de Mensagens

Objetivo

Histórico completo.

---

Filtros

Lidas

Não Lidas

Críticas

Financeiras

Sistema

Promoções

Arquivadas

---

# User Story

## US-113

Templates

Objetivo

Padronizar mensagens.

---

Exemplo

Título

Seguro vence amanhã

Mensagem

O seguro do veículo vencerá amanhã.

Valor

R$ 387,55

Ação

Abrir despesa

---

# APIs

POST

/api/v1/notifications/send

GET

/api/v1/notifications

GET

/api/v1/notifications/unread

PATCH

/api/v1/notifications/{id}/read

PATCH

/api/v1/notifications/preferences

GET

/api/v1/notifications/history

---

# Eventos

NotificationCreated

NotificationQueued

NotificationSent

NotificationDelivered

NotificationRead

NotificationFailed

PreferenceUpdated

---

# Modelo

Notification

↓

NotificationTemplate

↓

NotificationPreference

↓

NotificationDelivery

↓

NotificationChannel

↓

NotificationHistory

↓

NotificationProvider

---

# Providers

SMTP

Firebase

Twilio

WhatsApp Cloud API

Azure Communication Services

Amazon SES

Amazon SNS

OneSignal

---

# Regras de Negócio

BR-108-001

Toda notificação pertence a um Workspace.

BR-108-002

Usuário pode desativar canais.

BR-108-003

Alertas críticos nunca poderão ser desativados.

BR-108-004

Notificações duplicadas deverão ser agrupadas.

BR-108-005

Toda entrega deverá gerar auditoria.

BR-108-006

Retry obrigatório.

BR-108-007

Dead Letter Queue obrigatória.

BR-108-008

Templates versionados.

BR-108-009

Suporte à internacionalização.

BR-108-010

Suporte a placeholders dinâmicos.

---

# Placeholders

{{UserName}}

{{Amount}}

{{DueDate}}

{{Category}}

{{Goal}}

{{Debt}}

{{Subscription}}

{{Balance}}

---

# Scheduler

Notification Scheduler

↓

Template Engine

↓

Channel Selector

↓

Queue Publisher

↓

Retry Worker

↓

Delivery Tracker

↓

Dashboard

---

# Observabilidade

Registrar

WorkspaceId

NotificationId

TemplateVersion

Provider

ExecutionTime

Retries

CorrelationId

---

# Auditoria

Registrar

Canal

Conteúdo

Destino

Resultado

Erro

Timestamp

---

# Testes

Unitários

Integração

Retry

Carga

Performance

Rate Limit

Templates

---

# Requisitos Não Funcionais

100 mil notificações/hora

↓

Tempo máximo

2 segundos

↓

Retry

100%

↓

Disponibilidade

99,9%

---

# AI Context

O Notification Center deverá ser completamente desacoplado.

Nenhum Aggregate poderá conhecer este módulo.

Todos publicarão apenas Domain Events.

Sugestão

Domain Event

↓

Outbox

↓

Broker

↓

Notification Consumer

↓

Template Engine

↓

Provider

↓

Tracking

↓

Dashboard

---

# Product Vision

No futuro o Notification Center permitirá:

- IA gerar mensagens personalizadas.
- Resumos diários.
- Resumos semanais.
- Resumos mensais.
- Explicações financeiras.
- Alertas inteligentes.
- Digests.
- Comunicação omnichannel.
- A/B Testing de mensagens.
- Tradução automática.

O usuário deverá sentir que o Flora acompanha continuamente sua vida financeira.

-----

# Flora Finance

# Volume 20

## EPIC 20 — Dashboard Analytics Engine

Version: 1.0

Status: Approved

Owner: Product Owner

Domain: Analytics

Bounded Context: Dashboard

Projection: DashboardProjection

---

# Visão

O Dashboard é a principal interface do Flora Finance.

Ele não possui regras de negócio próprias.

Sua responsabilidade é consolidar informações produzidas pelos demais módulos e apresentá-las de forma clara, acionável e personalizada.

Nenhuma informação exibida no Dashboard deverá ser armazenada diretamente nele.

Todos os dados deverão ser derivados de projeções.

---

# Objetivos Estratégicos

- Centralizar indicadores.
- Facilitar tomada de decisão.
- Exibir saúde financeira.
- Destacar riscos.
- Evidenciar oportunidades.
- Permitir personalização.

---

# Princípios

- Tempo de carregamento inferior a 1 segundo.
- Informações sempre consistentes.
- Widgets independentes.
- Widgets configuráveis.
- Atualização incremental.
- Alta disponibilidade.

---

# Fontes de Dados

Income

Expense

Cash Flow

Debt

Goals

Subscriptions

Investments

Notifications

Calendar

Financial Intelligence

Budget

Authentication

---

# Conceito

Dashboard = composição de Widgets.

Cada Widget é independente.

Cada Widget possui sua própria Projection.

Nenhum Widget consulta diretamente outro Widget.

---

# Widget

Campos

Id

WorkspaceId

WidgetType

Title

Position

Size

Configuration

Visibility

RefreshInterval

CreatedAt

UpdatedAt

---

# WidgetType

FinancialHealth

CashFlow

DebtSummary

Budget

Goals

UpcomingBills

Subscriptions

InvestmentPortfolio

Insights

Notifications

Calendar

IncomeVsExpense

SpendingCategories

NetWorth

EmergencyReserve

Forecast

QuickActions

RecentTransactions

Custom

---

# User Story

## US-114

Dashboard Personalizável

Priority

Highest

Story Points

21

---

Como usuário

Quero personalizar meu Dashboard

Para visualizar apenas as informações relevantes para mim.

---

# Funcionalidades

Mover Widgets.

Redimensionar Widgets.

Ocultar Widgets.

Fixar Widgets.

Restaurar Layout.

Layouts múltiplos.

---

# User Story

## US-115

KPIs Financeiros

Objetivo

Apresentar indicadores principais.

---

Indicadores

Saldo Atual

Saldo Projetado

Patrimônio Líquido

Receitas

Despesas

Fluxo de Caixa

Comprometimento da Renda

Reserva de Emergência

Health Score

Debt Score

Budget Score

Goal Score

Investment Score

---

# User Story

## US-116

Insights Inteligentes

Objetivo

Exibir recomendações priorizadas.

---

Exemplos

Você ficará negativo em outubro.

Quite o cartão Amex primeiro.

Renegocie o Mercado Pago.

Seu orçamento de alimentação excedeu 92%.

Você economizará R$ 1.240 antecipando esta dívida.

---

# User Story

## US-117

Widgets Configuráveis

Objetivo

Permitir composição do Dashboard.

---

Cada Widget poderá definir

Filtros.

Período.

Categorias.

Contas.

Metas.

Moeda.

Tema.

Ordem.

---

# User Story

## US-118

Atualização em Tempo Real

Objetivo

Atualizar Dashboard automaticamente.

---

Fluxo

ExpenseCreated

↓

Projection Updated

↓

Dashboard Projection

↓

SignalR

↓

Angular Signals

↓

Widget Atualizado

---

# APIs

GET

/api/v1/dashboard

GET

/api/v1/dashboard/widgets

POST

/api/v1/dashboard/layout

PATCH

/api/v1/dashboard/widgets/{id}

GET

/api/v1/dashboard/summary

GET

/api/v1/dashboard/insights

---

# Eventos

DashboardProjectionUpdated

WidgetCreated

WidgetUpdated

LayoutChanged

InsightAdded

HealthScoreChanged

---

# Modelo

Dashboard

↓

DashboardLayout

↓

Widget

↓

WidgetProjection

↓

WidgetConfiguration

↓

WidgetSnapshot

---

# Widget Catalog

Financial Health Card

↓

Cash Flow Timeline

↓

Upcoming Bills

↓

Debt Ranking

↓

Goals Progress

↓

Budget Consumption

↓

Investment Allocation

↓

Subscriptions

↓

Monthly Evolution

↓

Income vs Expense

↓

Category Breakdown

↓

Recent Transactions

↓

Quick Actions

↓

Insights Feed

↓

Notifications

↓

Calendar

---

# Regras de Negócio

BR-114-001

Todo Dashboard pertence a um Workspace.

BR-114-002

Widgets nunca compartilham estado.

BR-114-003

Widgets deverão ser carregados independentemente.

BR-114-004

Widgets poderão falhar individualmente sem comprometer o Dashboard.

BR-114-005

Widgets deverão suportar Lazy Loading.

BR-114-006

Toda alteração de layout será persistida.

BR-114-007

Layouts poderão ser exportados futuramente.

BR-114-008

Widgets deverão possuir cache.

BR-114-009

Atualizações deverão ser incrementais.

BR-114-010

O Dashboard nunca recalcula regras de negócio.

---

# Layout

Desktop

12 colunas

↓

Tablet

8 colunas

↓

Mobile

4 colunas

---

# Dashboard Service

DashboardProjectionBuilder

↓

WidgetRegistry

↓

WidgetLoader

↓

WidgetProjectionService

↓

SignalRPublisher

↓

Angular Dashboard

---

# Observabilidade

Registrar

WorkspaceId

DashboardVersion

WidgetCount

LoadTime

CacheHit

CacheMiss

ExecutionTime

CorrelationId

---

# Auditoria

Registrar

Alterações de Layout

Widgets adicionados

Widgets removidos

Widgets ocultados

Preferências

---

# Testes

Unitários

Integração

Widgets

Performance

Lazy Loading

Responsividade

Acessibilidade

---

# Requisitos Não Funcionais

Dashboard inicial

↓

< 800 ms

Widgets

↓

Carregamento paralelo

↓

Atualização parcial

↓

Sem refresh completo

---

# AI Context

O Dashboard deverá ser implementado utilizando arquitetura baseada em composição.

Nenhum Widget deverá conhecer outro Widget.

Cada Widget possuirá:

Projection própria.

Endpoint próprio.

Cache próprio.

Permissões próprias.

Atualização própria.

Sugestão

Dashboard

↓

Widget Registry

↓

Widget Factory

↓

Projection Builder

↓

Cache Layer

↓

SignalR Publisher

↓

Angular Signals

---

# Product Vision

O Dashboard deverá se tornar o centro operacional do usuário.

Objetivos:

- Abrir o Flora e entender sua situação financeira em menos de 10 segundos.
- Identificar riscos sem navegar por menus.
- Executar ações rápidas diretamente pelos Widgets.
- Receber recomendações contextualizadas.
- Acompanhar evolução patrimonial.

No futuro, o Dashboard suportará:

- Widgets desenvolvidos por terceiros.
- Marketplace de Widgets.
- Dashboards compartilháveis.
- Dashboards para famílias.
- Dashboards empresariais.
- Dashboards para consultores financeiros.
- Widgets alimentados por IA.
- Temas personalizados.
- Layouts salvos por perfil.

------
# Flora Finance

# Volume 21

## EPIC 21 — Report Engine

Version: 1.0

Status: Approved

Owner: Product Owner

Domain: Reporting

Bounded Context: Reports

Aggregate Root: Report

---

# Visão

O Report Engine é responsável pela geração de documentos, análises e exportações do sistema.

Este módulo não cria dados.

Ele apenas transforma dados existentes em relatórios compreensíveis.

Todos os módulos poderão fornecer dados ao Report Engine.

Nenhum módulo poderá gerar PDFs diretamente.

---

# Objetivos Estratégicos

Centralizar geração de documentos.

Exportar dados.

Criar relatórios executivos.

Permitir auditoria.

Servir empresas.

Servir contadores.

Servir consultores financeiros.

---

# Objetivos Técnicos

Renderização desacoplada.

Templates versionados.

Exportação assíncrona.

Cache.

Fila de processamento.

---

# Aggregate

Report

---

# Dependências

Dashboard

Cash Flow

Income

Expense

Debt

Goals

Budget

Investments

Subscriptions

Calendar

Notifications

Financial Intelligence

---

# Tipos

Financial Summary

Executive Report

Cash Flow

Income Statement

Expense Analysis

Debt Report

Investment Report

Goal Progress

Budget Analysis

Subscription Report

Category Analysis

Tax Report

Annual Report

Monthly Report

Custom

---

# Exportações

PDF

CSV

Excel

JSON

XML

Markdown

HTML

---

# User Story

## US-119

Gerar Relatório

Priority

Highest

Story Points

13

---

Como usuário

Quero gerar relatórios

Para analisar minhas finanças.

---

# Fluxo

Selecionar relatório

↓

Selecionar período

↓

Selecionar filtros

↓

Gerar Snapshot

↓

Montar Dataset

↓

Renderizar

↓

Armazenar

↓

Disponibilizar Download

---

# User Story

## US-120

Templates

Objetivo

Padronizar documentos.

---

Cada template possuirá

Versão

Idioma

Tema

Formato

Cabeçalho

Rodapé

Widgets

Gráficos

KPIs

---

# User Story

## US-121

Filtros

Objetivo

Permitir filtros avançados.

---

Por

Conta

Categoria

Workspace

Meta

Cartão

Centro de custo

Projeto

Moeda

Período

Tags

---

# User Story

## US-122

Agendamento

Objetivo

Gerar automaticamente.

---

Exemplos

Todo dia

↓

Resumo diário

---

Toda segunda

↓

Resumo semanal

---

Todo mês

↓

Relatório financeiro

---

Todo ano

↓

Relatório patrimonial

---

# User Story

## US-123

Compartilhamento

Objetivo

Compartilhar relatórios.

---

Email

Link Seguro

Download

Exportação

Webhook

---

# APIs

POST

/api/v1/reports

GET

/api/v1/reports

GET

/api/v1/reports/{id}

GET

/api/v1/reports/download/{id}

POST

/api/v1/reports/schedule

GET

/api/v1/report-templates

---

# Eventos

ReportRequested

ReportStarted

ReportCompleted

ReportDownloaded

ReportShared

ScheduleExecuted

---

# Modelo

Report

↓

ReportTemplate

↓

ReportDataset

↓

ReportSnapshot

↓

ReportFile

↓

ReportSchedule

↓

ReportHistory

---

# Report Builder

Dataset Builder

↓

Projection Builder

↓

Template Engine

↓

Chart Engine

↓

Renderer

↓

Exporter

↓

Storage

---

# Formatos

PDF

↓

Excel

↓

CSV

↓

Markdown

↓

JSON

↓

HTML

---

# Dashboard

Mostrar

Últimos relatórios

Relatórios favoritos

Relatórios agendados

Downloads recentes

---

# Regras de Negócio

BR-119-001

Todo relatório deverá utilizar Snapshot.

BR-119-002

Nunca consultar dados em tempo real durante renderização.

BR-119-003

Templates deverão ser versionados.

BR-119-004

Toda geração deverá gerar auditoria.

BR-119-005

Arquivos deverão possuir expiração configurável.

BR-119-006

Relatórios poderão ser regenerados.

BR-119-007

Downloads deverão ser rastreados.

BR-119-008

Permitir internacionalização.

BR-119-009

Suportar temas.

BR-119-010

Renderização assíncrona obrigatória para relatórios pesados.

---

# Scheduler

Report Scheduler

↓

Snapshot Builder

↓

Dataset Builder

↓

Template Renderer

↓

Storage

↓

Notification Center

---

# Observabilidade

Registrar

WorkspaceId

ReportId

TemplateVersion

ExecutionTime

DatasetSize

OutputFormat

CorrelationId

---

# Auditoria

Registrar

Usuário

Tipo

Filtros

Arquivo

Download

Compartilhamento

Timestamp

---

# Testes

Unitários

Integração

Renderização

Performance

Templates

Exportação

Compatibilidade

---

# Requisitos Não Funcionais

100 mil linhas

↓

Excel

< 15 segundos

---

PDF

↓

100 páginas

< 10 segundos

---

CSV

↓

1 milhão linhas

Streaming

---

# AI Context

O Report Engine deverá utilizar arquitetura baseada em Pipelines.

Pipeline

↓

Snapshot Builder

↓

Dataset Builder

↓

Template Resolver

↓

Renderer

↓

Exporter

↓

Storage

↓

Notification

Cada etapa deverá ser independente.

Novos formatos poderão ser adicionados sem alterar os existentes.

Toda renderização deverá utilizar Snapshots imutáveis.

Nunca consultar diretamente Aggregates durante a geração.

---

# Roadmap Futuro

Power BI Connector

Looker

Grafana

Metabase

Google Sheets

Excel Online

Notion

Confluence

---

# Product Vision

O usuário deverá conseguir gerar um relatório completo de sua vida financeira em poucos segundos.

O sistema deverá permitir desde relatórios simples até documentos executivos utilizados por empresas, consultores financeiros e contadores.

O longo prazo prevê um Marketplace de Templates, permitindo que parceiros criem relatórios especializados para diferentes perfis de usuários.
-------
# Flora Finance

# Volume 22

## EPIC 22 — Open Finance Integration Platform

Version: 1.0

Status: Approved

Owner: Product Owner

Domain: Integrations

Bounded Context: Open Finance

Aggregate Root: FinancialInstitutionConnection

---

# Visão

O Open Finance Integration Platform é responsável por integrar o Flora Finance ao ecossistema financeiro brasileiro.

Este módulo permitirá sincronizar automaticamente:

- Bancos
- Cartões
- Contas Correntes
- Contas Digitais
- Empréstimos
- Financiamentos
- Investimentos
- PIX
- Saldo
- Extratos

O usuário deixará de cadastrar manualmente grande parte das movimentações.

Este módulo deverá ser totalmente desacoplado dos Aggregates financeiros.

Ele será apenas um provedor de dados.

---

# Objetivos Estratégicos

Eliminar lançamentos manuais.

Reduzir erros.

Sincronização automática.

Conciliação bancária.

Importação de investimentos.

Integração com IA.

---

# Objetivos Técnicos

Event Driven.

Background Workers.

Outbox Pattern.

Retry.

Rate Limit.

Idempotência.

Versionamento.

---

# Aggregate Root

FinancialInstitutionConnection

---

# Dependências

Workspace

FinancialAccount

Income

Expense

Investment

Debt

Cash Flow

Notifications

---

# User Story

## US-124

Conectar Instituição Financeira

Priority

Highest

Story Points

21

---

## História

Como usuário

Quero conectar minha instituição financeira

Para sincronizar automaticamente minhas informações.

---

# Fluxo

Selecionar instituição

↓

Consentimento

↓

OAuth/Open Finance

↓

Token

↓

Sincronização Inicial

↓

Criar Snapshot

↓

Importar Dados

↓

Criar Eventos

↓

Atualizar Dashboard

---

# Campos

Id

WorkspaceId

InstitutionId

InstitutionName

ConnectionStatus

ConsentId

AccessToken

RefreshToken

ExpiresAt

LastSynchronization

CreatedAt

UpdatedAt

DeletedAt

---

# ConnectionStatus

Pending

Authorizing

Authorized

Synchronizing

Active

Expired

Revoked

Disconnected

Error

---

# User Story

## US-125

Sincronizar Contas

Objetivo

Importar contas automaticamente.

---

Resultado

Conta Corrente

Conta Digital

Conta Poupança

Conta Internacional

Conta PJ

---

# User Story

## US-126

Sincronizar Transações

Objetivo

Importar movimentações.

---

Entradas

PIX

TED

DOC

Cartão

Transferência

Depósito

Pagamento

Tarifas

---

Resultado

Income

Expense

Transfer

Adjustment

---

# User Story

## US-127

Conciliação Inteligente

Objetivo

Evitar duplicidade.

---

Exemplo

Usuário lançou manualmente.

↓

Banco envia mesma operação.

↓

Sistema identifica.

↓

Sugere conciliação.

↓

Nunca duplicar.

---

# Estratégia

Matching por

Valor

Data

Conta

Descrição

Hash

Score

---

# User Story

## US-128

Sincronização Incremental

Objetivo

Buscar apenas alterações.

---

Fluxo

Última Sincronização

↓

Buscar alterações

↓

Validar

↓

Persistir

↓

Eventos

↓

Dashboard

---

# User Story

## US-129

Reconexão

Objetivo

Detectar expiração.

---

Token expirado

↓

Refresh

↓

Novo Token

↓

Sincronizar

---

Caso falhe

↓

Notificação

↓

Solicitar novo consentimento

---

# User Story

## US-130

Importação de Investimentos

Objetivo

Sincronizar carteira.

---

Ativos

Dividendos

Rendimentos

Posições

Movimentações

---

# APIs

POST

/api/v1/open-finance/connections

GET

/api/v1/open-finance/connections

POST

/api/v1/open-finance/synchronize

POST

/api/v1/open-finance/reconcile

GET

/api/v1/open-finance/institutions

DELETE

/api/v1/open-finance/connections/{id}

---

# Eventos

InstitutionConnected

SynchronizationStarted

SynchronizationCompleted

SynchronizationFailed

TransactionImported

TransactionMatched

AccountCreated

ConsentExpired

ReconnectRequired

---

# Modelo

FinancialInstitutionConnection

↓

Institution

↓

Consent

↓

SynchronizationJob

↓

ImportedTransaction

↓

Reconciliation

↓

SynchronizationHistory

↓

SynchronizationError

---

# Regras de Negócio

BR-124-001

Uma conexão pertence a um Workspace.

BR-124-002

Nunca armazenar credenciais bancárias.

BR-124-003

Tokens criptografados.

BR-124-004

Consentimentos deverão possuir expiração.

BR-124-005

Toda sincronização gera Snapshot.

BR-124-006

Toda sincronização gera Auditoria.

BR-124-007

Transações importadas nunca alteram histórico.

BR-124-008

Conciliação nunca poderá excluir lançamentos automaticamente.

BR-124-009

Operações deverão ser idempotentes.

BR-124-010

Sincronizações concorrentes são proibidas.

---

# Scheduler

Synchronization Scheduler

↓

Institution Connector

↓

Normalization Engine

↓

Matching Engine

↓

Projection Publisher

↓

Dashboard

↓

Notifications

---

# Normalization Engine

Objetivo

Transformar diferentes formatos bancários em um modelo único.

---

Modelo Interno

FinancialTransaction

↓

Income

↓

Expense

↓

Transfer

↓

Investment

---

# Matching Engine

Critérios

Valor

Data

Conta

Descrição

Hash

Machine Score

---

# Dashboard

Mostrar

Última sincronização

Instituições conectadas

Novas movimentações

Movimentações conciliadas

Falhas

Status

---

# Observabilidade

Registrar

WorkspaceId

InstitutionId

SynchronizationId

ExecutionTime

ImportedRecords

MatchedRecords

Errors

CorrelationId

---

# Auditoria

Registrar

Consentimento

Instituição

Usuário

Data

Quantidade importada

Quantidade conciliada

Erros

---

# Segurança

OAuth2 obrigatório.

Consentimento obrigatório.

Criptografia AES-256 para dados sensíveis.

Tokens nunca expostos à aplicação cliente.

TLS obrigatório.

Rotação automática de credenciais.

---

# Testes

Unitários

Integração

Contrato

Carga

Performance

Retry

Idempotência

Conciliação

---

# Requisitos Não Funcionais

100 instituições

↓

Conexões simultâneas

↓

100 mil transações

↓

Sincronização incremental

↓

Tempo máximo

5 minutos

---

# AI Context

Este módulo deverá utilizar uma arquitetura baseada em Connectors.

Connector

↓

Institution Adapter

↓

Normalization Engine

↓

Matching Engine

↓

Domain Events

↓

Projection Engine

↓

Dashboard

Cada instituição deverá implementar uma interface comum.

Exemplo

IBankConnector

BancoDoBrasilConnector

SantanderConnector

CaixaConnector

NubankConnector

InterConnector

BTGConnector

XPConnector

C6Connector

SicoobConnector

SicrediConnector

...

O sistema deverá seguir o princípio Open/Closed.

Adicionar um novo banco nunca deverá exigir alteração dos conectores existentes.

---

# Product Vision

No futuro, o usuário poderá conectar todas as suas instituições financeiras em poucos minutos.

O Flora consolidará automaticamente:

- patrimônio;
- fluxo de caixa;
- dívidas;
- investimentos;
- cartões;
- despesas;
- receitas.

A IA trabalhará sobre dados reais e atualizados, reduzindo lançamentos manuais e aumentando a precisão das recomendações.
-------
# Flora Finance

# Volume 23

## EPIC 23 — AI Financial Assistant

Version: 1.0

Status: Approved

Owner: Product Owner

Domain: Artificial Intelligence

Bounded Context: AI Assistant

Architecture Style: RAG + Tool Calling + Deterministic Engines

---

# Visão

O AI Financial Assistant é responsável por transformar todos os dados financeiros
em uma conversa inteligente.

Ele NÃO executa regras financeiras.

Ele consulta Engines existentes.

Ele interpreta.

Ele explica.

Ele recomenda.

Todas as recomendações deverão ser baseadas em cálculos previamente realizados
pelos motores determinísticos do Flora.

Nunca pela IA.

---

# Missão

Fazer o usuário sentir que possui um consultor financeiro particular.

---

# Objetivos

Explicar.

Ensinar.

Orientar.

Responder perguntas.

Montar planos.

Explicar gráficos.

Traduzir termos financeiros.

---

# Fora do Escopo

A IA nunca poderá

Mover dinheiro.

Criar despesas automaticamente.

Apagar dados.

Renegociar contratos.

Enviar pagamentos.

Tomar decisões financeiras sem confirmação.

---

# Arquitetura

User

↓

AI Gateway

↓

Prompt Builder

↓

Context Builder

↓

Tool Router

↓

Financial Engines

↓

LLM

↓

Response Builder

↓

Audit

---

# Componentes

Conversation Engine

Prompt Builder

Memory Service

Tool Router

Financial Context Builder

Recommendation Formatter

Conversation History

Feedback Engine

---

# Ferramentas Disponíveis

Search Expenses

Search Income

Search Investments

Search Goals

Search Cash Flow

Search Debt

Search Reports

Search Calendar

Search Notifications

Search Budget

Search Health Score

Search Dashboard

Projection Engine

Simulation Engine

Recommendation Engine

---

# User Story

## US-131

Conversar com IA

Priority

Highest

Story Points

21

---

Como usuário

Quero conversar com o Flora

Para entender minha situação financeira.

---

# Exemplos

Quanto posso gastar este mês?

↓

Você possui R$ 2.150 livres.

---

Qual dívida devo quitar primeiro?

↓

Motor de Priorização

↓

Resposta

---

Quando consigo comprar meu apartamento?

↓

Goal Engine

↓

Projection Engine

↓

Resposta

---

Por que fiquei negativo?

↓

Cash Flow

↓

Expense

↓

Resposta

---

# User Story

## US-132

Explicações

Objetivo

Explicar indicadores.

---

Exemplos

O que significa Health Score?

Explique Debt Score.

Por que meu orçamento está ruim?

Como funciona meu fluxo de caixa?

---

# User Story

## US-133

Plano Financeiro

Objetivo

Gerar Roadmap.

---

Resultado

Hoje

↓

Quitar Mercado Pago

↓

Semana seguinte

↓

Cancelar Spotify

↓

Mês seguinte

↓

Criar Reserva

↓

Novembro

↓

Usar Décimo Terceiro

↓

Janeiro

↓

Quitar Santander

---

# User Story

## US-134

Simulações Conversacionais

Objetivo

Permitir perguntas.

---

Exemplos

E se eu quitar o Amex?

E se eu vender meu carro?

E se eu guardar R$ 1.000 por mês?

E se eu atrasar esta dívida?

E se eu renegociar?

---

Fluxo

Pergunta

↓

Simulation Engine

↓

Resultado

↓

IA explica

---

# User Story

## US-135

Explicação de Relatórios

Objetivo

Traduzir relatórios.

---

Exemplo

PDF

↓

Resumo

↓

Pontos importantes

↓

Riscos

↓

Oportunidades

↓

Plano sugerido

---

# User Story

## US-136

Educação Financeira

Objetivo

Ensinar.

---

Conteúdos

Juros Compostos

Reserva Emergência

Investimentos

Orçamento

Cartões

Financiamentos

Tesouro

Inflação

Renda Fixa

Renda Variável

---

# APIs

POST

/api/v1/ai/chat

POST

/api/v1/ai/simulations

POST

/api/v1/ai/recommendations

POST

/api/v1/ai/explain

GET

/api/v1/ai/history

DELETE

/api/v1/ai/history

---

# Modelo

Conversation

↓

ConversationMessage

↓

ConversationContext

↓

ConversationToolCall

↓

ConversationCitation

↓

ConversationFeedback

---

# Tool Router

Pergunta

↓

Intent Detection

↓

Selecionar Engines

↓

Executar

↓

Montar Contexto

↓

Enviar para LLM

↓

Resposta

---

# Prompt Builder

System Prompt

+

Financial Context

+

Conversation Memory

+

Tool Results

+

User Prompt

↓

LLM

---

# Context Builder

Dados do usuário

↓

Workspace

↓

Health Score

↓

Cash Flow

↓

Goals

↓

Debt

↓

Subscriptions

↓

Budget

↓

Investments

↓

Insights

↓

Resposta

---

# Regras de Negócio

BR-131-001

Toda resposta financeira deverá citar a Engine utilizada.

BR-131-002

Nunca inventar números.

BR-131-003

Nunca responder sem contexto.

BR-131-004

Toda recomendação deverá possuir justificativa.

BR-131-005

Nunca modificar dados automaticamente.

BR-131-006

Toda ação deverá exigir confirmação.

BR-131-007

Toda conversa deverá possuir histórico.

BR-131-008

Memória deverá respeitar Workspace.

BR-131-009

Suporte multilíngue.

BR-131-010

Modo Offline deverá responder apenas perguntas conceituais.

---

# Segurança

Prompt Injection Detection

PII Filter

Workspace Isolation

Role Validation

Rate Limit

Audit Log

Tool Permission Validation

Output Validation

---

# Observabilidade

Registrar

ConversationId

WorkspaceId

Model

Tokens

Latency

ToolCalls

Errors

PromptVersion

---

# Auditoria

Registrar

Pergunta

Resposta

Ferramentas utilizadas

Motores consultados

Modelo

Timestamp

---

# Testes

Prompt Tests

Tool Calling

Conversation

Security

Prompt Injection

Hallucination

Performance

Regression

---

# Requisitos Não Funcionais

Primeira resposta

↓

< 3 segundos

---

Tool Calling

↓

100% auditável

---

Hallucination

↓

Zero em cálculos financeiros

---

# AI Context

A IA nunca deverá calcular juros.

Nunca deverá calcular fluxo.

Nunca deverá calcular projeções.

Nunca deverá decidir qual dívida pagar.

Ela deverá SEMPRE consultar os motores:

Debt Engine

Cash Flow Engine

Projection Engine

Budget Engine

Recommendation Engine

Goal Engine

Investment Engine

Depois disso:

Interpretar.

Explicar.

Resumir.

Traduzir.

Ensinar.

---

# Memória

A IA possuirá dois tipos de memória.

## Curto Prazo

Histórico da conversa atual.

---

## Longo Prazo

Preferências do usuário.

Objetivos.

Forma de comunicação.

Idioma.

Configurações.

Nunca armazenar dados sensíveis sem consentimento.

---

# Product Vision

O objetivo não é criar um chatbot.

O objetivo é criar um CFO Pessoal.

No futuro, o usuário poderá perguntar:

"Consigo comprar um apartamento em 2029?"

"O que preciso fazer para me aposentar aos 50 anos?"

"Vale mais investir ou quitar meu financiamento?"

"Explique por que minha saúde financeira caiu este mês."

Todas essas respostas deverão ser produzidas utilizando os motores determinísticos do Flora, com a IA atuando como interface conversacional e educacional.
------
# Flora Finance

# Volume 24

## EPIC 24 — Identity & Access Management Platform (IAM)

Version: 1.0

Status: Approved

Owner: Chief Architect

Layer: Platform

Bounded Context: Identity

Architecture: OAuth2 + OpenID Connect + RBAC + MFA

---

# Visão

O módulo Identity & Access Management é responsável por toda autenticação,
autorização e identidade do Flora Finance.

Nenhum módulo financeiro conhecerá usuários diretamente.

Todo acesso será realizado através do IAM.

O IAM será responsável por:

- Usuários
- Sessões
- Login
- OAuth
- MFA
- Permissões
- Papéis
- Convites
- Dispositivos
- Tokens
- Auditoria

---

# Objetivos Estratégicos

Segurança.

Escalabilidade.

Separação de responsabilidades.

Zero Trust.

Auditoria.

Suporte Enterprise.

---

# Objetivos Técnicos

JWT

Refresh Token Rotation

OAuth2

OIDC

RBAC

ABAC (roadmap)

MFA

Passwordless (roadmap)

Device Trust

Session Management

---

# Bounded Context

Identity

---

# Aggregate Root

IdentityUser

---

# Dependências

Nenhuma.

Identity nunca dependerá do domínio financeiro.

Todos os demais módulos dependerão dela.

---

# User Story

## US-137

Cadastro de Usuário

Priority

Highest

Story Points

13

---

Como visitante

Quero criar uma conta

Para utilizar o Flora Finance.

---

# Campos

Id

Email

PasswordHash

DisplayName

Language

Timezone

Currency

Avatar

Status

EmailConfirmed

CreatedAt

UpdatedAt

DeletedAt

---

# User Status

Pending

Active

Blocked

Suspended

Deleted

Archived

---

# Regras

BR-137-001

Email obrigatório.

BR-137-002

Email único.

BR-137-003

Senha nunca armazenada.

BR-137-004

Hash Argon2id.

BR-137-005

Password Policy obrigatória.

BR-137-006

Email Confirmation obrigatória.

BR-137-007

Soft Delete.

---

# User Story

## US-138

Login

Objetivo

Autenticar usuário.

---

Fluxo

Email

↓

Senha

↓

Validação

↓

JWT

↓

Refresh Token

↓

Sessão

↓

Workspace

---

# User Story

## US-139

MFA

Objetivo

Autenticação multifator.

---

Métodos

Authenticator App

Email

SMS

Passkeys (Roadmap)

Security Key

Recovery Codes

---

# User Story

## US-140

Gerenciamento de Sessões

Objetivo

Controlar dispositivos.

---

Visualizar

Notebook

Celular

Tablet

Desktop

Localização

IP

Último acesso

---

Permitir

Revogar sessão

↓

Logout remoto

↓

Encerrar todas

---

# User Story

## US-141

RBAC

Objetivo

Controle de acesso.

---

Papéis

Owner

Administrator

Manager

Member

Viewer

Auditor

Support

---

# Permissions

Expense.Read

Expense.Write

Expense.Delete

Income.*

Goal.*

Debt.*

Dashboard.*

Admin.*

Settings.*

AI.Chat

Reports.Export

---

# User Story

## US-142

Auditoria

Objetivo

Registrar todas as ações.

---

Registrar

Login

Logout

Falhas

Permissões

Sessões

MFA

Troca senha

Reset senha

---

# APIs

POST

/api/v1/auth/register

POST

/api/v1/auth/login

POST

/api/v1/auth/logout

POST

/api/v1/auth/refresh

POST

/api/v1/auth/mfa

POST

/api/v1/auth/reset-password

GET

/api/v1/sessions

DELETE

/api/v1/sessions/{id}

GET

/api/v1/users/me

PUT

/api/v1/users/me

---

# Eventos

UserRegistered

EmailConfirmed

UserLoggedIn

UserLoggedOut

SessionCreated

SessionRevoked

PasswordChanged

PasswordReset

MfaEnabled

MfaDisabled

---

# Modelo

IdentityUser

↓

IdentityRole

↓

IdentityPermission

↓

IdentitySession

↓

IdentityDevice

↓

RefreshToken

↓

SecurityAudit

---

# JWT

Claims

UserId

WorkspaceId

Roles

Permissions

Locale

Timezone

TokenVersion

---

# Segurança

Argon2id

JWT

Refresh Rotation

CSRF Protection

CSP

HSTS

SameSite Cookies

TLS 1.3

AES-256

Secret Rotation

Rate Limit

IP Reputation

Brute Force Protection

CAPTCHA (Roadmap)

---

# Password Policy

Mínimo

12 caracteres

↓

Maiúsculas

↓

Minúsculas

↓

Número

↓

Especial

↓

Blacklist

↓

History

---

# Observabilidade

Registrar

Login Time

Failed Attempts

Device

Latency

IP

Country

Browser

OS

CorrelationId

---

# Auditoria

Registrar

Quem

Quando

Onde

IP

Device

Action

Result

CorrelationId

---

# Testes

Unitários

Integração

Segurança

PenTest

JWT

OAuth

MFA

RBAC

Stress

---

# Requisitos Não Funcionais

100 mil usuários

↓

50 mil sessões

↓

Login

< 500 ms

↓

Disponibilidade

99,99%

---

# AI Context

Identity deverá ser completamente isolado.

Nenhum Aggregate financeiro conhecerá User.

Todos trabalharão apenas com

UserId

WorkspaceId

Claims

Permissions

Identity utilizará:

JWT

Refresh Tokens

Policy Based Authorization

Resource Based Authorization

Role Based Authorization

Nunca utilizar lógica de autorização nos Controllers.

Toda autorização deverá ser baseada em Policies.

---

# Product Vision

O IAM deverá permitir que o Flora evolua para:

Empresas.

Famílias.

Consultorias.

Contadores.

Planejadores Financeiros.

Escritórios.

Multiempresa.

White Label.

Marketplace.

API Pública.

Tudo isso sem alterar o domínio financeiro.

--------
# Flora Finance

# Volume 25

## EPIC 25 — Document Management & OCR Platform

Version: 1.0

Status: Approved

Owner: Chief Architect

Layer: Platform

Bounded Context: Documents

Aggregate Root: FinancialDocument

Architecture Style: Event Driven + OCR + AI Assisted Classification

---

# Visão

O Document Management Platform é responsável por armazenar,
processar e interpretar documentos financeiros.

O objetivo não é apenas armazenar PDFs.

O objetivo é transformar documentos em dados estruturados.

O sistema deverá conseguir interpretar automaticamente:

- Extratos
- Faturas
- Boletos
- Contratos
- Holerites
- DARFs
- Notas Fiscais
- Recibos
- Comprovantes PIX
- Comprovantes TED
- Comprovantes DOC
- Comprovantes Cartão

---

# Objetivos

Eliminar lançamentos manuais.

Extrair informações automaticamente.

Construir histórico documental.

Relacionar documentos ao domínio.

Permitir auditoria.

---

# Objetivos Técnicos

OCR

Document AI

Event Driven

Object Storage

Versionamento

Hashing

Pipeline

Metadata

---

# Aggregate

FinancialDocument

---

# Dependências

Workspace

Expense

Income

Debt

Investment

Reports

Notifications

AI Assistant

Open Finance

---

# User Story

## US-143

Upload Documento

Priority

Highest

Story Points

8

---

Como usuário

Quero enviar documentos

Para organizá-los automaticamente.

---

# Formatos

PDF

PNG

JPEG

WEBP

HEIC

TIFF

CSV

OFX

QFX

CNAB (Roadmap)

---

# Campos

Id

WorkspaceId

OriginalName

MimeType

Extension

StorageKey

SHA256

UploadedBy

UploadedAt

Status

DeletedAt

---

# Status

Uploading

Uploaded

Processing

Processed

Classified

Rejected

Archived

Deleted

---

# User Story

## US-144

OCR

Objetivo

Extrair texto.

---

Fluxo

Upload

↓

Storage

↓

OCR

↓

Texto

↓

Document AI

↓

Classificação

↓

Extração

↓

Eventos

---

# User Story

## US-145

Classificação

Objetivo

Identificar documento.

---

Tipos

Invoice

Receipt

Bank Statement

Payslip

Contract

Tax

PIX

TED

DOC

Credit Card Bill

Insurance

Mortgage

Loan

Other

---

# User Story

## US-146

Extração

Objetivo

Extrair informações.

---

Campos

Valor

Data

Instituição

CPF/CNPJ

Descrição

Categoria

Conta

Vencimento

Pagamento

Parcelas

Juros

---

# User Story

## US-147

Relacionamento

Objetivo

Relacionar documento ao domínio.

---

Exemplo

Fatura

↓

Expense

↓

Debt

↓

Cash Flow

↓

Dashboard

---

PIX

↓

Income

↓

Expense

↓

Transfer

---

# User Story

## US-148

Pesquisa

Objetivo

Encontrar documentos.

---

Filtros

Nome

Valor

Instituição

Categoria

Data

Tipo

Tags

OCR

Texto

---

# APIs

POST

/api/v1/documents

GET

/api/v1/documents

GET

/api/v1/documents/{id}

DELETE

/api/v1/documents/{id}

POST

/api/v1/documents/reprocess

POST

/api/v1/documents/classify

GET

/api/v1/documents/search

---

# Eventos

DocumentUploaded

DocumentProcessed

OCRCompleted

ClassificationCompleted

ExtractionCompleted

DocumentLinked

DocumentDeleted

---

# Modelo

FinancialDocument

↓

DocumentVersion

↓

DocumentMetadata

↓

OCRResult

↓

Classification

↓

Extraction

↓

DocumentLink

↓

SearchIndex

---

# Pipeline

Upload

↓

Virus Scan

↓

Storage

↓

OCR

↓

Document AI

↓

Extraction

↓

Validation

↓

Events

↓

Search Index

↓

Dashboard

---

# Storage

Original

↓

Thumbnail

↓

Preview

↓

OCR Text

↓

Metadata

↓

AI Result

---

# Regras de Negócio

BR-143-001

Todo documento pertence a um Workspace.

BR-143-002

SHA256 obrigatório.

BR-143-003

Nunca substituir arquivo.

BR-143-004

Versionamento obrigatório.

BR-143-005

OCR poderá ser reexecutado.

BR-143-006

Extração poderá ser corrigida manualmente.

BR-143-007

Documento nunca poderá alterar domínio automaticamente.

BR-143-008

Toda sugestão deverá exigir confirmação.

BR-143-009

Suporte a múltiplos idiomas.

BR-143-010

Storage criptografado.

---

# Segurança

Antivírus

Mime Validation

Magic Number Validation

AES-256

Signed URLs

Object Storage

PII Detection

LGPD

Retention Policy

---

# OCR

Primeira versão

Azure Document Intelligence

Roadmap

Google Document AI

AWS Textract

Tesseract

OCR Local

---

# Document AI

Extrair

Datas

Valores

Instituições

Categorias

Contas

CPF

CNPJ

Chaves PIX

Parcelas

Juros

---

# Observabilidade

Registrar

DocumentId

WorkspaceId

OCR Time

Extraction Time

Classification Time

Confidence

Pipeline Version

CorrelationId

---

# Auditoria

Registrar

Upload

Download

Visualização

Exclusão

Reprocessamento

Correções

Relacionamentos

---

# Testes

Upload

OCR

Storage

Segurança

Performance

Pipeline

Versionamento

Pesquisa

---

# Requisitos Não Funcionais

Arquivo

↓

100 MB

↓

OCR

< 20 segundos

↓

Pesquisa

< 300 ms

↓

Storage

Escalável

---

# AI Context

Documentos nunca deverão modificar o domínio.

Eles apenas sugerem alterações.

Fluxo

Documento

↓

OCR

↓

Extração

↓

Sugestão

↓

Usuário confirma

↓

Evento

↓

Domínio

Toda automação deverá possuir confirmação explícita.

---

# Product Vision

No futuro, o usuário poderá simplesmente tirar uma foto de uma fatura.

O Flora irá:

• identificar o documento;

• extrair todos os campos;

• sugerir a categoria;

• relacionar ao cartão correto;

• atualizar o fluxo de caixa;

• atualizar o Dashboard;

• criar lembretes;

• anexar o documento à despesa.

Tudo isso mantendo o usuário no controle da confirmação final.

---
# Flora Finance

# Volume 26

## EPIC 26 — Workflow & Automation Engine

Version: 1.0

Status: Approved

Owner: Chief Architect

Layer: Platform

Bounded Context: Automation

Aggregate Root: Workflow

Architecture Style: Event-Driven + Workflow Engine + State Machine

---

# Visão

O Workflow & Automation Engine é responsável por permitir que usuários criem automações financeiras sem escrever código.

O objetivo é transformar eventos financeiros em ações automatizadas.

Toda automação será composta por:

- Trigger
- Conditions
- Actions
- Variables
- Context
- History

---

# Objetivos Estratégicos

Eliminar tarefas repetitivas.

Automatizar processos.

Integrar módulos.

Permitir automações personalizadas.

Reduzir trabalho manual.

---

# Objetivos Técnicos

Low Code

No Code

State Machine

Workflow Engine

Retry

Compensação

Versionamento

Event Driven

---

# Aggregate Root

Workflow

---

# Dependências

Todos os módulos podem publicar eventos.

Nenhum módulo dependerá do Workflow.

O Workflow apenas consome eventos.

---

# User Story

## US-149

Criar Workflow

Priority

Highest

Story Points

21

---

Como usuário

Quero criar automações

Para reduzir tarefas manuais.

---

# Estrutura

Workflow

↓

Trigger

↓

Conditions

↓

Actions

↓

Execution

↓

History

---

# Campos

Id

WorkspaceId

Name

Description

Status

Version

TriggerType

CreatedAt

UpdatedAt

DeletedAt

---

# Status

Draft

Active

Paused

Disabled

Archived

Deleted

---

# Trigger Types

ExpenseCreated

IncomeReceived

DebtCreated

DebtPaid

GoalCompleted

GoalUpdated

SubscriptionRenewed

CashFlowNegative

BudgetExceeded

HealthScoreChanged

NotificationRead

DocumentProcessed

CalendarEventCreated

Manual

Webhook

Cron

---

# User Story

## US-150

Condições

Objetivo

Executar apenas quando critérios forem atendidos.

---

Operadores

Equals

Not Equals

Greater Than

Less Than

Between

Contains

StartsWith

EndsWith

Regex

Exists

---

Exemplo

Categoria

=

Cartão

↓

Valor

>

R$ 1.000

↓

Executar

---

# User Story

## US-151

Ações

Objetivo

Executar processos.

---

Ações

Criar Notificação

Enviar Email

Criar Meta

Criar Despesa

Criar Receita

Atualizar Dashboard

Gerar Relatório

Executar IA

Enviar Webhook

Criar Evento

Criar Tag

Executar Script (Roadmap)

---

# User Story

## US-152

Variáveis

Objetivo

Permitir reutilização.

---

Variáveis

{{Amount}}

{{Category}}

{{Workspace}}

{{Today}}

{{User}}

{{Goal}}

{{Balance}}

{{Debt}}

{{HealthScore}}

{{CashFlow}}

---

# User Story

## US-153

Templates

Objetivo

Disponibilizar automações prontas.

---

Exemplos

Sempre que receber salário

↓

Reservar 20%

↓

Meta Reserva

---

Sempre que Health Score cair

↓

Criar Insight

↓

Enviar Push

---

Sempre que cartão ultrapassar

80%

↓

Notificar

↓

Criar Relatório

---

# User Story

## US-154

Execução Manual

Objetivo

Permitir testes.

---

Executar

↓

Visualizar resultado

↓

Logs

↓

Histórico

---

# APIs

POST

/api/v1/workflows

GET

/api/v1/workflows

PUT

/api/v1/workflows/{id}

DELETE

/api/v1/workflows/{id}

POST

/api/v1/workflows/{id}/execute

GET

/api/v1/workflows/history

GET

/api/v1/workflows/templates

---

# Modelo

Workflow

↓

WorkflowVersion

↓

Trigger

↓

Condition

↓

Action

↓

Execution

↓

ExecutionLog

↓

ExecutionVariable

↓

ExecutionHistory

---

# Workflow Engine

Domain Event

↓

Workflow Resolver

↓

Condition Engine

↓

Action Executor

↓

Compensation Handler

↓

Audit

↓

Notification

---

# Regras de Negócio

BR-149-001

Todo Workflow pertence a um Workspace.

BR-149-002

Toda execução gera histórico.

BR-149-003

Workflow deverá ser versionado.

BR-149-004

Execuções deverão ser idempotentes.

BR-149-005

Suporte a Retry.

BR-149-006

Suporte a Compensação.

BR-149-007

Rollback apenas para ações compatíveis.

BR-149-008

Logs obrigatórios.

BR-149-009

Permitir Dry Run.

BR-149-010

Toda automação poderá ser pausada.

---

# Compensation

Caso uma Action falhe

↓

Executar Compensação

↓

Registrar Log

↓

Continuar ou Encerrar

Configuração do Workflow

---

# Scheduler

Workflow Dispatcher

↓

Trigger Resolver

↓

Condition Evaluator

↓

Action Pipeline

↓

Retry Handler

↓

Audit Publisher

↓

History

---

# Dashboard

Mostrar

Workflows Ativos

Últimas Execuções

Falhas

Tempo Médio

Automações Mais Utilizadas

Templates

---

# Observabilidade

Registrar

WorkflowId

ExecutionId

WorkspaceId

Duration

Retries

Failed Step

Trigger

CorrelationId

---

# Auditoria

Registrar

Quem criou

Quem alterou

Versão

Execuções

Falhas

Rollback

Timestamp

---

# Testes

Unitários

Integração

Workflow

Retry

Compensation

Performance

Concorrência

Stress

---

# Requisitos Não Funcionais

10.000 Workflows

↓

100.000 Execuções/Dia

↓

Tempo Médio

< 500 ms

↓

Retry

100%

↓

Escalonamento Horizontal

Obrigatório

---

# AI Context

O Workflow Engine deverá utilizar uma arquitetura semelhante ao Temporal.io ou Azure Durable Functions.

Estrutura

Event Bus

↓

Workflow Dispatcher

↓

Execution Context

↓

Condition Engine

↓

Action Executor

↓

Compensation

↓

History

↓

Audit

Nenhum Workflow deverá conter lógica financeira.

Toda lógica financeira deverá permanecer nos Engines do domínio.

O Workflow apenas orquestra.

---

# Workflow DSL (Roadmap)

No futuro, cada Workflow poderá ser exportado como JSON.

Exemplo

Trigger

↓

Conditions

↓

Actions

↓

Variables

↓

Metadata

Permitindo importação, compartilhamento e Marketplace de automações.

---

# Product Vision

O objetivo não é criar apenas um mecanismo de regras.

O objetivo é permitir que qualquer usuário automatize completamente sua rotina financeira.

Exemplos futuros

- Quando meu salário cair, separar automaticamente 25% para investimentos.
- Quando minha reserva atingir R$ 50.000, criar uma nova meta para entrada do apartamento.
- Sempre que uma despesa acima de R$ 2.000 for registrada, solicitar confirmação adicional.
- Quando o Health Score cair abaixo de 60, gerar um plano automático de recuperação.
- Toda segunda-feira às 8h, enviar um resumo financeiro semanal.
- Quando uma fatura for importada via OCR, criar automaticamente uma sugestão de categorização e iniciar o fluxo de aprovação.

O Workflow Engine será a base para todas as automações futuras do Flora Finance.
--------

# Flora Finance

# Volume 27

## EPIC 27 — Event Bus & Messaging Platform

Version: 1.0

Status: Approved

Owner: Chief Architect

Layer: Platform

Bounded Context: Messaging

Architecture Style: Event-Driven Architecture + Outbox Pattern + Inbox Pattern + Message Broker

---

# Visão

O Event Bus é a espinha dorsal do Flora Finance.

Nenhum módulo deverá chamar outro módulo diretamente para executar processos assíncronos.

Toda comunicação entre Bounded Contexts ocorrerá através de eventos.

O Event Bus é responsável por:

- Publicação
- Entrega
- Retry
- Dead Letter Queue
- Idempotência
- Rastreabilidade
- Observabilidade
- Versionamento de Eventos

---

# Objetivos Estratégicos

Desacoplamento.

Escalabilidade.

Alta disponibilidade.

Reprocessamento.

Baixo acoplamento.

Resiliência.

---

# Objetivos Técnicos

Outbox Pattern

Inbox Pattern

At Least Once Delivery

Idempotência

Retry Exponencial

DLQ

Tracing

Correlation

Event Versioning

---

# Dependências

Nenhuma.

Todos os módulos dependem do Event Bus.

O Event Bus não depende do domínio.

---

# Event Categories

Domain Events

Integration Events

System Events

Audit Events

Notification Events

Workflow Events

Projection Events

---

# User Story

## US-155

Publicar Evento

Priority

Highest

Story Points

13

---

Como desenvolvedor

Quero publicar eventos

Para desacoplar módulos.

---

# Fluxo

Command

↓

Aggregate

↓

Domain Event

↓

Outbox

↓

Publisher

↓

Broker

↓

Consumers

↓

Inbox

↓

Handler

↓

Audit

---

# Event Metadata

EventId

EventName

AggregateId

AggregateType

WorkspaceId

OccurredAt

CorrelationId

CausationId

Version

UserId

TenantId

Source

Payload

---

# Event Naming

ExpenseCreated

ExpenseUpdated

ExpenseDeleted

IncomeReceived

GoalCompleted

DebtPaid

BudgetExceeded

HealthScoreChanged

ProjectionUpdated

WorkflowExecuted

NotificationSent

DocumentProcessed

---

# User Story

## US-156

Outbox Pattern

Objetivo

Garantir entrega.

---

Fluxo

Transaction

↓

Persist Domain

↓

Persist Outbox

↓

Commit

↓

Publisher

↓

Broker

↓

Delete Outbox

---

# User Story

## US-157

Inbox Pattern

Objetivo

Garantir idempotência.

---

Fluxo

Receive

↓

Inbox

↓

Already Processed?

↓

No

↓

Execute

↓

Store Inbox

↓

Ack

---

# User Story

## US-158

Retry

Objetivo

Recuperação automática.

---

Tentativa

1

↓

5 segundos

↓

Tentativa

2

↓

30 segundos

↓

Tentativa

3

↓

2 minutos

↓

Tentativa

4

↓

15 minutos

↓

Dead Letter Queue

---

# User Story

## US-159

Versionamento

Objetivo

Permitir evolução.

---

ExpenseCreated v1

↓

ExpenseCreated v2

↓

ExpenseCreated v3

---

Compatibilidade obrigatória.

---

# User Story

## US-160

Replay

Objetivo

Reprocessar eventos.

---

Exemplo

Reconstruir Dashboard

↓

Replay

↓

Projection

---

Reconstruir Calendar

↓

Replay

↓

Timeline

---

# APIs

POST

/api/v1/events/publish

GET

/api/v1/events

GET

/api/v1/events/dead-letter

POST

/api/v1/events/replay

GET

/api/v1/events/health

---

# Modelo

Event

↓

OutboxMessage

↓

InboxMessage

↓

DeadLetter

↓

Subscription

↓

Consumer

↓

Producer

↓

ReplayJob

---

# Broker

Primeira versão

RabbitMQ

---

Roadmap

Azure Service Bus

Kafka

AWS SQS

NATS

Redis Streams

---

# Event Bus

Publisher

↓

Exchange

↓

Queue

↓

Consumer

↓

Inbox

↓

Handler

↓

Audit

---

# Event Contract

Todo evento deverá possuir

Id

Tipo

Versão

Timestamp

CorrelationId

CausationId

WorkspaceId

Payload

Metadata

---

# Regras de Negócio

BR-155-001

Todo evento possui versão.

BR-155-002

Todo evento possui CorrelationId.

BR-155-003

Todo evento é imutável.

BR-155-004

Consumidores deverão ser idempotentes.

BR-155-005

Retry obrigatório.

BR-155-006

DLQ obrigatória.

BR-155-007

Replay permitido.

BR-155-008

Payload nunca poderá ser alterado.

BR-155-009

Eventos deverão possuir Schema.

BR-155-010

Eventos deverão ser auditáveis.

---

# Event Schema

Metadata

↓

Payload

↓

Headers

↓

Version

↓

Signature (Roadmap)

---

# Observabilidade

Registrar

EventId

Queue

Exchange

Latency

Retries

Consumer

Producer

Duration

CorrelationId

---

# Auditoria

Registrar

Quem publicou

Quem consumiu

Quando

Resultado

Retries

DLQ

Replay

---

# Testes

Unitários

Integração

Contrato

Performance

Stress

Idempotência

Replay

DLQ

---

# Requisitos Não Funcionais

100 mil eventos/minuto

↓

Latência

< 100 ms

↓

Disponibilidade

99,99%

↓

Replay

100%

↓

Ordering

Por Aggregate

---

# AI Context

Toda comunicação assíncrona utilizará o Event Bus.

É proibido:

Aggregate → Aggregate

Controller → Controller

Module → Module

via chamadas diretas.

Toda integração deverá ocorrer por eventos.

Sugestão

Aggregate

↓

Domain Event

↓

Outbox

↓

Broker

↓

Consumer

↓

Inbox

↓

Application Handler

↓

Projection

---

# Event Catalog

ExpenseCreated

ExpenseUpdated

ExpenseDeleted

IncomeCreated

IncomeReceived

DebtCreated

DebtPaid

DebtRenegotiated

GoalCreated

GoalCompleted

BudgetExceeded

CashFlowCalculated

ProjectionUpdated

HealthScoreUpdated

NotificationCreated

NotificationDelivered

WorkflowStarted

WorkflowCompleted

DocumentUploaded

DocumentProcessed

OCRCompleted

OpenFinanceSynchronized

UserRegistered

UserLoggedIn

...

---

# Product Vision

Toda a plataforma Flora Finance deverá ser construída sobre eventos.

Isso permitirá:

- Escalabilidade horizontal.
- Reconstrução de projeções.
- Analytics em tempo real.
- Machine Learning.
- Auditoria completa.
- Event Sourcing parcial.
- Integração com parceiros.
- Marketplace.
- Plugins.
- API Pública.

O Event Bus será considerado uma infraestrutura crítica da plataforma.
----
# Flora Finance

# Volume 28

## EPIC 28 — Observability Platform

Version: 1.0

Status: Approved

Owner: Chief Architect

Layer: Platform

Bounded Context: Observability

Architecture Style: OpenTelemetry + Distributed Tracing + Metrics + Structured Logging

---

# Visão

A Observability Platform é responsável por permitir que toda a plataforma Flora Finance seja monitorada em tempo real.

O objetivo não é apenas registrar logs.

O objetivo é permitir responder rapidamente perguntas como:

- O que aconteceu?
- Onde aconteceu?
- Por que aconteceu?
- Quem foi afetado?
- Qual serviço apresentou falha?
- Quanto tempo demorou?
- Qual evento originou este erro?

Toda operação da plataforma deverá ser observável.

---

# Objetivos Estratégicos

Alta disponibilidade.

Diagnóstico rápido.

Correlação entre serviços.

Medição de performance.

Monitoramento contínuo.

Facilidade de suporte.

---

# Objetivos Técnicos

OpenTelemetry

Tracing Distribuído

Metrics

Structured Logging

Health Checks

Dashboards

Alertas

SLO

SLI

Error Budget

---

# Dependências

Todos os módulos.

A plataforma inteira deverá emitir telemetria.

---

# Componentes

Logging Platform

Metrics Platform

Tracing Platform

Health Platform

Alert Platform

Telemetry SDK

Diagnostics Dashboard

---

# User Story

## US-161

Structured Logging

Priority

Highest

Story Points

13

---

Como desenvolvedor

Quero registrar logs estruturados

Para facilitar diagnóstico.

---

# Campos Obrigatórios

Timestamp

Level

Message

CorrelationId

WorkspaceId

UserId

TraceId

SpanId

Application

Module

Environment

Version

Machine

Exception

Metadata

---

# Log Levels

Trace

Debug

Information

Warning

Error

Critical

---

# User Story

## US-162

Distributed Tracing

Objetivo

Rastrear requisições.

---

Fluxo

Angular

↓

Gateway

↓

API

↓

Application

↓

Domain

↓

RabbitMQ

↓

Consumer

↓

Projection

↓

Dashboard

---

Todo Trace deverá manter

TraceId

ParentSpan

SpanId

Duration

Status

---

# User Story

## US-163

Metrics

Objetivo

Medir comportamento.

---

Métricas

CPU

RAM

Latency

Requests

Errors

Retries

DLQ

Queue Size

Workflow Time

Dashboard Time

CashFlow Time

AI Response Time

OCR Time

---

# User Story

## US-164

Health Checks

Objetivo

Verificar disponibilidade.

---

Endpoints

/readiness

/liveness

/startup

/health

---

Cada módulo deverá possuir

Health Check próprio.

---

# User Story

## US-165

Alertas

Objetivo

Detectar problemas.

---

Exemplos

Erro > 5%

↓

Enviar Teams

↓

Enviar Slack

↓

Enviar Email

↓

Criar Incidente

---

Latência

>

2 segundos

↓

Alerta

---

Fila

>

1000 mensagens

↓

Alerta

---

# User Story

## US-166

Dashboards Operacionais

Objetivo

Visualizar operação.

---

Dashboards

API

RabbitMQ

Workflow

OCR

Open Finance

Dashboard

AI

Identity

Documents

Notifications

---

# APIs

GET

/api/v1/observability/health

GET

/api/v1/observability/metrics

GET

/api/v1/observability/traces

GET

/api/v1/observability/logs

GET

/api/v1/observability/dashboard

---

# Modelo

Telemetry

↓

Trace

↓

Span

↓

Metric

↓

Health

↓

LogEntry

↓

Alert

↓

Incident

---

# Métricas Padrão

Request Count

Request Duration

Error Count

Queue Length

DLQ Count

CPU

RAM

Disk

Connections

Rabbit Consumers

SignalR Connections

AI Tokens

Workflow Executions

---

# Health Checks

Database

RabbitMQ

Redis

Storage

Identity

OpenAI

OCR

SMTP

SignalR

Scheduler

---

# Regras de Negócio

BR-161-001

Todo Request gera Trace.

BR-161-002

Todo Evento gera CorrelationId.

BR-161-003

Todo Log deverá ser estruturado.

BR-161-004

Proibido utilizar logs em texto puro.

BR-161-005

Exceptions sempre deverão possuir StackTrace.

BR-161-006

Logs nunca poderão armazenar senhas.

BR-161-007

PII deverá ser mascarada.

BR-161-008

Health obrigatório para todos os módulos.

BR-161-009

Toda métrica deverá possuir Labels.

BR-161-010

Tracing obrigatório entre serviços.

---

# Observability Stack

OpenTelemetry

↓

OTLP

↓

Grafana Tempo

↓

Grafana Loki

↓

Prometheus

↓

Grafana

↓

AlertManager

---

# Logs

Sugestão

Serilog

↓

OTLP Exporter

↓

Loki

---

# Metrics

Prometheus

↓

Grafana

---

# Traces

OpenTelemetry

↓

Tempo

↓

Grafana

---

# Dashboard Operacional

Disponibilidade

↓

Erros

↓

Latência

↓

Top Endpoints

↓

Top Exceptions

↓

Top Consumers

↓

Top Queries

↓

Top Eventos

↓

Health

↓

Queues

---

# Observabilidade por Módulo

Todos os módulos deverão emitir

Logs

↓

Metrics

↓

Traces

↓

Health

↓

Events

---

# Auditoria

Registrar

Falhas

Incidentes

Health

Deploy

Restart

Escalonamento

---

# Testes

Carga

Stress

Chaos

Recovery

Health

Logging

Tracing

Metrics

---

# Requisitos Não Funcionais

100% dos Requests

↓

Tracing

---

100% dos Eventos

↓

CorrelationId

---

Disponibilidade

99,95%

---

Logs

↓

Retenção

90 dias

---

Métricas

↓

Retenção

1 ano

---

# AI Context

A IA poderá consultar a plataforma de observabilidade.

Exemplos

"O sistema está lento?"

↓

Consultar métricas.

---

"Por que houve erro?"

↓

Consultar traces.

---

"Qual serviço apresentou falha?"

↓

Consultar incidentes.

---

A IA nunca analisará logs diretamente.

Ela utilizará APIs específicas.

---

# Product Vision

A plataforma deverá permitir que um desenvolvedor consiga identificar qualquer problema em poucos minutos.

Objetivos futuros

- Root Cause Analysis automática.
- Anomalia por Machine Learning.
- Alertas Inteligentes.
- Auto Healing.
- Incident Timeline.
- Deployment Correlation.
- Performance Prediction.
- Capacity Planning.
- Cost Analysis.
- FinOps Dashboard.

A Observability Platform será considerada um módulo estratégico da infraestrutura.

--------
# Flora Finance

# Volume 29

## EPIC 29 — Multi-Tenant Architecture Platform

Version: 1.0

Status: Approved

Owner: Chief Architect

Layer: Platform

Bounded Context: Tenant Management

Architecture Style: Shared Database + Shared Schema + Tenant Isolation

---

# Visão

O Flora Finance será desenvolvido desde o primeiro dia como um SaaS Multi-Tenant.

Todo dado do sistema pertencerá obrigatoriamente a um Tenant.

Nenhuma entidade poderá existir fora de um Tenant.

A arquitetura deverá permitir:

- milhares de empresas
- milhares de usuários
- workspaces familiares
- escritórios de contabilidade
- consultorias financeiras
- white-label
- franquias
- multiempresa

sem alteração do domínio.

---

# Objetivos Estratégicos

Escalabilidade.

Isolamento.

Segurança.

White Label.

Planos SaaS.

Enterprise.

---

# Objetivos Técnicos

Tenant Isolation

Workspace Isolation

Soft Delete

Row Level Security Ready

Tenant Context

Tenant Resolution

Tenant Cache

Tenant Provisioning

---

# Conceitos

Tenant

↓

Workspace

↓

Users

↓

Financial Data

---

Exemplo

Tenant

Empresa ABC

↓

Workspace

Financeiro

↓

Usuários

João

Maria

Carlos

↓

Dados

Receitas

Despesas

Metas

Cartões

---

# Aggregate Root

Tenant

---

# Dependências

Identity

Todos os módulos financeiros

Todos os módulos da plataforma

---

# User Story

## US-167

Criar Tenant

Priority

Highest

Story Points

13

---

Como administrador

Quero criar um Tenant

Para utilizar o Flora.

---

# Campos

Id

Name

Slug

Status

Timezone

Locale

Currency

Plan

StorageLimit

UsersLimit

CreatedAt

UpdatedAt

DeletedAt

---

# Tenant Status

Pending

Active

Suspended

Blocked

Deleted

Archived

---

# User Story

## US-168

Resolver Tenant

Objetivo

Identificar Tenant.

---

Estratégias

Subdomínio

Header

JWT

API Key

Custom Domain

---

Exemplo

empresa.flora.app

↓

Resolver Tenant

↓

Contexto

↓

Aplicação

---

# User Story

## US-169

Tenant Context

Objetivo

Disponibilizar contexto.

---

Contexto

TenantId

WorkspaceId

UserId

Plan

Permissions

Timezone

Locale

---

Todos os serviços utilizarão

TenantContext

---

# User Story

## US-170

Provisionamento

Objetivo

Criar ambiente.

---

Fluxo

Novo Cliente

↓

Tenant

↓

Workspace

↓

Owner

↓

Plano

↓

Configurações

↓

Dashboard Inicial

↓

Templates

↓

Onboarding

---

# User Story

## US-171

Planos

Objetivo

Gerenciar recursos.

---

Free

Starter

Professional

Business

Enterprise

White Label

---

Cada plano define

Usuários

Storage

OCR

IA

Dashboards

Integrações

API

Workflows

Open Finance

---

# User Story

## US-172

Limites

Objetivo

Aplicar quotas.

---

Limites

Usuários

Workspaces

Storage

Documentos

IA

Tokens

API

OCR

Uploads

Automações

---

# APIs

POST

/api/v1/tenants

GET

/api/v1/tenants

GET

/api/v1/tenants/current

PATCH

/api/v1/tenants/{id}

DELETE

/api/v1/tenants/{id}

GET

/api/v1/plans

---

# Modelo

Tenant

↓

Workspace

↓

TenantSettings

↓

TenantPlan

↓

TenantQuota

↓

TenantBranding

↓

TenantAudit

↓

TenantUsage

---

# Branding

Logo

Nome

Cores

Domínio

Favicon

Tema

Emails

---

# White Label

Logo

↓

Domínio

↓

Tema

↓

Emails

↓

Aplicativo

↓

Documentos

↓

Relatórios

↓

IA

---

# Isolamento

Todos os Aggregates deverão possuir

TenantId

WorkspaceId

---

Nunca permitir

JOIN

entre Tenants.

---

# Regras de Negócio

BR-167-001

Todo Aggregate pertence a um Tenant.

BR-167-002

Todo Request resolve Tenant.

BR-167-003

Tenant obrigatório.

BR-167-004

Workspace obrigatório.

BR-167-005

Nunca permitir acesso cruzado.

BR-167-006

Cache isolado.

BR-167-007

Storage isolado.

BR-167-008

Logs isolados.

BR-167-009

Observabilidade por Tenant.

BR-167-010

Backups independentes.

---

# Segurança

JWT

↓

Tenant Validation

↓

Workspace Validation

↓

Permission Validation

↓

Resource Validation

↓

Controller

---

# Tenant Resolver

HTTP Request

↓

Tenant Resolver

↓

Tenant Cache

↓

Tenant Context

↓

Middleware

↓

Application

---

# Banco de Dados

Estratégia Inicial

Shared Database

↓

Shared Schema

↓

TenantId obrigatório

↓

WorkspaceId obrigatório

---

# Evolução

Roadmap

Shared Database

↓

Dedicated Database

↓

Dedicated Cluster

↓

Dedicated Region

---

Sem alterar domínio.

---

# Cache

Redis

↓

Prefix

TenantId

↓

WorkspaceId

↓

Key

---

# Storage

Blob

↓

Tenant

↓

Workspace

↓

Documents

↓

Reports

↓

Exports

---

# Observabilidade

Registrar

Tenant

Workspace

Plano

Usuários

Storage

Performance

API

Tokens

OCR

AI

---

# Auditoria

Registrar

Tenant

Workspace

Owner

Plano

Mudanças

Configurações

Branding

---

# Testes

Tenant Isolation

Security

Performance

Load

Stress

Provisioning

Migration

Quota

---

# Requisitos Não Funcionais

100.000 Tenants

↓

10 milhões usuários

↓

Disponibilidade

99,99%

↓

Tenant Resolution

< 10 ms

↓

Escalabilidade

Horizontal

---

# AI Context

Toda IA deverá receber

TenantId

WorkspaceId

Plan

Permissions

Nunca compartilhar contexto entre Tenants.

Toda memória será isolada.

Toda busca será filtrada.

Todo embedding será isolado.

Toda recomendação será contextualizada.

---

# Product Vision

O Flora deverá ser capaz de operar simultaneamente para:

Pessoa Física

Famílias

Consultorias

Escritórios Contábeis

Empresas

Holding Patrimonial

Family Offices

Bancos Digitais

Fintechs White Label

Tudo utilizando a mesma base de código.
---------
# Flora Finance

# Volume 30

# EPIC 30 — Administration & Platform Management

Version: 1.0

Status: Approved

Owner: Chief Architect

Layer: Platform

Bounded Context: Administration

Aggregate Root: PlatformAdministration

---

# Visão

O módulo Administration centraliza toda a administração da plataforma Flora Finance.

Ele não administra as finanças do usuário.

Ele administra o SaaS.

Seu objetivo é permitir operação, suporte, manutenção e gerenciamento da plataforma.

---

# Objetivos

Administrar Tenants.

Administrar Usuários.

Administrar Planos.

Administrar Recursos.

Administrar Auditoria.

Administrar IA.

Administrar Workspaces.

Administrar Storage.

Administrar OCR.

Administrar Integrações.

Administrar Billing.

Administrar Feature Flags.

---

# Aggregate Root

PlatformAdministration

---

# Dependências

Identity

Tenant

Billing

Documents

Workflow

Open Finance

Notifications

Reports

AI Assistant

Dashboard

Todos os demais módulos.

---

# User Story

## US-173

Gerenciar Tenant

Priority

Highest

Story Points

8

---

Como administrador

Quero administrar um Tenant

Para manter a plataforma.

---

Permitir

Ativar

Suspender

Arquivar

Excluir

Bloquear

Alterar plano

Visualizar consumo

Visualizar auditoria

---

# User Story

## US-174

Gerenciar Usuários

Objetivo

Administrar usuários.

---

Permitir

Bloquear

Desbloquear

Forçar troca senha

Revogar sessões

Revogar MFA

Reset senha

Remover Workspace

Trocar Owner

---

# User Story

## US-175

Billing

Objetivo

Administrar planos.

---

Free

Starter

Professional

Business

Enterprise

White Label

---

Permitir

Upgrade

Downgrade

Trial

Renovação

Cancelamento

Suspensão

---

# User Story

## US-176

Consumo

Objetivo

Visualizar utilização.

---

Mostrar

Storage

OCR

Tokens IA

Open Finance

Uploads

API

Relatórios

Automações

Usuários

Workspaces

---

# User Story

## US-177

Auditoria Global

Objetivo

Pesquisar qualquer evento.

---

Pesquisar por

Tenant

Workspace

Usuário

Evento

IP

Data

CorrelationId

Entidade

---

# User Story

## US-178

Feature Flags

Objetivo

Liberar funcionalidades.

---

Por

Tenant

Plano

Usuário

Workspace

Percentual

Ambiente

---

# User Story

## US-179

Central de Configurações

Objetivo

Configurar plataforma.

---

Configurações

Idioma

Moeda

Timezone

Storage

OCR

IA

Open Finance

Notificações

Segurança

Backup

---

# APIs

GET

/api/v1/admin/dashboard

GET

/api/v1/admin/tenants

GET

/api/v1/admin/users

GET

/api/v1/admin/audit

GET

/api/v1/admin/usage

PATCH

/api/v1/admin/tenant

PATCH

/api/v1/admin/user

PATCH

/api/v1/admin/settings

---

# Modelo

PlatformSettings

↓

TenantAdministration

↓

UserAdministration

↓

PlatformAudit

↓

FeatureFlag

↓

BillingAdministration

↓

Usage

↓

PlatformMetrics

---

# Dashboard Administrativo

Exibir

Tenants ativos

Usuários

Storage

Receita

OCR

Tokens IA

Open Finance

Falhas

Alertas

Health

Disponibilidade

---

# Regras de Negócio

BR-173-001

Somente Platform Administrators poderão acessar este módulo.

BR-173-002

Toda alteração gera auditoria.

BR-173-003

Nenhuma exclusão física será permitida.

BR-173-004

Toda ação crítica exigirá confirmação.

BR-173-005

Toda alteração de plano deverá preservar histórico.

BR-173-006

Feature Flags deverão ser versionadas.

BR-173-007

Nenhuma configuração poderá afetar outros Tenants.

BR-173-008

Toda operação deverá possuir CorrelationId.

BR-173-009

Logs administrativos terão retenção mínima de 10 anos.

BR-173-010

Toda alteração deverá ser reversível quando possível.

---

# Observabilidade

Registrar

Administrador

Tenant

Workspace

Operação

Duração

Resultado

CorrelationId

---

# Auditoria

Registrar

Antes

Depois

Quem

Quando

IP

Device

Motivo

---

# Testes

Permissões

Auditoria

Billing

Feature Flags

Performance

Stress

Segurança

---

# Requisitos Não Funcionais

100 mil tenants

↓

10 milhões usuários

↓

99,99%

↓

Operações administrativas

↓

< 500 ms

---

# Product Vision

O Administration será o centro operacional da plataforma Flora Finance.

Ele permitirá que equipes de suporte, operação e negócio administrem o SaaS sem acesso direto ao banco de dados.

Toda alteração administrativa será rastreável, auditável e reversível quando aplicável.

---------
Flora Finance
Volume 31
CORE FINANCE COMPLETE SPECIFICATION

Version 1.0

Status Approved

Domain Financial Core

Objetivo

Este documento encerra toda a especificação funcional do Core Finance.

Após este documento nenhuma regra de negócio do Core deverá permanecer indefinida.

O Core Finance compreende:

Financial Accounts
Categories
Expenses
Income
Transfers
Attachments
Tags
Cost Centers
Recurrence
Installments
Reconciliation
Imports
Audit
Aggregate

Expense

Estados

Draft

Pending

Scheduled

Paid

Overdue

Cancelled

Refunded

Reversed

Imported

Archived

Deleted

Objetivos

Registrar qualquer saída financeira.

Controlar vencimentos.

Permitir conciliação.

Permitir importação.

Permitir recorrência.

Permitir parcelamento.

Servir Budget.

Servir Cash Flow.

Servir Dashboard.

Servir IA.

Regras Gerais

RN-EXP-001

Toda despesa pertence obrigatoriamente a um Workspace.

RN-EXP-002

Toda despesa pertence obrigatoriamente a uma Conta Financeira.

RN-EXP-003

Categoria obrigatória.

RN-EXP-004

Valor maior que zero.

RN-EXP-005

Moeda obrigatória.

RN-EXP-006

Data obrigatória.

RN-EXP-007

Status inicial = Pending.

RN-EXP-008

Criada por usuário, Workflow, OCR ou Open Finance.

RN-EXP-009

Nunca excluir fisicamente.

RN-EXP-010

Soft Delete obrigatório.

Alteração

RN-EXP-011

Uma despesa Pending pode ser alterada livremente.

RN-EXP-012

Uma despesa Paid nunca altera valor.

RN-EXP-013

Uma despesa Paid poderá receber apenas observações.

RN-EXP-014

Uma despesa conciliada nunca altera conta.

RN-EXP-015

Mudança de valor gera novo Snapshot.

RN-EXP-016

Mudança de data recalcula Cash Flow.

RN-EXP-017

Mudança de categoria recalcula Budget.

RN-EXP-018

Mudança de conta recalcula saldo.

RN-EXP-019

Toda alteração gera auditoria.

RN-EXP-020

Toda alteração gera Domain Event.

Exclusão

RN-EXP-021

Nunca excluir despesas conciliadas.

RN-EXP-022

Nunca excluir despesas importadas.

RN-EXP-023

Nunca excluir despesas parceladas parcialmente pagas.

RN-EXP-024

Cancelar sempre gera histórico.

RN-EXP-025

Soft Delete mantém relacionamentos.

Pagamento

RN-EXP-026

Pagamento parcial permitido.

RN-EXP-027

Pagamento total muda Status para Paid.

RN-EXP-028

Pagamento superior ao saldo é proibido.

RN-EXP-029

Pagamento atualiza saldo da conta.

RN-EXP-030

Pagamento atualiza Dashboard.

RN-EXP-031

Pagamento atualiza Cash Flow.

RN-EXP-032

Pagamento atualiza Budget.

RN-EXP-033

Pagamento gera evento ExpensePaid.

Parcelamento

RN-EXP-034

Quantidade mínima de parcelas = 2.

RN-EXP-035

Parcelas herdam categoria.

RN-EXP-036

Parcelas herdam conta.

RN-EXP-037

Cada parcela possui vencimento próprio.

RN-EXP-038

Cada parcela possui status próprio.

RN-EXP-039

Pagamento de uma parcela não altera outras.

RN-EXP-040

Cancelar parcela não cancela financiamento.

RN-EXP-041

Permitir antecipação.

RN-EXP-042

Permitir quitação.

Recorrência

RN-EXP-043

Recorrência nunca gera parcelas.

RN-EXP-044

Cada recorrência gera nova Expense.

RN-EXP-045

Recorrência possui origem.

RN-EXP-046

Alteração não retroage.

RN-EXP-047

Permitir encerramento.

RN-EXP-048

Permitir pausa.

RN-EXP-049

Permitir retomada.

RN-EXP-050

Recorrência gera Projection futura.

Importação

RN-EXP-051

Open Finance gera Suggestion.

RN-EXP-052

OCR gera Suggestion.

RN-EXP-053

Importação nunca altera Expense manual.

RN-EXP-054

Matching obrigatório.

RN-EXP-055

Hash obrigatório.

RN-EXP-056

Importação duplicada proibida.

RN-EXP-057

Importação gera Auditoria.

RN-EXP-058

Importação preserva origem.

Conciliação

RN-EXP-059

Uma Expense conciliada torna-se imutável.

RN-EXP-060

Permitir desconciliação mediante permissão.

RN-EXP-061

Conciliação registra usuário.

RN-EXP-062

Conciliação registra data.

RN-EXP-063

Conciliação gera Projection.

RN-EXP-064

Conciliação atualiza saldo.

RN-EXP-065

Conciliação atualiza Dashboard.

Relacionamentos

Expense

→ Category

→ Account

→ Budget

→ Cash Flow

→ Tags

→ Cost Center

→ Attachments

→ Installments

→ Subscription

→ Workflow

→ AI

→ Calendar

→ Notifications

Eventos

ExpenseCreated

ExpenseUpdated

ExpenseDeleted

ExpenseCancelled

ExpensePaid

ExpenseRefunded

ExpenseImported

ExpenseMatched

ExpenseReconciled

ExpenseUnreconciled

ExpenseArchived

Permissões

Expense.Read

Expense.Create

Expense.Update

Expense.Delete

Expense.Pay

Expense.Cancel

Expense.Import

Expense.Reconcile

Expense.Export

Expense.Restore

Auditoria

Registrar

Valor anterior

Valor novo

Categoria anterior

Categoria nova

Conta anterior

Conta nova

Usuário

IP

CorrelationId

Timestamp

Casos de Uso

Criar despesa.

Editar despesa.

Cancelar despesa.

Pagar despesa.

Parcelar despesa.

Transformar em recorrência.

Importar despesa.

Conciliar despesa.

Desconciliar.

Anexar documentos.

Exportar.

Duplicar.

Arquivar.

Restaurar.

Objetivos de Performance

Criar despesa

<100 ms

Editar

<100 ms

Pagamento

<150 ms

Conciliação

<300 ms

Importação

Processamento assíncrono

Regras de Integração

Atualiza Budget.

Atualiza Dashboard.

Atualiza Cash Flow.

Atualiza Calendar.

Atualiza AI Context.

Atualiza Reports.

Atualiza Search.

Atualiza Health Score.

Atualiza Recommendation Engine.

Publica Domain Events.

Aggregate

Income

Objetivos

Representar qualquer entrada financeira.

Receber salário.

Receber dividendos.

Receber PIX.

Receber TED.

Receber DOC.

Receber cashback.

Receber restituições.

Receber aluguel.

Receber rendimentos.

Regras Gerais

RN-INC-001

Toda receita pertence a um Workspace.

RN-INC-002

Valor obrigatório e maior que zero.

RN-INC-003

Categoria obrigatória.

RN-INC-004

Conta obrigatória.

RN-INC-005

Receita pode ser prevista ou realizada.

RN-INC-006

Receita recorrente gera novas entradas automaticamente.

RN-INC-007

Receita conciliada torna-se imutável.

RN-INC-008

Receitas importadas preservam vínculo com a origem.

RN-INC-009

Receita recebida atualiza imediatamente o saldo da conta.

RN-INC-010

Toda alteração gera eventos, auditoria e recalcula Cash Flow, Dashboard, Budget e Health Score.

Escopo restante do Volume 31

Este volume ainda deverá conter, mantendo o mesmo nível de detalhamento:

Financial Accounts (≈80 regras)
Transfers (≈60 regras)
Categories (≈70 regras)
Tags (≈30 regras)
Cost Centers (≈40 regras)
Attachments (≈50 regras)
Recurrence Engine (≈90 regras)
Installment Engine (≈100 regras)
Reconciliation Engine (≈120 regras)

-------

# Flora Finance

# Volume 32

# Planning Domain Complete Specification

Version: 1.0

Status: Approved

Domain: Planning

Bounded Contexts

- Budget
- Goal
- Cash Reserve
- Financial Planning
- Forecast
- Cash Flow Rules
- Financial Calendar Rules

---

# Objetivo

Este documento define todas as regras de negócio relacionadas ao planejamento financeiro do usuário.

O objetivo é permitir que qualquer mecanismo de IA implemente integralmente o domínio sem inferir comportamento.

---

# Aggregate Root

Budget

---

## Estados

Draft

Active

Paused

Closed

Archived

Deleted

---

# Objetivos do Budget

Controlar gastos.

Controlar limites.

Alertar excessos.

Alimentar Dashboard.

Atualizar Cash Flow.

Atualizar Health Score.

Atualizar IA.

Produzir Insights.

---

# Regras Gerais

RN-BUD-001

Todo Budget pertence obrigatoriamente a um Workspace.

RN-BUD-002

Todo Budget pertence a um período.

RN-BUD-003

Não poderá existir mais de um Budget ativo para o mesmo período e escopo.

RN-BUD-004

Budget poderá ser Mensal, Trimestral ou Anual.

RN-BUD-005

Budget poderá possuir categorias específicas.

RN-BUD-006

Budget poderá controlar apenas uma conta financeira.

RN-BUD-007

Budget poderá controlar todas as contas.

RN-BUD-008

Budget poderá possuir centro de custo.

RN-BUD-009

Budget poderá possuir Tags.

RN-BUD-010

Soft Delete obrigatório.

---

# Criação

RN-BUD-011

Valor limite obrigatório.

RN-BUD-012

Período obrigatório.

RN-BUD-013

Nome obrigatório.

RN-BUD-014

Categoria opcional.

RN-BUD-015

Centro de custo opcional.

RN-BUD-016

Moeda obrigatória.

RN-BUD-017

Criar Budget gera evento BudgetCreated.

RN-BUD-018

Criar Budget gera Snapshot.

RN-BUD-019

Criar Budget atualiza Dashboard.

RN-BUD-020

Criar Budget atualiza IA.

---

# Consumo

RN-BUD-021

Toda Expense Paid consome orçamento.

RN-BUD-022

Expense Cancelled não consome orçamento.

RN-BUD-023

Expense Reversed remove consumo.

RN-BUD-024

Expense Refund reduz consumo.

RN-BUD-025

Transferências nunca consomem orçamento.

RN-BUD-026

Receitas nunca alteram orçamento.

RN-BUD-027

Parcelas consomem individualmente.

RN-BUD-028

Recorrências consomem individualmente.

RN-BUD-029

Conciliação recalcula consumo.

RN-BUD-030

Importações recalculam consumo.

---

# Percentuais

RN-BUD-031

Consumo percentual será recalculado em tempo real.

RN-BUD-032

Percentual = Consumo / Limite.

RN-BUD-033

Consumo superior a 100% gera excesso.

RN-BUD-034

Consumo igual a 100% gera orçamento esgotado.

RN-BUD-035

Consumo inferior a zero é proibido.

RN-BUD-036

Percentual sempre entre 0 e infinito.

RN-BUD-037

Valores negativos nunca permitidos.

RN-BUD-038

Budget nunca altera despesas.

RN-BUD-039

Budget apenas monitora.

RN-BUD-040

Budget nunca movimenta dinheiro.

---

# Alertas

RN-BUD-041

80% gera alerta Preventivo.

RN-BUD-042

90% gera alerta Alto.

RN-BUD-043

100% gera alerta Crítico.

RN-BUD-044

110% gera Insight Financeiro.

RN-BUD-045

Limites configuráveis.

RN-BUD-046

Alertas nunca duplicados.

RN-BUD-047

Alertas poderão ser ignorados.

RN-BUD-048

Alertas ignorados permanecem auditáveis.

RN-BUD-049

Toda mudança gera novo cálculo.

RN-BUD-050

Toda mudança gera Projection.

---

# Encerramento

RN-BUD-051

Budget encerrado torna-se somente leitura.

RN-BUD-052

Budget arquivado permanece consultável.

RN-BUD-053

Budget poderá ser duplicado.

RN-BUD-054

Budget nunca poderá ser reaberto.

RN-BUD-055

Novo período gera novo Budget.

---

# Aggregate Root

Goal

---

## Estados

Draft

Active

Paused

Completed

Cancelled

Expired

Archived

---

# Objetivos

Representar qualquer objetivo financeiro.

Comprar imóvel.

Comprar carro.

Reserva emergência.

Viagem.

Investimento.

Aposentadoria.

Qualquer meta financeira.

---

# Regras Gerais

RN-GOL-001

Toda Goal pertence a um Workspace.

RN-GOL-002

Nome obrigatório.

RN-GOL-003

Valor alvo obrigatório.

RN-GOL-004

Valor alvo maior que zero.

RN-GOL-005

Data alvo opcional.

RN-GOL-006

Categoria opcional.

RN-GOL-007

Conta destino opcional.

RN-GOL-008

Soft Delete obrigatório.

RN-GOL-009

Toda Goal gera Projection.

RN-GOL-010

Toda Goal gera Dashboard.

---

# Progresso

RN-GOL-011

Contribuições aumentam progresso.

RN-GOL-012

Saques reduzem progresso.

RN-GOL-013

Nunca permitir progresso negativo.

RN-GOL-014

Nunca permitir progresso acima do valor acumulado.

RN-GOL-015

Meta concluída gera evento.

RN-GOL-016

Meta concluída nunca recebe novas contribuições automáticas.

RN-GOL-017

Permitir reabertura manual.

RN-GOL-018

Permitir arquivamento.

RN-GOL-019

Permitir cancelamento.

RN-GOL-020

Permitir duplicação.

---

# Contribuições

RN-GOL-021

Contribuições poderão ser manuais.

RN-GOL-022

Contribuições poderão ser automáticas.

RN-GOL-023

Workflow poderá contribuir.

RN-GOL-024

Receita poderá contribuir.

RN-GOL-025

Décimo terceiro poderá contribuir.

RN-GOL-026

Férias poderão contribuir.

RN-GOL-027

Dividendos poderão contribuir.

RN-GOL-028

Cashback poderá contribuir.

RN-GOL-029

Transferências poderão contribuir.

RN-GOL-030

Toda contribuição gera auditoria.

---

# Aggregate

Cash Reserve

---

# Objetivo

Representar reserva de emergência.

---

# Regras

RN-RES-001

Apenas uma reserva principal por Workspace.

RN-RES-002

Reserva possui meta mínima.

RN-RES-003

Meta sugerida entre 3 e 12 meses de despesas.

RN-RES-004

IA poderá sugerir aumento.

RN-RES-005

Reserva compõe Health Score.

RN-RES-006

Reserva compõe Patrimônio.

RN-RES-007

Saques reduzem proteção financeira.

RN-RES-008

Reposições atualizam Dashboard.

RN-RES-009

Nunca permitir saldo negativo.

RN-RES-010

Toda alteração gera Projection.

---

# Aggregate

Forecast

---

# Objetivo

Projetar cenário futuro.

---

# Regras

RN-FOR-001

Forecast nunca altera domínio.

RN-FOR-002

Forecast sempre utiliza Snapshot.

RN-FOR-003

Forecast poderá considerar inflação futuramente.

RN-FOR-004

Forecast poderá considerar reajustes.

RN-FOR-005

Forecast poderá considerar investimentos.

RN-FOR-006

Forecast poderá considerar financiamentos.

RN-FOR-007

Forecast deverá ser reproduzível.

RN-FOR-008

Forecast nunca utiliza IA para cálculos.

RN-FOR-009

Forecast apenas consome Projection Engine.

RN-FOR-010

Forecast poderá gerar múltiplos cenários.

---

# Eventos do Domínio

BudgetCreated

BudgetClosed

BudgetExceeded

GoalCreated

GoalContributionAdded

GoalCompleted

GoalCancelled

ReserveUpdated

ForecastGenerated

PlanningProjectionUpdated

---

# Integrações Obrigatórias

Todo Budget atualiza:

- Dashboard
- Cash Flow
- Recommendation Engine
- Health Score
- Reports
- AI Context

Toda Goal atualiza:

- Dashboard
- Patrimônio
- Cash Flow
- Recommendation Engine
- Financial Calendar
- AI Context

Todo Forecast atualiza:

- Dashboard
- Simulation Engine
- Recommendation Engine

---

# Objetivo Final do Domínio

O domínio de Planning deverá ser capaz de responder, de forma determinística, perguntas como:

- Quanto posso gastar este mês?
- Quando atingirei minha meta?
- Qual será meu saldo em seis meses?
- Minha reserva de emergência é suficiente?
- Quanto preciso economizar por mês para atingir um objetivo?
- Qual será o impacto financeiro se eu antecipar uma dívida?

Nenhuma dessas respostas dependerá diretamente de IA. Todos os cálculos deverão ser produzidos pelos motores de domínio já especificados e apenas consumidos pela camada de inteligência artificial.

-----
# Flora Finance

# Volume 33

# Debt Domain Complete Specification

Version: 1.0

Status: Approved

Domain: Debt Management

Bounded Contexts

- Credit Cards
- Loans
- Financing
- Debt Negotiation
- Debt Prioritization
- Debt Health
- Payment Strategies

---

# Objetivo

Este documento define todas as regras de negócio relacionadas à gestão de dívidas do Flora Finance.

O domínio Debt deverá ser capaz de representar qualquer obrigação financeira do usuário, desde uma simples compra parcelada até financiamentos imobiliários com centenas de parcelas.

Todo cálculo deverá ser determinístico.

A IA nunca realizará cálculos financeiros.

---

# Aggregate Root

Debt

---

## Tipos

CreditCard

Loan

Mortgage

VehicleFinancing

PayrollLoan

PersonalLoan

CreditLine

TaxDebt

PrivateDebt

Other

---

## Estados

Draft

Pending

Active

Renegotiated

Settled

Cancelled

Defaulted

WrittenOff

Archived

Deleted

---

# Objetivos

Representar dívidas.

Calcular saldo devedor.

Projetar quitação.

Priorizar pagamentos.

Calcular juros.

Gerar projeções.

Atualizar patrimônio.

Atualizar Dashboard.

Atualizar Health Score.

---

# Regras Gerais

RN-DEBT-001

Toda dívida pertence a um Workspace.

RN-DEBT-002

Toda dívida possui um tipo.

RN-DEBT-003

Valor original obrigatório.

RN-DEBT-004

Saldo devedor obrigatório.

RN-DEBT-005

Data de contratação obrigatória.

RN-DEBT-006

Taxa de juros opcional apenas para dívidas sem incidência de juros.

RN-DEBT-007

Toda dívida gera histórico.

RN-DEBT-008

Soft Delete obrigatório.

RN-DEBT-009

Toda dívida participa do cálculo patrimonial.

RN-DEBT-010

Toda dívida atualiza o Health Score.

---

# Saldo Devedor

RN-DEBT-011

Saldo devedor nunca poderá ser negativo.

RN-DEBT-012

Pagamento reduz saldo devedor.

RN-DEBT-013

Juros aumentam saldo devedor.

RN-DEBT-014

Amortizações reduzem principal.

RN-DEBT-015

Pagamento parcial permitido.

RN-DEBT-016

Pagamento total altera Status para Settled.

RN-DEBT-017

Liquidação gera evento DebtSettled.

RN-DEBT-018

Toda alteração recalcula patrimônio líquido.

RN-DEBT-019

Toda alteração recalcula Cash Flow.

RN-DEBT-020

Toda alteração gera Snapshot.

---

# Parcelas

RN-DEBT-021

Toda parcela possui vencimento independente.

RN-DEBT-022

Cada parcela possui status próprio.

RN-DEBT-023

Parcelas poderão ser antecipadas.

RN-DEBT-024

Parcelas vencidas geram alerta.

RN-DEBT-025

Parcelas pagas tornam-se imutáveis.

RN-DEBT-026

Parcelas poderão ser renegociadas.

RN-DEBT-027

Pagamento parcial de parcela é permitido.

RN-DEBT-028

Parcela quitada atualiza saldo devedor.

RN-DEBT-029

Parcela cancelada gera auditoria.

RN-DEBT-030

Toda parcela gera Projection.

---

# Juros

RN-DEBT-031

Juros poderão ser simples.

RN-DEBT-032

Juros poderão ser compostos.

RN-DEBT-033

Taxa anual convertida para taxa mensal quando necessário.

RN-DEBT-034

Toda simulação deverá informar o método utilizado.

RN-DEBT-035

Juros nunca poderão ser negativos.

RN-DEBT-036

Mudança de taxa gera nova projeção.

RN-DEBT-037

Toda projeção deverá ser reproduzível.

RN-DEBT-038

Histórico de taxas deverá ser preservado.

RN-DEBT-039

Juros futuros nunca alteram histórico.

RN-DEBT-040

Toda alteração gera auditoria.

---

# Renegociação

RN-DEBT-041

Renegociação nunca substitui o contrato original.

RN-DEBT-042

Toda renegociação gera versão.

RN-DEBT-043

Histórico completo obrigatório.

RN-DEBT-044

Permitir redução de juros.

RN-DEBT-045

Permitir aumento de prazo.

RN-DEBT-046

Permitir desconto.

RN-DEBT-047

Permitir entrada.

RN-DEBT-048

Toda renegociação recalcula projeções.

RN-DEBT-049

Toda renegociação gera eventos.

RN-DEBT-050

Renegociação nunca altera pagamentos históricos.

---

# Liquidação

RN-DEBT-051

Permitir quitação antecipada.

RN-DEBT-052

Permitir desconto de liquidação.

RN-DEBT-053

Calcular economia de juros.

RN-DEBT-054

Atualizar patrimônio imediatamente.

RN-DEBT-055

Atualizar Dashboard.

RN-DEBT-056

Atualizar IA.

RN-DEBT-057

Atualizar Recommendation Engine.

RN-DEBT-058

Atualizar Cash Flow.

RN-DEBT-059

Atualizar Health Score.

RN-DEBT-060

Registrar auditoria completa.

---

# Priorização

RN-DEBT-061

Sistema deverá calcular prioridade automaticamente.

RN-DEBT-062

Usuário poderá alterar prioridade manualmente.

RN-DEBT-063

Priorização considera juros.

RN-DEBT-064

Priorização considera atraso.

RN-DEBT-065

Priorização considera impacto patrimonial.

RN-DEBT-066

Priorização considera risco.

RN-DEBT-067

Priorização considera garantias.

RN-DEBT-068

Priorização considera fluxo de caixa.

RN-DEBT-069

Priorização nunca altera pagamentos automaticamente.

RN-DEBT-070

IA apenas explica a priorização.

---

# Estratégias

Snowball

Avalanche

Highest Interest

Lowest Balance

Manual

Hybrid

---

# Regras

RN-DEBT-071

Sistema suporta múltiplas estratégias.

RN-DEBT-072

Estratégia poderá ser alterada.

RN-DEBT-073

Toda simulação preserva histórico.

RN-DEBT-074

Estratégia nunca executa pagamentos.

RN-DEBT-075

Resultado deverá ser reproduzível.

RN-DEBT-076

Toda estratégia deverá apresentar justificativa.

RN-DEBT-077

Toda estratégia poderá ser comparada.

RN-DEBT-078

Permitir salvar simulações.

RN-DEBT-079

Permitir compartilhar simulações.

RN-DEBT-080

Toda alteração gera Recommendation.

---

# Aggregate

Credit Card

---

## Objetivo

Representar cartões de crédito.

---

# Regras Gerais

RN-CARD-001

Todo cartão pertence a um Workspace.

RN-CARD-002

Todo cartão possui bandeira.

RN-CARD-003

Todo cartão possui limite.

RN-CARD-004

Limite utilizado nunca poderá exceder o limite disponível sem registro de excesso.

RN-CARD-005

Permitir múltiplos cartões.

RN-CARD-006

Permitir cartões inativos.

RN-CARD-007

Permitir cancelamento.

RN-CARD-008

Cancelar cartão não remove histórico.

RN-CARD-009

Toda compra pertence a uma fatura.

RN-CARD-010

Toda compra possui data de lançamento.

---

# Limite

RN-CARD-011

Toda compra reduz limite disponível.

RN-CARD-012

Pagamento libera limite.

RN-CARD-013

Estorno libera limite.

RN-CARD-014

Compra cancelada libera limite.

RN-CARD-015

Parcelamentos comprometem limite integralmente no momento da compra.

RN-CARD-016

O limite será liberado progressivamente conforme as parcelas forem quitadas.

RN-CARD-017

Compras futuras nunca alteram limite atual.

RN-CARD-018

Permitir limite emergencial.

RN-CARD-019

Permitir limite temporário.

RN-CARD-020

Histórico de alterações obrigatório.

---

# Faturas

RN-CARD-021

Cada cartão possui um ciclo de faturamento.

RN-CARD-022

Cada fatura possui data de fechamento.

RN-CARD-023

Cada fatura possui data de vencimento.

RN-CARD-024

Compras após o fechamento pertencem à próxima fatura.

RN-CARD-025

Pagamento parcial gera saldo remanescente.

RN-CARD-026

Pagamento mínimo deverá ser registrado.

RN-CARD-027

Juros do rotativo deverão ser calculados separadamente.

RN-CARD-028

Permitir antecipação de parcelas.

RN-CARD-029

Permitir fechamento extraordinário apenas se suportado pela instituição.

RN-CARD-030

Toda fatura gera Snapshot financeiro.

---

# Cartões Cancelados

RN-CARD-031

Cartão cancelado não aceita novas compras.

RN-CARD-032

Faturas existentes permanecem ativas.

RN-CARD-033

Parcelamentos continuam normalmente.

RN-CARD-034

Cartão poderá ser arquivado.

RN-CARD-035

Nunca excluir histórico.

---

# Objetivos do Sistema

O módulo Debt deverá responder, de forma determinística:

- Qual dívida devo quitar primeiro?
- Quanto economizarei antecipando parcelas?
- Quanto pagarei de juros até o fim do contrato?
- Qual cartão está mais comprometido?
- Quanto do meu patrimônio está comprometido por dívidas?
- Quanto tempo falta para quitar todas as dívidas?
- Qual será meu saldo devedor em qualquer data futura?
- Qual estratégia (Snowball, Avalanche ou outra) reduz mais juros considerando meu fluxo de caixa?

Todas essas respostas deverão ser produzidas exclusivamente pelos motores do domínio Debt e apenas apresentadas pela camada de IA.

---


# Flora Finance

# Volume 34

# Wealth Domain Complete Specification

Version: 1.0

Status: Approved

Domain: Wealth Management

Bounded Contexts

- Assets
- Investments
- Investment Portfolio
- Net Worth
- Wealth Evolution
- Retirement Planning
- Financial Independence (FIRE)

---

# Objetivo

O domínio Wealth é responsável por representar o patrimônio completo do usuário.

O patrimônio deverá considerar ativos e passivos, permitindo visualizar a evolução patrimonial ao longo do tempo.

Nenhum cálculo será realizado pela IA.

Todos os cálculos serão produzidos pelo domínio Wealth.

---

# Aggregate Root

Asset

---

## Tipos

Cash

CheckingAccount

Savings

Investment

Stock

ETF

FII

Bond

Treasury

Cryptocurrency

Property

Vehicle

Business

Collectible

Other

---

## Estados

Draft

Active

Inactive

Sold

Transferred

WrittenOff

Archived

Deleted

---

# Objetivos

Representar ativos.

Calcular patrimônio.

Atualizar patrimônio líquido.

Projetar evolução.

Atualizar Dashboard.

Atualizar Recommendation Engine.

Atualizar Health Score.

---

# Regras Gerais

RN-AST-001

Todo ativo pertence obrigatoriamente a um Workspace.

RN-AST-002

Todo ativo possui um tipo.

RN-AST-003

Nome obrigatório.

RN-AST-004

Valor atual obrigatório.

RN-AST-005

Valor nunca poderá ser negativo.

RN-AST-006

Todo ativo possui moeda.

RN-AST-007

Soft Delete obrigatório.

RN-AST-008

Histórico obrigatório.

RN-AST-009

Todo ativo participa do cálculo patrimonial.

RN-AST-010

Todo ativo gera Snapshot.

---

# Avaliação

RN-AST-011

Valor poderá ser atualizado manualmente.

RN-AST-012

Valor poderá ser atualizado automaticamente.

RN-AST-013

Toda atualização preserva histórico.

RN-AST-014

Nunca sobrescrever avaliações anteriores.

RN-AST-015

Toda avaliação possui data de referência.

RN-AST-016

Toda avaliação gera Projection.

RN-AST-017

Toda avaliação atualiza Dashboard.

RN-AST-018

Toda avaliação atualiza Patrimônio Líquido.

RN-AST-019

Toda avaliação recalcula evolução patrimonial.

RN-AST-020

Toda alteração gera auditoria.

---

# Venda

RN-AST-021

Venda encerra ativo.

RN-AST-022

Venda gera histórico.

RN-AST-023

Venda preserva avaliações.

RN-AST-024

Venda poderá gerar ganho.

RN-AST-025

Venda poderá gerar prejuízo.

RN-AST-026

Venda atualiza patrimônio.

RN-AST-027

Venda atualiza Cash Flow.

RN-AST-028

Venda gera eventos.

RN-AST-029

Venda nunca remove histórico.

RN-AST-030

Venda poderá ser revertida mediante permissão.

---

# Aggregate

Investment

---

## Objetivos

Representar aplicações financeiras.

---

## Tipos

FixedIncome

Treasury

CDB

LCI

LCA

Debenture

CRI

CRA

Stock

ETF

REIT

FII

MutualFund

Crypto

PrivateEquity

Other

---

# Regras Gerais

RN-INV-001

Todo investimento pertence a um Asset.

RN-INV-002

Quantidade obrigatória.

RN-INV-003

Preço médio obrigatório.

RN-INV-004

Valor investido obrigatório.

RN-INV-005

Data de aquisição obrigatória.

RN-INV-006

Permitir múltiplas compras.

RN-INV-007

Permitir vendas parciais.

RN-INV-008

Permitir reinvestimentos.

RN-INV-009

Permitir aportes.

RN-INV-010

Histórico obrigatório.

---

# Aportes

RN-INV-011

Novo aporte recalcula preço médio.

RN-INV-012

Novo aporte recalcula patrimônio.

RN-INV-013

Novo aporte atualiza Dashboard.

RN-INV-014

Novo aporte atualiza evolução patrimonial.

RN-INV-015

Novo aporte gera Projection.

RN-INV-016

Novo aporte gera auditoria.

RN-INV-017

Novo aporte nunca altera histórico.

RN-INV-018

Permitir aportes automáticos.

RN-INV-019

Permitir Workflow.

RN-INV-020

Permitir Open Finance.

---

# Dividendos

RN-INV-021

Dividendos representam Income.

RN-INV-022

Dividendos poderão ser reinvestidos.

RN-INV-023

Dividendos poderão ser sacados.

RN-INV-024

Dividendos atualizam patrimônio.

RN-INV-025

Dividendos atualizam Dashboard.

RN-INV-026

Dividendos geram histórico.

RN-INV-027

Dividendos poderão ser importados.

RN-INV-028

Dividendos nunca alteram preço médio.

RN-INV-029

Dividendos geram eventos.

RN-INV-030

Dividendos alimentam Recommendation Engine.

---

# Rentabilidade

RN-INV-031

Rentabilidade nominal.

RN-INV-032

Rentabilidade percentual.

RN-INV-033

Rentabilidade anualizada.

RN-INV-034

Rentabilidade acumulada.

RN-INV-035

Rentabilidade líquida.

RN-INV-036

Rentabilidade bruta.

RN-INV-037

Rentabilidade nunca altera patrimônio histórico.

RN-INV-038

Toda atualização preserva histórico.

RN-INV-039

Toda atualização gera Snapshot.

RN-INV-040

Toda atualização gera Projection.

---

# Aggregate

Net Worth

---

# Objetivo

Representar patrimônio líquido.

---

# Fórmula

Ativos

-

Passivos

=

Patrimônio Líquido

---

# Regras

RN-NET-001

Patrimônio nunca poderá ser editado manualmente.

RN-NET-002

Patrimônio sempre será calculado.

RN-NET-003

Todo Asset influencia patrimônio.

RN-NET-004

Toda Debt influencia patrimônio.

RN-NET-005

Atualização em tempo real.

RN-NET-006

Snapshot diário.

RN-NET-007

Permitir histórico.

RN-NET-008

Permitir comparação entre períodos.

RN-NET-009

Permitir projeções.

RN-NET-010

Toda alteração gera Dashboard.

---

# Aggregate

Retirement Plan

---

# Objetivos

Planejamento de aposentadoria.

---

# Regras

RN-RET-001

Usuário poderá definir idade alvo.

RN-RET-002

Usuário poderá definir patrimônio alvo.

RN-RET-003

Sistema calculará necessidade mensal.

RN-RET-004

Sistema calculará patrimônio projetado.

RN-RET-005

Sistema poderá utilizar inflação configurável.

RN-RET-006

Sistema poderá utilizar rentabilidade esperada.

RN-RET-007

Sistema poderá gerar múltiplos cenários.

RN-RET-008

Sistema nunca alterará investimentos automaticamente.

RN-RET-009

Toda simulação gera Projection.

RN-RET-010

Toda simulação poderá ser salva.

---

# Aggregate

Financial Independence (FIRE)

---

# Objetivos

Calcular independência financeira.

---

# Regras

RN-FIRE-001

Calcular patrimônio necessário.

RN-FIRE-002

Calcular taxa de retirada.

RN-FIRE-003

Calcular tempo restante.

RN-FIRE-004

Permitir cenários.

RN-FIRE-005

Permitir inflação.

RN-FIRE-006

Permitir rentabilidade.

RN-FIRE-007

Permitir aumento de aportes.

RN-FIRE-008

Permitir redução de despesas.

RN-FIRE-009

Nunca alterar domínio.

RN-FIRE-010

Gerar Recommendation.

---

# Evolução Patrimonial

RN-WEA-001

Registrar patrimônio diariamente.

RN-WEA-002

Permitir comparação mensal.

RN-WEA-003

Permitir comparação anual.

RN-WEA-004

Calcular crescimento absoluto.

RN-WEA-005

Calcular crescimento percentual.

RN-WEA-006

Registrar maiores altas.

RN-WEA-007

Registrar maiores perdas.

RN-WEA-008

Permitir filtros por ativos.

RN-WEA-009

Permitir exportação.

RN-WEA-010

Nunca apagar histórico.

---

# Eventos do Domínio

AssetCreated

AssetUpdated

AssetSold

AssetTransferred

InvestmentCreated

InvestmentContributionAdded

InvestmentSold

DividendReceived

NetWorthUpdated

RetirementProjectionGenerated

FireProjectionGenerated

WealthProjectionUpdated

---

# Integrações Obrigatórias

Todo Asset atualiza

- Dashboard
- Net Worth
- Recommendation Engine
- Reports
- AI Context

Todo Investment atualiza

- Dashboard
- Wealth Evolution
- Patrimônio Líquido
- Recommendation Engine
- Goal Engine
- Reports

Todo Dividendo atualiza

- Income
- Cash Flow
- Dashboard
- Patrimônio
- Recommendation Engine

---

# Objetivo Final do Domínio

O domínio Wealth deverá responder, de forma determinística:

- Qual é meu patrimônio líquido hoje?
- Quanto meu patrimônio evoluiu nos últimos anos?
- Quanto preciso investir por mês para atingir minha independência financeira?
- Qual investimento representa maior parte da minha carteira?
- Qual é a distribuição percentual dos meus ativos?
- Quanto recebi de dividendos em um período?
- Qual foi minha rentabilidade consolidada?
- Quando atingirei meu patrimônio alvo considerando meus aportes atuais?

Todas essas respostas deverão ser produzidas exclusivamente pelos motores do domínio Wealth e apenas apresentadas pela camada de IA.

----

# Flora Finance

# Volume 35

# Platform Domain Complete Specification

Version: 1.0

Status: Approved

Domain: Platform

Bounded Contexts

- Open Finance
- Document Management
- Artificial Intelligence
- Workflow Automation
- Notification Center
- Reports
- Administration
- Identity & Security

---

# Objetivo

O domínio Platform é responsável por fornecer serviços transversais para todos os módulos do Flora Finance.

Nenhum desses componentes deverá conter regras financeiras próprias.

Eles apenas suportam os domínios Core Finance, Planning, Debt e Wealth.

---

# Aggregate

Open Finance Connection

---

## Estados

Pending

Authorizing

Authorized

Synchronizing

Paused

Expired

Revoked

Disconnected

Deleted

---

# Objetivos

Sincronizar contas.

Importar movimentações.

Atualizar saldos.

Importar cartões.

Importar investimentos.

Importar empréstimos.

Nunca alterar informações manualmente cadastradas.

---

# Regras

RN-OF-001

Uma conexão pertence a apenas um Workspace.

RN-OF-002

Cada instituição gera uma conexão independente.

RN-OF-003

Tokens nunca serão armazenados em texto puro.

RN-OF-004

Toda sincronização gera histórico.

RN-OF-005

Importações nunca alteram registros manuais automaticamente.

RN-OF-006

Duplicidades deverão ser detectadas.

RN-OF-007

Toda movimentação importada possuirá identificador da origem.

RN-OF-008

Toda sincronização poderá ser reexecutada.

RN-OF-009

Conexões expiradas nunca sincronizam.

RN-OF-010

Toda sincronização publica eventos.

---

# Aggregate

Document

---

## Objetivos

Armazenar documentos financeiros.

Permitir OCR.

Relacionar documentos ao domínio.

Preservar histórico.

---

# Regras

RN-DOC-001

Todo documento pertence a um Workspace.

RN-DOC-002

Todo documento possui hash SHA-256.

RN-DOC-003

Arquivos nunca poderão ser sobrescritos.

RN-DOC-004

Versionamento obrigatório.

RN-DOC-005

OCR nunca altera domínio automaticamente.

RN-DOC-006

Toda extração gera sugestões.

RN-DOC-007

Usuário deverá confirmar sugestões.

RN-DOC-008

Documentos nunca serão excluídos fisicamente.

RN-DOC-009

Toda alteração gera auditoria.

RN-DOC-010

Documentos poderão ser vinculados a qualquer entidade financeira.

---

# Aggregate

Workflow

---

## Objetivos

Automatizar tarefas.

Executar ações.

Consumir eventos.

Orquestrar processos.

---

# Regras

RN-WF-001

Todo Workflow pertence a um Workspace.

RN-WF-002

Todo Workflow possui Trigger.

RN-WF-003

Todo Workflow possui Actions.

RN-WF-004

Toda execução gera histórico.

RN-WF-005

Execuções deverão ser idempotentes.

RN-WF-006

Falhas poderão ser reprocessadas.

RN-WF-007

Rollback apenas quando suportado.

RN-WF-008

Workflow nunca altera domínio sem confirmação quando exigido.

RN-WF-009

Permitir execução manual.

RN-WF-010

Toda execução publica eventos.

---

# Aggregate

Notification

---

## Tipos

Push

Email

SMS

WhatsApp

InApp

Webhook

---

# Objetivos

Informar eventos importantes.

Alertar vencimentos.

Notificar mudanças.

---

# Regras

RN-NOT-001

Toda notificação pertence a um usuário.

RN-NOT-002

Usuário poderá desativar categorias.

RN-NOT-003

Alertas críticos nunca poderão ser totalmente desabilitados.

RN-NOT-004

Notificações nunca serão enviadas duplicadas para o mesmo evento.

RN-NOT-005

Falhas deverão ser reprocessadas.

RN-NOT-006

Histórico obrigatório.

RN-NOT-007

Leitura deverá ser registrada.

RN-NOT-008

Toda notificação poderá possuir prioridade.

RN-NOT-009

Templates deverão ser versionados.

RN-NOT-010

Toda entrega gera auditoria.

---

# Aggregate

Artificial Intelligence

---

## Objetivos

Explicar dados.

Gerar insights.

Responder perguntas.

Produzir recomendações.

Jamais modificar domínio.

---

# Regras

RN-AI-001

A IA nunca cria transações financeiras automaticamente.

RN-AI-002

Toda recomendação deverá possuir justificativa.

RN-AI-003

Toda recomendação poderá ser ignorada.

RN-AI-004

A IA nunca altera patrimônio.

RN-AI-005

A IA nunca altera saldos.

RN-AI-006

A IA poderá gerar simulações.

RN-AI-007

Toda resposta deverá ser contextualizada pelo Workspace.

RN-AI-008

Toda consulta será registrada para auditoria.

RN-AI-009

A IA utilizará apenas dados autorizados.

RN-AI-010

Toda ação proposta dependerá de confirmação do usuário.

---

# Aggregate

Report

---

## Objetivos

Consolidar informações.

Permitir exportação.

Produzir indicadores.

---

# Regras

RN-REP-001

Relatórios nunca alteram domínio.

RN-REP-002

Relatórios sempre utilizam snapshots.

RN-REP-003

Exportações deverão preservar filtros aplicados.

RN-REP-004

Permitir PDF.

RN-REP-005

Permitir Excel.

RN-REP-006

Permitir CSV.

RN-REP-007

Toda exportação gera auditoria.

RN-REP-008

Relatórios poderão ser agendados.

RN-REP-009

Relatórios poderão ser compartilhados.

RN-REP-010

Toda geração publica eventos.

---

# Aggregate

Identity

---

## Objetivos

Autenticação.

Autorização.

Controle de acesso.

---

# Regras

RN-ID-001

Todo usuário pertence a pelo menos um Workspace.

RN-ID-002

MFA opcional para usuários comuns e obrigatório para administradores da plataforma.

RN-ID-003

Toda sessão possui expiração.

RN-ID-004

Revogação imediata de sessões comprometidas.

RN-ID-005

Permissões baseadas em papéis (RBAC).

RN-ID-006

Permissões poderão ser refinadas por Workspace.

RN-ID-007

Tentativas inválidas gerarão bloqueio progressivo.

RN-ID-008

Toda autenticação gera auditoria.

RN-ID-009

Senhas nunca serão armazenadas em texto puro.

RN-ID-010

Toda alteração de credenciais exige reautenticação.

---

# Aggregate

Administration

---

## Objetivos

Administrar a plataforma.

Gerenciar tenants.

Gerenciar planos.

Gerenciar limites.

---

# Regras

RN-ADM-001

Somente administradores da plataforma possuem acesso.

RN-ADM-002

Toda alteração gera auditoria.

RN-ADM-003

Toda alteração crítica exige confirmação.

RN-ADM-004

Nenhum administrador poderá acessar dados financeiros sem autorização explícita.

RN-ADM-005

Planos nunca alteram histórico.

RN-ADM-006

Limites deverão ser aplicados em tempo real.

RN-ADM-007

Feature Flags poderão ser específicas por Tenant.

RN-ADM-008

Toda alteração deverá possuir histórico completo.

RN-ADM-009

Nenhuma exclusão física será permitida.

RN-ADM-010

Todas as operações deverão ser rastreáveis.

---

# Regras Gerais da Plataforma

RN-PLT-001

Todos os Aggregates pertencem obrigatoriamente a um Workspace.

RN-PLT-002

Todo Aggregate deverá possuir auditoria.

RN-PLT-003

Soft Delete será o padrão da plataforma.

RN-PLT-004

Todo evento relevante deverá publicar Domain Events.

RN-PLT-005

Toda integração externa deverá ser resiliente.

RN-PLT-006

Nenhum módulo poderá alterar outro módulo diretamente.

RN-PLT-007

Toda comunicação assíncrona utilizará o Event Bus.

RN-PLT-008

Toda operação deverá possuir CorrelationId.

RN-PLT-009

Todas as alterações relevantes deverão atualizar o Dashboard quando aplicável.

RN-PLT-010

Toda ação executada automaticamente deverá ser auditável.

---

# Objetivo Final da Plataforma

A plataforma deverá responder de forma determinística:

- Quais contas estão sincronizadas?
- Quais documentos aguardam confirmação?
- Quais Workflows estão ativos?
- Quais notificações ainda não foram lidas?
- Quais recomendações foram aceitas ou ignoradas?
- Quais integrações apresentaram falha?
- Quem executou determinada ação administrativa?
- Quais relatórios foram gerados e compartilhados?

Todos esses comportamentos deverão ser implementados pelos serviços da plataforma, preservando a separação entre infraestrutura e domínio financeiro.

---

# Encerramento da Especificação Funcional V1

Com este documento considera-se concluída a especificação funcional da versão 1 do Flora Finance.

Todos os domínios principais foram definidos:

- Core Finance
- Planning
- Debt
- Wealth
- Platform

Os próximos documentos deixam de tratar de regras de negócio e passam a especificar exclusivamente a persistência dos dados.

A modelagem do banco de dados deverá ser capaz de implementar integralmente todas as regras descritas nos volumes anteriores, sem necessidade de inferência adicional.

-----
# Flora Finance

# Volume 36

# Database Master Specification

Version: 1.0

Status: Approved

Document Type: Database Architecture Specification

Target Database: PostgreSQL 17+

ORM: Entity Framework Core 10

---

# Objetivo

Este documento estabelece todas as convenções obrigatórias da camada de persistência.

Nenhuma tabela poderá ser criada sem obedecer estas especificações.

Todas as futuras migrations deverão seguir este documento.

---

# Filosofia

A modelagem deverá priorizar:

- Integridade
- Performance
- Escalabilidade
- Simplicidade
- Auditoria
- Evolução

O banco nunca deverá conter lógica de negócio.

Toda regra permanecerá no domínio.

---

# Organização

A aplicação utilizará um único banco PostgreSQL.

Os domínios serão separados por Schemas.

---

Schemas

identity

finance

planning

wealth

debt

platform

documents

notifications

workflow

integration

analytics

administration

audit

---

# Convenções de Nome

Tabelas

PascalCase

Exemplo

Expense

Budget

Goal

Investment

Notification

---

Colunas

PascalCase

Id

WorkspaceId

CreatedAt

UpdatedAt

DeletedAt

---

Chaves Primárias

Sempre

Id UUID

Nunca utilizar SERIAL.

Nunca utilizar BIGINT como PK.

---

UUID

Versão

UUIDv7

Caso indisponível

UUIDv4

---

# Auditoria

Todas as tabelas deverão possuir

Id

CreatedAt

CreatedBy

UpdatedAt

UpdatedBy

DeletedAt

DeletedBy

Version

---

Version

Utilizar Concurrency Token.

Optimistic Concurrency.

---

# Soft Delete

Obrigatório.

Nunca excluir registros.

Excluir significa

DeletedAt != NULL

---

# Tenant

Toda entidade funcional possuirá

TenantId

WorkspaceId

---

Exceto

Lookup Tables

Enums

Configurações Globais

---

# Datas

Sempre utilizar

timestamp with time zone

Nunca timestamp simples.

---

# Valores Monetários

Utilizar

numeric(19,4)

Nunca

float

double

real

money

---

# Percentuais

numeric(9,4)

---

# Quantidades

numeric(19,6)

---

# Boolean

boolean

---

# Texto

varchar

Sempre que houver limite conhecido.

text

Somente para conteúdo ilimitado.

---

# Chaves Estrangeiras

Obrigatórias.

Nunca armazenar referência sem FK.

---

# Delete

ON DELETE RESTRICT

Como padrão.

Cascade apenas quando explicitamente aprovado.

---

# Índices

Obrigatórios

PK

FK

TenantId

WorkspaceId

CreatedAt

Status

Business Keys

---

Índices Compostos

Sempre considerar

WorkspaceId + Status

WorkspaceId + CreatedAt

WorkspaceId + DeletedAt

WorkspaceId + CategoryId

---

# Uniques

Sempre criar quando existir regra de negócio.

Exemplo

Email

Slug

ExternalId

Hash

---

# Checks

Utilizar sempre que possível.

Exemplo

Amount > 0

Percentage >= 0

Percentage <=100

DueDate >= CreatedAt

---

# Enum

Nunca utilizar enum do PostgreSQL.

Todos os enums serão implementados

na aplicação.

Persistidos como

smallint

---

# Nomenclatura FK

FK

Expense

↓

Account

AccountId

---

Nunca

Account_Id

FK_Expense

etc.

---

# Nomenclatura Index

IX

Tabela

Campos

Exemplo

IX_Expense_WorkspaceId

IX_Expense_Status

IX_Expense_CategoryId

---

# Nomenclatura Unique

UX

Tabela

Campo

---

# Nomenclatura Check

CK

Tabela

Regra

---

# Auditoria

Toda alteração crítica gera

AuditLog

Nunca Trigger.

---

# Histórico

Utilizar

Snapshot

Quando necessário.

Nunca duplicar tabelas.

---

# Versionamento

Snapshots

Projection

Audit

Nunca Event Sourcing completo.

---

# Pesquisa

Utilizar

GIN

para

Full Text Search

quando necessário.

---

# JSON

Utilizar

jsonb

Apenas para

Metadata

Configurações

Payloads externos

Nunca para domínio.

---

# Arquivos

Nunca armazenar binários.

Somente

StorageKey

Hash

MimeType

Metadata

---

# Segurança

Dados sensíveis

criptografados

na aplicação.

Nunca no banco.

---

# Máscaras

CPF

CNPJ

Telefone

Email

Nunca mascarar no banco.

Sempre na aplicação.

---

# Migrações

Todas via

Entity Framework

Nunca scripts manuais.

Exceto

Hotfix aprovado.

---

# Seeds

Somente

Lookup Tables

Categorias padrão

Países

Moedas

Idiomas

Permissões

Papéis

Feature Flags

---

# Backup

Point In Time Recovery

Obrigatório.

---

# Retenção

Audit

10 anos

Logs

90 dias

Snapshots

5 anos

---

# Performance

Toda consulta deverá:

Filtrar

WorkspaceId

↓

DeletedAt

↓

Status

Antes de filtros opcionais.

---

# Paginação

Obrigatória.

Offset

e

Cursor.

---

# Ordenação

Sempre determinística.

Nunca confiar

na ordem física.

---

# Nullability

Toda coluna deverá ser

NOT NULL

por padrão.

Nullable apenas

quando fizer sentido.

---

# Modelagem

Preferir

1:N

Evitar

N:N

Sempre que possível.

Utilizar tabela intermediária.

---

# Cascatas

Nunca utilizar cascatas em entidades financeiras.

Toda exclusão será lógica.

---

# Integridade

Nunca permitir órfãos.

---

# Convenções Entity Framework

Todas as entidades deverão herdar

BaseEntity

---

BaseEntity

Id

CreatedAt

CreatedBy

UpdatedAt

UpdatedBy

DeletedAt

DeletedBy

Version

TenantId

WorkspaceId

---

# Aggregate Root

Somente Aggregate Roots possuirão Repository.

Entidades filhas nunca possuirão Repository próprio.

---

# Objetivo Final

A modelagem física deverá ser totalmente previsível.

Nenhuma tabela poderá quebrar estas convenções.

Toda implementação futura deverá ser consistente com este documento.
-----


