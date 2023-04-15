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
            RegisterButtonEvent("Btn_Shop", 商店);
            RegisterButtonEvent("Btn_Setting", 设置);
        }

        public void 商店() 
        {
            OpenUIForm(ProConst.UI_SHOP);
        }
        public void 背包()
        {

        }
        public void 任务()
        {

        }
        public void 设置()
        {
            OpenUIForm(ProConst.UI_SETTING);
        }

    }
}
