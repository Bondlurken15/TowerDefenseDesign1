using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    //Configureble parameters
    [Header("Lvl Text")]
    [SerializeField] GameObject shopCanvas;
    [SerializeField] TextMeshProUGUI dmgText;
    [SerializeField] TextMeshProUGUI bulletSpeedText;
    [SerializeField] TextMeshProUGUI pierceText;
    [SerializeField] TextMeshProUGUI towerText;

    [Header("Cost Text")]
    [SerializeField] TextMeshProUGUI dmgCostText;
    [SerializeField] TextMeshProUGUI bulletSpeedCostText;
    [SerializeField] TextMeshProUGUI pierceCostText;
    [SerializeField] TextMeshProUGUI healthCostText;
    [SerializeField] TextMeshProUGUI towerCostText;

    [Header("Money")]
    [SerializeField] int startMoney = 200;
    [SerializeField] int dmgCost = 100;
    [SerializeField] int healthCost = 100;
    [SerializeField] int bulletSpeedCost = 200;
    [SerializeField] int pierceCost = 1000;
    [SerializeField] int towerCost = 10000;

    [Header("Towers")]
    [SerializeField] GameObject[] towers;
    [SerializeField] int amountOfTowers = 4;


    //Privet variables
    public static Bullet modifiedBulletData = new Bullet();
    float modifeyUpgrade = 1.001f;
    int amountOfUpgrade;
    float bunusUpgarde = 1.0f;
    bool maxTower;

    //Cached references
    void Start()
    {
        ModifyMoney(startMoney);

        dmgCostText.text = ("Dmg:" + dmgCost + "$");
        bulletSpeedCostText.text = ("BulletSpeed: " + bulletSpeedCost + "$");
        pierceCostText.text = ("pierce: " + pierceCost + "$");
        healthCostText.text = ("Health: " + healthCost + "$");

        GameManager.GlobalGameManager.PlayerHealthText();

        ActivateTowers();
    }
    public void DmgUpgrade()
    {
        if (GameManager.GlobalGameManager.CurrentPlayerData.playerMoney < dmgCost) { return; }
        ModifyMoney(-dmgCost);
        modifiedBulletData.ModifyDmg(modifeyUpgrade * (bunusUpgarde + (amountOfUpgrade * 0.001f)));
        amountOfUpgrade++;
        dmgText.text = ("Dmg: " + modifiedBulletData.ImpactDamage.ToString("f0"));
    }
    public void HealthUpgrade()
    {
        if (GameManager.GlobalGameManager.CurrentPlayerData.playerMoney < healthCost) { return; }
        ModifyMoney(-healthCost);
        GameManager.GlobalGameManager.ModifyHealth(modifeyUpgrade * (bunusUpgarde + (amountOfUpgrade * 0.001f)));
        amountOfUpgrade++;
        GameManager.GlobalGameManager.PlayerHealthText();
    }
    public void BulletSpeedUpgrade()
    {
        if (GameManager.GlobalGameManager.CurrentPlayerData.playerMoney < bulletSpeedCost) { return; }
        ModifyMoney(-bulletSpeedCost);
        modifiedBulletData.ModifyBulletSpeed(amountOfUpgrade * 0.01f);
        amountOfUpgrade++;
        bulletSpeedText.text = ("Bullet Speed: " + modifiedBulletData.SpeedFactor.ToString("f1"));
    }
    public void PierceUpgrade()
    {
        if (GameManager.GlobalGameManager.CurrentPlayerData.playerMoney < pierceCost) { return; }
        ModifyMoney(-pierceCost);
        modifiedBulletData.ModifyPierce(modifeyUpgrade * (bunusUpgarde + (amountOfUpgrade * 0.001f)));
        amountOfUpgrade++;
        pierceText.text = ("Pierce: " + modifiedBulletData.ImpactHealth.ToString("f0"));
    }
    public void TowerUpgrade()
    {
        if (GameManager.GlobalGameManager.CurrentPlayerData.playerMoney < towerCost || maxTower != false) { return; }
        ModifyMoney(-towerCost);
        amountOfTowers++;
        amountOfUpgrade++;
        towerText.text = ("Tower: " + amountOfTowers.ToString(""));
        if(amountOfTowers == 10)
        {
            maxTower = true;
        }
        ActivateTowers();
    }
    void ActivateTowers()
    {
        for (int i = 0; i < amountOfTowers; i++)
        {
            towers[i].SetActive(true);
        }
    }
    void ModifyMoney(int iteamCost)
    {
        GameManager.GlobalGameManager.CurrentPlayerData.playerMoney += iteamCost;
    }
    public void EnterShop()
    {
        shopCanvas.SetActive(true);
    }
    public void ExitShop()
    {
        shopCanvas.SetActive(false);
    }
}
