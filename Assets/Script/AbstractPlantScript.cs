using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class AbstractPlantScript : MonoBehaviour
{
    protected Image[] images;
    protected bool isEnabled = true;
    public virtual void Start()
    {
        images = gameObject.GetComponentsInChildren<Image>();
    }

    public void Enable()
    {
        isEnabled = true;
        foreach (var image in images)
        {
            image.color = Color.white;
        }
    }

    public void Disable()
    {
        isEnabled = false;
        foreach (var image in images)
        {
            image.color = Color.gray;
        }
    }
}
