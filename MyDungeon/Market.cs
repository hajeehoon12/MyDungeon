using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyDungeon
{
    public class Market
    {
        int act;
        bool actIsNum;
        ItemData tempItem;
        Program program;

        string market_Name = "상점명 초기화전"; // 상점명 초기화 전 Default 값

        List<ItemData> Market_Item = new List<ItemData>(); // 상점에 저장할 아이템의 정보


        public Market(string name) // 초기화값
        {
            program = new Program();
            // Market_Item 구성 (아이템타입(오른손,왼손,목걸이,방어구,소모품), 개체명, 공격력, 방어력, 금액, 수량(보유여부), 설명)
            market_Name = name;

            Market_Item.Add(new ItemData(0, "철검", 20, 0, 5000, 1, "철로 만든 검"));
            Market_Item.Add(new ItemData(3, "가죽갑옷", 0, 10, 2000, 1, "질긴가죽으로 만든 가죽갑옷"));
            Market_Item.Add(new ItemData(1, "튼튼한방패", 5, 10, 3000, 1, "가벼운 공격은 막을 것 같은 튼튼한 방패"));
            Market_Item.Add(new ItemData(2, "세련된목걸이", 3, 3, 10000, 1, "세공이 잘 된 목걸이"));

            Market_Item.Add(new ItemData(0, "강철검", 30, 0, 10000, 1, "무겁지만 무엇이라도 베어버릴것 같은 날카로운 검"));
            Market_Item.Add(new ItemData(3, "강철 갑옷", 0, 20, 7000, 1, "왠만한 공격은 다 막아낼 것 같은 강철로 만든 갑옷"));
            Market_Item.Add(new ItemData(1, "강철 방패", 10, 10, 5000, 1, "무겁지만 제성능을 톡톡히 해내는 강철로 만든 방패"));
            Market_Item.Add(new ItemData(2, "화려한목걸이", 15, 15, 30000, 1, "사용자에게 미지의 힘을 화려한 목걸이"));

            Market_Item.Add(new ItemData(4, "회복물약", 0, 0, 1000, 99, "사용자의 상처를 치유하고 활력이 돋게 하는 물약"));

            

        }

        public ItemData DeepCopy(int act) // 아이템 정보만 복사하기 위한 함수
        {
            ItemData tempItem = new ItemData(0, "0", 0, 0, 0, 0, "0");
            tempItem = new ItemData(Market_Item[act].ItemType, $"{Market_Item[act].ItemName}", Market_Item[act].ItemAtk, Market_Item[act].ItemDef, Market_Item[act].ItemPrice, 1, $"{Market_Item[act].ItemExp}");
            return tempItem;
        }




        public void Show_Market(Player player)
        {
            Console.WriteLine("\r\n ######  ########  #######  ########  ######## \r\n##    ##    ##    ##     ## ##     ## ##       \r\n##          ##    ##     ## ##     ## ##       \r\n ######     ##    ##     ## ########  ######   \r\n      ##    ##    ##     ## ##   ##   ##       \r\n##    ##    ##    ##     ## ##    ##  ##       \r\n ######     ##     #######  ##     ## ######## \r\n");
            Console.WriteLine("\n\n=================================================================================");
            Console.WriteLine($" ★{market_Name}★ \n "); // 상점명
            
            Console.WriteLine($"       [보유 골드] : {player.stat.Gold} G        \n"); // 플레이어 보유 골드 표시
            Console.WriteLine("           [아이템 목록]           \n");

            for (int i = 0; i < Market_Item.Count; i++) // 인벤토리 표시
            {

                Console.Write("- ");
                
                Console.Write($"{Market_Item[i].ItemName}");
                Console.Write($" X {Market_Item[i].Amount}|");
                Console.Write($" 공격력 +{Market_Item[i].ItemAtk.ToString("D4")} |");
                Console.Write($" 방어력 +{Market_Item[i].ItemDef.ToString("D4")} |");
                if (Market_Item[i].ItemExp != null) Console.Write($"  {Market_Item[i].ItemExp} ");
                if (Market_Item[i].Amount != 0)
                {
                    if (Market_Item[i].ItemPrice != null) Console.Write($"| {Market_Item[i].ItemPrice} G");
                }
                else
                {
                    Console.Write("| ☆구매완료!☆");
                }
                Console.WriteLine(" "); //줄바꿈용
            }
            Console.WriteLine("=================================================================================\n\n");
            Market_Menu(player); // 상점 선택창

        }

        private void Market_Menu(Player player) // 상점에서 메뉴 선택창
        {
            
            do
            {
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("-1. 나가기");

                Console.Write("\n원하시는 행동을 숫자로 입력해주세요.\n");
                Console.Write(">>");
                actIsNum = int.TryParse(Console.ReadLine(), out act);
                Console.Clear();
            } while (!actIsNum);

            switch (act)
            {
                case -1: // 바깥 메뉴로 가기
                    Console.WriteLine("\n\n☆상점을 나갔습니다.☆");
                    break;
                case 1: // 아이템 구매
                    Market_Purchase_Menu(player);
                    break;
                case 2: // 아이템 판매
                    Market_Sale(player);
                    break;
                default:
                    Console.WriteLine("\n=====잘못된 입력입니다. 다시 입력해주세요=====");
                    Market_Menu(player);
                    break;
            }
        }

        private void Market_Purchase_Menu(Player player) // 상점 구매창
        {



            Console.WriteLine("\r\n########  ##     ## ########   ######  ##     ##    ###     ######  ######## \r\n##     ## ##     ## ##     ## ##    ## ##     ##   ## ##   ##    ## ##       \r\n##     ## ##     ## ##     ## ##       ##     ##  ##   ##  ##       ##       \r\n########  ##     ## ########  ##       ######### ##     ##  ######  ######   \r\n##        ##     ## ##   ##   ##       ##     ## #########       ## ##       \r\n##        ##     ## ##    ##  ##    ## ##     ## ##     ## ##    ## ##       \r\n##         #######  ##     ##  ######  ##     ## ##     ##  ######  ######## \r\n");
            Console.WriteLine("\n\n=================================================================================");
            Console.WriteLine($" ★{market_Name} - 아이템 구매 ★\n"); // 상점명
            
            
            Console.WriteLine($"       [보유 골드] : {player.stat.Gold} G        \n"); // 플레이어 보유 골드 표시
            Console.WriteLine("           [아이템 목록]           \n");

            for (int i = 0; i < Market_Item.Count; i++) // 인벤토리 표시
            {

                Console.Write($"{i} ");

                Console.Write($"{Market_Item[i].ItemName}");
                Console.Write($" X {Market_Item[i].Amount}");
                Console.Write($"|  공격력 +{Market_Item[i].ItemAtk.ToString("D4")} |");
                Console.Write($" 방어력 +{Market_Item[i].ItemDef.ToString("D4")} |");
                if (Market_Item[i].ItemExp != null) Console.Write($"  {Market_Item[i].ItemExp} ");
                if (Market_Item[i].Amount != 0)
                {
                    if (Market_Item[i].ItemPrice != null) Console.Write($"| {Market_Item[i].ItemPrice} G");
                }
                else
                {
                    Console.Write("| ☆매진됨!☆");
                }
                Console.WriteLine(" "); //줄바꿈용
            }
            Console.WriteLine("=================================================================================\n\n");



            Market_Purchase_Menu_Menu(player);

        }
        private void Market_Purchase_Menu_Menu(Player player) // 마켓 구매 단계에서의 행동 출력
        {
            act = 0;
            do
            {

                Console.WriteLine("-1. 나가기");

                Console.Write("\n원하시는 행동을 숫자로 입력해주세요. 아이템 구매를 원하시면 해당 아이템 번호를 입력하세요.\n");
                Console.Write(">>");
                actIsNum = int.TryParse(Console.ReadLine(), out act);
                Console.Clear();
            } while (!actIsNum);

            switch (act)
            {
                case -1: // 바깥 메뉴로 가기
                    Console.WriteLine("\n\n ☆상점을 나갑니다 총총... ☆\n");
                    program.SelectAct(player);
                    break;
                default:


                    if (act > Market_Item.Count - 1 || act < -1) // 상점의 아이템 데이터 범위를 벗어날 경우
                    {
                        Console.Clear();
                        Console.WriteLine("☆존재하지 않는 아이템을 선택하셨습니다.☆");
                        Market_Purchase_Menu(player);
                    }
                    if (Market_Item[act] != null)
                    {
                        if (Market_Item[act].Amount == 0) // 재고 없음
                        {
                            Console.WriteLine("☆매진된 아이템입니다.☆");
                        }
                        else if (player.stat.Gold < Market_Item[act].ItemPrice) // 골드 부족
                        {
                            Console.WriteLine("☆Gold 가 부족합니다.☆");
                            Market_Purchase_Menu(player);
                        }
                        else if (player.stat.Gold >= Market_Item[act].ItemPrice || Market_Item[act].Amount > 0) // 정상구매
                        {
                            player.stat.Gold -= Market_Item[act].ItemPrice;
                            Console.WriteLine($"\n☆\"{Market_Item[act].ItemName}\" 아이템을 구매했습니다.☆");
                            Market_Item[act].Amount -= 1;
                            tempItem = DeepCopy(act);
                            player.ItemAmount_Change(tempItem, 1); // 사는 아이템 정보, 갯수
                        }                 
                        else
                        {
                            Console.WriteLine("☆불길한 오류입니다.☆");  // 알 수 없는 오류
                            Market_Purchase_Menu(player);
                        }

                    }
                    Market_Purchase_Menu(player);
                    break;
            }
        }

        private void Market_Sale(Player player) // 상점에서의 판매 창
        {
            List<ItemData> ItemInfo = new List<ItemData>();
            ItemInfo = player.inven.ItemInfo; // 얕은 복사 , 플레이어 인벤창 불러오기

            Console.WriteLine("\n\n=================================================================================");
            Console.WriteLine($"★{market_Name} - 판매★\n");
            Console.WriteLine("           [아이템 목록]           \n");
            Console.WriteLine($"\n\n보유골드 : {player.stat.Gold}\n");

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

            Console.WriteLine("=================================================================================\n\n");
            Market_Sale_Menu(player);
        }
        private void Market_Sale_Menu(Player player)
        {

            
            List<ItemData> tempItemInfo = player.inven.ItemInfo; // 임시 값 복사, 플레이어의 인벤 정보 불러오기

            do
            {

                Console.WriteLine("-1. 나가기");

                Console.Write("\n원하시는 행동을 숫자로 입력해주세요. 아이템 구매를 원하시면 해당 아이템 번호를 입력하세요.\n");
                Console.Write(">>");
                actIsNum = int.TryParse(Console.ReadLine(), out act);
            } while (!actIsNum);

            switch (act)
            {
                case -1: // 바깥 메뉴로 가기
                    Console.WriteLine("\n\n 조금 더 물건을 둘러봅니다.");
                    break;
                default:


                    if (act > tempItemInfo.Count - 1 || act < -1) // 상점의 아이템 데이터 범위를 벗어날 경우
                    {
                        Console.Clear();
                        Console.WriteLine("존재하지 않는 아이템을 선택하셨습니다.");
                        Market_Sale_Menu(player);
                    }
                    if (tempItemInfo[act] != null)
                    {
                        if (tempItemInfo[act].Amount == 0) // 재고 없음
                        {
                            Console.WriteLine("매진된 아이템입니다.");
                        }
                        else if (player.stat.Gold >= tempItemInfo[act].ItemPrice || tempItemInfo[act].Amount > 0) // 정상구매
                        {
                            
                            player.stat.Gold += (int)((tempItemInfo[act].ItemPrice) * 0.85);

                            Console.WriteLine($"\"{tempItemInfo[act].ItemName}\" 아이템을 판매했습니다.");

                            ItemData choosedItem = new ItemData(tempItemInfo[act].ItemType, $"{tempItemInfo[act].ItemName}",
                                tempItemInfo[act].ItemAtk, tempItemInfo[act].ItemDef, tempItemInfo[act].ItemPrice,
                                1, $"{tempItemInfo[act].ItemExp}");

                            player.ItemAmount_Change(choosedItem, -1); // 사는 아이템 정보, 갯수
                            Market_ItemChange(choosedItem, 1);

                        }
                        else if (player.stat.Gold < tempItemInfo[act].ItemPrice) // 골드 부족
                        {
                            Console.WriteLine("Gold 가 부족합니다.");
                        }
                        else
                        {
                            Console.WriteLine("불길한 오류입니다.");  // 알 수 없는 오류
                        }

                    }
                    Market_Sale(player);
                    break;
            }

        }

        public void Market_ItemChange(ItemData itemData, int amt_change) // 마켓에 아이템 추가, (아이템 정보, 변화될 아이템 수량)
        {
            for (int i = 0; i < Market_Item.Count; i++) // 인벤토리 표시
            {
                if ( Market_Item[i].ItemName == itemData.ItemName ) // 같은 것이 존재한다면 수량만 추가
                {
                    Market_Item[i].Amount += amt_change;
                    return;
                }

            }
            Market_Item.Add(itemData);

        }

    }

}
