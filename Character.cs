using System;
namespace AutoBattle
{
    public class Character
    {
        public string name;
        public float health;
        public float baseDamage;
        public float damageMultiplier;
        public int pushingChance;
        public float pushingDamage;
        public Point position;
        public Character target;
        public bool isAlive = true;
        Point pushingDirection;
 
        public void Die()
        {
            position = new Point(-5, -5);
        }

        public void OnCharacterTurn()
        {                   
            Move();                               
        }

        void Move()
        {
            int diffX = (target.position.x - position.x);
            int diffY = (target.position.y - position.y);


            if (Math.Abs(diffX) <= 1 && Math.Abs(diffY) <= 1)
            {
                Console.WriteLine("In Combat");                 
                
                pushingDirection = target.position;
                //Getting Direction to push
                if (diffY == 0)
                {
                    if (target.position.x > 0 && target.position.x < GameInfo.width - 1)
                        pushingDirection.x = diffX > 0 ? ++pushingDirection.x : --pushingDirection.x;                                      
                }
                else if (diffX == 0)
                {
                    Console.WriteLine(target.position.y);
                    if (target.position.y > 0 && target.position.y < GameInfo.height - 1)                   
                        pushingDirection.y = diffY > 0 ? ++pushingDirection.y : --pushingDirection.y;                                         
                }
                Attack();
            }
            else if (diffX == diffY)
            {
                position.y = diffY > 0 ? ++position.y : --position.y;
                string moveDir = diffY > 0 ? $"{name} moves right" : $"{name} moves left";
                Console.WriteLine(moveDir);
            }
            else if (Math.Abs(diffX) < Math.Abs(diffY))
            {
                if (diffX == 0)
                {
                    position.y = diffY > 0 ? ++position.y : --position.y;
                    string moveDir = diffY > 0 ? $"{name} moves down" : $"{name} moves up";
                    Console.WriteLine(moveDir);
                    return;
                }
                position.x = diffX > 0 ? ++position.x : --position.x;
                string moveDirection = diffX > 0 ? $"{name} moves right" : $"{name} moves left";
                Console.WriteLine(moveDirection);
            }
            else
            {
                if (diffY == 0)
                {
                    position.x = diffX > 0 ? ++position.x : --position.x;
                    string moveDirection = diffX > 0 ? $"{name} moves right" : $"{name} moves left";
                    Console.WriteLine(moveDirection);
                    return;
                }
                position.y = diffY > 0 ? ++position.y : --position.y;
                string moveDir = diffY > 0 ? $"{name} moves down" : $"{name} moves up";
                Console.WriteLine(moveDir);
            }           
        }

        public void Attack()
        {
            float attackDamage = baseDamage * damageMultiplier;
            Console.WriteLine($"{name} attacks {target.name} and deals {attackDamage} of damage!");
            target.health -= attackDamage;
            Console.WriteLine($"{target.name} health: {target.health}");           
            
            if (TryToPush())
                PushEnemy();
            if (target.health <= 0)
                target.isAlive = false;                       
        }
        
        void PushEnemy()
        {
            target.health -= pushingDamage;
            Console.WriteLine($"{name} pushes {target.name} and deals {pushingDamage} of damage!");
            Console.WriteLine($"{target.name} health: {target.health}");
            target.position = pushingDirection;
            if (target.health <= 0)
                target.isAlive = false;
        }

        bool TryToPush()
        {
            var random = new Random();
            int result = random.Next(0, 100);
            Console.WriteLine($"{name} is trying to push {target.name}!");
            if (result <= pushingChance)
                return true;
            Console.WriteLine($"{name} couldn't make it!");
            return false;
        }
    }
}
