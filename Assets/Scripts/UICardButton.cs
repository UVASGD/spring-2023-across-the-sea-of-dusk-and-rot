using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICardButton : MonoBehaviour
{
    public bool selected;
    // default card color
    public Color defaultcolor = new Color32(0, 0, 0, 0);
    //card color after being selected
    public Color colorOnSelected = new Color32(54, 224, 92, 112);

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
            this.GetComponent<Image>().color = defaultcolor;
        }
        else
        {
            selected = true;
            this.GetComponent<Image>().color = colorOnSelected;
        }
    }
}
