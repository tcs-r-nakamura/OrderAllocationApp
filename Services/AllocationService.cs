using OrderAllocationApp.Entities;

namespace OrderAllocationApp.Services;

/// <summary>
/// 引当業務ロジック。注文を入力順に処理し、在庫の範囲内で引当を行う。
/// 元の在庫データは変更しない（内部コピーで処理する）。
/// </summary>
public class AllocationService : IAllocationService
{
    private const string MessageSuccess = "引当成功";
    private const string MessageProductNotFound = "商品コードが在庫一覧に存在しません";
    private const string MessageShortageFormat = "在庫不足: {0}個不足";

    /// <summary>
    /// 注文一覧と在庫一覧を受け取り、引当結果を返す。
    /// 在庫の内部コピーを使用するため、渡した Stock オブジェクトは変更されない。
    /// </summary>
    public IReadOnlyList<AllocationResult> Execute(IReadOnlyList<Order> orders, IReadOnlyList<Stock> stocks)
    {
        // 在庫の内部コピーを作成（元の Stock データを変更しない）
        var stockMap = new Dictionary<string, int>();
        foreach (var stock in stocks)
        {
            stockMap[stock.ProductCode] = stock.Quantity;
        }

        var results = new List<AllocationResult>();
        foreach (var order in orders)
        {
            results.Add(ProcessOrder(order, stockMap));
        }

        return results;
    }

    /// <summary>
    /// 1件の注文に対して引当処理を行う。
    /// </summary>
    private static AllocationResult ProcessOrder(Order order, Dictionary<string, int> stockMap)
    {
        if (!stockMap.ContainsKey(order.ProductCode))
        {
            return new AllocationResult
            {
                OrderId = order.OrderId,
                IsSuccess = false,
                AllocatedQuantity = 0,
                ShortageQuantity = order.Quantity,
                Message = MessageProductNotFound
            };
        }

        var available = stockMap[order.ProductCode];

        if (available >= order.Quantity)
        {
            // 全量引当
            stockMap[order.ProductCode] = available - order.Quantity;
            return new AllocationResult
            {
                OrderId = order.OrderId,
                IsSuccess = true,
                AllocatedQuantity = order.Quantity,
                ShortageQuantity = 0,
                Message = MessageSuccess
            };
        }

        // 部分引当（在庫不足）
        var shortage = order.Quantity - available;
        stockMap[order.ProductCode] = 0;
        return new AllocationResult
        {
            OrderId = order.OrderId,
            IsSuccess = false,
            AllocatedQuantity = available,
            ShortageQuantity = shortage,
            Message = string.Format(MessageShortageFormat, shortage)
        };
    }
}
