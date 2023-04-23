using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{


    [SerializeField]
    private List<GameObject> cards;

    private List<Vector3> circlePoints;
    private Vector3 cardDimensions;
    private float circleRadius;
    private float circleCenter;
    private float cardOverlapAmount = 0.1f;

    private int currentCardCount = 0;
    private bool update = true;

    // Start is called before the first frame update
    void Start()
    {
        circlePoints = GetComponentInChildren<Circle>().getCirclePoints();
        circleRadius = GetComponentInChildren<Circle>().getCircleRadius();
        circleCenter = GetComponentInChildren<Circle>().getCircleCenter();
        //prefab includes a card object; only purpose is to get dimensions
        cardDimensions = GetComponentInChildren<Card>().getCardDimensions();
        transform.Find("Card_Placeholder").gameObject.SetActive(false); //set gameobject to false afterwards to avoid disrupting other scripts

        //add to set of cards in hand the children that are cards
        int cardCount = 0;
        foreach(Transform child in this.transform){
            if(child.GetComponent<Card>() && child.gameObject.activeInHierarchy){
                cards.Add(child.gameObject);
                cardCount += 1;
            }
        }
        CheckCardsInHand();
        print(cards.Count);
        print(cardDimensions);
    }

    // Update is called once per frame
    void Update()
    {   
        CheckCardsInHand();
        if(update){
            StartCoroutine(RepositionCardsInHand());
        }
    }

    private void CheckCardsInHand(){
        int cardCount = 0;
        foreach(Transform child in this.transform){
            if(child.GetComponent<Card>() && child.gameObject.activeInHierarchy){
                cardCount += 1;
            }
        }
        // print("current count: " + currentCardCount);
        // print("card count: " + cardCount);
        if(cardCount != currentCardCount){
            update = true;
        }
        
    }

    public void AddCard(GameObject card){
        if(card.GetComponent<Card>() == null) return;
        cards.Add(card);
        update = true;
    }
    public void RemoveCard(GameObject card){
        if(card.GetComponent<Card>() == null)return;
        cards.Remove(card);
    }

    IEnumerator RepositionCardsInHand(){
        print("repositioning cards");
        int numCards = cards.Count;
        float cardStartingPosX = 0;
        Vector3 screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, 0));

        // print("world center point: ");
        // print(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, 0)));
        if(numCards % 2 == 0){
            if(numCards == 0){
                yield return null;
            }
            else{
                cardStartingPosX = screenCenter.x - (cards.Count/2 * (float)cardDimensions.x - (float)cardDimensions.x/2);
            }
        }
        else{
            Debug.Log("center X: " + screenCenter.x);
            Debug.Log("card width: " + cardDimensions.x);
            if(numCards == 1){
                cardStartingPosX = 0.0f;
            }
            else{
                cardStartingPosX = screenCenter.x -(cards.Count/2 * (float)cardDimensions.x);
                // print(cardStartingPosX);
            }
        }
        // Debug.Log("circle radius: " + circleRadius);
        // Debug.Log("circle center Y: " + circleCenter);
        for(int i = 0; i < cards.Count; i++){
            float angle = Mathf.Atan2(-circleCenter, cardStartingPosX);
            // print("angle: " + angle);
            float temp = Mathf.Sin(angle);
            Debug.Log("starting X: " + cardStartingPosX);
            Debug.Log("angle: " + angle*Mathf.Rad2Deg);
            Debug.Log("sin of angle: " + temp);

            float newY = Mathf.Sin(angle) * circleRadius + circleCenter;
            // Debug.Log("new X: " + cardStartingPosX + (float)(cardDimensions.x)/2);
            // Debug.Log("new Y: " + newY);
            // print(cardStartingPosX);
            // print(newY);
            cards[i].GetComponent<Card>().SetInitialPosition(new Vector3(cardStartingPosX, newY, cards[i].transform.position.z));
            cards[i].GetComponent<Card>().SetRotation(new Vector3(0, 0, angle*Mathf.Rad2Deg - 90));
            cardStartingPosX += cardDimensions.x;
        }
        update = false;
        yield return null;
    }
}
