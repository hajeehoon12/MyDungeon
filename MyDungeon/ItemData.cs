using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDungeon
{
    public class ItemData
    {
        public int ItemType;          // 아이템 식별번호 였지만 장비 타입으로 임시설정 // 0. 오른손 1. 왼손 2. 양손 3. 방어구 4. 소모품
        public bool IsEquip;        // 플레이어가 장비하고 있는지
        public int Amount { get; set; }   // 플레이어가 몇개나 소유하고 있는지
        public string ItemName;     // 아이템 이름
        public int ItemAtk;     // 아이템 공격력 수치
        public int ItemDef;    // 아이템 방어력 수치
        public string ItemExp; // 아이템 설명
        public int ItemPrice { get; set; }

        public ItemData(int _itemType, string _itemName, int _itemAtk, int _itemDef,  int _itemPrice, int _Amount, string _itemExp)
        {
            ItemType = _itemType;
            ItemName = FormatAndPad(_itemName, 10 );// 8글자로 고정 패딩실시
            ItemAtk = _itemAtk;
            ItemDef = _itemDef;
            ItemExp = _itemExp;
            ItemPrice = _itemPrice;
            IsEquip = false;
            Amount = _Amount;
        }

        static string FormatAndPad(string _text, int _width)
        {
            int _remainingSpace = 0;
            if (_text != null)
            {
                _remainingSpace = _width - _text.Length;
            }
            if (_remainingSpace <= 0)
            {
                return _text;
            }
            else
            {
                int _leftPadding = _remainingSpace / 2;
                int _rightPadding = _remainingSpace - _leftPadding;
                string _formattedText = new string(' ', 2 * _leftPadding) + _text + new string(' ', 2 * _rightPadding);
                return _formattedText;
            }
        }

    }
}
