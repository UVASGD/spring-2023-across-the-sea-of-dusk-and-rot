using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{


    [SerializeField]
    List<Card> cards;

    private List<Vector3> circlePoints;
    private Vector3 cardDimensions;
    private float circleRadius;

    // Start is called before the first frame update
    void Start()
    {
        circlePoints = GetComponentInChildren<Circle>().getCirclePoints();
        circleRadius = GetComponentInChildren<Circle>().getCircleRadius();

        //prefab includes a card object; only purpose is to get dimensions
        cardDimensions = GetComponentInChildren<Card>().getCardDimensions();
        transform.Find("Card").gameObject.SetActive(false); //set gameobject to false afterwards to avoid disrupting other scripts
        print(cardDimensions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCard(Card card){
        cards.Add(card);
    }


    IEnumerator RepositionCardsInHand(){
        
        yield return null;
    }
}
