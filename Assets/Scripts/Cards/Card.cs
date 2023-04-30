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
        artworkimage.texture = card.artwork;
        cardTransform = GetComponent<Transform>();
        scalechange = new Vector3(0.2f,0.2f,0.0f);
        ogColor = GetComponent<Renderer>().material.color;
        meshRenderer = cardTransform.GetChild(0).GetComponent<Renderer>();
        gm = FindObjectOfType<GameManager>();
        bag = GameObject.Find("Bag");
        card.setRotLevel(1);
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
    public bool GetTouchStatus(){
        return isTouchActiveForCard;
    }
    public void SetTouchStatus(bool newTouchStatus){
        isTouchActiveForCard = newTouchStatus;
    }
    public void SetMaterial(Material m){
        meshRenderer.material = m;
    }
    public CardData GetCardData(){
        return transform.GetComponent<Card>().card;
    }
    public void Initialize(){
        //add properties
    }
    public void PlaySelectedCard(){
        PlayCard();
        print(bag.GetComponent<Bag>() != null);
        print(selectedCard);
        bag.GetComponent<Bag>().AddCard(selectedCard);
        selectedCard.transform.SetParent(bag.transform);
        selectedCard.GetComponent<Card>().SetTouchStatus(false);
    }
    private void PlayCard(){
        //play card
        //print("Sending Card Data");
        CardData data = selectedCard.GetComponent<Card>().card;
        switch (data.type)
        {
            case Type.DEFENSE:
                gm.Defend((int)data.getEffect(data.type), selectedCard.GetComponent<Card>());
                break;
            case Type.HEAL:
                gm.Heal((int)data.getEffect(data.type), selectedCard.GetComponent<Card>());
                break;
            case Type.CLEAN:
                gm.CleanNextCard((int)data.getEffect(data.type));
                break;
            case Type.ENHANCE:
                gm.EnhanceNextCard(data.getEffect(data.type),selectedCard.GetComponent<Card>());
                break;
            default:
                gm.Attack((int)data.getEffect(data.type), selectedCard.GetComponent<Card>());
                break;
        }
        data.setRotLevel((int)data.getRotLevel()+1);
        updateRotTexture();
        
    }
    // Update is called once per frame
    void Update()
    {
        if(card.type == Type.CLEAN){
            effectText.text = "Cleans "+card.getEffect(card.type)+" Off Next Played Card";
        }
        else if(card.type == Type.ENHANCE){
            effectText.text = "Modefies Next Card By "+card.getEffect(card.type)+"X";
        }
        else{
            effectText.text = "This card does "+card.getEffect(card.type)+" "+card.type;
        }
        if(!Input.GetMouseButton(0)){
            //temp play logic
            float mousePos = Input.mousePosition.y;
            if(mousePos > 400 && selectedCard != null){
                removeFromHand = true;
            }
            if(removeFromHand){
                
                PlaySelectedCard();

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
                    if(hit.transform.parent.gameObject == this.gameObject && 
                        selectedCard == null &&
                        isTouchActiveForCard == true){
                        selectedCard = this.gameObject;

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
        Card currentCard = selectedCard.GetComponent<Card>();
        int rot = (int)currentCard.GetCardData().getRotLevel();
        switch(rot){
            case 2:
                currentCard.SetMaterial(rot2);
                break;
            case 3:
                currentCard.SetMaterial(rot3);
                break;
            case 4:
                currentCard.SetMaterial(rot4);
                break;
            case 5:
                currentCard.SetMaterial(rot5);
                break;
            default:
                currentCard.SetMaterial(rot1);
                break;
            
        }
    }
}
