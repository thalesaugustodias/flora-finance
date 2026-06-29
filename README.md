# Flora Finance

Implementação inicial organizada a partir dos volumes numerados do backlog.

## Estrutura

- `docs/specs/`: textos extraídos dos PDFs dos volumes 1 a 6 e conteúdo Markdown dos volumes 7 a 9.
- `src/domain/`: entidades, value objects e regras de negócio centrais.
- `src/features/`: serviços de domínio para inteligência financeira, simulações e projeções.
- `test/`: testes unitários com `node:test`.

## Escopo implementado

- Volume 7: `Workspace` com criação automática, defaults (`Meu Financeiro`, `BRL`, `pt-BR`, `America/Sao_Paulo`), slug, soft delete e eventos.
- Volume 8: `FinancialAccount` com validações, tipos de conta, saldo inicial negativo permitido, arquivamento lógico e evento.
- Volume 9: `Income` com validações obrigatórias, valor positivo, vínculo a workspace/conta/categoria e eventos para recálculo.
- Volumes 1 a 6: base de domínio para despesas e serviços puros de inteligência financeira (`financialHealthScore`, `rankDebts`, `forecastCashflow`) sem persistência automática, seguindo as regras de simulação reversível.

## Comandos

```bash
npm test
npm run check
```
