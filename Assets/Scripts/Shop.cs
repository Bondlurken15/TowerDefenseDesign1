using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject shopCanvas;
    [SerializeField] TextMeshProUGUI dmgText;
    [SerializeField] int dmgKost = 5;
    [SerializeField] int startMoney = 200;
    public static Bullet modifiedBulletData = new Bullet();
    float modifeyUpgrade = 1.001f;
    int amountOfUpgrade;
    float bunusUpgarde = 1.0f;

    GameManager gameManager;
    void Start()
    {
        //modifiedBulletData.Empty();
        ModifyMoney(startMoney);
    }
    void Update()
    {
        Debug.Log(modifiedBulletData.ImpactDamage);
    }
    public void DmgUpgrade()
    {
        if (GameManager.GlobalGameManager.CurrentPlayerData.playerMoney < dmgKost) { return; }
        ModifyMoney(-dmgKost);
        modifiedBulletData.ModifyDmg(modifeyUpgrade * (bunusUpgarde + (amountOfUpgrade * 0.001f)));
        amountOfUpgrade++;
        dmgText.text = ("Dmg: " + modifiedBulletData.ImpactDamage.ToString("f0"));
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
