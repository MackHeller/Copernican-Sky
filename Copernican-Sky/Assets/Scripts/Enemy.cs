public class Enemy{
    Skills enemySkills;
    string name;
    Personality personality;
    public Enemy(string name, Skills enemySkills, PersonalityTypes type)
    {
        this.name = name;
        this.enemySkills = enemySkills;
        this.personality = Personality.buildPersonality(type);
    }
}

public class Personality
{
    public static Personality buildPersonality(PersonalityTypes type)
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
