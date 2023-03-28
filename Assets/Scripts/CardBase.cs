using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class CardBase : ScriptableObject
{
    public string cardName;
    public int maxLevel = 5;
    public int initialLevel = 5;
    public Sprite image;
    public CardType type = CardType.attack;
    public int baseAttack = 0;
    public int baseDefense = 0;
    public int baseHeal = 0;
    public int degradePerUse = 0;
    public Special special = Special.normal;
}

public enum Special
{
    normal,
    // others to be added
    special,
}

public enum CardType
{
    attack,
    defense,
    recover,
    special
}