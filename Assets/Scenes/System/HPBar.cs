using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class HPBar : MonoBehaviour
{
    public Image fillImage;     // HPバーの塗り部分
    public int maxHP = 100;     // 最大HP
    public int currentHP;       // 現在のHP
    public TMP_Text hpText;


    void Start()
    {
        currentHP = maxHP;     // 初期HPを最大値に設定
        UpdateBar();
    }

    // ダメージを受ける関数（敵の攻撃や衝突ダメージ用）
    public void TakeDamage(int amount)
    {
        currentHP -= amount;

        if (currentHP < 0)
        {
            currentHP = 0; // HPが0未満にならないようにする

            //プレイヤーを探して
            GameObject obj = GameObject.Find("Player");

            //プレイヤーをデリート
            Destroy(obj);

            //ゲームオーバーシーンへの遷移
            SceneManager.LoadScene("GameOverScene");
        }
        UpdateBar();
    }

    // 回復する関数（回復アイテム、吸血効果など）
    public void Heal(int amount)
    {
        currentHP += amount;

        if (currentHP > maxHP)
            currentHP = maxHP; // 最大HPを超えないようにする

        UpdateBar();
    }

    public void MaxHeal()
    {
        currentHP = maxHP;

        UpdateBar();
    }

    // HPバーの表示を更新
    void UpdateBar()
    {
        fillImage.fillAmount = (float)currentHP / maxHP;

        if (hpText != null)
            hpText.text = $"{currentHP} / {maxHP}";
    }

    public void IncreaseMaxHP(int amount, bool healToFull = false)
    {
        maxHP += amount;

        if (healToFull)
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP += amount; 
            if (currentHP > maxHP)
                currentHP = maxHP;
        }

        UpdateBar();
    }

}


