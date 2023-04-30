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
    public int attack; //Attack Dmg
    public int defense; //Defense for Player
    public int heal; //Heals Player
    public Type type; //Enum Determines role of card
    public double clean; //Clears Rot, degrades with each use
    public double enhance; //Powers next attack
    private double rot; //Rot level increases with each use, degrading the effect of the card

    private void Start() {
        if(type == Type.CLEAN){
            clean = 5;
        }
    }
    private int getAttack(){
        if(rot == 0){
            rot = 1;
        }
        double rotMod = 1/rot;
        int modifiedAttack = (int)(attack*rotMod);
        return modifiedAttack;
    }
    private int getDefense(){
        if(rot == 0){
            rot = 1;
        }
        double rotMod = 1/rot;
        int modifiedDefense = (int)(defense*rotMod);
        return modifiedDefense;
    }
    private int getHeal(){
        if(rot == 0){
            rot = 1;
        }
        double rotMod = 1/rot;
        int modifiedHeal = (int)(heal*rotMod);
        return modifiedHeal;
    }
    private int getClean(){
        int modifiedClean = (int)(clean - (0.667*rot));
        return modifiedClean;
    }
    private double getEnhance(){
        if(rot == 0){
            rot = 1;
        }
        double rotMod = 1/rot;
        double modifiedEnhance = (int)(enhance*rotMod);
        if(modifiedEnhance<1.2){ modifiedEnhance = 1.2; }
        return modifiedEnhance;
    }
    public double getRotLevel(){
        return rot;
    }
    public void setRotLevel(int newRotLevel){
        Debug.Log("Old Rot lvl: "+rot);
        Debug.Log(newRotLevel);
        if(newRotLevel>5){
            newRotLevel = 5;
        }
        if(newRotLevel<=0){
            rot = 1;
        }
        else{
            rot = newRotLevel;            
        }
        Debug.Log("New Rot Lvl: "+rot);
    }

    public double getEffect(Type type){
        switch(type){
            case Type.ATTACK:
                return getAttack();
            case Type.DEFENSE:
                return getDefense();
            case Type.HEAL:
                return getHeal();
            case Type.CLEAN:
                return getClean(); //Special Cards will need to be implemented later on
            case Type.ENHANCE:
                return getEnhance();
            default:
                return 0;
            
        }
    }

}

public enum Type{
    ATTACK,
    DEFENSE,
    HEAL,
    ENHANCE,
    CLEAN
}
