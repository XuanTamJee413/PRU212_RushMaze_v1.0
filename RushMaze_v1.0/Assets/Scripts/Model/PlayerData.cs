using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Data
{
    [Serializable]
    public class PlayerData
    {
        public int MaxHp;
        public int CurrentHp;
        public int MaxMana;
        public int CurrentMana;
        public float PositionX;
        public float PositionY;
        public int Power;
        public int Exp;
        public int Dame;
        public int Gold;
        public int Key;

        // Tính level từ kinh nghiệm
        public int Level => 1 + Exp / 100;

        public PlayerData(int maxHp, int currentHp, int maxMana, int currentMana, float x, float y, int power, int exp, int dame, int gold, int key)
        {
            MaxHp = maxHp;
            CurrentHp = currentHp;
            MaxMana = maxMana;
            CurrentMana = currentMana;
            PositionX = x;
            PositionY = y;
            Power = power;
            Exp = exp;
            Dame = dame;
            Gold = gold;
            Key = key;
        }

        // Dữ liệu mặc định cho nhân vật mới
        public static PlayerData DefaultPlayer()
        {
            return new PlayerData(100, 90, 50, 40, 0, 0, 10, 10, 10, 100,0);
        }
    }

}
