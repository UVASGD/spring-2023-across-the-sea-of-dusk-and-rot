using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

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
        if(!Input.GetMouseButton(0)){
            //temp play logic
            float mousePos = Input.mousePosition.y;
            print("mouse pos: " + mousePos);
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
                    // print(hit.transform.parent.gameObject);
                    // print(this.gameObject);
                    // print("------");

                    // print(hit.collider.name);
                    if(hit.transform.parent.gameObject == this.gameObject && selectedCard == null){
                        print("selecting card, got gameobject");
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
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed*Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), lerpSpeedRotate*Time.deltaTime);

        }
    } 
}
