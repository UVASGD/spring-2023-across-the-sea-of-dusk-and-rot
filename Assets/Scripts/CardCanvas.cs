using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardCanvas : MonoBehaviour
{
    public int canvasIndex = 0;
    public List<Image> CardImages;
    public List<Card> PossessedCards;
    public int SinglePageCardCount = 6;

    private void OnEnable()
    {
        PossessedCards = GetComponentInParent<CardPage>().PossessedCards;


        foreach (Image CardImage in CardImages)
        {
            CardImage.gameObject.SetActive(false);
        }

        for (int i = 0; i < Mathf.Min(PossessedCards.Count - canvasIndex * SinglePageCardCount, SinglePageCardCount); i++)
        {
            Image image = CardImages[i];
            Card card = PossessedCards[i + canvasIndex * SinglePageCardCount];
            image.gameObject.SetActive(true);
            image.transform.Find("Description").GetComponent<TextMeshProUGUI>().SetText(card.Description());
        }
    }

}
