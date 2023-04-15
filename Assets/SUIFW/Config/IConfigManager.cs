using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    /// <summary>
    /// ͳ�����ù������ӿ�
    /// </summary>
    public interface IConfigManager
    {
        /// <summary>
        /// Ӧ�����ã�ֻ����
        /// ���ܣ��õ���ֵ�Լ�������
        /// </summary>
        Dictionary<string, string> Appsetting { get; }

        /// <summary>
        /// �õ������ļ��������
        /// </summary>
        /// <returns></returns>
        int GetAppSettingMaxNumber();
    }

    /// <summary>
    /// ������Ϣ
    /// </summary>
    [System.Serializable]
    internal class KeyValuesInfo
    {
        /// <summary>
        /// ������Ϣ�б�
        /// </summary>
        public List<KeyValuesNode> ConfigInfo = null;
    }

    /// <summary>
    /// ������Ϣ�ڵ�
    /// </summary>
    [System.Serializable]
    internal class KeyValuesNode 
    {
        public string Key = null;
        public string Value = null;
    }
}
