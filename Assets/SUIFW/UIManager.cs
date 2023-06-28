using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    /// <summary>
    /// UI管理器
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        #region 单例

        private static UIManager _Instance = null;

        /// <summary>
        /// 获取UI管理器 单例
        /// </summary>
        public static UIManager GetInstance()
        {
            if (_Instance == null)
                _Instance = new GameObject("_UIManager").AddComponent<UIManager>();
            return _Instance;
        }
        #endregion

        #region 字段属性
        // UI窗体预设路径 1.窗体预设名称、2.表示窗体预设路径 （从文件读入Dic）
        private Dictionary<string, string> _DicFormsPaths;
        // UI窗体所有缓存集合
        private Dictionary<string, BaseUIForm> _DicAllUIForms;
        // 当前正在显示的UI窗体
        private Dictionary<string, BaseUIForm> _DicCurrentShowUIForms;
        // 栈结构（反向切换管理）
        private Stack<BaseUIForm> _StackCurrentUIForms;

        // UI节点 Canvas
        private Transform _TraCanvas = null;
        // 普通全屏幕节点
        private Transform _TraNormal = null;
        // 固定显示节点
        private Transform _TraFixed = null;
        // 弹出节点
        private Transform _TraPopUp = null;
        // UI管理脚本的节点
        private Transform _TraUIScripts = null;
        // 语言类型
        private LanguageType languageType = LanguageType.CN;
        #endregion

        #region 生命周期函数 Awake初始化
        // 初始化核心数据，加载“UI窗体路径”到集合中
        private void Awake()
        {
            // 字段初始化
            _DicAllUIForms = new Dictionary<string, BaseUIForm>();
            _DicCurrentShowUIForms = new Dictionary<string, BaseUIForm>();
            _DicFormsPaths = new Dictionary<string, string>();
            _StackCurrentUIForms = new Stack<BaseUIForm>();

            // 加载根UI节点
            InitRootCanvasLoading();

            // 获取节点
            _TraCanvas = GameObject.FindGameObjectWithTag("Canvas").transform;
            _TraNormal = GameObject.FindGameObjectWithTag("Canvas/Normal").transform;
            _TraFixed = GameObject.FindGameObjectWithTag("Canvas/Fixed").transform;
            _TraPopUp = GameObject.FindGameObjectWithTag("Canvas/PopUp").transform;
            _TraUIScripts = GameObject.FindGameObjectWithTag("Canvas/UIScripts").transform;

            // 把本脚本作为UIScripts的子节点
            transform.SetParent(_TraUIScripts);

            // 设置场景转换不摧毁
            DontDestroyOnLoad(_TraCanvas);

            // 初始化UI窗体路径
            InitUIFormsPathData();
        }

        #endregion

        #region 公共方法
        /// <summary>
        /// 显示（打开）UI窗体
        /// </summary>
        public void ShowUIForms(string uiFormName)
        {
            BaseUIForm baseUIFormTemp = null;

            if (string.IsNullOrEmpty(uiFormName))
            {
                Debug.Log("传入UI窗体字符串为空");
                return;
            }

            // 根据UI窗体名称，加载到所有UI窗体缓存集合中
            baseUIFormTemp = LoadFormsToAllUIFormsCatch(uiFormName);

            if (baseUIFormTemp == null)
            {
                Debug.Log("传入UI窗体为空");
                return;
            }

            // 是否清空栈集合数据
            if (baseUIFormTemp.CurrentUIType.IsClearStack)
                ClearStackArray();

            //根据不同UI窗体的显示模式，分别做不同的加载处理
            switch (baseUIFormTemp.CurrentUIType.UIForm_ShowMode)
            {
                case UIFormShowMode.Normal:
                    OpenUIForms(uiFormName);
                    break;
                case UIFormShowMode.ReverseChange:
                    PushUIFormToStack(uiFormName);
                    break;
                case UIFormShowMode.HideOther:
                    OpenUIFormsAndHideOther(uiFormName);
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 关闭（隐藏）UI窗体
        /// </summary>
        /// <param name="uiFormName"></param>
        public void HideUIForms(string uiFormName)
        {
            BaseUIForm baseUIFormTemp = null;
            if (string.IsNullOrEmpty(uiFormName) || !_DicAllUIForms.TryGetValue(uiFormName, out baseUIFormTemp)) return;

            switch (baseUIFormTemp.CurrentUIType.UIForm_ShowMode)
            {
                case UIFormShowMode.Normal:
                    CloseUIForms(uiFormName);
                    break;
                case UIFormShowMode.ReverseChange:
                    PopUIFormToStack(uiFormName);
                    break;
                case UIFormShowMode.HideOther:
                    CloseUIFormsAndShowOther(uiFormName);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化加载（根UI窗体）Canvas预设
        /// </summary>
        private void InitRootCanvasLoading()
        {
            if (Resources.Load("UIPrefabs/Canvas") == null)
                Debug.Log("无法加载Canvas预制体！");
            else
                Instantiate(Resources.Load("UIPrefabs/Canvas", typeof(GameObject)));

        }

        /// <summary>
        /// 根据UI窗体名称，把窗体加载到“所有UI窗体缓存集合”中
        /// </summary>
        /// <param name="uiFormName"></param>
        /// <returns></returns>
        private BaseUIForm LoadFormsToAllUIFormsCatch(string uiFormName)
        {
            BaseUIForm baseUIFormTemp = null;

            // 如果缓存集合中不存在该窗体，则加载进来，然后返回
            if (!_DicAllUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
            {
                baseUIFormTemp = LoadUIForm(uiFormName);
                return baseUIFormTemp;
            }
            // 已经存在直接返回
            else if (_DicAllUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
            {
                return baseUIFormTemp;
            }
            return null;
        }

        /// <summary>
        /// 从资源处加载指定名称UI窗体，挂载到场景中
        /// </summary>
        /// <param name="uiFormName"></param>
        /// <returns></returns>
        private BaseUIForm LoadUIForm(string uiFormName)
        {
            string strUIFormPaths = null;
            GameObject goCloneUIPrefab = null;
            BaseUIForm baseUIFormTemp = null;
            // 根据窗体名称加载UI预制体路径
            if (_DicFormsPaths.TryGetValue(uiFormName, out strUIFormPaths))
            {
                goCloneUIPrefab = Resources.Load<GameObject>(strUIFormPaths);
                goCloneUIPrefab = Instantiate<GameObject>(goCloneUIPrefab);
            }
            else
                Debug.Log("路径集合中找不到该窗体，请检查名字或路径是否错误、是否已经添加！" + uiFormName);

            // 设置UI预制体的父节点
            if (_TraCanvas && goCloneUIPrefab)
            {
                // 获取预制体挂载的脚本，通过脚本设置的类型来预制体放到相应节点下
                baseUIFormTemp = goCloneUIPrefab.GetComponent<BaseUIForm>();
                if (!baseUIFormTemp)
                {
                    Debug.Log(uiFormName + "没有挂载脚本！");
                    return null;
                }

                // 根据UI窗体类型来挂载到不同的节点下，方便管理和调试
                switch (baseUIFormTemp.CurrentUIType.UIForm_Type)
                {
                    case UIFormType.Normal:
                        goCloneUIPrefab.transform.SetParent(_TraNormal, false);
                        break;
                    case UIFormType.Fixed:
                        goCloneUIPrefab.transform.SetParent(_TraFixed, false);
                        break;
                    case UIFormType.PopUp:
                        goCloneUIPrefab.transform.SetParent(_TraPopUp, false);
                        break;
                    default:
                        break;
                }

                // 加载后隐藏
                goCloneUIPrefab.SetActive(false);

                // 加载后加入缓存集合
                _DicAllUIForms.Add(uiFormName, baseUIFormTemp);

                return baseUIFormTemp;
            }
            else if (!_TraCanvas)
                Debug.Log("Canvas为空！");
            else if (!goCloneUIPrefab)
                Debug.Log("找不到窗体预制体" +uiFormName);

            return null;
        }

        /// <summary>
        /// 打开NorMal窗体
        /// </summary>
        /// <param name="uiFormName"></param>
        private void OpenUIForms(string uiFormName)
        {
            BaseUIForm baseUIFormTemp;

            // 如果集合中存在，则直接返回
            if (!_DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
            {
                // 从全部缓存集合中获取，然后加入当前集合
                if (_DicAllUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
                {
                    _DicCurrentShowUIForms.Add(uiFormName, baseUIFormTemp);
                    // 显示当前窗体
                    baseUIFormTemp.Display();
                }
            }
        }

        /// <summary>
        /// 关闭Normal窗体
        /// </summary>
        /// <param name="uiFormName"></param>
        private void CloseUIForms(string uiFormName)
        {
            BaseUIForm baseUIFormTemp;
            if (_DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
            {
                _DicCurrentShowUIForms.Remove(uiFormName);
                baseUIFormTemp.Hiding();
            }
        }

        /// <summary>
        /// UI窗体入栈
        /// </summary>
        /// <param name="uiFormName"></param>
        private void PushUIFormToStack(string uiFormName)
        {
            BaseUIForm topUIForm;
            BaseUIForm baseUIFormTemp;

            // 判断栈中是否有其他窗体，有则冻结（对于UI动效的冻结、无法点击、可以显示）
            // 只留下栈顶可操作
            if (_StackCurrentUIForms.Count > 0)
            {
                topUIForm = _StackCurrentUIForms.Peek();
                // 每次打开都冻结最顶部，如果Push很多窗体，那么每次栈顶元素被下一个栈顶替代时都会被冻结
                topUIForm.Freeze();
            }


            // 判断所有窗体集合中是否存在需要入栈的窗体
            if (_DicAllUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
            {
                // 显示
                baseUIFormTemp.Display();
                // 入栈
                _StackCurrentUIForms.Push(baseUIFormTemp);
            }
            else
                Debug.Log("窗体入栈时无法在所有窗体集合中查找到:" + uiFormName);

        }

        /// <summary>
        /// UI窗体出栈
        /// </summary>
        /// <param name="uiFormName"></param>
        private void PopUIFormToStack(string uiFormName)
        {

            // 如果栈中还有一个以上元素
            if (_StackCurrentUIForms.Count >= 1)
            {
                // 出栈
                BaseUIForm topUIForms = _StackCurrentUIForms.Pop();
                // 隐藏
                topUIForms.Hiding();

                // 如果有两个及以上
                if (_StackCurrentUIForms.Count >= 2)
                {
                    // 则新的栈顶(下一个窗体)要重新显示（因为栈顶在入栈时被冻结了，所以需要重新显示一下）
                    BaseUIForm nextUIForms = _StackCurrentUIForms.Peek();
                    nextUIForms.Redisplay();
                }
            }
        }

        /// <summary>
        /// 打开UI窗体，并且隐藏其他
        /// </summary>
        private void OpenUIFormsAndHideOther(string uiFormName)
        {
            BaseUIForm baseUIFormTemp;

            if (!_DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
            {
                // 把 “正在显示” 集合和 “栈” 集合的所有窗体都隐藏
                foreach (BaseUIForm baseUI in _DicCurrentShowUIForms.Values)
                {
                    baseUI.Hiding();
                }
                foreach (BaseUIForm baseUI in _StackCurrentUIForms)
                {
                    baseUI.Hiding();
                }

                // 把当前窗体加入到 “正在显示” 集合中，正常显示处理
                if (_DicAllUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
                {
                    _DicCurrentShowUIForms.Add(uiFormName, baseUIFormTemp);
                    baseUIFormTemp.Display();
                }
            }
        }

        /// <summary>
        /// 关闭UI窗体，并且显示其他
        /// </summary>
        /// <param name="uiFormName"></param>
        private void CloseUIFormsAndShowOther(string uiFormName)
        {
            BaseUIForm baseUIFormTemp;

            if (_DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
            {
                // 隐藏当前窗体
                baseUIFormTemp.Hiding();
                _DicCurrentShowUIForms.Remove(uiFormName);

                // 把之前隐藏的窗体都显示出来
                // （因为在这两个集合中的必然是之前正常显示的，因为退出时直接就把这俩集合中不显示的给删除了
                //  所以直接遍历即可，重新显示）
                foreach (KeyValuePair<string, BaseUIForm> baseUI in _DicCurrentShowUIForms)
                {
                    baseUI.Value.Redisplay();
                }
                foreach (BaseUIForm baseUI in _StackCurrentUIForms)
                {
                    baseUI.Redisplay();
                }
            }
        }

        /// <summary>
        /// 清空栈集合数据
        /// </summary>
        /// <returns></returns>
        private bool ClearStackArray()
        {
            if (_StackCurrentUIForms != null && _StackCurrentUIForms.Count >= 1)
            {
                _StackCurrentUIForms.Clear();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 初始化UI窗体预设的路径数据
        /// </summary>
        private void InitUIFormsPathData()
        {
            IConfigManager configManager = new ConfigManagerByJson("Config\\UIFormsConfigInfo");

            if (configManager != null)
            {
                _DicFormsPaths = configManager.Appsetting;
            }
        }
        #endregion

        #region 测试方法
        public int GetAllUIFormsCount()
        {
            return _DicAllUIForms.Count;
        }
        public int GetCurrentUIFormsCount()
        {
            return _DicCurrentShowUIForms.Count;
        }
        public int GetStackUIFormsCount()
        {
            return _StackCurrentUIForms.Count;
        }

        #endregion
    }
}
