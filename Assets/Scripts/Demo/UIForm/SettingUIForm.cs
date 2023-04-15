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
            RegisterButtonEvent("Btn_AudioSetting", ����);
            RegisterButtonEvent("Btn_VideoSetting", ��Ƶ);
            RegisterButtonEvent("Btn_Close", ����);
        }

        public void ����()
        {
            CloseUIForm(ProConst.UI_SETTING);
        }
        public void ����()
        {
            OpenUIForm(ProConst.UI_SETTING_AUDIO);
        }
        public void ��Ƶ()
        {
            OpenUIForm(ProConst.UI_SETTING_VIDEO);
        }
    }
}