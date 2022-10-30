namespace AutoBattle
{
    public struct Point
    {
        public int y;
        public int x;
            
        public Point(int y, int x)
        {
            this.y = y;
            this.x = x;
        }

        public static bool operator == (Point lhs, Point rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y;
        }

        public static bool operator !=(Point lhs, Point rhs)
        {
            return !(lhs == rhs);
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
    }
}
