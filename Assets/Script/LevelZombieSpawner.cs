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
    public string pathToLevelFile = "";
    [CanBeNull] public LevelData levelData = null;
    public Transform[] spawnPoints;
    public GameObject Zombie;

    /// <summary>
    /// How long until the first wave starts
    /// </summary>
    public int gracePeriod = 20;
    /// <summary>
    /// How long since the previous wave until the game starts to check whether to begin the next wave.
    /// </summary>
    public int waveGracePeriod = 10;
    /// <summary>
    /// Maximum time a wave can last before the next wave spawn.
    /// </summary>
    public int maximumWavePeriod = 30;
    /// <summary>
    /// Tracks what waves this is
    /// </summary>
    private int currentWave = 0;
    /// <summary>
    /// Tracks how long this wave has been going.
    /// </summary>
    private int waveTimer = 0;

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
        levelData = GetLevelData(pathToLevelFile);
        Debug.Log($"Wave Count: {levelData.waves.Count}");
        Invoke("BeginSpawning",gracePeriod);
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
        Debug.Log($"Attempting to start wave {currentWave}");
        if (currentWave == levelData.waves.Count)
        {
            Win();
            return;
        }
        var waveData = levelData.waves[currentWave];
        if (waveData.isLargeWave)
        {
            //TODO large wave stuffs
        }
        SpawnZombies(waveData.zombies);
        waveTimer = waveGracePeriod;
        currentWave++;
        InvokeRepeating("CheckNextSpawn",waveGracePeriod,1);
    }

    private void SpawnZombies(List<ZombieSpawnData> zombies)
    {
        //TODO Create spawning behavior
        foreach (var zombie in zombies)
        {
            int r = UnityEngine.Random.Range(0, spawnPoints.Length);
            for (int i = 0; i < zombie.amount; i++)
            {
                GameObject myZombie = Instantiate(Zombie, spawnPoints[r].position, Quaternion.identity);
            }
        }
        
    }

    private void CheckNextSpawn()
    {
        if (waveTimer >= maximumWavePeriod)
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

        waveTimer++;
    }


    void Win()
    {
        Debug.Log("You win!");
    }
}
