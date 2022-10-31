using System;
namespace AutoBattle
{
    using Direction = Point;
    
    public class Character
    {
        public string name;
        public float health;
        public float baseDamage;
        public float damageMultiplier;
        public Double pushChance;
        public float pushDamage;
        public Point position;
        public Character target;
        public bool isAlive = true;

        public void Die()
        {
            position = new Point(-5, -5);
        }

        public void OnCharacterTurn()
        {                   
            MakeTurn();                               
        }

        void MakeTurn()
        {
            int diffX = (target.position.x - position.x);
            int diffY = (target.position.y - position.y);
            Direction moveDirection = new Direction(0, 0);

            if (Math.Abs(diffX) + Math.Abs(diffY) == 1)
            {
                Direction pushDirection = new Direction(diffX, diffY);
                Attack(pushDirection);
            }
           
            else if(diffX == 0)
                moveDirection = new Direction(0, diffY/Math.Abs(diffY));
       
            else if(diffY == 0)
                moveDirection = new Direction(diffX / Math.Abs(diffX), 0);
                       
            else if(Math.Abs(diffX) < Math.Abs(diffY))
                moveDirection = new Direction(diffX / Math.Abs(diffX), 0);

            else            
                moveDirection = new Direction(0, diffY / Math.Abs(diffY));            
            
            if(moveDirection.x != 0 || moveDirection.y != 0)
            {
                position += moveDirection;
                if (moveDirection.x < 0) Console.WriteLine($"{name} moves left");
                else if(moveDirection.x > 0) Console.WriteLine($"{name} moves right");
                else if (moveDirection.y > 0) Console.WriteLine($"{name} moves up");
                else Console.WriteLine($"{name} moves down");
            }
        }

        public void Attack(Direction direction)
        {
            float initialTargetHealth = target.health;
            Console.WriteLine("In Combat");
            float attackDamage = baseDamage * damageMultiplier;
            target.health -= attackDamage;
            if (TryToPush()) PushEnemy(direction);
            Console.WriteLine($"{name} attacks {target.name} and deals {initialTargetHealth - target.health} of damage!");
            Console.WriteLine($"{target.name} health: {target.health}");
            target.isAlive = target.health > 0;   
        }
        
        void PushEnemy(Direction direction)
        {
            target.health -= pushDamage;
            target.position += direction;
            target.position.x = Math.Clamp(target.position.x, 0, GameInfo.width - 1);
            target.position.y = Math.Clamp(target.position.y, 0, GameInfo.height - 1);
        }

        bool TryToPush()
        { 
            bool push = new Random().NextDouble() < pushChance;
            if(push)Console.WriteLine($"{name}'s attack pushes {target.name}!");
            else Console.WriteLine($"{name} failed to push {target.name}!");
            return push;
        }
    }
}
