using System.Numerics;

namespace MyDungeon
{
    public class Program
    {
        
        
        
        public void SelectAct(Player player) // 메뉴 선택
        {
           

            int act; //메뉴
            bool actIsNum;
            


            do
            {

                Console.WriteLine("      ######  ####### ######   #####  #######    #    ######  " +
                    "          \r\n     #     # #       #     # #     #    #      # #   #     # \r\n" +
                                  "    #     # #       #     # #          #     #   #  #     # \r\n" +
                                  "   # ## #  # # # # #     #  # # #     #    #     # # # #  \r\n" +
                                  "  #   #   #       #     #       #    #    # # # # #    #   \r\n" +
                                  " #    #  #       #     # #     #    #    #     # #     #  \r\n" +
                                  "#     # ####### ######   #####     #    #     # #      # \r\n\n"); // 아스키아트


                Console.WriteLine($"탐험가 ★{player.Name}★님 REDSTAR 마을에 오신 여러분 환영합니다!!" +
                "\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

                Console.WriteLine("\n1. 상태 보기 \n2. 인벤토리 \n3. 상점 \n4. 던전 \n5. 휴식하기");
                Console.Write("\n원하시는 행동을 숫자로 입력해주세요 : ");
                actIsNum = int.TryParse(Console.ReadLine(), out act);
            } while (!actIsNum);

            switch (act)
            {
                case 1: // 상태보기
                    Console.WriteLine("\n☆상태보기가 선택되었습니다.☆");
                    player.CharInfo();
                    SelectAct(player);
                    break;
                case 2:
                    Console.WriteLine("\n☆인벤토리가 선택되었습니다.☆");
                    player.InvenInfo(player);
                    SelectAct(player);
                    break;
                case 3:
                    Console.WriteLine("\n☆상점이 선택되었습니다.☆");
                    player.MarketVisit(player);
                    SelectAct(player);
                    break;
                case 4:
                    Console.WriteLine("\n☆던전이 선택되었습니다.☆");
                    player.GoDungeon(player, 1);
                    break;
                case 5:
                    Console.WriteLine("\n☆휴식이 선택되었습니다.☆");
                    player.DoCamping(player);
                    break;
                case 6:
                    Console.WriteLine();
                    // 저장하기 코드 입력할것
                    SelectAct(player); // 저장하고 메인메뉴로 다시
                    break;

                default:
                    Console.WriteLine("\n☆====잘못된 입력입니다. 다시 입력해주세요====☆");
                    SelectAct(player);
                    break;

            }
            SelectAct(player);
            
        }



        static void Main()
        {
            

            string playerName;

            Console.WriteLine("              ..######..########.....##.....#######...########....##...." +
                         "\r\n             .##.....#.##.....##...##.##...##.....##....##......##.##.." +
                         "\r\n            .##.......##.....##..##...##..##.....##....##.....##...##." +
                         "\r\n           ..######..########..##.....##.########.....##....##.....##" +
                         "\r\n          .......##.##........#########.##...##......##....#########" +
                         "\r\n         .##....##.##........##.....##.##....##.....##....##.....##" +
                         "\r\n        ..######..##........##.....##.##.....##....##....##.....##");
             
            Console.WriteLine("\r\n      .########..##.....##.##....##..######...########..#######..##....##" +
                              "\r\n     .##.....##.##.....##.###...##.##....##..##.......##.....##.###...##" +
                              "\r\n    .##.....##.##.....##.####..##.##........##.......##.....##.####..##" +
                              "\r\n   .##.....##.##.....##.##.##.##.##...####.######...##.....##.##.##.##" +
                              "\r\n  .##.....##.##.....##.##..####.##....##..##.......##.....##.##..####" +
                              "\r\n .##.....##.##.....##.##...###.##....##..##.......##.....##.##...###" +
                              "\r\n.########...#######..##....##..######...########..#######..##....## \r\n\n");


            Console.Write("           ☆게임을 플레이할 플레이어의 이름을 적으세요☆ : ");
            playerName = Console.ReadLine();


            Console.WriteLine($"\n\n=======당신의 플레이어 닉네임 : {playerName}======= \n\n");

            Player player = new Player(playerName);
            Program program = new Program();
            program.SelectAct(player);
        }
    }
}
