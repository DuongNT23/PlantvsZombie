using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Levels;
using Assets.Script.Levels.SpawnData;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelZombieSpawner : MonoBehaviour
{
    [CanBeNull] public LevelData LevelData = null;
    public Transform[] SpawnPoints;
    public GameObject Zombie;

    [SerializeField] private AudioClip _firstWaveClip;
    [SerializeField] private List<ZombieType> _zombieTypes;

    private Dictionary<string, int> _zombieIndexes = new Dictionary<string, int>()
    {
        {"normal",0},
        {"conehead",1},
        {"buckethead",2}
    };

    /// <summary>
    /// How long until the first wave starts
    /// </summary>
    public int GracePeriod = 20;
    /// <summary>
    /// How long since the previous wave until the game starts to check whether to begin the next wave.
    /// </summary>
    public int WaveGracePeriod = 10;
    /// <summary>
    /// Maximum time a wave can last before the next wave spawn.
    /// </summary>
    public int MaximumWavePeriod = 30;
    /// <summary>
    /// Tracks what waves this is
    /// </summary>
    private int _currentWave = 0;
    /// <summary>
    /// Tracks how long this wave has been going.
    /// </summary>
    private int _waveTimer = 0;

    private LevelData GetLevelData(string levelJsonPath)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(levelJsonPath);
        if (jsonFile == null)
        {
            Debug.LogError("Failed to load JSON file.");
            return null;
        }
        string jsonString = jsonFile.text;
        LevelData readLevelData = JsonUtility.FromJson<LevelData>(jsonString);
        if (readLevelData == null)
        {
            Debug.LogError("levelData has been read but it's null");
        }
        return readLevelData;
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelData = LevelDataManager.Instance.GetLevelData();
        Debug.Log($"Wave Count: {LevelData.waves.Count}");
        Invoke("BeginSpawning",GracePeriod);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BeginSpawning()
    {
        Debug.Log("The zombies are coming.");
        SoundManager.Instance.PlaySound(_firstWaveClip);
        StartNextWave();
    }

    void StartNextWave()
    {
        Debug.Log($"Attempting to start wave {_currentWave}");
        Debug.Log($"Wave count = {LevelData.waves.Count}");
        if (_currentWave == LevelData.waves.Count)
        {
            Win();
            return;
        }
        var waveData = LevelData.waves[_currentWave];
        if (waveData.isLargeWave)
        {
            //TODO large wave stuffs
        }
        SpawnZombies(waveData);
        _waveTimer = WaveGracePeriod;
        _currentWave++;
        InvokeRepeating("CheckNextSpawn",WaveGracePeriod,1);
    }

    private void SpawnZombies(WaveSpawnData waveSpawnData)
    {
        var zombies = waveSpawnData.zombies;
        if (waveSpawnData.spreadEvenly)
        {
            SpawnZombiesEvenly(zombies);
        }
        else
        {
            SpawnZombiesAnywhere(zombies);
        }
        //foreach (var zombie in zombies)
        //{
        //    int r = UnityEngine.Random.Range(0, SpawnPoints.Length);
        //    for (int i = 0; i < zombie.amount; i++)
        //    {
        //        GameObject myZombie = Instantiate(Zombie, SpawnPoints[r].position, Quaternion.identity);
        //    }
        //}
        
    }

    private void SpawnZombiesEvenly(List<ZombieSpawnData> zombies)
    {
        List<int> spawnPositions = new List<int>() { 0, 1, 2, 3, 4 };
        foreach (var zombie in zombies)
        {
            for (int i = 0; i < zombie.amount; i++)
            {
                if (spawnPositions.Count == 0)
                {
                    spawnPositions = new List<int>() { 0, 1, 2, 3, 4 };
                }
                var r = Random.Range(0, spawnPositions.Count);
                var randomSpawnPos = spawnPositions[r];
                var spawnPosition = GetSpawnPosition(SpawnPoints[randomSpawnPos].position);
                spawnPositions.RemoveAt(r);
                ZombieFactory.Instance.InstantiateFromType(spawnPosition, zombie.type);
                //if (TryGetType(zombie.type,out var type))
                //{
                //    //myZombie.GetComponent<Zombie>().type = type;
                //}
            }
        }
    }

    private Vector3 GetSpawnPosition(Vector3 spawnPoint)
    {
        var spawnPosition = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z);
        spawnPosition.x += Random.Range(-1, 1);
        return spawnPosition;
    } 

    private bool TryGetType(string typeString, out ZombieType type)
    {
        if (_zombieIndexes.TryGetValue(typeString, out var index))
        {
            type = _zombieTypes[index];
            return true;
        }
        type = null;
        return false;
    }

    private void SpawnZombiesAnywhere(List<ZombieSpawnData> zombies)
    {
        foreach (var zombie in zombies)
        {
            for (int i = 0; i < zombie.amount; i++)
            {
                var r = Random.Range(0, SpawnPoints.Length);
                var spawnPosition = GetSpawnPosition(SpawnPoints[r].position);
                ZombieFactory.Instance.InstantiateFromType(spawnPosition, zombie.type);
                //if (TryGetType(zombie.type, out var type))
                //{
                //    //myZombie.GetComponent<Zombie>().type = type;
                //}
            }
        }
    }

    private void CheckNextSpawn()
    {
        if (_waveTimer >= MaximumWavePeriod)
        {
            CancelInvoke(nameof(CheckNextSpawn));
            StartNextWave();
            Debug.Log("Timeout, began next wave.");
            return;
        }
        //Check if there are any zombies remaining on screen
        var remainingZombies = GameObject.FindObjectsOfType(typeof(Zombie)).Length;
        if (remainingZombies == 0)
        {
            CancelInvoke(nameof(CheckNextSpawn));
            StartNextWave();
            Debug.Log($"No Zombies Remaining, begin next wave.");
            return;
        }

        _waveTimer++;
    }


    void Win()
    {
        Debug.Log("You win!");
    }
}
