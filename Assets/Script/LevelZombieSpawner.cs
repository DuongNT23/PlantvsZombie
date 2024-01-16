using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.Levels;
using Assets.Script.Levels.SpawnData;
using JetBrains.Annotations;
using UnityEngine;

public class LevelZombieSpawner : MonoBehaviour
{
    //TODO replace this with a method to set the level file
    public string PathToLevelFile = "";
    [CanBeNull] public LevelData LevelData = null;
    public Transform[] SpawnPoints;
    public GameObject Zombie;

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
        LevelData = GetLevelData(PathToLevelFile);
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
        StartNextWave();
    }

    void StartNextWave()
    {
        Debug.Log($"Attempting to start wave {_currentWave}");
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
        SpawnZombies(waveData.zombies);
        _waveTimer = WaveGracePeriod;
        _currentWave++;
        InvokeRepeating("CheckNextSpawn",WaveGracePeriod,1);
    }

    private void SpawnZombies(List<ZombieSpawnData> zombies)
    {
        //TODO Create spawning behavior
        foreach (var zombie in zombies)
        {
            int r = UnityEngine.Random.Range(0, SpawnPoints.Length);
            for (int i = 0; i < zombie.amount; i++)
            {
                GameObject myZombie = Instantiate(Zombie, SpawnPoints[r].position, Quaternion.identity);
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
