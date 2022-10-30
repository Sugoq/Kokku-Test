namespace AutoBattle
{
    public class GameInfo
    {
        public static int width = 6;
        public static int height = 4;

        public static Character player = new Character
        {
            name = "Kokku",
            health = 20,
            baseDamage = 6,
            damageMultiplier = 1.3f,
            pushingChance = 25,
            pushingDamage = 6
        };

        public static Character enemy = new Character
        {
            name = "Enemy",
            health = 20,
            baseDamage = 5,
            damageMultiplier = 1.3f,
            pushingChance = 100,
            pushingDamage = 10
        };
    }
}
