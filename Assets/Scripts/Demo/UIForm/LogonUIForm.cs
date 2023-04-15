using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SUIFW;

namespace Demo
{
    public class LogonUIForm : BaseUIForm
    {
        public Text textTitle;
        private void Awake()
        {
            //SettingUIFormType(UIFormType.Normal, UIFormShowMode.HideOther, UIFormLucenyType.Lucency);
            SettingUIFormTypeByEditors();
            RegisterButtonEvent("Btn_Logon", OnBtn_Logon);
        }

        private void Start()
        {
            textTitle.text = LauguageMgr.GetInstance().ShowText("LogonSystem");
        }

        public void OnBtn_Logon()
        {
            OpenUIForm(ProConst.UI_MAIN);
        }
    }
}
