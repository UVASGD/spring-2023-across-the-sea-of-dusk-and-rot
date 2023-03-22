using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class CardBase : ScriptableObject
{
    public string name;
    public string description;
    public Sprite image;
    public CardType type;
    public RotState state;
    public int attack;
    public int defense;
    public int heal;
    public SpecialType special;

    //Getter Methods
    public string getAttack(){
        string s = "This card does "+attack+" damage!";
        return s;
    }
    public string getDefense(){
        string s = "This card protects against "+defense+" damage!";
        return s;
    }
    public string getHeal(){
        string s = "This card heals "+heal+" health!";
        return s;
    }
    public RotState getRotState(){
        return state;
    }
    public string getSpecial(){
        string s = "IDK for now, we can work this out later";
        return s;
    }
    public string getEffect(CardType type){
        switch(type){
            case CardType.Attack:
                return getAttack();
            case CardType.Defense:
                return getDefense();
            case CardType.Heal:
                return getHeal();
            case CardType.Special:
                return getSpecial();
            default:
                return getAttack();
        }
    }
}

public enum CardType{
    Attack,
    Defense,
    Heal,
    Special
}

public enum RotState{
    Pristine,
    Worn,
    Moldy,
    Rotten
}

public enum SpecialType{
    Example
}
