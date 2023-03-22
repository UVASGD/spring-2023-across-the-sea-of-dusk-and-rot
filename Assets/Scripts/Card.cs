using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardBase card;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text effectText;
    public RotState state;
    public Image artworkImage;
    public CardType type;

    private void Start() {
        nameText.text = card.name;
        descriptionText.text = card.description;
        artworkImage.sprite = card.image;
        effectText.text = card.getEffect(type);
        state = card.state;
    }
}
