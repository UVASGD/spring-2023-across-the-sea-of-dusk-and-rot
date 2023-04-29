using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    List<GameObject> cards = new();
    bool dealCards = false; //this should be changed to when its the players turn
    float deckXPadding = 1.8f;
    float deckYPadding = 1.5f;

    private Transform hand;
    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.Find("Hand").transform;
        this.transform.position = hand.position;
    }

    public void AddCard(GameObject card){

        float depth = card.transform.position.z - Camera.main.transform.position.z;
        // float z = Camera.main.WorldToScreenPoint(new Vector3(0, 0, Camera.main.transform.position.z)).z;
        Vector3 bottomRightScreenCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, depth));

        // Vector3 bottomLeftScreenCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        print(bottomRightScreenCorner);
        // print(bottomLeftScreenCorner);
        Vector3 cardDimensions = card.GetComponent<Card>().getCardDimensions();

        float stackedCardsY = cards.Count * cardDimensions.z;

        float startingX = bottomRightScreenCorner.x + deckXPadding;
        float startingY = bottomRightScreenCorner.y + deckYPadding + stackedCardsY;

        card.GetComponent<Card>().SetInitialPosition(new Vector3(startingX, startingY, card.transform.position.z));
        card.GetComponent<Card>().SetRotation(new Vector3(90, 0, 0));
        cards.Add(card);
        // print(cardDimensions);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.K)){
            dealCards = true;
        }

        if(dealCards){
            for(int i = cards.Count - 1; i >= 0; i--){
                cards[i].GetComponent<Card>().SetTouchStatus(true);
                cards[i].transform.SetParent(hand);
                cards.Remove(cards[i]);
            }
            hand.gameObject.GetComponent<Hand>().ForceUpdate();
            dealCards = false;
        }
        
    }
}
