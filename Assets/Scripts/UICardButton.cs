using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICardButton : MonoBehaviour
{
    public bool selected;

    // Start is called before the first frame update
    void Start()
    {
        selected = false;
        this.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
    }

    public void Onclick()
    {
        if (selected)
        {
            selected = false;
            this.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        }
        else
        {
            selected = true;
            this.GetComponent<Image>().color = new Color32(54, 224, 92, 112);
        }
    }
}
