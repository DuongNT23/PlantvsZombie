using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private string levelPath;
    [SerializeField] private string levelName;
    [SerializeField] private TextMeshProUGUI levelNameText;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartLevel);
    }

    private void StartLevel()
    {
        LevelDataManager.Instance.SetLevelPath(levelPath);
        SceneManager.LoadScene("PlantSelect");
    }

    private void OnValidate()
    {
        if (levelNameText != null)
        {
            levelNameText.text = levelName;
        }
    }
}
