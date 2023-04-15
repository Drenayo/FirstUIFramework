using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    /// <summary>
    /// 语言国际化
    /// </summary>
    public class LauguageMgr
    {
        #region 单例
        private static LauguageMgr _Instance;
        public static LauguageMgr GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new LauguageMgr();
            }
            return _Instance;

        }
        #endregion

        /// <summary>
        /// 多语言数据缓存集合
        /// </summary>
        private Dictionary<string, string> _DicLauguageCache;
        
        // 构造
        private LauguageMgr()
        {
            _DicLauguageCache = new Dictionary<string, string>();
            InitLauguageCcche();

        }

        /// <summary>
        /// 初始化语言缓存集合
        /// </summary>
        private void InitLauguageCcche()
        {
            IConfigManager config = new ConfigManagerByJson("Config\\LauguageJSONConfig_CN");
            if (config != null)
            {
                _DicLauguageCache = config.Appsetting;
            }
        }

        /// <summary>
        /// 显示文本
        /// </summary>
        /// <param name="lauguageID">语言ID</param>
        /// <returns></returns>
        public string ShowText(string lauguageID)
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(lauguageID)) return null;

            if (_DicLauguageCache != null && _DicLauguageCache.Count >= 1)
            {
                if (_DicLauguageCache.TryGetValue(lauguageID, out str))
                {
                    return str;
                }
            }
            return null;
        }
    }
}
