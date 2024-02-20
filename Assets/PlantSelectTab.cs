using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Assets.Script;
using UnityEngine;

public class PlantSelectTab : MonoBehaviour
{
    public List<PlantScriptSelect> Selects = new List<PlantScriptSelect>(9);

    [SerializeField] private GameObject[] slots;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AppendPlant(PlantScriptSelect select)
    {
        var package = Instantiate(select, this.transform);
        package.isAddMode = false;
        package.copy = select;
        select.copy = package;
        select.Disable();
        Selects.Add(package);
        Render();
    }

    public void RemovePlant(PlantScriptSelect select)
    {
        if (Selects.Remove(select))
        {
            select.copy.Enable();
            select.copy.copy = null;
            Destroy(select.gameObject);
            Render();
        }
        else
        {
            Debug.LogWarning("Error");
        }
    }

    private void Render()
    {
        int i = 0;
        foreach (var select in Selects)
        {
            select.transform.position = slots[i].transform.position;
            select.transform.SetParent(slots[i].transform);
            i++;
        }
    }
}
