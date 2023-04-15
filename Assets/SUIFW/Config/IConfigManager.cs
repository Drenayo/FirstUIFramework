using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    /// <summary>
    /// 统用配置管理器接口
    /// </summary>
    public interface IConfigManager
    {
        /// <summary>
        /// 应用设置（只读）
        /// 功能：得到键值对集合数据
        /// </summary>
        Dictionary<string, string> Appsetting { get; }

        /// <summary>
        /// 得到配置文件最大数量
        /// </summary>
        /// <returns></returns>
        int GetAppSettingMaxNumber();
    }

    /// <summary>
    /// 配置信息
    /// </summary>
    [System.Serializable]
    internal class KeyValuesInfo
    {
        /// <summary>
        /// 配置信息列表
        /// </summary>
        public List<KeyValuesNode> ConfigInfo = null;
    }

    /// <summary>
    /// 配置信息节点
    /// </summary>
    [System.Serializable]
    internal class KeyValuesNode 
    {
        public string Key = null;
        public string Value = null;
    }
}
