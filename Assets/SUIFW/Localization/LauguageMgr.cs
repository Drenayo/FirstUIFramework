using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    /// <summary>
    /// ���Թ��ʻ�
    /// </summary>
    public class LauguageMgr
    {
        #region ����
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
        /// ���������ݻ��漯��
        /// </summary>
        private Dictionary<string, string> _DicLauguageCache;
        
        // ����
        private LauguageMgr()
        {
            _DicLauguageCache = new Dictionary<string, string>();
            InitLauguageCcche();

        }

        /// <summary>
        /// ��ʼ�����Ի��漯��
        /// </summary>
        private void InitLauguageCcche()
        {
            //IConfigManager config = new ConfigManagerByJson("Config\\LauguageJSONConfig_CN");
            IConfigManager config = SetLanguage(LanguageType.CN);

            if (config != null)
            {
                _DicLauguageCache = config.Appsetting;
            }
        }

        /// <summary>
        /// ��ʾ�ı�
        /// </summary>
        /// <param name="lauguageID">����ID</param>
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

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="languageType">����</param>
        private IConfigManager SetLanguage(LanguageType languageType)
        {
            if (languageType == LanguageType.CN)
            {
                IConfigManager config = new ConfigManagerByJson("Config\\LauguageJSONConfig_CN");
                return config;
            }
            else if(languageType == LanguageType.EN)
            {
                IConfigManager config = new ConfigManagerByJson("Config\\LauguageJSONConfig_EN");
                return config;
            }
            return null;
        }
    }
}
