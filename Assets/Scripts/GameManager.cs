using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera camera;
    private Card card;
    private Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out RaycastHit hitInfo)){
                if(hitInfo.collider.gameObject.tag=="Card"){
                    card = hitInfo.collider.gameObject.GetComponent<CardDisplay>().card;
                    Debug.Log("Selected "+card.name);
                }
                if(hitInfo.collider.gameObject.tag=="Enemy"){
                    enemy = hitInfo.collider.gameObject.GetComponent<Enemy>();
                    Debug.Log("Selected "+enemy.name);
                    enemy.DealDamage(card.attack);
                    Debug.Log(enemy.currHealth);
                    card = null;
                    Debug.Log("Card now null"+card);
                }
            }
        }
    }
}
