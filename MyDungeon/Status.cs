using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDungeon
{
    public class Status
    {



        public int Level { get; set; }
        string Name { get; set; }
        public float Attack { get; set; }
        public int Defense { get; set; }
        public int Hp { get; set; }
        public float Gold { get; set; }

        public int MaxHp { get; set; }

        public int Exp { get; set; }

        public int AttackInc { get; set; }
        public int DefInc { get; set; }


        string job = "전사";

        public Status(string name)
        {
            Name = name;
            Level = 1;
            Attack = 10;
            Defense = 8;
            Hp = 100;
            Gold = 1000;
            Exp = 0;
            MaxHp = 100;
        }

        public void Show_stat()
        {


            Console.WriteLine("\r\n ######  ########    ###    ######## ##     ##  ######  \r\n##    ##    ##      ## ##      ##    ##     ## ##    ## \r\n##          ##     ##   ##     ##    ##     ## ##       \r\n ######     ##    ##     ##    ##    ##     ##  ######  \r\n      ##    ##    #########    ##    ##     ##       ## \r\n##    ##    ##    ##     ##    ##    ##     ## ##    ## \r\n ######     ##    ##     ##    ##     #######   ######  \r\n");
            Console.WriteLine("\n\n캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine("=================================");
            Console.WriteLine("             [상태창]           \n");
            Console.WriteLine($"Lv. {Level}");
            Console.WriteLine($"{Name} ({job})");
            Console.WriteLine($"공격력 : {Attack}");
            Console.WriteLine($"방어력 : {Defense}");
            Console.WriteLine($"체 력 : {Hp} / {MaxHp}");
            Console.WriteLine($"Gold : {Gold}");
            Console.WriteLine("=================================\n\n");

            
        }
        public void Show_stat(int atkinc, int definc)
        {

            Console.WriteLine("\r\n ######  ########    ###    ######## ##     ##  ######  \r\n##    ##    ##      ## ##      ##    ##     ## ##    ## \r\n##          ##     ##   ##     ##    ##     ## ##       \r\n ######     ##    ##     ##    ##    ##     ##  ######  \r\n      ##    ##    #########    ##    ##     ##       ## \r\n##    ##    ##    ##     ##    ##    ##     ## ##    ## \r\n ######     ##    ##     ##    ##     #######   ######  \r\n");

            Console.WriteLine("\n\n☆캐릭터의 정보가 표시됩니다.☆\n");
            Console.WriteLine("=================================");
            Console.WriteLine("             [상태창]           \n");
            Console.WriteLine($"Lv. {Level}");
            Console.WriteLine($"{Name} ({job})");

            DefInc = definc;
            AttackInc = atkinc;

            if (atkinc != 0)
            {
                Console.WriteLine($"공격력 : {Attack} + ({atkinc})");
            }
            else
            {
                Console.WriteLine($"공격력 : {Attack}");
            }
            if (definc != 0)
            {
                Console.WriteLine($"방어력 : {Defense} + ({definc})");
            }
            else
            {
                Console.WriteLine($"방어력 : {Defense}");
            }
            Console.WriteLine($"체 력 : {Hp} / {MaxHp}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine("=================================\n\n");


        }
        public void Stat_menu()
        {
            int act;
            bool actIsNum;
            do
            {
                Console.WriteLine("-1. 나가기");

                Console.Write("\n원하시는 행동을 숫자로 입력해주세요.\n");
                Console.Write(">>");
                actIsNum = int.TryParse(Console.ReadLine(), out act);
            } while (!actIsNum);

            switch (act)
            {
                case -1: // 바깥 메뉴로 가기
                    Console.WriteLine("\n\n");
                    break;


                default:
                    Console.WriteLine("\n=====잘못된 입력입니다. 다시 입력해주세요=====");
                    Show_stat();
                    Stat_menu();
                    break;
            }
        }

        public void isLevelUp() // 레벨업 검사
        {
            if (Exp == 2 * Level) // 경험치가 최대치에 도달하면 레벨업 진행
            {
                Attack += 0.5f;
                Defense += 1;
                Level += 1;
                Exp = 0;
                MaxHp += 20;
            }
        }

    }
}
