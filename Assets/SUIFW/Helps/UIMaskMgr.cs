using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SUIFW
{
    public class UIMaskMgr : MonoBehaviour
    {
        #region ����
        private static UIMaskMgr instance = null;
        public static UIMaskMgr GetInstance()
        {
            if (instance == null)
            {
                instance = new GameObject("_UIMaskScript").AddComponent<UIMaskMgr>();
            }
            return instance;
        }
        #endregion

        #region �ֶ�����
        // UI���ڵ�
        private GameObject _GoCanvasRootNode = null;
        // UI�ű��ڵ�
        private Transform _TraUIScriptsNode = null;
        // ���㸸���
        private GameObject _GoTopPanel;
        // �������
        private GameObject _GoMaskPanel;
        // �������
        private Image _ImageMaskComponent;
        // UI�����
        private Camera _UICamera;
        // UI�����ԭʼ�����
        private float _OriginalUICameralDepth;

        #endregion

        #region �������ں��� Awake��ʼ��
        private void Awake()
        {
            // �õ�UI���ڵ㡢�ű��ڵ�
            _GoCanvasRootNode = GameObject.FindGameObjectWithTag("Canvas");
            _TraUIScriptsNode = GameObject.FindGameObjectWithTag("Canvas/UIScripts").transform;
            // �ѱ���ʵ���������ű��ڵ��Ӷ���
            UnityHelper.AddChildNodeToParentNode(_TraUIScriptsNode, transform);
            // �õ����㡢�������
            _GoTopPanel = _GoCanvasRootNode;
            _GoMaskPanel = UnityHelper.FindTheChildNode(_GoCanvasRootNode, "UIMask").gameObject;
            _ImageMaskComponent = _GoMaskPanel.GetComponent<Image>();
            // �õ�UI�����ԭʼ����
            _UICamera = GameObject.FindGameObjectWithTag("Canvas/Camera").GetComponent<Camera>();
            _OriginalUICameralDepth = _UICamera.depth;

        }
        #endregion

        #region ����

        /// <summary>
        /// ��������״̬
        /// </summary>
        /// <param name="goDisplayUIForms">Ҫ��ʾ�Ĵ���</param>
        /// <param name="uiFormLuceny">͸��������</param>
        public void SetMaskWindow(GameObject goDisplayUIForms, UIFormLucenyType uiFormLuceny = UIFormLucenyType.Lucency)
        {
            // ���㸸��������
            _GoTopPanel.transform.SetAsLastSibling();

            // �������ִ��壬����͸����
            _GoMaskPanel.SetActive(true);
            switch (uiFormLuceny)
            {
                case UIFormLucenyType.Lucency:  // ��ȫ͸��
                    _ImageMaskComponent.color = new Color(0, 0, 0, 0f);
                    break;
                case UIFormLucenyType.Translucence:  // ��͸����
                    _ImageMaskComponent.color = new Color(0, 0, 0, .5f);
                    break;
                case UIFormLucenyType.ImPenetrable: // ��͸����
                    _ImageMaskComponent.color = new Color(0, 0, 0, .7f);
                    break;
                case UIFormLucenyType.Pentrate: // ���Դ�͸
                    if (_GoMaskPanel.activeSelf)
                        _GoMaskPanel.SetActive(false);
                    _ImageMaskComponent.color = new Color(1, 1, 1, 0f);
                    break;
                default:
                    break;
            }

            // �������ƣ���ʾ��������
            _GoMaskPanel.transform.SetAsLastSibling();
            goDisplayUIForms.transform.SetAsLastSibling();

            //�������������
            _UICamera.depth += 100;
        }


        /// <summary>
        /// ȡ������״̬
        /// </summary>
        /// <param name="goDisplayUIForms">Ҫ��ʾ�Ĵ���</param>
        /// <param name="uIFormLuceny">͸��������</param>
        public void CancelMaskWindow()
        {
            // ���㸸��������
            _GoTopPanel.transform.SetAsFirstSibling();

            // �������ִ���
            _GoMaskPanel.SetActive(false);

            //��������� �ָ�
            _UICamera.depth = _OriginalUICameralDepth;
        }
        #endregion


    }
}
