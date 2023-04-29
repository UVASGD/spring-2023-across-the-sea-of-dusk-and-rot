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
    private PlayerBoat player;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<Enemy>();
        playersTurn = true;
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerBoat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playersTurn){
            int countdown = 100;
            while(countdown>=0){
                countdown -= 1;
            }
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
        player.HealPlayer(health);
    }
    public void Defend(int defense){
        player.DefendPlayer(defense);
    }
}
