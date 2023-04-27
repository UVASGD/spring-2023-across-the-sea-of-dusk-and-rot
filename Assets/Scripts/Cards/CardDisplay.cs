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
    private MeshRenderer meshRenderer;

    //Rot Textures
    public Material rot1;
    public Material rot2;
    public Material rot3;
    public Material rot4;
    public Material rot5;

    
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
        meshRenderer = GetComponent<MeshRenderer>();
        card.setRotLevel(1);
    }

    private void Update() {
        effectText.text = "This card does "+card.getEffect(card.type)+" "+card.type;
    }

    private void OnMouseEnter() {
        renderer.material.color = Color.yellow;
        transform.localScale += scalechange;
    }
    private void OnMouseExit() {
        renderer.material.color = ogColor;
        transform.localScale -= scalechange;
    }

    public void updateRotTexture(){
        int rot = (int)card.getRotLevel();
        switch(rot){
            case 2:
                meshRenderer.material = rot2;
                break;
            case 3:
                meshRenderer.material = rot3;
                break;
            case 4:
                meshRenderer.material = rot4;
                break;
            case 5:
                meshRenderer.material = rot5;
                break;
            default:
                meshRenderer.material = rot1;
                break;
            
        }
    }
}
