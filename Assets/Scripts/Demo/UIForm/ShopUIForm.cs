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
            RegisterButtonEvent("Btn_Close", ����);
            RegisterButtonEvent("Btn_Hat", ñ��);
            RegisterButtonEvent("Btn_Jacket", ����);
            RegisterButtonEvent("Btn_Pants", ����);
            RegisterButtonEvent("Btn_Shoe", Ь��);
        }


        public void ����()
        {
            CloseUIForm(ProConst.UI_SHOP);
        }
        public void ñ��()
        {
            OpenUIForm(ProConst.UI_ITEMDETAILS);
            string[] array = new string[] { "ñ��", "ñ����һ��ͷ��װ������Ը���ɫ������Ժͷ����������Ϸ�У�ñ��ͨ������ĳЩ�������Ի�Ч���������ӽ�ɫ�Ĺ����������������ƶ��ٶȵ�", "100"};
            SendMessage("Item", "", array);
        }

        public void ����()
        {
            OpenUIForm(ProConst.UI_ITEMDETAILS);
            string[] array = new string[] { "����", "������һ��������װ�����Ը��ǽ�ɫ���ϰ����֡���������Ϸ�У�����ͨ������������ɫ�ķ��������������ӽ�ɫ���������ԣ���ħ���ֿ������������ʵȡ�", "100" };
            SendMessage("Item", "", array);
        }
        public void ����()
        {
            OpenUIForm(ProConst.UI_ITEMDETAILS);
            string[] array = new string[] { "����", "������һ������װ�������ڸ��Ǿ��õ��°����֡���������Ϸ�У�����ͨ�����԰�����ɫ��߷��������ƶ��ٶȣ�Ҳ���ܾ���һЩ��������ԣ��翹������ˮ�ȡ�", "100" };
            SendMessage("Item", "", array);

        }
        public void Ь��()
        {
            OpenUIForm(ProConst.UI_ITEMDETAILS);
            string[] array = new string[] { "Ь��", "Ь����һ�ֽŲ�װ�������ڱ�����ɫ�Ų���ͬʱҲ���Ը������������ƶ��ٶȺ����ݶȡ���������Ϸ�У�Ь��ͨ��������߽�ɫ���ƶ��ٶȺ���Ծ���������һ����ܾ���һЩ�������ԣ���̤���������ܵȡ�", "100" };
            SendMessage("Item", "", array);
        }
    }
}
