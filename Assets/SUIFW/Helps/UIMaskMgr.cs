using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SUIFW
{
    public class UIMaskMgr : MonoBehaviour
    {
        #region 单例
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

        #region 字段属性
        // UI根节点
        private GameObject _GoCanvasRootNode = null;
        // UI脚本节点
        private Transform _TraUIScriptsNode = null;
        // 顶层父面板
        private GameObject _GoTopPanel;
        // 遮罩面板
        private GameObject _GoMaskPanel;
        // 遮罩组件
        private Image _ImageMaskComponent;
        // UI摄像机
        private Camera _UICamera;
        // UI摄像机原始“层深”
        private float _OriginalUICameralDepth;

        #endregion

        #region 生命周期函数 Awake初始化
        private void Awake()
        {
            // 得到UI根节点、脚本节点
            _GoCanvasRootNode = GameObject.FindGameObjectWithTag("Canvas");
            _TraUIScriptsNode = GameObject.FindGameObjectWithTag("Canvas/UIScripts").transform;
            // 把本类实例对象归入脚本节点子对象
            UnityHelper.AddChildNodeToParentNode(_TraUIScriptsNode, transform);
            // 得到顶层、遮罩面板
            _GoTopPanel = _GoCanvasRootNode;
            _GoMaskPanel = UnityHelper.FindTheChildNode(_GoCanvasRootNode, "UIMask").gameObject;
            _ImageMaskComponent = _GoMaskPanel.GetComponent<Image>();
            // 得到UI摄像机原始层深
            _UICamera = GameObject.FindGameObjectWithTag("Canvas/Camera").GetComponent<Camera>();
            _OriginalUICameralDepth = _UICamera.depth;

        }
        #endregion

        #region 方法

        /// <summary>
        /// 设置遮罩状态
        /// </summary>
        /// <param name="goDisplayUIForms">要显示的窗体</param>
        /// <param name="uiFormLuceny">透明度类型</param>
        public void SetMaskWindow(GameObject goDisplayUIForms, UIFormLucenyType uiFormLuceny = UIFormLucenyType.Lucency)
        {
            // 顶层父窗体下移
            _GoTopPanel.transform.SetAsLastSibling();

            // 启用遮罩窗体，设置透明度
            _GoMaskPanel.SetActive(true);
            switch (uiFormLuceny)
            {
                case UIFormLucenyType.Lucency:  // 完全透明
                    _ImageMaskComponent.color = new Color(0, 0, 0, 0f);
                    break;
                case UIFormLucenyType.Translucence:  // 半透明度
                    _ImageMaskComponent.color = new Color(0, 0, 0, .5f);
                    break;
                case UIFormLucenyType.ImPenetrable: // 低透明度
                    _ImageMaskComponent.color = new Color(0, 0, 0, .7f);
                    break;
                case UIFormLucenyType.Pentrate: // 可以穿透
                    if (_GoMaskPanel.activeSelf)
                        _GoMaskPanel.SetActive(false);
                    _ImageMaskComponent.color = new Color(1, 1, 1, 0f);
                    break;
                default:
                    break;
            }

            // 遮罩下移，显示窗口下移
            _GoMaskPanel.transform.SetAsLastSibling();
            goDisplayUIForms.transform.SetAsLastSibling();

            //摄像机层深增加
            _UICamera.depth += 100;
        }


        /// <summary>
        /// 取消遮罩状态
        /// </summary>
        /// <param name="goDisplayUIForms">要显示的窗体</param>
        /// <param name="uIFormLuceny">透明度类型</param>
        public void CancelMaskWindow()
        {
            // 顶层父窗体上移
            _GoTopPanel.transform.SetAsFirstSibling();

            // 禁用遮罩窗体
            _GoMaskPanel.SetActive(false);

            //摄像机层深 恢复
            _UICamera.depth = _OriginalUICameralDepth;
        }
        #endregion


    }
}
