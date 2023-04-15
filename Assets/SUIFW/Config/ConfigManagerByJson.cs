using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    public class ConfigManagerByJson : IConfigManager
    {
        private static Dictionary<string, string> _AppSetting;
        public Dictionary<string, string> Appsetting { get { return _AppSetting; } }

        /// <summary>
        /// �õ�AppSetting�����ֵ
        /// </summary>
        /// <returns></returns>
        public int GetAppSettingMaxNumber()
        {
            if (_AppSetting != null && _AppSetting.Count >= 1)
                return _AppSetting.Count;
            return 0;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="jsonPath">Json�����ļ�·��</param>
        public ConfigManagerByJson(string jsonPath)
        {
            _AppSetting = new Dictionary<string, string>();
            InitAndAnalysisJson(jsonPath);
        }

        /// <summary>
        /// ��ʼ������Json���ݣ����ص�������
        /// </summary>
        /// <param name="jsonPath"></param>
        private void InitAndAnalysisJson(string jsonPath)
        {
            TextAsset configInfo = null;
            KeyValuesInfo keyValuesInfo = null;

            try
            {
                configInfo = Resources.Load<TextAsset>(jsonPath);
                keyValuesInfo = JsonUtility.FromJson<KeyValuesInfo>(configInfo.text);
            }
            catch
            {
                // �׳��Զ����쳣
                throw new JsonAnlysisException("Json�����쳣��");
            }

            // �����ݼ��ص�AppSetting������
            foreach (KeyValuesNode nodeInfo in keyValuesInfo.ConfigInfo)
            {
                _AppSetting.Add(nodeInfo.Key, nodeInfo.Value);
            }
        }
    }
}
