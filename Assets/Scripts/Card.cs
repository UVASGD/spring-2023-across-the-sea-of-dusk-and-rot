using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    // cardBase stores basic information
    public CardBase cardBase;
    /*
     * CardBase attributes
    public string cardName;
    public int maxLevel = 5;
    public Sprite image;
    public CardType type = CardType.attack;
    public int baseAttack = 0;
    public int baseDefense = 0;
    public int baseHeal = 0;
    public int degradePerUse = 0;
    public Special special = Special.normal;
     */
    public int level;

    private void Awake()
    {
        level = cardBase.initialLevel;
    }

    public int getAttack()
    {
        return cardBase.baseAttack * level;
    }

    public int getDefense()
    {
        return cardBase.baseDefense * level;
    }

    public int getHeal()
    {
        return cardBase.baseHeal * level;
    }

    public int degrade(int g)
    {
        level = Mathf.Max(0, level - g);
        return level;
    }

    public int upgrade(int g)
    {
        level = Mathf.Min(cardBase.maxLevel, level + g);
        return level;
    }

    public string Description()
    {
        if (level == 0)
        {
            return "This card is rotten.";
        }
        if (cardBase.special != Special.normal)
        {
            // To be changed
            return "SPECIAL!!!!!";
        }

        string description = "";
        if(getAttack() > 0)
        {
            description += "Deal " + getAttack() + " damage\n";
        }
        if(getDefense() > 0)
        {
            description += "Generate " + getDefense() + " defense\n";
        }
        if(getHeal() > 0)
        {
            description += "Heal " + getHeal() + " HP";
        }
        return description;
    }

}

