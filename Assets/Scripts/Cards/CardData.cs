using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class CardData : ScriptableObject
{
    public string name;
    public string description;

    public Texture artwork;
    ///////////////////////
    public int attack;
    public int defense;
    public int heal;
    public Type type;
    private float rot;

    private int getAttack(){
        Debug.Log("Rot: "+rot);
        Debug.Log("Rot modifier "+1/rot);
        return (int)Mathf.Ceil(attack*(1/rot));
    }
    private int getDefense(){
        return defense;
    }
    private int getHeal(){
        return heal;
    }
    public float getRotLevel(){
        return rot;
    }
    public void setRotLevel(float newRotLevel){
        //This ensures a card will always do at least 1 damage
        if(newRotLevel>=attack){
            newRotLevel = attack;
        }
        rot = newRotLevel;
    }

    public int getEffect(Type type){
        switch(type){
            case Type.ATTACK:
                return getAttack();
            case Type.DEFENSE:
                return getDefense();
            case Type.HEAL:
                return getHeal();
            case Type.SPECIAL:
                return 0; //Special Cards will need to be implemented later on
            default:
                return 0;
            
        }
    }

}

public enum Type{
    ATTACK,
    DEFENSE,
    HEAL,
    SPECIAL

}
