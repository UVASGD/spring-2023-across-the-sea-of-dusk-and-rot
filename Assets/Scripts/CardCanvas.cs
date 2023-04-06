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
    public CardPage parentCardpage;

    public List<Sprite> cardTextures;
    private readonly Dictionary<CardType, int> textureMapping = new()
    {
        {CardType.attack, 0},
        {CardType.defense, 1},
        {CardType.recover, 2},
        {CardType.special, 3}
    };

    private void Awake()
    {
        parentCardpage = GetComponentInParent<CardPage>();
    }

    private void OnEnable()
    {

        PossessedCards = parentCardpage.PossessedCards;
        cardTextures = parentCardpage.cardTextures;

        foreach (Image CardImage in CardImages)
        {
            CardImage.gameObject.SetActive(false);
        }

        for (int i = 0; i < Mathf.Min(PossessedCards.Count - canvasIndex * SinglePageCardCount, SinglePageCardCount); i++)
        {
            Image image = CardImages[i];
            Card card = PossessedCards[i + canvasIndex * SinglePageCardCount];

            //set texture of the cardImage
            int textureIndex = textureMapping[card.cardBase.type];
            Sprite t = cardTextures[textureIndex];
            image.sprite = t;

            //add description for the card
            image.gameObject.SetActive(true);
            image.transform.Find("Description").GetComponent<TextMeshProUGUI>().SetText(card.Description());
        }
    }

}
