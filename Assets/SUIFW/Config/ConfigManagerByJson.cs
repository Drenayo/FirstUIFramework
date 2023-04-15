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
        /// 得到AppSetting的最大值
        /// </summary>
        /// <returns></returns>
        public int GetAppSettingMaxNumber()
        {
            if (_AppSetting != null && _AppSetting.Count >= 1)
                return _AppSetting.Count;
            return 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jsonPath">Json配置文件路径</param>
        public ConfigManagerByJson(string jsonPath)
        {
            _AppSetting = new Dictionary<string, string>();
            InitAndAnalysisJson(jsonPath);
        }

        /// <summary>
        /// 初始化解析Json数据，加载到集合中
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
                // 抛出自定义异常
                throw new JsonAnlysisException("Json解析异常！");
            }

            // 把数据加载到AppSetting集合中
            foreach (KeyValuesNode nodeInfo in keyValuesInfo.ConfigInfo)
            {
                _AppSetting.Add(nodeInfo.Key, nodeInfo.Value);
            }
        }
    }
}
