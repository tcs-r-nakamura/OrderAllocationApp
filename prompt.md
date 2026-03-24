あなたはC#エンジニアです。以下の要件に基づき、注文引当プログラムをC#で作成してください。GUIはWPFでMVVMパターンを使用し、動作可能な状態で提示してください。

【目的・ゴール】
- 注文一覧と在庫一覧を入力し、引当結果を確認できるアプリケーション
- 完全なC# WPFプロジェクトコードを生成する（XAML含む）

【言語・フレームワーク】
- 言語：C# .NET 8以上
- GUI：WPF
- 設計パターン：MVVM
- ロジックはAllocationServicesに分離すること

【プロジェクト名・名前空間】
- プロジェクト名: OrderAllocationApp
- ルート名前空間: OrderAllocationApp
- フォルダ構成: Entities, Services, ViewsModels, Views

【入力・出力仕様】
- 入力：
  - Order: OrderId, ProductCode, Quantity
  - Stock: ProductCode, Quantity
- 出力：
  - AllocationResult: OrderId, IsSuccess, AllocatedQuantity, ShortageQuantity, Message

【業務ロジック】
- 注文は入力順に処理
- 在庫が十分なら全量引当
- 在庫が不足している場合は不足数を返す
- 商品コードが在庫一覧に存在しない場合はエラー扱い
- 部分引当やエラー情報も結果に含める

【UI要件】
- DataGridに注文一覧と在庫一覧を表示
- 「引当実行」ボタンでAllocationServicesを呼び出す
- 引当結果をDataGridに表示
- ObservableCollectionを使い、データ変更が自動でUIに反映される
- Windowサイズ: 800x600, タイトル: "注文引当アプリ"
- DataGrid列幅は自動調整、ボタンは上部に配置

【Entities】
- 変更通知はViewsModelsが行う
- 各エンティティは自身のフィールドに対するバリデーションロジックを Validate() メソッドとして持つこと
- Order.Validate()：商品コードの空チェック、数量の下限チェック
- Stock.Validate()：商品コードの空チェック、数量の下限チェック
- 下限値などのマジックナンバーは各エンティティ内の定数として定義すること

【ViewsModels】
- MainViewsModels: ObservableCollectionで注文・在庫・割当結果を管理
- 引当実行ボタンに紐づくICommand名: ExecuteAllocationCommand

【構造・クラス設計】
- Entities: Order, Stock, AllocationResult
- Services: AllocationServices（業務ロジック）
- ViewsModels: MainViewsModels（UI連携、ObservableCollection管理）
- Views: MainWindow.xaml

【コード品質】
- コメントを適切に入れる
- マジックナンバーの徹底排除
- nullは極力非許容
- すべての string プロパティは null ではなく string.Empty で初期化すること
- バリデーションチェックを追加すること
- アンチパターンを避ける(if文の｛｝を可読性の為に省略しない等)
- 命名規則はC#標準（PascalCase, camelCase）
- GUIとロジックを分離、単体テスト可能な構造
- 単一責任の法則を遵守
- 継承は使用せずコンポジションを使用すること
- MVVMの原則を厳守
- 変数宣言は var を使用すること（右辺から型が明らかな場合）。ただし全ファイルで統一すること
- if ブロックが return / throw で終わる場合、後続の else は記述しないこと（else after return はアンチパターン）
- 条件式は可読性の観点から肯定系で記述すること
- 省略して可読性が下がる場合は省略せずに記述すること
- LINQで .Any() のみ使用する場合は .ToList() で中間リストを生成しないこと（不要なメモリ確保と冗長な変数を避ける）
- 1つのモジュールの中で、その型やフィールドに対する専念したロジックがあるのならば、1つのモジュールにまとめる

【初期サンプルデータ】
- Orders:
  - 1, A001, 5
  - 2, B002, 2
- Stocks:
  - A001, 3
  - B002, 10

【出力形式】
- 完全なC# WPFプロジェクトコード
- XAMLとViewsModels、Entity、Servicesクラスをすべて含む
- 実行可能な状態で提示
- 画面表示サンプルも含める
- メッセージには成功時（青文字）失敗時（赤文字）を付与
- 引当結果の文字色・背景色は IValueConverter を使わず、XAML の DataTrigger で表現すること

【制約・例外処理】
- 在庫不足や商品コード不存在の場合は例外ではなくAllocationResultに含める
- 将来の拡張を考慮し、Servicesクラスは再利用可能にする

【補助情報】
- サンプルデータを使った動作例も含める
- CSVやファイル入出力は不要（基本UI操作のみ）

これらの条件に沿って、動作可能で可読性・保守性・再利用性の高いC# WPFプロジェクトコードを生成してください。
また、最後にリファクタリングを行ってください。その際に【コード品質】の項目を注視して下さい。また、使われていないコードがあれば削除してください。
