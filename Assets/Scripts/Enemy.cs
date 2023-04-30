using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public string name;
    public int health;
    public int currHealth;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackText;
    public int attack;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = health;
        nameText.text = name;
        healthText.text = "Health: "+currHealth.ToString();
        attackText.text = "Attack: "+attack;
    }

    private void Update() {
        healthText.SetText("Health: "+currHealth.ToString());
        attackText.SetText("Attack: "+attack);
        
    }

    public void DealDamage(int damage){
        currHealth -= damage;
    }
}
