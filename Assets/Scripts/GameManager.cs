using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class PlayerData
{
    [Header("Static Values")]
    public int playerMaxHealth = 10;
    [Header("Dynamic Values")]
    public int playerHealth = 10;
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
    public TextMeshProUGUI PlayerHealthText = null;
    public TextMeshProUGUI PlayerMoneyText = null;
    public TextMeshProUGUI WaveNumberText = null;
    [Header("Dynamic References")]
    public List<EnemyBase> AllEnemies = new List<EnemyBase>();


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
        DontDestroyOnLoad(this.gameObject);

    }
    private void Update()
    {
        PlayerMoneyText.text = CurrentPlayerData.playerMoney.ToString() + "$";
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
    private void OnTriggerEnter(Collider other)
    {
        var enemyCollider = other.gameObject.GetComponent<EnemyBase>();
        if (enemyCollider != null)
        {
            CurrentPlayerData.playerHealth -= enemyCollider.EnemyData.Damage;
            if(PlayerHealthText != null)
            {
                PlayerHealthText.text = CurrentPlayerData.playerHealth.ToString() + " HP";
            }
            GameObject.Destroy(enemyCollider.gameObject);
        }
    }
}
