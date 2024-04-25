using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDungeon
{
    public enum leveldun {쉬움 = 0, 어려움 =1 , 지옥 = 2};
    public class Dungeon
    {
        Camp camp = new Camp();
        int probability = 0;


        public List<DungeonData> dungeons = new List<DungeonData>();

        public Dungeon()
        {
            dungeons.Add(new DungeonData(0, 10, 1000, 0)); // Easy
            dungeons.Add(new DungeonData(1, 15, 1700, 0)); // Hard
            dungeons.Add(new DungeonData(2, 20, 2500, 0)); // Hell

        }

        public (int ,int) PlayerEquipMent(Player player)
        {
             int atk = 0;
             int def = 0;

             for (int i = 0; i < player.inven.ItemInfo.Count; i++) // 인벤토리 표시
             {
                 if (player.inven.ItemInfo[i].IsEquip == true)
                 {
                    atk += player.inven.ItemInfo[i].ItemAtk;
                    def += player.inven.ItemInfo[i].ItemDef;
                 }
             }
             return (atk, def);
        }
        

        public void Dungeon_Menu(Player player)
        {

            int act; //메뉴
            bool actIsNum;
            var leveling = Enum.GetValues(typeof(leveldun));

            (int atk_inc, int def_inc)= PlayerEquipMent(player);

            Console.WriteLine("\n=================================================================================");
            Console.WriteLine("★던전입장★\n");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine($"현재 당신의 체력: {player.stat.Hp} / {player.stat.MaxHp}");
            Console.WriteLine($"현재 당신의 자금: {player.stat.Gold}");
            Console.WriteLine($"현재 당신의 레벨: {player.stat.Level} , 공격력: {player.stat.Attack} +({atk_inc}) , 방어력: {player.stat.Defense} + ({def_inc})");
            Console.WriteLine($"레벨업까지 남은 경험치 : {player.stat.Level - player.stat.Exp}");

            Console.WriteLine("\n\n-1. 나가기");

            

            for (int i = 0; i < dungeons.Count; i++) // 던전 성공확률 계산
            {

                float pd = player.stat.Defense + def_inc;    // 플레이어 방어력
                float dd = dungeons[i].defenseRate; // 던전 적정 방어력

                

                if (pd >= (dd * 2))
                {
                    dungeons[i].probability = 100;
                }
                else if (pd*2 <= dd)
                {
                    dungeons[i].probability = 0;
                }
                else
                {
                    dungeons[i].probability =(int)( (pd / dd) * 50.0f );
                    
                }
                
            }

            foreach (var value in leveling)
            { 

                Console.Write($"{(int)value}. {(leveldun)value} 던전  | 방어력 {dungeons[(int)value].defenseRate} 이상 권장 " +
                    $"| 평균 보상 금액 : {dungeons[(int)value].reward} | 탐사 성공 확률 : {dungeons[(int)value].probability} % \n");
            }


            Console.WriteLine("\n=================================================================================\n");

            if (player.stat.Hp <= 0)
            {
                Console.WriteLine("\n=================================================================================\n");
                Console.Write("\n ★체력이 없어서 플레이어가 피곤합니다. 휴식하기로 강제 이동됩니다.★\n");
                Console.WriteLine("\n=================================================================================\n");
                Console.Write("이동하시려면 아무키나 입력하세요!");
                string confirm=Console.ReadLine();
                camp.Camping(player);
            }

            do
            {

                Console.Write("\n원하시는 행동을 숫자로 입력해주세요 : ");
                actIsNum = int.TryParse(Console.ReadLine(), out act);
            } while (!actIsNum);


            switch (act)
            {
                case -1: // 나가기         
                    break;
                case 0:
                    Console.WriteLine("\n☆쉬움 던전을 선택하셨습니다.☆\n");
                    DungeonPlay(player, 0);
                    break;
                case 1:
                    Console.WriteLine("\n☆어려움 던전을 선택하셨습니다.☆\n");
                    DungeonPlay(player, 1);
                    break;
                case 2:
                    Console.WriteLine("\n☆지옥 던전을 선택하셨습니다.☆\n");
                    DungeonPlay(player, 2);
                    break;
                default:
                    Console.WriteLine("\n☆====잘못된 입력입니다. 다시 입력해주세요====☆");
                    Dungeon_Menu(player);
                    break;

            }


        }


        private void DungeonPlay(Player player, int level)
        {
            int defgap = 0;
            Random rand1 = new Random();
            int num = rand1.Next(20, 35);

            Random rand2 = new Random();
            int num2 = rand1.Next((int)player.stat.Attack, (int)player.stat.Attack * 2);

            if (IsDungeonClear(player, level))
            {
                defgap = player.stat.Defense - dungeons[level].defenseRate;
                
                
                Console.WriteLine($"축하합니다!! \n{(leveldun)level}던전을 클리어 하였습니다.");

                Console.WriteLine("\n[탐험 결과]");
                Console.WriteLine($"\n체력 {player.stat.Hp} -> {player.stat.Hp-num-defgap}");
                Console.WriteLine($"Gold {player.stat.Gold} -> {player.stat.Gold + dungeons[level].reward * (1+ num2 * 0.02)} G");


                player.stat.Hp -= num + defgap;
                


                player.stat.Gold += dungeons[level].reward;
                player.stat.Exp += 1;
                player.stat.isLevelUp();
                Console.WriteLine($"레벨업까지 남은 경험치 : {player.stat.Level - player.stat.Exp}");
                if (player.stat.Hp <= 0)
                {
                    player.stat.Hp = 0;
                    Console.WriteLine("☆★ 체력이 모두 소모되었습니다! 체력이 없으면 던전입장을 못하니 휴식하시길 바랍니다! ★☆");
                }
            }
            else
            {
                Console.WriteLine("=================================");
                Console.WriteLine("[탐험 결과]");
                Console.WriteLine("던전 탐험에 실패하셨습니다. >_<");
                Console.WriteLine($"\n체력 {player.stat.Hp}->{player.stat.Hp/2}");
                Console.WriteLine("=================================");
                player.stat.Hp = (int)(player.stat.Hp * (0.5));
            }
            Dungeon_Menu(player);
        }

        public bool IsDungeonClear(Player player, int level) // 던전 클리어 계산
        {

            Random rand = new Random();
            int num = rand.Next(0, 101);

            if (num < dungeons[level].probability)
            {
                return true;
            }
            else
            {
                return false;
            }
        
        }



    }

    
}
