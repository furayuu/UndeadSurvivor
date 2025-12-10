using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Image fillImage;    // XPバーの塗り部分（XPBar_Fill をドラッグ）
    public int currentXP = 0;  // 現在の経験値
    public int maxXP = 100;    // 次のレベルまでに必要な経験値
    public HPBar hpbar;

    private void Start()
    {
        //経験値バーの初期化
        UpdateBar();
    }

    // 経験値を追加する関数
    public void AddXP(int amount)
    {
        currentXP += amount;

        // レベルアップ処理（上限を超えたらリセット）
        if (currentXP >= maxXP)
        {
            currentXP = 0; // レベルアップ後に経験値をリセット（必要に応じて変更）
            maxXP += 10; //必要経験値を１０増やす
            Debug.Log("レベルアップ！");
            //最大HP増加
            hpbar.maxHP += 10;
            //現在HPがMAXHPよりも低ければ全回復
            if (hpbar.currentHP < hpbar.maxHP)
            {
                hpbar.MaxHeal();
            } 
        }
        UpdateBar(); // バーの表示を更新
    }
    // 経験値バーの表示を更新する関数
    void UpdateBar()
    {
        Debug.Log("更新しました。");
        float fill = (float)currentXP / maxXP; // 割合を計算（0〜1）
        fillImage.fillAmount = fill; // Image の塗り量を変更
    }


    // テスト用：スペースキーを押すと経験値を追加
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            AddXP(10); // 10ポイント追加
            Debug.Log("経験値を追加しました");

        }
    }
}