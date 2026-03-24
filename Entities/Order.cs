using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrderAllocationApp.Entities;

/// <summary>
/// 注文エンティティ。DataGrid 編集対応のため INotifyPropertyChanged を実装する。
/// </summary>
public class Order : INotifyPropertyChanged
{
    /// <summary>注文数量の最小値（0件注文は不正）</summary>
    public const int MinQuantity = 1;

    private int _orderId;
    private string _productCode = string.Empty;
    private int _quantity;

    /// <summary>注文ID</summary>
    public int OrderId
    {
        get => _orderId;
        set
        {
            _orderId = value;
            OnPropertyChanged();
        }
    }

    /// <summary>商品コード</summary>
    public string ProductCode
    {
        get => _productCode;
        set
        {
            _productCode = value;
            OnPropertyChanged();
        }
    }

    /// <summary>注文数量</summary>
    public int Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// 注文エンティティのバリデーション。
    /// 商品コードの空チェックと数量の下限チェックを行う。
    /// </summary>
    public IReadOnlyList<string> Validate()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(ProductCode))
        {
            errors.Add($"注文ID {OrderId}: 商品コードは必須です。");
        }

        if (Quantity < MinQuantity)
        {
            errors.Add($"注文ID {OrderId}: 数量は {MinQuantity} 以上を指定してください。");
        }

        return errors;
    }
}
