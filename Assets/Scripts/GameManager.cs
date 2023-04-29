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
    private bool playersTurn;
    private GameObject[] tempArray;
    private PlayerBoat player;
    private Hand hand;
    private Bag bag;

    private bool redealHand = false;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<Enemy>();
        playersTurn = true;
        tempArray = GameObject.FindGameObjectsWithTag("Player");
        player = tempArray[0].GetComponent<PlayerBoat>();
        hand = GameObject.Find("Hand").GetComponent<Hand>();
        bag = GameObject.Find("Bag").GetComponent<Bag>();
    }

    // Update is called once per frame
    void Update()
    {
        if(redealHand && playersTurn){
            bag.SetDealCards(true);
            redealHand = false;
        }

        if(playersTurn){
            if(hand.GetNumberOfCardsInHand() <= 0){
                playersTurn = false;
                redealHand = true;
            }
            if(Input.GetKeyDown(KeyCode.L)){
                playersTurn = false;
                redealHand = true;
            }
        }
        else{
            StartCoroutine(EnemyAction());
        }

        
    }

    public void Attack(int attack){
        if(playersTurn){
            print("Card Data received");
            enemy.DealDamage(attack);   
            playersTurn = false;
        }
    }

    public void Heal(int health){
        if(playersTurn){
            print("Card Data received");
            player.HealPlayer(health);   
            playersTurn = false;
        }
    }
    public void Defend(int defense){
        if(playersTurn){
            print("Card Data received");
            player.DefendPlayer(defense);   
            playersTurn = false;
        }
    }
    IEnumerator EnemyAction(){
        yield return new WaitForSeconds(2);
        player.AttackPlayer(enemy.attack);
        playersTurn = true;
    }
}
