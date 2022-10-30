using System;
namespace AutoBattle
{
    public class Grid
    {
        public int width;
        public int height;

        public Character playerCharacter;
        public Character enemyCharacter;

        public Grid(int width, int height)
        {
            this.width = width;
            this.height = height;
            Console.WriteLine("The battle field has been created\n");
        }

        public void DrawBattlefield()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Point position = new Point(i, j);
                    
                        if(playerCharacter.position == position || enemyCharacter.position == position)
                        {
                            Console.Write("[X]\t");
                            goto EndLoop;
                        }
                                           
                    Console.Write("[ ]\t");
                    EndLoop:
                    continue;
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }

        public Point GetRandomPoint()
        {
            //Delete the commentaries in the function below if you want to use random spawn positions

            var rand = new Random();
            int x = rand.Next(0, width);
            int y = rand.Next(0, height);
            return new Point(x, y);
        }
        
        public void InstantiateCharacters(Character player, Character enemy)
        {
            playerCharacter = player;
            enemyCharacter = enemy;

            playerCharacter.target = enemyCharacter;
            enemyCharacter.target = playerCharacter;

            //playerCharacter.position = GetRandomPoint();
            //enemyCharacter.position = GetRandomPoint();
            
            //Comment the next two lines if you want to use random spawn positions
            playerCharacter.position = new Point(0, width - 1);
            enemyCharacter.position = new Point(height -1, 0);
            
            if(playerCharacter.position == enemyCharacter.position)
            {
                while (playerCharacter == enemyCharacter)
                    enemyCharacter.position = GetRandomPoint();
            }
        }
    }
}
