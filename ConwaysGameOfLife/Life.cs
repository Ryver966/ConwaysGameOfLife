using System;
using System.IO;

namespace ConwaysGameOfLife
{
    class Life
    {
        const int width = 80;
        const int height = 30;
        public byte[,] currentLife = new byte[width, height];
        public byte[,] nextLife = new byte[width, height];

        static StreamWriter stdout = new StreamWriter(Console.OpenStandardOutput(), System.Text.Encoding.GetEncoding(437));
        static char blob = System.Text.Encoding.GetEncoding(437).GetChars(new byte[] { 219 })[0];

        public void RadrawLife()
        {
            int data;
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
            
            for(int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; ++col)
                {
                    data = currentLife[col, row];
                    if(data == 0)
                    {
                        stdout.Write(' ');
                    }
                    else if (data == 1)
                    {
                        stdout.Write(blob);
                    }
                    else
                    {
                        stdout.Write(' ');
                    }
                }
                if (row != height - 1)
                    stdout.Write("\n");
            }
            stdout.Flush();
        }

        public void EdgeWrap(ref int x, ref int y)
        {
            if (x < 0) x += width;
            else if (x > width - 1) x -= width;
            if (y < 0) y += height;
            else if (y > height - 1) y -= height;
        }

        public int CountNeighbours(int x, int y)
        {
            int x1, y1, count = 0;

            for (int sx = x - 1; sx < x + 2; sx++)
            {
                for (int sy = y - 1; sy < y + 2; sy++)
                {
                    if ((sx == x) && (sy == y))
                    {
                        continue;
                    }
                    x1 = sx;
                    y1 = sy;
                    EdgeWrap(ref x1, ref y1);
                    if (currentLife[x1, y1] == 1)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public void Update()
        {
            int n;

            for(int x = 0; x< width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    n = CountNeighbours(x, y);

                    if(n < 2)
                    {
                        nextLife[x, y] = (byte)(currentLife[x, y] == 1 ? 2 : 0);
                    }
                    else if(n == 2)
                    {
                        nextLife[x, y] = currentLife[x, y];
                    }
                    else if (n == 3)
                    {
                        nextLife[x, y] = 1;
                    }
                    else
                    {
                        nextLife[x, y] = 0;
                    }
                }
            }

            Array.Copy(nextLife, currentLife, width * height);
            RadrawLife();
        }

       public void CopyPattern(byte[,] src, byte[,] dst, int xoffset, int yoffset)
        {
            for (int y = 0; y < src.GetLength(0); y++)
            {
                for(int x = 0; x< src.GetLength(1); x++)
                {
                    dst[x + xoffset, y + yoffset] = src[y, x];
                }
            }
        }
    }

}
