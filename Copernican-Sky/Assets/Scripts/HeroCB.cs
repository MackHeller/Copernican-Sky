using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCB : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //used for adding equip items buffs to flat skill values
    public Skills addSkills(Skills flat, Skills toAdd)
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

public class Skills
{
    public int skill;
    public int strength;
    public int speed;
    public int reaction;
    public int initiative;
    public int constitution;
    public int armour;
    public int footwork;
    public Skills(int[] skillList)
    {
        skill = skillList[0];
        strength = skillList[1];
        speed = skillList[2];
        reaction = skillList[3];
        initiative = skillList[4];
        constitution = skillList[5];
        armour = skillList[6];
        footwork = skillList[7];
    }
}
