using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //used for adding equip items buffs to flat skill values
    public Skills addSkillsTogether(Skills flat, Skills toAdd)
    {
        flat.skill += toAdd.skill;
        flat.strength += toAdd.strength;
        flat.speed += toAdd.speed;
        flat.reaction += toAdd.reaction;
        flat.initiative += toAdd.initiative;
        flat.constitution += toAdd.constitution;
        flat.armour += toAdd.armour;
        flat.footwork += toAdd.footwork;
        return flat;
    }
}
