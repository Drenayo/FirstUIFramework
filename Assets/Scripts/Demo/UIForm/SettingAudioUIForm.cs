using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;

namespace Demo
{
    public class SettingAudioUIForm : BaseUIForm
    {
        void Awake()
        {
            SettingUIFormTypeByEditors();

            RegisterButtonEvent("Btn_Close", ����);
        }


        public void ����()
        {
            CloseUIForm(ProConst.UI_SETTING_AUDIO);
        }
    }
}