using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyDungeon
{
    

    public class Inventory
    {
        
        string Name { get; set; }
        int act;
        bool actIsNum;
        
       
        public List<ItemData> ItemInfo = new List<ItemData>();
        


        public Inventory(string name) // 초기화값
        {
            // ItemInfo 구성 (장비 타입(오른손,왼손,목걸이,방어구,소모품), 개체명, 공격력, 방어력, 금액, 수량(보유여부), 설명)
            Name = name;
            
            ItemInfo.Add(new ItemData(0, "목검", 10, 0, 50, 1, "나무로 만든 조잡한 검입니다."));
            ItemInfo.Add(new ItemData(3, "천갑옷", 0, 3, 10, 1, "방어기능이라고는 없는 천으로 만든 옷입니다."));
            ItemInfo.Add(new ItemData(1, "나무방패", 3, 5, 30, 1, "주먹질 정도는 막을 수 있을 것 같은 낡은 나무 방패입니다."));
            ItemInfo.Add(new ItemData(2, "낡은목걸이", 1, 1, 100, 1, "툭 치면 부러질 듯한 목걸이입니다."));
            ItemInfo.Add(new ItemData(4, "소형회복물약", 4, 0, 100, 3, "사용자의 활력을 돋게 해줄 물약입니다."));

        }
        public void Show_Inven(Player player) // 인벤토리 표시
        {
            
            Console.WriteLine("\n\n=================================================================================");
            Console.WriteLine($"★플레이어 {Name} 의 인벤토리★ \n");
            Console.WriteLine("           [아이템 목록]           \n");
            
            for (int i = 0; i < ItemInfo.Count; i++) // 인벤토리 표시
            {
                
                Console.Write("- ");
                if (ItemInfo[i].IsEquip) Console.Write("[E]");
                Console.Write($"{ItemInfo[i].ItemName}");
                Console.Write($" X {ItemInfo[i].Amount}|");
                if (ItemInfo[i].ItemAtk > 0) Console.Write($"   공격력 +{ItemInfo[i].ItemAtk.ToString("D4")} |");
                if (ItemInfo[i].ItemDef > 0) Console.Write($"   방어력 +{ItemInfo[i].ItemDef.ToString("D4")} |");
                if (ItemInfo[i].ItemExp != null) Console.Write($"  {ItemInfo[i].ItemExp} \n");

            
            }

            Console.WriteLine("=================================================================================\n\n");

            Show_Inven_Menu(player);

            

        }
        private void Show_Inven_Menu(Player player)
        {
            do
            {
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("-1. 나가기");
                Console.Write("\n원하시는 행동을 숫자로 입력해주세요.\n");
                Console.Write(">>");
                actIsNum = int.TryParse(Console.ReadLine(), out act);
            } while (!actIsNum);

            switch (act)
            {
                case 1: //장착 관리
                    Item_manage(player); // 장착관리 입성

                    break;
                case -1:
                    Console.WriteLine("\n\n");
                    
                    
                    //아무것도 하지 않고 break 할 경우 SelectAct() 로 돌아감
                    break;

                default:
                    Console.WriteLine("\n☆====잘못된 입력입니다. 다시 입력해주세요====☆");
                    player.CharInfo();
                    break;
            }
        }

        public void Item_manage(Player player) // 장착 관리 실행
        {
            

            Console.WriteLine("\n\n=================================================================================");
            Console.WriteLine("★인벤토리 - 장착 관리★\n");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("           [아이템 목록]           \n");
            
            for (int i = 0; i < ItemInfo.Count; i++) // 인벤토리 표시
            {

                Console.Write($"{i} ");
                if (ItemInfo[i].IsEquip) Console.Write("[E]");
                Console.Write($"{ItemInfo[i].ItemName}");
                Console.Write($" X {ItemInfo[i].Amount}|");
                if (ItemInfo[i].ItemAtk > 0) Console.Write($" 공격력 +{ItemInfo[i].ItemAtk.ToString("D4")} |");
                if (ItemInfo[i].ItemDef > 0) Console.Write($" 방어력 +{ItemInfo[i].ItemDef.ToString("D4")} |");
                if (ItemInfo[i].ItemExp != null) Console.Write($"  {ItemInfo[i].ItemExp} \n");


            }

            Console.WriteLine("==================================================================================\n\n");


            Item_manage_Menu(player);

        }
        private void Item_manage_Menu(Player player)
        {
            act = 0;
            do
            {
                Console.WriteLine("-1. 나가기");
                Console.Write("\n원하시는 행동을 숫자로 입력해주세요. 장비장착을 원하시면 해당 장비의 번호를 입력하세요. \n");
                Console.Write(">>");
                actIsNum = int.TryParse(Console.ReadLine(), out act);
            } while (!actIsNum);

            switch (act)
            {
                case -1: //장착 관리
                    Show_Inven(player); // 장착관리 입성

                    break;
                default:

                    // 아이템의 장착여부를 바꿈
                    if (act > ItemInfo.Count - 1 || act <= -1) // 지정된 ItemData의 범위를 넘어섰을 경우 Error 발생 출력 후 재실행
                    {
                        Console.Clear(); // 
                        Console.WriteLine("☆존재하지 않는 아이템을 선택하셨습니다.☆");
                        Item_manage(player);
                    }


                    if (ItemInfo[act] != null)
                    {
                        if (ItemInfo[act].IsEquip) // 장착되고 있었을 경우 해제
                        {
                            Console.WriteLine($"{ItemInfo[act].ItemName} 을(를) 해제합니다.\n");
                            ItemInfo[act].IsEquip = !ItemInfo[act].IsEquip;
                        }
                        else if (ItemInfo[act].Amount == 0) // 아이템이 존재하지 않습니다.
                        {
                            Console.WriteLine($"☆아이템이 존재하지 않습니다.☆\n");
                        }
                        else // 장착되지 않았을 경우 ()
                        {
                            if (ItemInfo[act].ItemType == 4) // 소모품일 경우 명력 거부
                            {
                                Console.WriteLine("☆소모품은 장착할 수 없습니다.☆");
                            }
                            else // 장착 가능한 용품일 경우
                            {
                                for (int i = 0; i < ItemInfo.Count; i++) // 같은 타입의 장비가 장착되있는지 검사
                                {
                                    if (ItemInfo[i].IsEquip && ItemInfo[act].ItemName != ItemInfo[i].ItemName &&
                                        ItemInfo[act].ItemType == ItemInfo[i].ItemType) // 찾으면 같은 타입 장비 해제 and 선택한 장비 장착
                                    {
                                        Console.WriteLine($"☆{ItemInfo[i].ItemName} 을(를) 해제하고 {ItemInfo[act].ItemName} 을(를) 장착합니다.☆\n");
                                        ItemInfo[i].IsEquip = false;
                                        ItemInfo[act].IsEquip = true;

                                        Item_manage(player);
                                        break; // 혹시 모를 break 선언
                                    }

                                }
                                if (act != -1)
                                {
                                    ItemInfo[act].IsEquip = true; // 다찾아도 없으면 그냥 장착
                                    Console.WriteLine($"☆{ItemInfo[act].ItemName} 을(를) 장착합니다.☆\n");
                                }
                            }


                        }
                         // 자기 자신의 역 , 장비 장착 상태 변경
                    }

                    Item_manage(player);
                    break;
            }
        }

        public (int,int) Item_Ability_Total() // 아이템으로 인해 증가하는 능력치의 총합
        {
            int atk = 0;
            int def = 0;

            for (int i = 0; i < ItemInfo.Count; i++) // 인벤토리 표시
            {
                if (ItemInfo[i].IsEquip == true)
                {
                    atk += ItemInfo[i].ItemAtk;
                    def += ItemInfo[i].ItemDef;
                }
            }
            return (atk, def);

        }



    }

    
}
