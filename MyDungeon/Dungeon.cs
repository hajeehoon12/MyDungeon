using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDungeon
{
    public enum leveldun {쉬움 = 0, 보통 = 1, 어려움 = 2, 지옥 = 3};
    
    public class Dungeon
    {
        Program forsave;
        Camp camp = new Camp();
        int probability = 0;


        public List<DungeonData> dungeons = new List<DungeonData>();

        public Dungeon()
        {
            forsave = new Program(); // 게임 클리어 시 저장용


            dungeons.Add(new DungeonData(0, 15, 1000, 0)); // Easy
            dungeons.Add(new DungeonData(1, 30, 1700, 0)); // Normal
            dungeons.Add(new DungeonData(2, 40, 2500, 0)); // Hard
            dungeons.Add(new DungeonData(3, 60, 5000, 0)); // Hell

        }

        public (int ,int) PlayerEquipMent(Player player) // 플레이어 장비 착용, 능력치 실시간 적용 출력 함수
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
        

        public void Dungeon_Menu(Player player) // 던전의 가장 기본 메뉴
        {

            int act; //메뉴
            bool actIsNum;
            var leveling = Enum.GetValues(typeof(leveldun));

            (int atk_inc, int def_inc)= PlayerEquipMent(player);
            player.stat.isLevelUp(); // 레벨업 갱신 검사
            Console.WriteLine("\n=================================================================================");
            Console.WriteLine("★던전입장★\n");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine($"현재 당신의 체력: {player.stat.Hp} / {player.stat.MaxHp}");
            Console.WriteLine($"현재 당신의 자금: {player.stat.Gold}");
            Console.WriteLine($"현재 당신의 레벨: {player.stat.Level} , 공격력: {player.stat.Attack} +({atk_inc}) , 방어력: {player.stat.Defense} + ({def_inc})");
            Console.WriteLine($"레벨업까지 남은 경험치 : {player.stat.Level * 2 - player.stat.Exp}");

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
                Console.Write($"\n ★체력이 모두 소진되어 {player.Name}이(가) 명령을 거부합니다. 휴식하기로 강제 이동됩니다.★\n");
                Console.WriteLine("\n=================================================================================\n");
                Console.Write("이동하시려면 아무키나 입력하세요!");
                string confirm=Console.ReadLine();
                camp.Camping(player);
            }

            do
            {
                Console.Write("\n원하시는 행동을 숫자로 입력해주세요 : ");
                actIsNum = int.TryParse(Console.ReadLine(), out act);
                Console.Clear();
            } while (!actIsNum);

            if (0 <= act && act <= 3) // 난이도에 맞는 던전 실행
            {
                Console.WriteLine($"\n☆{(leveldun)act} 던전을 선택하셨습니다.☆\n");
                DungeonPlay(player, act);
            }

        }


        private void DungeonPlay(Player player, int level)
        {
            int defgap = 0;
            Random rand1 = new Random();
            int num = rand1.Next(20, 35); // 성공 시 기본 체력소모 20~35

            Random rand2 = new Random();
            int num2 = rand1.Next((int)player.stat.Attack, (int)player.stat.Attack * 2);

            Random rand3 = new Random(); // 긍,부정적 효과 랜덤
            int num3 = rand3.Next(0, 6);

            Random rand4 = new Random(); // 증감폭 랜덤
            int num4 = rand4.Next(25, 75);

            Double Randvar = (Double)num4 / 50.0d;  // 0.5~ 1.5 배 사이의 추가 적용

            if (IsDungeonClear(player, level)) // 던전 성공 검사 후 성공시
            {
                if (level == 3) // 지옥 난이도 클리어 시 , 게임 클리어 및 저장 , 그리고 이어서 게임함
                {
                    Console.WriteLine("지옥 난이도의 던전을 클리어하여 게임을 클리어 했습니다! 축하드립니다!");
                    Console.WriteLine("\n\n\n\n");
                    Console.WriteLine("★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★");
                    Console.WriteLine("\r\n _____                                    _           _         _    _                    \r\n/  __ \\                                  | |         | |       | |  (_)                   \r\n| /  \\/  ___   _ __    __ _  _ __   __ _ | |_  _   _ | |  __ _ | |_  _   ___   _ __   ___ \r\n| |     / _ \\ | '_ \\  / _` || '__| / _` || __|| | | || | / _` || __|| | / _ \\ | '_ \\ / __|\r\n| \\__/\\| (_) || | | || (_| || |   | (_| || |_ | |_| || || (_| || |_ | || (_) || | | |\\__ \\\r\n \\____/ \\___/ |_| |_| \\__, ||_|    \\__,_| \\__| \\__,_||_| \\__,_| \\__||_| \\___/ |_| |_||___/\r\n                       __/ |                                                              \r\n                      |___/                                                               \r\n");
                    Console.WriteLine("★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★");

                    Console.WriteLine("해당 기록은 자동으로 저장되고 게임을 이어합니다.");

                    forsave.SavePlayerInfo(player);

                    Thread.Sleep(5000);

                    forsave.SelectAct(player);

                }





                defgap = player.stat.Defense - dungeons[level].defenseRate; // 플레이어 방어력 - 던전 적정 방어력 계산

                Console.WriteLine("★★★★★★★★★★★★★★★★★★★★★★★★★");
                Console.WriteLine($"축하합니다!! \n{(leveldun)level}던전을 클리어 하였습니다.");
                Console.WriteLine("\n[탐험 결과]");
                Console.WriteLine($"\n체력 {player.stat.Hp} -> {player.stat.Hp - num - defgap}");
                Console.WriteLine($"Gold {player.stat.Gold} -> {player.stat.Gold + dungeons[level].reward * (1 + num2 * 0.02)} G");


                player.stat.Hp -= num + defgap;
                player.stat.Gold += dungeons[level].reward;
                player.stat.Exp += 1;

                player.stat.isLevelUp();

                Console.WriteLine($"레벨업까지 남은 경험치 : {player.stat.Level - player.stat.Exp}");
                Console.WriteLine("★★★★★★★★★★★★★★★★★★★★★★★★★");
            }
            else // 던전 클리어 실패 시
            {
                

                Console.WriteLine($"{player.Name} 이(가) 던전 탐험에 실패하여 도주를 선택했습니다.");
                Console.WriteLine($"{player.Name} 에게 무작위 부정적인 효과를 적용합니다.");


                switch (num3)
                {
                    case 0:
                    case 1: // 체력 절반 , 체력 소진

                        Double decrate = Math.Round(100.0d * (1.0d - (0.5d * Randvar)));
                        Console.WriteLine($"{player.Name} 이(가) 몬스터들과의 사투 끝에 겨우 살아서 도망쳐나옵니다!");
                        Console.WriteLine("★★★★★★★★★★★★★★★★★★★★★★★★★");
                        Console.WriteLine("[탐험 결과]");
                        Console.WriteLine($"{player.Name} 이(가) 도주하는데 온힘을 쏟아 현재 체력의 {Math.Round(decrate)} % 만큼 감소 되었습니다.");
                        Console.WriteLine($"\n체력 {player.stat.Hp}->{(int)(player.stat.Hp * (0.5 * Randvar))}");
                        Console.WriteLine("★★★★★★★★★★★★★★★★★★★★★★★★★");
                        player.stat.Hp = (int)(player.stat.Hp * (0.5 * Randvar));

                        break;
                    case 2:
                    case 3: // 함정 체력 -20
                        Console.WriteLine($"{player.Name} 이(가) 던전의 함정을 발동시켰습니다."  );
                        Console.WriteLine("=================================");
                        Console.WriteLine("[탐험 결과]");
                        Console.WriteLine($"{player.Name} 이(가) 던전의 치명적인 함정으로 인해 {Math.Round(Randvar * 20)} 잃었습니다!");
                        Console.WriteLine($"\n체력 {player.stat.Hp}->{player.stat.Hp - Math.Round(Randvar * 20)}");
                        Console.WriteLine("=================================");
                        player.stat.Hp = (int)(player.stat.Hp - Math.Round(Randvar * 20));

                        break;

                    case 4: // 정신력으로 인한 최대체력감소
                        Console.WriteLine($"감당할 수 없는 강력한 몬스터가 나타나 {player.Name}이 도망칩니다!");
                        Console.WriteLine("=================================");
                        Console.WriteLine("[탐험 결과]");
                        Console.WriteLine($"{player.Name} 이(가) 마음이 꺾여 최대체력이 영구적으로 {Math.Round(Randvar * 5)} 감소합니다!");
                        Console.WriteLine($"\n최대 체력 {player.stat.MaxHp}->{player.stat.MaxHp - Math.Round(Randvar *5) }");
                        Console.WriteLine("=================================");
                        player.stat.MaxHp = (int)(player.stat.MaxHp - Math.Round(Randvar * 5));
                        if (player.stat.MaxHp < player.stat.Hp)
                        {
                            player.stat.Hp = player.stat.MaxHp;
                        }
                        

                        break;
                    case 5: // 무사고
                        Console.WriteLine($"{player.Name} 이(가) 배가고파 던전탐험을 포기합니다.");
                        Console.WriteLine("=================================");
                        Console.WriteLine("[탐험 결과]");
                        Console.WriteLine("아무일도 일어나지 않습니다.");
                        Console.WriteLine("=================================");
                        player.stat.MaxHp = (int)(player.stat.MaxHp - Math.Round(Randvar * 5));
                        

                        break;
                    default:
                        break;
                
                }

            }

            if (player.stat.Hp <= 0) // 던전을 마치고 체력이 모두 소진되었을 때 메세지 출력
            {
                player.stat.Hp = 0;
                Console.WriteLine($"★체력이 모두 소진되어 {player.Name}이(가) 피곤해합니다. 휴식하기로 강제 이동됩니다.★"); 
                camp.Camping(player); // 휴식 장소 강제이동
            }
            Dungeon_Menu(player); // 일반적인 경우 던전선택창으로 다시 이동
        }

        public bool IsDungeonClear(Player player, int level) // 던전 클리어 확률을 통한 클리어 여부 확인
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
