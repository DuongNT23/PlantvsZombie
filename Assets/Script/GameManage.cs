using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Assets.Script.Levels.SpawnData;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManage : MonoBehaviour
{
    public GameObject currentPlant;
    public Sprite currentPlantSprite;
    public Transform tiles;
    public LayerMask tileMask;
    public int suns;
    public TextMeshProUGUI sunText;
    public LayerMask sunMask;
    public bool isUsingShovel = false;

    private int plantCost = 0;
    private PlantScript plantScript;

    [SerializeField] private AudioClip _sunCollectClip;
    [SerializeField] private AudioClip _levelMusic;
    [SerializeField] private SpriteRenderer backgroundRenderer;

    //This is for dynamically load plant in
    //TODO Load plants dynamically in from select screen
    [SerializeField] private GameObject[] plantSlots;
    [SerializeField] private GameObject[] plantSelected;
    public void BuyPlant(GameObject plant, Sprite sprite, int cost, PlantScript plantScript)
    {
        isUsingShovel = false;
        currentPlant = plant;
        currentPlantSprite = sprite;
        plantCost = cost;
        this.plantScript = plantScript;
    }

    private void Start()
    {
        LevelData levelData = LevelDataManager.Instance.GetLevelData();
        suns = levelData.startingSun;
        //Background
        if (levelData.background is { Length: > 0 })
        {
            var sprite = Resources.Load<Sprite>(levelData.background);
            if (sprite == null)
            {
                Debug.LogError($"Failed to load background. ({levelData.background})");
            }
            else
            {
                backgroundRenderer.sprite = sprite;
            }
            
        }
        
        var selections = PlantSelectDataHandler.Instance.PlantSelections;
        if (selections == null || selections.Length == 0)
        {
            Debug.Log("Empty, loading debug plants.");
        }
        else
        {
            plantSelected = selections;
        }
        
        Debug.Log($"Selected {plantSelected.Length} plants.");
        int i = 0;
        foreach (var plant in plantSelected)
        {
            if (i < plantSlots.Length)
            {
                var slot = plantSlots[i];
                Instantiate(plant, slot.transform);
                Debug.Log("Instantiated");
                i++;
            }
            else
            {
                break;
            }
        }
        SoundManager.Instance.PlayMusic(_levelMusic);
    }

    private void Update()
    {
        sunText.text = suns.ToString();

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileMask);
    
        foreach(Transform t in tiles)
        {
            t.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            CancelBuyPlant();
            isUsingShovel = false;
            return;
        }

        if (hit.collider )
        {
            var tile = hit.collider.GetComponent<Tile>();
            if (isUsingShovel && tile.plant != null && Input.GetMouseButtonDown(0))
            {
                Destroy(tile.plant);
                isUsingShovel = false;
            }
            else if (currentPlant && tile.plant == null)
            {
                hit.collider.GetComponent<SpriteRenderer>().sprite = currentPlantSprite;
                hit.collider.GetComponent<SpriteRenderer>().enabled = true;

                if (Input.GetMouseButtonDown(0))
                {
                    var plant = Instantiate(currentPlant, tile.transform.position, Quaternion.identity);
                    tile.plant = plant;
                    this.suns -= plantCost;
                    plantScript.Bought();
                    CancelBuyPlant();
                }
            }
        }


        RaycastHit2D sunHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, sunMask);

        if (sunHit.collider)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                suns += 25;
                Destroy(sunHit.collider.gameObject);
                SoundManager.Instance.PlaySound(_sunCollectClip);
            }
        }

    }

    private void CancelBuyPlant()
    {
        plantCost = 0;
        currentPlant = null;
        currentPlantSprite = null;
    }

    public void GetShovel()
    {
        CancelBuyPlant();
        isUsingShovel = true;
    }
}
