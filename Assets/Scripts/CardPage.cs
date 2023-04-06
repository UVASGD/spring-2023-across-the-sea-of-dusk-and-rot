using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardPage : MonoBehaviour
{
    public List<Canvas> canvases;
    public int curIndex;
    public Transform leftButton;
    public Transform rightButton;
    public List<Card> PossessedCards;
    public int SinglePageCardCount = 6;

    public List<Sprite> cardTextures;


    public void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Sprite t = Resources.Load<Sprite>("UICards/card" + (i + 1).ToString());
            cardTextures.Add(t);
        }
        curIndex = 0;
        DisplayCanvas(curIndex);
    }

    public void OnLeftButtonDown()
    {
        curIndex--;
        DisplayCanvas(curIndex);
    }


    public void OnRightButtonDown()
    {
        curIndex++;
        DisplayCanvas(curIndex);
    }


    public void DisplayCanvas(int index)
    {
        for (int i = 0; i < canvases.Count; i++)
        {
            canvases[i].gameObject.SetActive(false);
        }
        Canvas curCanvas = canvases[index];
        curCanvas.gameObject.SetActive(true);
        leftButton = curCanvas.transform.Find("LeftButton");
        rightButton = curCanvas.transform.Find("RightButton");
        int maxIndex = (int)Mathf.Ceil((float)PossessedCards.Count / SinglePageCardCount) - 1;


        if (index == maxIndex)
        {
            rightButton.gameObject.SetActive(false);
        }
        else
        {
            rightButton.gameObject.SetActive(true);
        }

        if (index == 0)
        {
            leftButton.gameObject.SetActive(false);
        }
        else
        {
            leftButton.gameObject.SetActive(true);
        }
    }

}
