using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using OrderAllocationApp.Entities;
using OrderAllocationApp.Services;

namespace OrderAllocationApp.ViewModels;

/// <summary>
/// メイン画面の ViewModel。注文・在庫・引当結果のコレクションを管理し、
/// 引当実行コマンドを提供する。
/// </summary>
public class MainViewModel
{
    private readonly IAllocationService _allocationService;

    /// <summary>注文一覧（DataGrid にバインドする）</summary>
    public ObservableCollection<Order> Orders { get; } = new();

    /// <summary>在庫一覧（DataGrid にバインドする）</summary>
    public ObservableCollection<Stock> Stocks { get; } = new();

    /// <summary>引当結果一覧（DataGrid にバインドする）</summary>
    public ObservableCollection<AllocationResult> AllocationResults { get; } = new();

    /// <summary>引当実行ボタンにバインドするコマンド</summary>
    public ICommand ExecuteAllocationCommand { get; }

    /// <summary>
    /// XAML 用デフォルトコンストラクタ。サンプルデータをロードする。
    /// </summary>
    public MainViewModel() : this(new AllocationService())
    {
        LoadSampleData();
    }

    /// <summary>
    /// DI 用コンストラクタ。テスト時にサービスを差し替えられる。
    /// </summary>
    public MainViewModel(IAllocationService allocationService)
    {
        _allocationService = allocationService;
        ExecuteAllocationCommand = new RelayCommand(ExecuteAllocation);
    }

    /// <summary>
    /// 初期表示用のサンプルデータをロードする。
    /// </summary>
    private void LoadSampleData()
    {
        Orders.Add(new Order { OrderId = 1, ProductCode = "A001", Quantity = 5 });
        Orders.Add(new Order { OrderId = 2, ProductCode = "B002", Quantity = 2 });
        Stocks.Add(new Stock { ProductCode = "A001", Quantity = 3 });
        Stocks.Add(new Stock { ProductCode = "B002", Quantity = 10 });
    }

    /// <summary>
    /// 引当実行コマンドの処理。バリデーション後に AllocationService を呼び出す。
    /// </summary>
    private void ExecuteAllocation()
    {
        var validationErrors = CollectValidationErrors();
        if (validationErrors.Count > 0)
        {
            ShowValidationErrors(validationErrors);
            return;
        }

        var results = _allocationService.Execute(Orders.ToList(), Stocks.ToList());

        AllocationResults.Clear();
        foreach (var result in results)
        {
            AllocationResults.Add(result);
        }
    }

    /// <summary>
    /// 全注文・全在庫のバリデーションエラーを収集して返す。
    /// </summary>
    private IReadOnlyList<string> CollectValidationErrors()
    {
        var errors = new List<string>();

        foreach (var order in Orders)
        {
            errors.AddRange(order.Validate());
        }

        foreach (var stock in Stocks)
        {
            errors.AddRange(stock.Validate());
        }

        return errors;
    }

    /// <summary>
    /// バリデーションエラーをメッセージボックスで表示する。
    /// </summary>
    private static void ShowValidationErrors(IReadOnlyList<string> errors)
    {
        var message = new StringBuilder();
        foreach (var error in errors)
        {
            message.AppendLine(error);
        }
        MessageBox.Show(message.ToString(), "入力エラー", MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}
