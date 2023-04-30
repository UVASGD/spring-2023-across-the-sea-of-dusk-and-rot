using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int nextSceneID;
    private CardData card;
    private Card display;
    private Enemy enemy;
    private Dictionary<Card,int> cardRotLevels;
    private bool playersTurn;
    private PlayerBoat player;
    private Hand hand;
    private Bag bag;
    private bool cleanNext;
    private int cleanValue;
    private bool redealHand = false;
    private bool enhanceNext;
    private double enhanceValue;
    public GameObject gameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<Enemy>();
        playersTurn = true;
        player = GameObject.Find("Player").GetComponent<PlayerBoat>();
        hand = GameObject.Find("Hand").GetComponent<Hand>();
        bag = GameObject.Find("Bag").GetComponent<Bag>();
        cleanNext = false;
        cleanValue = 0;
        enhanceNext = false;
        enhanceValue = 0;
        gameOver.SetActive(false);
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
        else if(!playersTurn){
            player.AttackPlayer(enemy.attack);
            playersTurn = true;
        }
        if(player.health<=0){
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
        if(enemy.currHealth<=0){
            gameOver.SetActive(true);
            SceneManager.LoadScene(nextSceneID);
        }

        
    }

    public void Attack(int attack, Card card){
        if(playersTurn){
            //print("Card Data received");
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
            //print("Card Data received");
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
            //print("Card Data received");
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
            card.setCleaned();
            data.setRotLevel((int)data.getRotLevel()-cleanValue);
            cleanNext = false;
            cleanValue = 0;
        }
    }
}
