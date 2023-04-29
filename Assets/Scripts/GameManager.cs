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
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<Enemy>();
        playersTurn = true;
        tempArray = GameObject.FindGameObjectsWithTag("Player");
        player = tempArray[0].GetComponent<PlayerBoat>();
        print(player);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playersTurn){
            print("ENEMY ATTACK!");
            player.AttackPlayer(enemy.attack);
            playersTurn = true;
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
}
