using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    [System.Serializable]
    public class UIType
    {

        /// <summary>
        /// �Ƿ���� UIջ
        /// </summary>
        public bool IsClearStack = false;

        /// <summary>
        /// UI����λ������
        /// </summary>
        public UIFormType UIForm_Type = UIFormType.Normal;
        /// <summary>
        /// UI������ʾ����
        /// </summary>
        public UIFormShowMode UIForm_ShowMode = UIFormShowMode.Normal;
        /// <summary>
        /// UI����͸��������
        /// </summary>
        public UIFormLucenyType UIForm_LucencyType = UIFormLucenyType.Lucency;
    }
}
