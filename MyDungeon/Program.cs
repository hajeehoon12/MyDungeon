using System.Numerics;
using System.Threading;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;


namespace MyDungeon
{
    
    public class Program
    {

        public void SavePlayerInfo(Player player) // 현재 플레이어 데이터 저장
        {
            string _fileName = player.Name+".json"; // 저장할 파일명 지정
            //string _itemFileName = "itemData.json"; // 이후 추가 직렬화 할 데이터가 있으면 쓸 양식

            string _userGameFolder = System.IO.Directory.GetParent(System.Environment.CurrentDirectory).Parent.FullName;
            Console.WriteLine($"저장될 폴더 명 : {System.IO.Directory.GetParent(System.Environment.CurrentDirectory).Parent.FullName}");
            string _filePath = Path.Combine(_userGameFolder, _fileName);
            //string _itemFilePath = Path.Combine(_userGameFolder, _itemFileName);

            string _playerJson = JsonConvert.SerializeObject(player, Formatting.Indented);
            
            File.WriteAllText(_filePath, _playerJson);
            
            Console.WriteLine("저장이 완료되었습니다.");
            Console.WriteLine($"플레이어의 정보가 해당 경로로 지정되었습니다. :{_filePath}");
            Console.WriteLine("★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★");

        }

        private void LoadGameData(Player player)
        {
            Console.WriteLine("\r\n##        #######     ###    ########  \r\n##       ##     ##   ## ##   ##     ## \r\n##       ##     ##  ##   ##  ##     ## \r\n##       ##     ## ##     ## ##     ## \r\n##       ##     ## ######### ##     ## \r\n##       ##     ## ##     ## ##     ## \r\n########  #######  ##     ## ########  \r\n");
            Console.WriteLine("불러올 플레이어명을 입력하세요.\n>>");
            string temp_name = Console.ReadLine();


            string _fileName = temp_name + ".json";

            string _userGameFolder = System.IO.Directory.GetParent(System.Environment.CurrentDirectory).Parent.FullName;

            

            string _filePath = Path.Combine(_userGameFolder, _fileName); // 저장된 파일의 예상 경로
            Console.WriteLine($"데이터를 찾는 폴더 명 : {_filePath}");
            if (File.Exists(_filePath)) // 해당 경로에 저장된 것이 있었으면
            {
                string _playerJson = File.ReadAllText(_filePath);
                player = JsonConvert.DeserializeObject<Player>(_playerJson);
                Console.WriteLine($"{temp_name} 의 게임 데이터를 불러옵니다."); // 저장된 플레이어 데이터의 플레이어명

                Thread.Sleep(1000);
                Console.WriteLine($"플레이어명 : {temp_name} 의 게임 데이터를 불러오는데 성공했습니다!!");
                Console.WriteLine("\r\n ######     ###    ##     ## ######## \r\n##    ##   ## ##   ##     ## ##       \r\n##        ##   ##  ##     ## ##       \r\n ######  ##     ## ##     ## ######   \r\n      ## #########  ##   ##  ##       \r\n##    ## ##     ##   ## ##   ##       \r\n ######  ##     ##    ###    ######## \r\n");
                SelectAct(player);

            }
            else
            {
                Console.WriteLine("\n☆해당 플레이어명으로 저장된 게임이 없습니다! 메인 메뉴로 돌아갑니다!☆");
                Console.WriteLine("★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★");

                Thread.Sleep(1000);

                SelectAct(player);
            }

            
        }


        public void SelectAct(Player player) // 메뉴 선택
        {
           

            int act; //메뉴
            bool actIsNum;
            


            do
            {

                Console.WriteLine("\r\n##     ## #### ##       ##          ###     ######   ######## \r\n##     ##  ##  ##       ##         ## ##   ##    ##  ##       \r\n##     ##  ##  ##       ##        ##   ##  ##        ##       \r\n##     ##  ##  ##       ##       ##     ## ##   #### ######   \r\n ##   ##   ##  ##       ##       ######### ##    ##  ##       \r\n  ## ##    ##  ##       ##       ##     ## ##    ##  ##       \r\n   ###    #### ######## ######## ##     ##  ######   ######## \r\n");

                

                Console.WriteLine($"탐험가 ★{player.Name}★님 REDSTAR 마을에 오신 여러분 환영합니다!!" +
                "\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

                Console.WriteLine("\n1. 상태 보기 \n2. 인벤토리 \n3. 상점 \n4. 던전 \n5. 휴식하기 \n6. 저장하기 \n7. 불러오기 \n8. 게임종료");
                Console.Write("\n★원하시는 행동을 숫자로 입력해주세요★ : ");
                actIsNum = int.TryParse(Console.ReadLine(), out act);
                Console.Clear();
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
                    Console.WriteLine("\n☆저장이 선택되었습니다.☆");
                    // 저장하기 코드 입력할것
                    SavePlayerInfo(player);
                    SelectAct(player); // 저장하고 메인메뉴로 다시
                    break;
                case 7:
                    LoadGameData(player);
                    break;
                case 8:
                    Console.WriteLine("\r\n ######      ###    ##     ## ########    ######## ##    ## ########  \r\n##    ##    ## ##   ###   ### ##          ##       ###   ## ##     ## \r\n##         ##   ##  #### #### ##          ##       ####  ## ##     ## \r\n##   #### ##     ## ## ### ## ######      ######   ## ## ## ##     ## \r\n##    ##  ######### ##     ## ##          ##       ##  #### ##     ## \r\n##    ##  ##     ## ##     ## ##          ##       ##   ### ##     ## \r\n ######   ##     ## ##     ## ########    ######## ##    ## ########  \r\n");
                    Console.WriteLine("\n☆게임 종료를 선택하셨습니다! 2초후에 종료됩니다!!☆");
                    
                    Thread.Sleep(2000);
                    if (false) // 게임 종료 취소 조건 넣을 예정
                    {
                        Console.WriteLine("\n☆게임 종료를 취소했습니다!☆"); // 취소 후 메인화면으로 복귀
                        SelectAct(player);
                        
                    }
                    else
                    {
                        Environment.Exit(0); // 게임 종료
                    }
                    
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
            Console.SetWindowSize(120, 35); // 콘솔창 크기 조절

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
