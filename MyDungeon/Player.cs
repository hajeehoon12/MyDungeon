using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDungeon
{
    [Serializable]public class Player
    {
        public string Name; // 이름 저장용
        public Status stat; // 상태창 저장용
        Market market;
        Dungeon dungeon;
        Camp camp;
        Program program;

        public Inventory inven; // 플레이어 인벤토리
        int atkinc = 0;
        int definc = 0;

        public Player(string name)
        {
            Name = name;
            stat = new Status(Name);
            inven = new Inventory(name);
            market = new Market("초보자상점");
            dungeon = new Dungeon();
            camp = new Camp();
            program = new Program();

            Console.WriteLine("생성된 캐릭터의 정보를 출력합니다.");
            stat.Show_stat(); // 생성할 때, 캐릭터 정보를 출력

        }

        

        public void CharInfo() // 캐릭터 상태창 정보
        {
            // 캐릭터 상태창을 띄우기전에 장비한 아이템의 정보가 반영되어야함

            (atkinc,definc) = inven.Item_Ability_Total(); // 상태창을 보여주기 전에 아이템 능력치의 총합을 반영함

            if (atkinc == 0 && definc == 0) // 아이템으로 인한 능력치 변화가 없을 때
            {
                stat.Show_stat();   // 상태창
            }
            else
            {
                stat.Show_stat(atkinc, definc); // 능력치 변화의 존재
            }

            
            stat.Stat_menu();   // 상태창 관리 메뉴
        }
        public void InvenInfo(Player player) // 캐릭터 인벤토리 정보
        {
            //Program.instance.SelectAct();
            inven.Show_Inven(player);
        }

        public void MarketVisit(Player player) // 상점 방문
        {
            market.Show_Market(player) ;
        }

        public void ItemAmount_Change(ItemData itemData, int amt_change) // 플레이어가 획득한 아이템 추가
        {
            for (int i = 0; i < inven.ItemInfo.Count; i++) // 인벤토리 표시
            {
                if (inven.ItemInfo[i].ItemName == itemData.ItemName) // 같은 것이 존재한다면 수량만 추가
                {
                    inven.ItemInfo[i].Amount += amt_change;
                    if (inven.ItemInfo[i].Amount == 0)
                    {
                        inven.ItemInfo[i].IsEquip = false;
                    }
                    return;
                }
                

            }
            inven.ItemInfo.Add(itemData);

        }
        public void GoDungeon(Player player, int level)
        {
            dungeon.Dungeon_Menu(player);
        }
        public void DoCamping(Player player)
        {
            camp.Camping(player);
            
        }

    }
}
