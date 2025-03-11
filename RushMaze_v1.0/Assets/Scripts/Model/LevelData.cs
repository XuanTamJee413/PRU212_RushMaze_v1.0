namespace Assets.Scripts.Model
{
    public static class LevelData
    {
        public static int MazeWidth { get; set; }
        public static int MazeHeight { get; set; }
        public static int MonsterCount { get; set; }
        public static int CoinCount { get; set; }

        public static void SetLevelData(int width, int height, int monsters, int coins)
        {
            MazeWidth = width;
            MazeHeight = height;
            MonsterCount = monsters;
            CoinCount = coins;
        }
    }
}
