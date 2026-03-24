using System.Collections.Generic;
using OrderAllocationApp.Entities;

namespace OrderAllocationApp.Services;

/// <summary>
/// 引当サービスのインターフェース。再利用・テスト差し替えを可能にする。
/// </summary>
public interface IAllocationService
{
    /// <summary>
    /// 注文一覧と在庫一覧を元に引当処理を実行し、結果を返す。
    /// </summary>
    IReadOnlyList<AllocationResult> Execute(IReadOnlyList<Order> orders, IReadOnlyList<Stock> stocks);
}
