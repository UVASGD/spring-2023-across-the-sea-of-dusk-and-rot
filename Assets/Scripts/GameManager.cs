using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera camera;
    private CardData card;
    private Card display;
    private Enemy enemy;
    private Dictionary<Card,int> cardRotLevels;
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
                    card = hitInfo.collider.gameObject.GetComponent<Card>().card;
                    display = hitInfo.collider.gameObject.GetComponent<Card>();
                }
                if(hitInfo.collider.gameObject.tag=="Enemy"){
                    enemy = hitInfo.collider.gameObject.GetComponent<Enemy>();
                    enemy.DealDamage(card.getEffect(card.type));
                    Debug.Log(enemy.currHealth);
                    card.setRotLevel(card.getRotLevel()+1);
                    display.updateRotTexture();
                    Debug.Log("Rot at level "+card.getRotLevel()+", attack now at "+card.getEffect(card.type));
                    card = null;
                }
            }
        }
    }
}
