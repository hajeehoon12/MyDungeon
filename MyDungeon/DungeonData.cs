using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDungeon
{
    public class DungeonData
    {
        public int dungeonType; // 0 : Easy 1: Hard 2: Hell
        public int defenseRate; // 던전의 적정 방어력 (클리어 확률에 직접적인 영향)
        public int reward; // 던전 보상 Easy: 1000 G, Hard:1700 G, Hell: 2500 G  
        public int probability { get; set; }

        public DungeonData(int _dungeonType, int _defenseRate, int _reward, int _probability)
        {
            dungeonType = _dungeonType;
            defenseRate = _defenseRate; 
            reward = _reward;
            probability = _probability;
        }

    }
}
