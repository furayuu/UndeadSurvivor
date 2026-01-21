using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Image fillImage;
    public int currentXP = 0;
    public int maxXP = 100;
    public HPBar hpbar;
    public Player player;

    private void Start()
    {
        UpdateBar();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;

        if (currentXP >= maxXP)
        {
            LevelUp();
        }

        UpdateBar();
    }

    void LevelUp()
    {
        currentXP = 0;
        maxXP += 10;

        //最大HPの増加
        hpbar.maxHP += 10;

        //レベルアップごとに速度を増していく
        player.IncreaseSpeed(0.3f);

        Debug.Log("Level Up!");
    }

    void UpdateBar()
    {
        fillImage.fillAmount = (float)currentXP / maxXP;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            LevelUp();

            Debug.Log("LEVEL UP!");
        }
    }
}
