using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    /// <summary>
    /// UI������
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        #region ����

        private static UIManager _Instance = null;

        /// <summary>
        /// ��ȡUI������ ����
        /// </summary>
        public static UIManager GetInstance()
        {
            if (_Instance == null)
                _Instance = new GameObject("_UIManager").AddComponent<UIManager>();
            return _Instance;
        }
        #endregion

        #region �ֶ�����
        // UI����Ԥ��·�� 1.����Ԥ�����ơ�2.��ʾ����Ԥ��·�� �����ļ�����Dic��
        private Dictionary<string, string> _DicFormsPaths;
        // UI�������л��漯��
        private Dictionary<string, BaseUIForm> _DicAllUIForms;
        // ��ǰ������ʾ��UI����
        private Dictionary<string, BaseUIForm> _DicCurrentShowUIForms;
        // ջ�ṹ�������л�����
        private Stack<BaseUIForm> _StackCurrentUIForms;

        // UI�ڵ� Canvas
        private Transform _TraCanvas = null;
        // ��ͨȫ��Ļ�ڵ�
        private Transform _TraNormal = null;
        // �̶���ʾ�ڵ�
        private Transform _TraFixed = null;
        // �����ڵ�
        private Transform _TraPopUp = null;
        // UI����ű��Ľڵ�
        private Transform _TraUIScripts = null;
        // ��������
        private LanguageType languageType = LanguageType.CN;
        #endregion

        #region �������ں��� Awake��ʼ��
        // ��ʼ���������ݣ����ء�UI����·������������
        private void Awake()
        {
            // �ֶγ�ʼ��
            _DicAllUIForms = new Dictionary<string, BaseUIForm>();
            _DicCurrentShowUIForms = new Dictionary<string, BaseUIForm>();
            _DicFormsPaths = new Dictionary<string, string>();
            _StackCurrentUIForms = new Stack<BaseUIForm>();

            // ���ظ�UI�ڵ�
            InitRootCanvasLoading();

            // ��ȡ�ڵ�
            _TraCanvas = GameObject.FindGameObjectWithTag("Canvas").transform;
            _TraNormal = GameObject.FindGameObjectWithTag("Canvas/Normal").transform;
            _TraFixed = GameObject.FindGameObjectWithTag("Canvas/Fixed").transform;
            _TraPopUp = GameObject.FindGameObjectWithTag("Canvas/PopUp").transform;
            _TraUIScripts = GameObject.FindGameObjectWithTag("Canvas/UIScripts").transform;

            // �ѱ��ű���ΪUIScripts���ӽڵ�
            transform.SetParent(_TraUIScripts);

            // ���ó���ת�����ݻ�
            DontDestroyOnLoad(_TraCanvas);

            // ��ʼ��UI����·��
            InitUIFormsPathData();
        }

        #endregion

        #region ��������
        /// <summary>
        /// ��ʾ���򿪣�UI����
        /// </summary>
        public void ShowUIForms(string uiFormName)
        {
            BaseUIForm baseUIFormTemp = null;

            if (string.IsNullOrEmpty(uiFormName))
            {
                Debug.Log("����UI�����ַ���Ϊ��");
                return;
            }

            // ����UI�������ƣ����ص�����UI���建�漯����
            baseUIFormTemp = LoadFormsToAllUIFormsCatch(uiFormName);

            if (baseUIFormTemp == null)
            {
                Debug.Log("����UI����Ϊ��");
                return;
            }

            // �Ƿ����ջ��������
            if (baseUIFormTemp.CurrentUIType.IsClearStack)
                ClearStackArray();

            //���ݲ�ͬUI�������ʾģʽ���ֱ�����ͬ�ļ��ش���
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
        /// �رգ����أ�UI����
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

        #region ˽�з���

        /// <summary>
        /// ��ʼ�����أ���UI���壩CanvasԤ��
        /// </summary>
        private void InitRootCanvasLoading()
        {
            if (Resources.Load("UIPrefabs/Canvas") == null)
                Debug.Log("�޷�����CanvasԤ���壡");
            else
                Instantiate(Resources.Load("UIPrefabs/Canvas", typeof(GameObject)));

        }

        /// <summary>
        /// ����UI�������ƣ��Ѵ�����ص�������UI���建�漯�ϡ���
        /// </summary>
        /// <param name="uiFormName"></param>
        /// <returns></returns>
        private BaseUIForm LoadFormsToAllUIFormsCatch(string uiFormName)
        {
            BaseUIForm baseUIFormTemp = null;

            // ������漯���в����ڸô��壬����ؽ�����Ȼ�󷵻�
            if (!_DicAllUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
            {
                baseUIFormTemp = LoadUIForm(uiFormName);
                return baseUIFormTemp;
            }
            // �Ѿ�����ֱ�ӷ���
            else if (_DicAllUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
            {
                return baseUIFormTemp;
            }
            return null;
        }

        /// <summary>
        /// ����Դ������ָ������UI���壬���ص�������
        /// </summary>
        /// <param name="uiFormName"></param>
        /// <returns></returns>
        private BaseUIForm LoadUIForm(string uiFormName)
        {
            string strUIFormPaths = null;
            GameObject goCloneUIPrefab = null;
            BaseUIForm baseUIFormTemp = null;
            // ���ݴ������Ƽ���UIԤ����·��
            if (_DicFormsPaths.TryGetValue(uiFormName, out strUIFormPaths))
            {
                goCloneUIPrefab = Resources.Load<GameObject>(strUIFormPaths);
                goCloneUIPrefab = Instantiate<GameObject>(goCloneUIPrefab);
            }
            else
                Debug.Log("·���������Ҳ����ô��壬�������ֻ�·���Ƿ�����Ƿ��Ѿ���ӣ�" + uiFormName);

            // ����UIԤ����ĸ��ڵ�
            if (_TraCanvas && goCloneUIPrefab)
            {
                // ��ȡԤ������صĽű���ͨ���ű����õ�������Ԥ����ŵ���Ӧ�ڵ���
                baseUIFormTemp = goCloneUIPrefab.GetComponent<BaseUIForm>();
                if (!baseUIFormTemp)
                {
                    Debug.Log(uiFormName + "û�й��ؽű���");
                    return null;
                }

                // ����UI�������������ص���ͬ�Ľڵ��£��������͵���
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

                // ���غ�����
                goCloneUIPrefab.SetActive(false);

                // ���غ���뻺�漯��
                _DicAllUIForms.Add(uiFormName, baseUIFormTemp);

                return baseUIFormTemp;
            }
            else if (!_TraCanvas)
                Debug.Log("CanvasΪ�գ�");
            else if (!goCloneUIPrefab)
                Debug.Log("�Ҳ�������Ԥ����" +uiFormName);

            return null;
        }

        /// <summary>
        /// ��NorMal����
        /// </summary>
        /// <param name="uiFormName"></param>
        private void OpenUIForms(string uiFormName)
        {
            BaseUIForm baseUIFormTemp;

            // ��������д��ڣ���ֱ�ӷ���
            if (!_DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
            {
                // ��ȫ�����漯���л�ȡ��Ȼ����뵱ǰ����
                if (_DicAllUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
                {
                    _DicCurrentShowUIForms.Add(uiFormName, baseUIFormTemp);
                    // ��ʾ��ǰ����
                    baseUIFormTemp.Display();
                }
            }
        }

        /// <summary>
        /// �ر�Normal����
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
        /// UI������ջ
        /// </summary>
        /// <param name="uiFormName"></param>
        private void PushUIFormToStack(string uiFormName)
        {
            BaseUIForm topUIForm;
            BaseUIForm baseUIFormTemp;

            // �ж�ջ���Ƿ����������壬���򶳽ᣨ����UI��Ч�Ķ��ᡢ�޷������������ʾ��
            // ֻ����ջ���ɲ���
            if (_StackCurrentUIForms.Count > 0)
            {
                topUIForm = _StackCurrentUIForms.Peek();
                // ÿ�δ򿪶�������������Push�ܶര�壬��ôÿ��ջ��Ԫ�ر���һ��ջ�����ʱ���ᱻ����
                topUIForm.Freeze();
            }


            // �ж����д��弯�����Ƿ������Ҫ��ջ�Ĵ���
            if (_DicAllUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
            {
                // ��ʾ
                baseUIFormTemp.Display();
                // ��ջ
                _StackCurrentUIForms.Push(baseUIFormTemp);
            }
            else
                Debug.Log("������ջʱ�޷������д��弯���в��ҵ�:" + uiFormName);

        }

        /// <summary>
        /// UI�����ջ
        /// </summary>
        /// <param name="uiFormName"></param>
        private void PopUIFormToStack(string uiFormName)
        {

            // ���ջ�л���һ������Ԫ��
            if (_StackCurrentUIForms.Count >= 1)
            {
                // ��ջ
                BaseUIForm topUIForms = _StackCurrentUIForms.Pop();
                // ����
                topUIForms.Hiding();

                // ���������������
                if (_StackCurrentUIForms.Count >= 2)
                {
                    // ���µ�ջ��(��һ������)Ҫ������ʾ����Ϊջ������ջʱ�������ˣ�������Ҫ������ʾһ�£�
                    BaseUIForm nextUIForms = _StackCurrentUIForms.Peek();
                    nextUIForms.Redisplay();
                }
            }
        }

        /// <summary>
        /// ��UI���壬������������
        /// </summary>
        private void OpenUIFormsAndHideOther(string uiFormName)
        {
            BaseUIForm baseUIFormTemp;

            if (!_DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
            {
                // �� ��������ʾ�� ���Ϻ� ��ջ�� ���ϵ����д��嶼����
                foreach (BaseUIForm baseUI in _DicCurrentShowUIForms.Values)
                {
                    baseUI.Hiding();
                }
                foreach (BaseUIForm baseUI in _StackCurrentUIForms)
                {
                    baseUI.Hiding();
                }

                // �ѵ�ǰ������뵽 ��������ʾ�� �����У�������ʾ����
                if (_DicAllUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
                {
                    _DicCurrentShowUIForms.Add(uiFormName, baseUIFormTemp);
                    baseUIFormTemp.Display();
                }
            }
        }

        /// <summary>
        /// �ر�UI���壬������ʾ����
        /// </summary>
        /// <param name="uiFormName"></param>
        private void CloseUIFormsAndShowOther(string uiFormName)
        {
            BaseUIForm baseUIFormTemp;

            if (_DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUIFormTemp))
            {
                // ���ص�ǰ����
                baseUIFormTemp.Hiding();
                _DicCurrentShowUIForms.Remove(uiFormName);

                // ��֮ǰ���صĴ��嶼��ʾ����
                // ����Ϊ�������������еı�Ȼ��֮ǰ������ʾ�ģ���Ϊ�˳�ʱֱ�ӾͰ����������в���ʾ�ĸ�ɾ����
                //  ����ֱ�ӱ������ɣ�������ʾ��
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
        /// ���ջ��������
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
        /// ��ʼ��UI����Ԥ���·������
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

        #region ���Է���
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
