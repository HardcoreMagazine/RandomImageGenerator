namespace RandomImageGenerator
{
    internal class RandomImageGenerator
    {
        private const int w = 120, h = 29;
        private static int pointA, pointB;
        private static char[] space = new char[w * h];
        public char[] image;

        static int RNG(int? minValue = null, int? maxValue = null)
        {
            Random num = new Random(Guid.NewGuid().GetHashCode());
            if (minValue == null)
                if (maxValue == null)
                    return num.Next(0, int.MaxValue);
                else
                    return num.Next(0, (int)maxValue);
            else
                if (maxValue == null)
                return num.Next((int)minValue, int.MaxValue);
            else
                return num.Next((int)minValue, (int)maxValue);
        }

        private static int GetPos(int X, int Y)
        {
            if (w == X & h == Y)
                // Removed 1 elem on the start,
                // extra -1 elem because array indexing starts with 0
                return X * Y - 1;
            else
                return Y * w + X;
        }

        private static void FillSpace(char c)
        {
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    space[GetPos(x, y)] = c;
                }
            }
        }

        private static void ConnectDots()
        {
            int index = pointA;
            sbyte[] directions = new sbyte[] { -120, -1, 1, 120 };
            sbyte direction;
            Random random = new Random();
            while (index != pointB)
            {
                random.ShuffleArray(directions);
                direction = directions[0];

                if (Math.Abs(direction) != w) //-1, 1
                {
                    if (index+direction >= 0 & index+direction < space.Length & (index+direction)%w != 0)
                    {
                        index += direction;
                        if (index!= pointB & index != pointA)
                            space[index] = '#'; // Would not recommend other symbols
                    }
                }
                else //-120, 120
                {
                    if (index+direction >= 0 & index+direction < space.Length)
                    {
                        index += direction;
                        if (index!= pointB & index != pointA)
                            space[index] = '#';
                    }
                }
            }
        }

        public RandomImageGenerator()
        {
            FillSpace(' ');
            pointA = GetPos(0, RNG(0, h));
            pointB = GetPos(w - 1, RNG(0, h));
            ConnectDots();
            image = space;
        }
    }
}
