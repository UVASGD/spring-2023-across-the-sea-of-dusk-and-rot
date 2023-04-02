using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    Ray ray;
    RaycastHit hit;
    Vector3 initialPosition;
    Vector3 rotationEulerVectors;

    private bool followMouse = false;
    static GameObject selectedCard;

    public float lerpSpeed = 10.0f;
    public float lerpSpeedRotate = 0.1f;

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

    public void Initialize(){
        //add properties
    }
    // Update is called once per frame
    void Update()
    {
        if(!Input.GetMouseButton(0)){
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
            // transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * speed)
        }
        else{
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(initialPosition);
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed*Time.deltaTime);

        }
    } 
}
