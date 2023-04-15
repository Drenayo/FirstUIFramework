using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;

namespace Demo
{
    public class SettingUIForm : BaseUIForm
    {
        void Awake()
        {
            SettingUIFormTypeByEditors();
            RegisterButtonEvent("Btn_AudioSetting", …˘“Ù);
            RegisterButtonEvent("Btn_VideoSetting",  ”∆µ);
            RegisterButtonEvent("Btn_Close", ∑µªÿ);
        }

        public void ∑µªÿ()
        {
            CloseUIForm(ProConst.UI_SETTING);
        }
        public void …˘“Ù()
        {
            OpenUIForm(ProConst.UI_SETTING_AUDIO);
        }
        public void  ”∆µ()
        {
            OpenUIForm(ProConst.UI_SETTING_VIDEO);
        }
    }
}