using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    List<GameObject> cards;
    bool dealCards = false; //this should be changed to when its the players turn

    private Transform hand;
    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.Find("Hand").transform;
    }

    public void AddCard(GameObject card){
        cards.Add(card);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.K)){
            dealCards = true;
        }

        if(dealCards){
            for(int i = cards.Count - 1; i >= 0; i--){
                cards[i].transform.SetParent(hand);
                cards.Remove(cards[i]);
            }
        }
        
    }
}
