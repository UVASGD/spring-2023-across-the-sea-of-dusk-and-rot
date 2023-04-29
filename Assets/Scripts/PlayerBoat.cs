using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBoat : MonoBehaviour
{
    private int health;
    private int defense;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI defenseText;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        defense = 0;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.SetText("Health: "+health);
        defenseText.SetText("Defense: "+defense);
    }

    public void AttackPlayer(int attack){
        if(defense!=0){
            defense -= attack;
            if(defense<0){
                health += defense;
                defense = 0;
            }
        }
        else{
            health -= attack;
        }
    }
    public void HealPlayer(int healAmount){
        health += healAmount;
    }
    public void DefendPlayer(int shield){
        defense += shield;
    }
}
