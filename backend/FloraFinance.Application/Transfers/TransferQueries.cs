using FloraFinance.Application.Abstractions;

namespace FloraFinance.Application.Transfers;

public sealed class TransferQueryHandler(ITransferRepository transfers)
{
    public async Task<IReadOnlyList<TransferResponse>> ListAsync(Guid workspaceId, DateOnly? from, DateOnly? to, CancellationToken cancellationToken)
        => await transfers.ListAsync(workspaceId, from, to, cancellationToken);
}
