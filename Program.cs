using System;
namespace AutoBattle
{
    public class Program
    {
        static void Main(string[] args)
        {

            int width = GameInfo.width;
            int height = GameInfo.height;
            string winnerName = null;
            Grid grid = new Grid(width, height);
            Character playerCharacter = GameInfo.player;
            Character enemyCharacter = GameInfo.enemy;
            bool player1Turn = true;
            bool gameOver = false;
            bool endGame = false;
           
            Setup();

            void Setup()
            {
                GetEnemyName();
            }

            void GetEnemyName()
            {
                Console.WriteLine("Hello, Kokku!");
                Console.WriteLine("Who are you battling against?");
                Console.Write("Enemy's name: ");
                string s = Console.ReadLine();
                enemyCharacter.name = s;
                StartGame();
            }

            void StartGame()
            {
                Console.WriteLine("Starting Game");
                grid.InstantiateCharacters(playerCharacter, enemyCharacter);
                grid.DrawBattlefield();
                StartTurn();
            }

            void StartTurn()
            {                             
                if (player1Turn)
                {
                    if (!playerCharacter.isAlive)
                    {
                        gameOver = true;
                        winnerName = enemyCharacter.name;
                        playerCharacter.Die();
                    }                                              
                    else HandleTurn(playerCharacter);
                }
                else
                {
                    if (!enemyCharacter.isAlive)
                    {
                        winnerName = playerCharacter.name;
                        gameOver = true;
                        enemyCharacter.Die();
                    }
                    else HandleTurn(enemyCharacter);
                }
                if (gameOver)
                    EndGame();
            }

            void HandleTurn(Character character)
            {
                if (gameOver) return;
                        
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Click on any key to start the next turn...\n");
                Console.WriteLine(Environment.NewLine + Environment.NewLine);
                ConsoleKeyInfo key = Console.ReadKey();
                character.OnCharacterTurn();
                Console.WriteLine(Environment.NewLine);
                grid.DrawBattlefield();
                player1Turn = !player1Turn;
                StartTurn();
            }

            void EndGame()
            {
                if (endGame) return;
                endGame = true;
                Console.WriteLine($"Battle is over!");
                Console.WriteLine($"The winner is: {winnerName}");
            }
        }
    }
}
