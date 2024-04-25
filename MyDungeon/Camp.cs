using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDungeon
{
    public class Camp
    {
        Program program = new Program();
        int act = 0;
        bool actIsNum = false;
        int price = 500;

        public Camp()
        {
            price = 500;
        }
            
        public void Camping(Player player)
        {
            Console.WriteLine("\n\n==================================================================================\n");
            Console.WriteLine("★[휴식하기]★\n");
            Console.Write($"{price} G 를 내면 체력을 회복할 수 있습니다.");
            Console.Write($"(보유 골드 : {player.stat.Gold} G) \n\n");

            Console.WriteLine("-1. 나가기");
            Console.WriteLine("0. 휴식하기");
            


            Console.WriteLine("\n\n==================================================================================\n\n");

            do
            {

                Console.WriteLine("\n원하시는 행동을 숫자로 입력해주세요 : ");
                Console.Write(">>");
                actIsNum = int.TryParse(Console.ReadLine(), out act);
            } while (!actIsNum);


            switch (act)
            {
                case -1: // 나가기         
                    break;
                case 0: // 휴식하기

                    if (player.stat.Gold >= 500)
                    {
                        Console.WriteLine("\n\n==================================================================================\n");
                        Console.WriteLine($"{price} G 를 내고 휴식을 진행합니다.");
                        Console.WriteLine("\n[휴식 결과]\n");
                        Console.WriteLine($"체력 {player.stat.Hp} -> 100");
                        Console.WriteLine($"Gold {player.stat.Gold} G-> {player.stat.Gold - price} ");
                        player.stat.Hp = 100;
                        player.stat.Gold -= 500;
                        Console.WriteLine("\n\n==================================================================================\n");
                        program.SelectAct(player);
                        
                    }
                    else
                    {
                        Console.WriteLine("\n\n==================================================================================\n");
                        Console.WriteLine("돈이 부족합니다!! 거지뇨속");
                        Console.WriteLine("\n\n==================================================================================\n");
                    }
            
                    break;
               
                
                default:
                    Console.WriteLine("\n====잘못된 입력입니다. 다시 입력해주세요====");
                    Camping(player);
                    break;

            }

        }
   
    }
}
