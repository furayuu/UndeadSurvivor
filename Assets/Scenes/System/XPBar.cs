using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Image fillImage;
    public int currentXP = 0;
    public int maxXP = 100;

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

        Debug.Log("Level Up!");
    }

    void UpdateBar()
    {
        fillImage.fillAmount = (float)currentXP / maxXP;
    }
}
