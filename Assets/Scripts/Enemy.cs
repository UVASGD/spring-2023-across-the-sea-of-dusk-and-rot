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
    public int attack;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = health;
        nameText.text = name;
        healthText.text = currHealth.ToString();
    }

    private void Update() {
        healthText.SetText(currHealth.ToString());
    }

    public void DealDamage(int attack){
        currHealth -= attack;
    }
}
