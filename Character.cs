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

        void Die()
        {
            position = Point.infinity;
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
                Translate(moveDirection);
                if (moveDirection.x < 0) Console.WriteLine($"{name} moves left");
                else if(moveDirection.x > 0) Console.WriteLine($"{name} moves right");
                else if (moveDirection.y > 0) Console.WriteLine($"{name} moves up");
                else Console.WriteLine($"{name} moves down");
            }
        }

        void Translate(Direction direction)
        {
            position += direction;
            position.x = Math.Clamp(position.x, 0, GameInfo.width - 1);
            position.y = Math.Clamp(position.y, 0, GameInfo.height - 1);
        }

        void Attack(Direction direction)
        {
            float initialTargetHealth = target.health;
            float attackDamage = baseDamage * damageMultiplier;
            target.TakeDamage(attackDamage);
            if (TryToPush()) PushEnemy(direction);
            Console.WriteLine("In Combat");
            Console.WriteLine($"{name} attacks {target.name} and deals {initialTargetHealth - target.health} of damage!");
            Console.WriteLine($"{target.name} health: {target.health}");
        }

        void TakeDamage(float damage)
        {
            health -= damage;
            isAlive = health > 0;
            if (!isAlive) Die();
        }
        
        void PushEnemy(Direction direction)
        {
            target.TakeDamage(pushDamage);
            target.Translate(direction);
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
