using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] int dmgKost = 5;
    public static Bullet modifiedBulletData = new Bullet();
    float modifeyUpgrade = 1.001f;
    int amountOfUpgrade;
    float bunusUpgarde = 1.0f;
    void Start()
    {
        //modifiedBulletData.Empty();
    }

    void Update()
    {
        
    }

    void DmgUpgrade()
    {
        if (GameManager.GlobalGameManager.CurrentPlayerData.playerMoney < dmgKost) { return; }
        modifiedBulletData.ImpactDamage += modifeyUpgrade * (bunusUpgarde + (amountOfUpgrade * 0.001f));
        amountOfUpgrade++;
    }
}
