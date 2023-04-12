using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public RawImage artworkimage;
    public TextMeshProUGUI effectText;
    private Renderer renderer;
    private Transform transform;
    private Vector3 scalechange;
    private Color ogColor;
    // Start is called before the first frame update
    void Start()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;
        effectText.text = "This card does "+card.getEffect(card.type)+" "+card.type;
        artworkimage.texture = card.artwork;
        renderer = GetComponent<Renderer>();
        transform = GetComponent<Transform>();
        scalechange = new Vector3(0.2f,0.2f,0.0f);
        ogColor = renderer.material.color;
    }

    private void OnMouseEnter() {
        renderer.material.color = Color.yellow;
        transform.localScale += scalechange;
    }
    private void OnMouseExit() {
        renderer.material.color = ogColor;
        transform.localScale -= scalechange;
    }
}
