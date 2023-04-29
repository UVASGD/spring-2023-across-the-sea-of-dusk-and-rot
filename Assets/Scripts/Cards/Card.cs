using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public CardData card;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public RawImage artworkimage;
    public TextMeshProUGUI effectText;
    private Transform cardTransform;
    private Vector3 scalechange;
    private Color ogColor;
    private Renderer meshRenderer;

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

    private GameManager gm;
    private bool isTouchActiveForCard = true;
    public GameObject bag;
    // private GameObject hand;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = Camera.main.WorldToScreenPoint(transform.position);
        nameText.text = card.name;
        descriptionText.text = card.description;
        effectText.text = "This card does "+card.getEffect(card.type)+" "+card.type;
        artworkimage.texture = card.artwork;
        cardTransform = GetComponent<Transform>();
        scalechange = new Vector3(0.2f,0.2f,0.0f);
        ogColor = GetComponent<Renderer>().material.color;
        meshRenderer = cardTransform.GetChild(0).GetComponent<Renderer>();
        card.setRotLevel(1);
        gm = FindObjectOfType<GameManager>();
        bag = GameObject.Find("Bag");
        print("bag: " + bag);
    }

    public void SetInitialPosition(Vector3 newPosition){
        initialPosition = Camera.main.WorldToScreenPoint(newPosition);
    }
    public Vector3 getCardDimensions(){
        GameObject child = transform.GetChild(0).gameObject;
        return child.GetComponent<BoxCollider>().bounds.size;
    }
    public void SetRotation(Vector3 newRotation){
        rotation = newRotation;
    }
    
    public void PlaySelectedCard(){
        PlayCard();
        transform.parent.gameObject.GetComponent<Hand>().RemoveCard(selectedCard);
    public bool GetTouchStatus(){
        return isTouchActiveForCard;
    }
    public void SetTouchStatus(bool newTouchStatus){
        isTouchActiveForCard = newTouchStatus;
    }
    public void Initialize(){
        //add properties
    }
    public void PlaySelectedCard(){
        PlayCard();
        print("bag: " + bag);
        print(bag.GetComponent<Bag>() != null);
        print(selectedCard);
        bag.GetComponent<Bag>().AddCard(selectedCard);
        selectedCard.transform.SetParent(bag.transform);
        selectedCard.GetComponent<Card>().SetTouchStatus(false);
    }
    private void PlayCard(){
        //play card
        print("Sending Card Data");
        switch (card.type)
        {
            case Type.DEFENSE:
                print("Defense "+card.type);
                gm.Defend(card.getEffect(card.type));
                break;
            case Type.HEAL:
                print("Heal "+card.type);
                gm.Heal(card.getEffect(card.type));
                break;
            default:
                print("Attack "+card.type);
                gm.Attack(card.getEffect(card.type));
                break;
        }
        
        card.setRotLevel(card.getRotLevel()+1);
        updateRotTexture();
        
    }
    // Update is called once per frame
    void Update()
    {
        //print("GameManager: "+gm.gameObject);
        effectText.text = "This card does "+card.getEffect(card.type)+" "+card.type;

        if(!Input.GetMouseButton(0)){
            //temp play logic
            float mousePos = Input.mousePosition.y;
            if(mousePos > 400 && selectedCard != null){
                removeFromHand = true;
            }
            if(removeFromHand){
                
                PlaySelectedCard();
                // this.GetComponentInParent<Hand>().RemoveCard(selectedCard);

                int width = Screen.width;
                int height = Screen.height;

                Vector3 bottomRightScreenCorner = Camera.main.ScreenToWorldPoint(new Vector3(width/2, -height/2, 0));

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
                    // print(hit.transform.parent.gameObject);
                    // print(this.gameObject);
                    // print("------");

                    // print(hit.collider.name);
                    // print(isTouchActiveForCard);
                    if(hit.transform.parent.gameObject == this.gameObject && 
                        selectedCard == null &&
                        isTouchActiveForCard == true){
                        selectedCard = this.gameObject;
                        print("selecting card, got gameobject: " + selectedCard);

                    }
                    followMouse = true;
                }
            }
        }

        if(followMouse &&  selectedCard == this.gameObject){
            float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed*Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), lerpSpeedRotate*Time.deltaTime);
        }
        else{
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(initialPosition);
            cardTransform.position = Vector3.Lerp(cardTransform.position, targetPosition, lerpSpeed*Time.deltaTime);
            cardTransform.rotation = Quaternion.Lerp(cardTransform.rotation, Quaternion.Euler(rotation), lerpSpeedRotate*Time.deltaTime);

        }
    } 

    public void updateRotTexture(){
        int rot = (int)card.getRotLevel();
        switch(rot){
            case 2:
                print("Texture 2");
                meshRenderer.material = rot2;
                print(meshRenderer.material);
                break;
            case 3:
                print("Texture 3");
                meshRenderer.material = rot3;
                print(meshRenderer.material);
                break;
            case 4:
                print("Texture 4");
                meshRenderer.material = rot4;
                print(meshRenderer.material);
                break;
            case 5:
                print("Texture 5");
                meshRenderer.material = rot5;
                print(meshRenderer.material);
                break;
            default:
                meshRenderer.material = rot1;
                break;
            
        }
    }
}
