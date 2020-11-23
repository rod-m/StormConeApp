namespace DefaultNamespace
{
    public class Point
    {
        private float x, y;

        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float X
        {
            get => x;
        }

        public float Y
        {
            get => y;
        }
    }
}