using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Assets.Script;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlantSelectManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private PlantSelectTab tab;
    [SerializeField] private int plantLimit = 6;

    // Start is called before the first frame update
    void Start()
    {
        tab.plantLimit = plantLimit;
        startButton.onClick.AddListener(StartLevel);
        DenyBegin();
    }

    public void AllowBegin()
    {
        startButton.interactable = true;
    }

    public void DenyBegin()
    {
        startButton.interactable = false;
    }

    private void StartLevel()
    {
        var list = new List<GameObject>();
        foreach (var select in tab.Selects)
        {
            list.Add(select.seedPackage);
        }
        PlantSelectDataHandler.Instance.PlantSelections = list.ToArray();
        SceneManager.LoadScene("NewSpawnerTestScene");
    }
}
