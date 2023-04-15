using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;

namespace Demo
{
    public class ShopUIForm : BaseUIForm
    {
        void Awake()
        {
            //base.CurrentUIType.UIForm_Type = UIFormType.PopUp;
            //base.CurrentUIType.UIForm_ShowMode = UIFormShowMode.ReverseChange;
            SettingUIFormTypeByEditors();
            RegisterButtonEvent("Btn_Close", 返回);
            RegisterButtonEvent("Btn_Hat", 帽子);
            RegisterButtonEvent("Btn_Jacket", 上衣);
            RegisterButtonEvent("Btn_Pants", 裤子);
            RegisterButtonEvent("Btn_Shoe", 鞋子);
        }


        public void 返回()
        {
            CloseUIForm(ProConst.UI_SHOP);
        }
        public void 帽子()
        {
            OpenUIForm(ProConst.UI_ITEMDETAILS);
            string[] array = new string[] { "帽子", "帽子是一种头部装饰物，可以给角色增添个性和风格。在玄幻游戏中，帽子通常具有某些特殊属性或效果，如增加角色的攻击力、防御力、移动速度等", "100"};
            SendMessage("Item", "", array);
        }

        public void 上衣()
        {
            OpenUIForm(ProConst.UI_ITEMDETAILS);
            string[] array = new string[] { "上衣", "上衣是一种身体上装，可以覆盖角色的上半身部分。在玄幻游戏中，上衣通常可以提升角色的防御力，或者增加角色的特殊属性，如魔法抵抗能力、暴击率等。", "100" };
            SendMessage("Item", "", array);
        }
        public void 裤子()
        {
            OpenUIForm(ProConst.UI_ITEMDETAILS);
            string[] array = new string[] { "裤子", "裤子是一种下身装备，用于覆盖觉得的下半身部分。在玄幻游戏中，裤子通常可以帮助角色提高防御力和移动速度，也可能具有一些特殊的属性，如抗寒、抗水等。", "100" };
            SendMessage("Item", "", array);

        }
        public void 鞋子()
        {
            OpenUIForm(ProConst.UI_ITEMDETAILS);
            string[] array = new string[] { "鞋子", "鞋子是一种脚部装备，用于保护角色脚部，同时也可以赋予人物更快的移动速度和敏捷度。在玄幻游戏中，鞋子通常可以提高角色的移动速度和跳跃能力，并且还可能具有一些特殊属性，如踏击力、闪避等。", "100" };
            SendMessage("Item", "", array);
        }
    }
}
