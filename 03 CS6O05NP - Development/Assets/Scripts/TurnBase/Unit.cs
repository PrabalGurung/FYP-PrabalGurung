/* 
Stores Player/ Party /Enemy value that will be used in turn based battle 
It will also be used to look at player stats
Works Remaining:       
        - Better Calculation (crit system, defense system, attribute/ support enhancement)
        - More attributes (Seperation of Phy.Dmg/SP.Dmg/Tru.Dmg
        - Speed Calculation
        - Save unit HP Mana and more after battle
Problems:               - N.A.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHp;
    public int maxMana;

    public int currentHp;
    public int currentMana;

    public int speed;

    // Checks the damage taken by a unit and returns if the unit has expired or not
    public bool TakeDamage(int dmg)
    {
        currentHp -= dmg;
        if (currentHp <= 0) 
        {
            return true;
        } 
        else 
        {
            return false;
       }; 
    }

    // Heals unit current hp
    public void Heal(int dmg)
    {
        currentHp += dmg;
    }
}
