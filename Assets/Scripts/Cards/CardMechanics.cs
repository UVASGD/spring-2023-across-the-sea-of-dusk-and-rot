using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardMechanics : MonoBehaviour
{
    public CardData card;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public RawImage artworkimage;
    public TextMeshProUGUI effectText;
    private Renderer renderer;
    private Transform cardTransform;
    private Vector3 scalechange;
    private Color ogColor;
    private MeshRenderer meshRenderer;

    //Rot Textures
    public Material rot1;
    public Material rot2;
    public Material rot3;
    public Material rot4;
    public Material rot5;

    Ray ray;
    RaycastHit hit;
    Vector3 initialPosition;
    Vector3 rotation = new Vector3(0, 0, 0);

    private bool followMouse = false;
    private static GameObject selectedCard;
    private static bool removeFromHand = false;

    public float lerpSpeed = 10.0f;
    public float lerpSpeedRotate = 5.0f;



    // Start is called before the first frame update
    void Start()
    {
        initialPosition = Camera.main.WorldToScreenPoint(transform.position);
        nameText.text = card.name;
        descriptionText.text = card.description;
        effectText.text = "This card does "+card.getEffect(card.type)+" "+card.type;
        artworkimage.texture = card.artwork;
        renderer = GetComponent<Renderer>();
        cardTransform = GetComponent<Transform>();
        scalechange = new Vector3(0.2f,0.2f,0.0f);
        ogColor = renderer.material.color;
        meshRenderer = GetComponent<MeshRenderer>();
        card.setRotLevel(1);
    }

    public void SetInitialPosition(Vector3 newPosition){
        initialPosition = Camera.main.WorldToScreenPoint(newPosition);
    }
    public Vector3 getCardDimensions(){
        return GetComponentInChildren<BoxCollider>().bounds.size;
    }
    public void SetRotation(Vector3 newRotation){
        rotation = newRotation;
    }
    public void Initialize(){
        //add properties
    }
    public void PlaySelectedCard(){
        PlayCard();
        Destroy(selectedCard);
    }
    private void PlayCard(){
        //play card
    }
    // Update is called once per frame
    void Update()
    {
        effectText.text = "This card does "+card.getEffect(card.type)+" "+card.type;

        if(!Input.GetMouseButton(0)){
            //temp play logic
            float mousePos = Input.mousePosition.y;
            // print("mouse pos: " + mousePos);
            if(mousePos > 400 && selectedCard != null){
                removeFromHand = true;
            }
            if(removeFromHand){
                this.GetComponentInParent<Hand>().RemoveCard(selectedCard);
                PlaySelectedCard();
                removeFromHand = false;
            }
            
            selectedCard = null;
            followMouse = false;
        }
        else{
            if(!followMouse){
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit))
                {
                    print("------");
                    print(hit.transform.parent.gameObject);
                    print(this.gameObject);
                    print("------");

                    // print(hit.collider.name);
                    print("trying to access hit card");
                    print(hit.transform.parent.gameObject);
                    if(hit.transform.gameObject == this.gameObject && selectedCard == null){
                        print("selecting card, got gameobject");
                        selectedCard = this.gameObject;
                    }
                    followMouse = true;
                }
            }
        }

        if(followMouse &&  selectedCard == this.gameObject){
            print("trying to follow");
            float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed*Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 180, 0)), lerpSpeedRotate*Time.deltaTime);
        }
        else{
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(initialPosition);
            cardTransform.position = Vector3.Lerp(cardTransform.position, targetPosition, lerpSpeed*Time.deltaTime);
            cardTransform.rotation = Quaternion.Lerp(cardTransform.rotation, Quaternion.Euler(rotation), lerpSpeedRotate*Time.deltaTime);

        }
    } 
    private void OnMouseEnter() {
        renderer.material.color = Color.yellow;
        cardTransform.localScale += scalechange;
    }
    private void OnMouseExit() {
        renderer.material.color = ogColor;
        cardTransform.localScale -= scalechange;
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
