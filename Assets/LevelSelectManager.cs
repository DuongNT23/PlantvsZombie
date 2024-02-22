using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] private GameObject levelList;

    private Color PassedColor = new Color(0.35f, 0.8f, 0.5f);

    [SerializeField] private int levelPassed;
    // Start is called before the first frame update
    void Start()
    {
        var children = levelList.GetComponentsInChildren<LevelSelectButton>();
        int i = 0;
        foreach (var child in children)
        {
            var button = child.GetComponent<Button>();
            button.interactable = true;
            if (i < levelPassed)
            {
                child.GetComponent<Image>().color = PassedColor;
                i++;
            }
            else
            {
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
