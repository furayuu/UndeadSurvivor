using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Image fillImage;    // XPバーの塗り部分（XPBar_Fill をドラッグ）
    public int currentXP = 0;  // 現在の経験値
    public int maxXP = 100;    // 次のレベルまでに必要な経験値

    // 経験値を追加する関数
    public void AddXP(int amount)
    {
        currentXP += amount;

        // レベルアップ処理（上限を超えたらリセット）
        if (currentXP >= maxXP)
        {
            currentXP = 0; // レベルアップ後に経験値をリセット（必要に応じて変更）
            // Debug.Log("レベルアップ！");
        }

        UpdateBar(); // バーの表示を更新
    }

    // 経験値バーの表示を更新する関数
    void UpdateBar()
    {
        float fill = (float)currentXP / maxXP; // 割合を計算（0〜1）
        fillImage.fillAmount = fill; // Image の塗り量を変更
    }

    // テスト用：スペースキーを押すと経験値を追加
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddXP(10); // 10ポイント追加
        }
    }
}
