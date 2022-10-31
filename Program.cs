using System;
using System.Diagnostics;

namespace AutoBattle
{
    public class Program
    {
        static void Main(string[] args)
        {
            int width = GameInfo.width;
            int height = GameInfo.height;
            Grid grid = new Grid(width, height);
            Character playerCharacter = GameInfo.player;
            Character enemyCharacter = GameInfo.enemy;
            bool playerTurn = true;
            bool gameRunning = true;

            Console.WriteLine("Hello, Kokku!");
            Console.WriteLine("Who are you battling against?");
            SetEnemyName(ref enemyCharacter);
            
            Console.WriteLine("Starting Game");
            if (gameRunning)
            {
                grid.InstantiateCharacters(playerCharacter, enemyCharacter);
                grid.DrawBattlefield();
            }
            
            while (gameRunning)
            {              
                Character currentCharacter = playerTurn ? playerCharacter : enemyCharacter;
                Character nextCharacter = playerTurn ? enemyCharacter : playerCharacter;                
                HandleTurn(currentCharacter);
                grid.DrawBattlefield();
                
                if(!nextCharacter.isAlive)
                {
                    Console.WriteLine($"Battle is over!");
                    Console.WriteLine($"The winner is: {currentCharacter.name}");
                    Console.WriteLine("Click on any key to close the game");
                    grid.DrawBattlefield();
                    Console.ReadKey();
                    gameRunning = false;
                    Process[] processes = Process.GetProcessesByName("HHTCtrlp");
                    foreach (var process in processes)
                    {
                        process.Kill();
                    }
                }
                playerTurn = !playerTurn;
            }                      
        }
       
        static void SetEnemyName(ref Character enemyCharacter)
        {
            Console.Write("Enemy's name: ");
            enemyCharacter.name = Console.ReadLine();
        }

        static void HandleTurn(Character character)
        {
            Console.Write(Environment.NewLine + Environment.NewLine);
            Console.WriteLine("Click on any key to start the next turn...\n");
            Console.WriteLine(Environment.NewLine + Environment.NewLine);
            Console.ReadKey();
            character.OnCharacterTurn();
            Console.WriteLine(Environment.NewLine);
        }
    }
}
