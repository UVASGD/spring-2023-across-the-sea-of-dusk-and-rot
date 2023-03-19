using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardType Type;
    public int level = 10;
    public float baseAttack = 1;
    public float baseDefense = 1;
    public float baseRecover = 1;


    public string Description()
    {
        if(Type == CardType.attack)
        {
            return "This is a level " + level + " attack Card";
        }

        if(Type == CardType.defense)
        {
            return "This is a level " + level + " defense Card";
        }

        return "";
    }

}

public enum CardType
{
    attack,
    defense,
    recover,
    special
}