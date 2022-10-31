namespace AutoBattle
{
    public struct Point
    {
        public int x;
        public int y;
            
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator == (Point lhs, Point rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y;
        }

        public static bool operator !=(Point lhs, Point rhs)
        {
            return !(lhs == rhs);
        }
        
        public static Point operator +(Point lhs, Point rhs)
        {
            return new Point(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        public override bool Equals(object obj)
        {
            Point point =  (Point)obj;
            return point == this;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + x.GetHashCode();
            hash = hash * 23 + y.GetHashCode();
            return hash;
        }

        public static readonly Point infinity = new Point(-10000, -10000);
    }
}
