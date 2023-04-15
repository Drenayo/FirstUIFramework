using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;

namespace Demo
{
    public class StartDemo : MonoBehaviour
    {
        // 添加一句注释，试试水
        void Start()
        {
            UIManager.GetInstance().ShowUIForms(ProConst.UI_LOGON);
        }

        private void OnGUI()
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 30;
            style.normal.textColor = Color.yellow;

            GUI.Label(new Rect(0, 0, 50, 20), "全部窗体：" + UIManager.GetInstance().GetAllUIFormsCount().ToString(), style);
            GUI.Label(new Rect(0, 40, 50, 20), "当前窗体:" + UIManager.GetInstance().GetCurrentUIFormsCount().ToString(), style);
            GUI.Label(new Rect(0, 80, 50, 20), "栈窗体：" + UIManager.GetInstance().GetStackUIFormsCount().ToString(), style);
        }
    }
}
