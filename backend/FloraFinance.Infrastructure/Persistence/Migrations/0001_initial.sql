CREATE TABLE IF NOT EXISTS workspaces (
    id uuid PRIMARY KEY,
    user_id uuid NOT NULL,
    name varchar(120) NOT NULL,
    slug varchar(120) NOT NULL UNIQUE,
    currency varchar(3) NOT NULL,
    language varchar(16) NOT NULL,
    timezone varchar(64) NOT NULL,
    created_at timestamptz NOT NULL,
    updated_at timestamptz NULL,
    deleted_at timestamptz NULL
);
CREATE INDEX IF NOT EXISTS ix_workspaces_user_id ON workspaces(user_id) WHERE deleted_at IS NULL;

CREATE TABLE IF NOT EXISTS financial_accounts (
    id uuid PRIMARY KEY,
    workspace_id uuid NOT NULL REFERENCES workspaces(id),
    name varchar(120) NOT NULL,
    bank varchar(120) NULL,
    type varchar(32) NOT NULL,
    currency varchar(3) NOT NULL,
    initial_balance numeric(18,2) NOT NULL,
    current_balance numeric(18,2) NOT NULL,
    color varchar(16) NOT NULL,
    icon varchar(64) NOT NULL,
    is_archived boolean NOT NULL DEFAULT false,
    created_at timestamptz NOT NULL,
    updated_at timestamptz NULL,
    deleted_at timestamptz NULL
);
CREATE INDEX IF NOT EXISTS ix_financial_accounts_workspace_id ON financial_accounts(workspace_id) WHERE deleted_at IS NULL;

CREATE TABLE IF NOT EXISTS categories (
    id uuid PRIMARY KEY,
    workspace_id uuid NOT NULL REFERENCES workspaces(id),
    name varchar(120) NOT NULL,
    kind varchar(16) NOT NULL,
    is_system boolean NOT NULL DEFAULT false,
    created_at timestamptz NOT NULL,
    updated_at timestamptz NULL,
    deleted_at timestamptz NULL
);
CREATE INDEX IF NOT EXISTS ix_categories_workspace_id ON categories(workspace_id) WHERE deleted_at IS NULL;

CREATE TABLE IF NOT EXISTS incomes (
    id uuid PRIMARY KEY,
    workspace_id uuid NOT NULL REFERENCES workspaces(id),
    account_id uuid NOT NULL REFERENCES financial_accounts(id),
    category_id uuid NOT NULL REFERENCES categories(id),
    description varchar(160) NOT NULL,
    amount numeric(18,2) NOT NULL CHECK (amount > 0),
    currency varchar(3) NOT NULL,
    received_date date NOT NULL,
    observation text NULL,
    created_at timestamptz NOT NULL,
    updated_at timestamptz NULL,
    deleted_at timestamptz NULL
);
CREATE INDEX IF NOT EXISTS ix_incomes_workspace_received_date ON incomes(workspace_id, received_date) WHERE deleted_at IS NULL;

CREATE TABLE IF NOT EXISTS expenses (
    id uuid PRIMARY KEY,
    workspace_id uuid NOT NULL REFERENCES workspaces(id),
    account_id uuid NOT NULL REFERENCES financial_accounts(id),
    category_id uuid NOT NULL REFERENCES categories(id),
    description varchar(160) NOT NULL,
    amount numeric(18,2) NOT NULL CHECK (amount > 0),
    currency varchar(3) NOT NULL,
    due_date date NOT NULL,
    paid_date date NULL,
    observation text NULL,
    is_recurring boolean NOT NULL DEFAULT false,
    installment_number integer NULL,
    installment_total integer NULL,
    created_at timestamptz NOT NULL,
    updated_at timestamptz NULL,
    deleted_at timestamptz NULL,
    CONSTRAINT ck_expenses_installment CHECK (
        (installment_number IS NULL AND installment_total IS NULL)
        OR (installment_number BETWEEN 1 AND installment_total)
    )
);
CREATE INDEX IF NOT EXISTS ix_expenses_workspace_due_date ON expenses(workspace_id, due_date) WHERE deleted_at IS NULL;

CREATE TABLE IF NOT EXISTS transfers (
    id uuid PRIMARY KEY,
    workspace_id uuid NOT NULL REFERENCES workspaces(id),
    source_account_id uuid NOT NULL REFERENCES financial_accounts(id),
    destination_account_id uuid NOT NULL REFERENCES financial_accounts(id),
    amount numeric(18,2) NOT NULL CHECK (amount > 0),
    currency varchar(3) NOT NULL,
    transfer_date date NOT NULL,
    description varchar(160) NULL,
    created_at timestamptz NOT NULL,
    updated_at timestamptz NULL,
    deleted_at timestamptz NULL,
    CONSTRAINT ck_transfers_distinct_accounts CHECK (source_account_id <> destination_account_id)
);
CREATE INDEX IF NOT EXISTS ix_transfers_workspace_transfer_date ON transfers(workspace_id, transfer_date) WHERE deleted_at IS NULL;
