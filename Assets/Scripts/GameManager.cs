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
    private bool cleanNext;
    private int cleanValue;
    private bool redealHand = false;
    private bool enhanceNext;
    private double enhanceValue;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<Enemy>();
        playersTurn = true;
        tempArray = GameObject.FindGameObjectsWithTag("Player");
        player = tempArray[0].GetComponent<PlayerBoat>();
        hand = GameObject.Find("Hand").GetComponent<Hand>();
        bag = GameObject.Find("Bag").GetComponent<Bag>();
        cleanNext = false;
        cleanValue = 0;
        enhanceNext = false;
        enhanceValue = 0;
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
            player.AttackPlayer(enemy.attack);
            playersTurn = true;
        }

        
    }

    public void Attack(int attack, Card card){
        if(playersTurn){
            print("Card Data received");
            if(enhanceNext){
                enemy.DealDamage((int)(attack*enhanceValue));
                enhanceNext = false;
                enhanceValue = 0;
            }
            else{
                enemy.DealDamage(attack);
            }
            performClean(cleanNext, card);
        }
    }

    public void Heal(int health, Card card){
        if(playersTurn){
            print("Card Data received");
            if(enhanceNext){
                player.HealPlayer((int)(health*enhanceValue));
                enhanceNext = false;
                enhanceValue = 0;
            }
            else{
                player.HealPlayer(health); 
            }
            performClean(cleanNext, card);
        }
    }
    public void Defend(int defense, Card card){
        if(playersTurn){
            print("Card Data received");
            if(enhanceNext){
                player.DefendPlayer((int)(defense*enhanceValue));
                enhanceNext = false;
                enhanceValue = 0;
            }
            else{
                player.DefendPlayer(defense); 
            }
            performClean(cleanNext, card);
        }
    }
    public void CleanNextCard(int clean){
        cleanNext = true;
        cleanValue = clean;
    }
    public void EnhanceNextCard(double value, Card card){
        enhanceNext = true;
        enhanceValue = value;
        performClean(cleanNext, card);
    }

    private void performClean(bool c, Card card){
        if(c){
            CardData data = card.GetCardData();
            print("Cleaning "+cleanValue);
            print("Rot Before: "+data.getRotLevel());
            data.setRotLevel((int)data.getRotLevel()-cleanValue);
            print("Rot After: "+data.getRotLevel());
            cleanNext = false;
            cleanValue = 0;
        }
    }
}
