﻿using System;
/**
 * This is the hero class (one of three) that contains most of stats and information for the playable characters.
 * While it is primarily used by the combat controller, the other two controllers will also use this class to get
 * character information when they need it (for ex. party character names.) 
 * */
public class HeroStats
{
    private Skills playerSkills;
    private string name;
    private Character character;
    public HeroStats(string name, Character character, Skills playerSkills)
    {
        this.name = name;
        this.character = character;
        this.playerSkills = playerSkills;
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
