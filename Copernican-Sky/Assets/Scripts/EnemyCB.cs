public class EnemyCB  {
    Skills enemySkills;
    string name;
    Personality personality;
    public EnemyCB(string name, Character character, Skills enemySkills, Personality personality)
    {
        this.name = name;
        this.enemySkills = enemySkills;
        this.personality = personality;
    }
}

public class Personality
{
    public Personality buildItem(PersonalityTypes type)
    {
        switch (type)
        {
            case PersonalityTypes.AGGRESSIVE:
                return new Personality();
            case PersonalityTypes.PASSIVE:
                return new Personality();
            case PersonalityTypes.RECKLESS:
                return new Personality();
            case PersonalityTypes.TIMID:
                return new Personality();
            default:
                throw new System.Exception();
        }
    }
    private Personality()
    {

    }
}
//TODO make more types
public enum PersonalityTypes
{
    AGGRESSIVE,
    PASSIVE,
    RECKLESS,
    TIMID
}
