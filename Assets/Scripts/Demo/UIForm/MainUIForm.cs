using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;

namespace Demo
{
    public class MainUIForm : BaseUIForm
    {
        void Awake()
        {
            //SettingUIFormType(UIFormType.Normal, UIFormShowMode.Normal, UIFormLucenyType.Lucency);
            SettingUIFormTypeByEditors();
            RegisterButtonEvent("Btn_Shop", �̵�);
            RegisterButtonEvent("Btn_Setting", ����);
        }

        public void �̵�() 
        {
            OpenUIForm(ProConst.UI_SHOP);
        }
        public void ����()
        {

        }
        public void ����()
        {

        }
        public void ����()
        {
            OpenUIForm(ProConst.UI_SETTING);
        }

    }
}
