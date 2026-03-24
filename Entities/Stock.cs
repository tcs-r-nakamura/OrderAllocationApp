using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrderAllocationApp.Entities;

/// <summary>
/// 在庫エンティティ。DataGrid 編集対応のため INotifyPropertyChanged を実装する。
/// </summary>
public class Stock : INotifyPropertyChanged
{
    /// <summary>在庫数量の最小値（在庫0は有効な状態）</summary>
    public const int MinQuantity = 0;

    private string _productCode = string.Empty;
    private int _quantity;

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

    /// <summary>在庫数量</summary>
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
    /// 在庫エンティティのバリデーション。
    /// 商品コードの空チェックと数量の下限チェックを行う。
    /// </summary>
    public IReadOnlyList<string> Validate()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(ProductCode))
        {
            errors.Add("在庫: 商品コードは必須です。");
        }

        if (Quantity < MinQuantity)
        {
            errors.Add($"在庫 {ProductCode}: 数量は {MinQuantity} 以上を指定してください。");
        }

        return errors;
    }
}
