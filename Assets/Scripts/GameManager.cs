using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class PlayerData
{
    [Header("Static Values")]
    public float playerMaxHealth = 100;
    [Header("Dynamic Values")]
    public float playerHealth = 100;
    public int playerMoney = 0;


    public void Reset()
    {
        playerHealth = playerMaxHealth;
        playerMoney = 0;
    }
}
public class GameManager : MonoBehaviour
{
    public static GameManager GlobalGameManager = null;

    [Header("Spawnable Objects")]
    public List<GameObject> SpawnableObjects = new List<GameObject>();
    public Dictionary<string, GameObject> SpawnableObjectsMap = new Dictionary<string, GameObject>();
    
    [Header("Enemies & Tracking")]
    public List<EnemyWaveContainer> EnemyWavesInLevel = new List<EnemyWaveContainer>();
    public int CurrentEnemyWave = 0;
    public Transform EnemyStartingPos = null;
    [Header("Static References")]
    public PlayerData CurrentPlayerData = null;
    public TextMeshProUGUI playerHealthText = null;
    public TextMeshProUGUI playerHealthText2 = null;
    public TextMeshProUGUI PlayerMoneyText = null;
    public TextMeshProUGUI WaveNumberText = null;
    [Header("Dynamic References")]
    public List<EnemyBase> AllEnemies = new List<EnemyBase>();

    SceneChanger sceneChanger;

    public Enemy EnemyData = new Enemy();
    private void Awake()
    {
        GlobalGameManager = this;
        foreach (GameObject spawnable in SpawnableObjects)
        {
            var spawnableGO = GameObject.Instantiate(spawnable, this.gameObject.transform);
            spawnableGO.SetActive(false);
            SpawnableObjectsMap.Add(spawnable.name, spawnableGO);
        }
        CurrentPlayerData.Reset();
    }
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        sceneChanger = FindObjectOfType<SceneChanger>();
    }
    private void Update()
    {
        PlayerMoneyText.text = CurrentPlayerData.playerMoney.ToString() + "$";

        if (CurrentPlayerData.playerHealth <= 0)
        {
            sceneChanger.ChangeScene(2);
        }
    }
    public void OnSpawnNextWave()
    {
        DoSpawnWave();
    }
    private void DoSpawnWave()   
    {
        foreach(string wavePart in EnemyWavesInLevel[CurrentEnemyWave].WaveKey)
        {
           var enemyWave = SpawnObject(wavePart,EnemyStartingPos.position);
            enemyWave.GetComponent<EnemyWave>().MovementDirection = this.transform.position - EnemyStartingPos.position;
        }
        CurrentEnemyWave++;
        if (CurrentEnemyWave >= EnemyWavesInLevel.Count)
        { CurrentEnemyWave = 0; }
        WaveNumberText.text = $"Wave {CurrentEnemyWave}/{EnemyWavesInLevel.Count}";
    }
    public void ModifyHealthForEnemies(int healthModifier)
    {
        foreach (var enemy in AllEnemies)
        {
            enemy.EnemyData.Health += healthModifier;
        }
    }
    public GameObject SpawnObject(string key, Vector3 aPostion = new Vector3())
    {
        if (key.Length < 1)
        {
            return null;
        }
        if (SpawnableObjectsMap.ContainsKey(key))
        {
            var GO = Instantiate(SpawnableObjectsMap[key]);
            GO.SetActive(true);
            GO.transform.position = aPostion;
            return GO;
            
        }
        return null;
    }
    public void ModifyHealth(float healthToAdd)
    {
        CurrentPlayerData.playerHealth += healthToAdd;
    }
    private void OnTriggerEnter(Collider other)
    {
        var enemyCollider = other.gameObject.GetComponent<EnemyBase>();
        if (enemyCollider != null)
        {
            CurrentPlayerData.playerHealth -= enemyCollider.EnemyData.Damage;
            if(playerHealthText != null)
            {
                PlayerHealthText();
            }
            GameObject.Destroy(enemyCollider.gameObject);
        }
    }
    public void PlayerHealthText()
    {
        playerHealthText.text = ("Health: " + GameManager.GlobalGameManager.CurrentPlayerData.playerHealth.ToString("f0") + "hp");
        playerHealthText2.text = ("Health: " + GameManager.GlobalGameManager.CurrentPlayerData.playerHealth.ToString("f0"));
    }
}
