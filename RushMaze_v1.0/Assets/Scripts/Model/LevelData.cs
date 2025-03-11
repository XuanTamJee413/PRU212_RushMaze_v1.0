using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    public class LevelData
    {
        public static int mazeWidth;
        public static int mazeHeight;
        public static int monsterCount;
        public static int coinCount;

        public static void SetLevelData(int width, int height, int monsters, int coins)
        {
            mazeWidth = width;
            mazeHeight = height;
            monsterCount = monsters;
            coinCount = coins;
        }
    }
}
