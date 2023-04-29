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
        enemy = GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Attack(int attack){
        print("Card Data received");
        enemy.DealDamage(attack);
    }
}
