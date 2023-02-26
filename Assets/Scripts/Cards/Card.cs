using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    Ray ray;
    RaycastHit hit;
    Vector3 initialPosition;

    bool followMouse = false;

    public float lerpSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = Camera.main.WorldToScreenPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetMouseButton(0)){
            followMouse = false;
        }
        else{
            if(!followMouse){
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit))
                {
                    print(hit.collider.name);
                    followMouse = true;
                }
            }
        }

        if(followMouse){
            float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed*Time.deltaTime);
        }
        else{
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(initialPosition);
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed*Time.deltaTime);

        }
    } 
}
