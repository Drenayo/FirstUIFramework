using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    [System.Serializable]
    public class UIType
    {

        /// <summary>
        /// 是否清空 UI栈
        /// </summary>
        public bool IsClearStack = false;

        /// <summary>
        /// UI窗体位置类型
        /// </summary>
        public UIFormType UIForm_Type = UIFormType.Normal;
        /// <summary>
        /// UI窗体显示类型
        /// </summary>
        public UIFormShowMode UIForm_ShowMode = UIFormShowMode.Normal;
        /// <summary>
        /// UI窗体透明度类型
        /// </summary>
        public UIFormLucenyType UIForm_LucencyType = UIFormLucenyType.Lucency;
    }
}
