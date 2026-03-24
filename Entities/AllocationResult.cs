namespace OrderAllocationApp.Entities;

/// <summary>
/// 引当結果エンティティ。引当後に変更されないため init プロパティで不変とする。
/// </summary>
public class AllocationResult
{
    /// <summary>注文ID</summary>
    public int OrderId { get; init; }

    /// <summary>引当成否</summary>
    public bool IsSuccess { get; init; }

    /// <summary>引当済み数量</summary>
    public int AllocatedQuantity { get; init; }

    /// <summary>不足数量（全量引当時は 0）</summary>
    public int ShortageQuantity { get; init; }

    /// <summary>結果メッセージ</summary>
    public string Message { get; init; } = string.Empty;
}
