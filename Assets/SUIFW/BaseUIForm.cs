using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SUIFW
{
    /// <summary>
    /// UI基类
    /// </summary>
    public class BaseUIForm : MonoBehaviour
    {
        private UIType _CurrentUIType = new UIType();
        /// <summary>
        /// 当前UI窗体类型
        /// </summary>
        public UIType CurrentUIType { get { return _CurrentUIType; } set { _CurrentUIType = CurrentUIType; } }

        public UIType UITypeByEditors;

        #region 四种窗体状态
        /// <summary>
        /// 显示状态
        /// </summary>
        public virtual void Display()
        {
            this.gameObject.SetActive(true);

            // 如果窗体是弹出模式 则启用模态
            if (_CurrentUIType.UIForm_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().SetMaskWindow(gameObject, _CurrentUIType.UIForm_LucencyType);
            }

            //Debug.Log(this.gameObject.name + "显示");
        }

        /// <summary>
        /// 隐藏状态
        /// </summary>
        public virtual void Hiding()
        {
            this.gameObject.SetActive(false);

            // 如果窗体是弹出模式 则取消模态
            if (_CurrentUIType.UIForm_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().CancelMaskWindow();
            }

            //Debug.Log(this.gameObject.name + "隐藏");
        }

        /// <summary>
        /// 再次显示状态
        /// </summary>
        public virtual void Redisplay()
        {
            this.gameObject.SetActive(true);

            // 如果窗体是弹出模式 则启用模态
            if (_CurrentUIType.UIForm_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().SetMaskWindow(gameObject, _CurrentUIType.UIForm_LucencyType);
            }


            //Debug.Log(this.gameObject.name + "再次显示");
        }

        /// <summary>
        /// 冻结状态
        /// </summary>
        public virtual void Freeze()
        {
            // 冻结状态暂时不需要太多操作，因为下层窗体无法点击是由Mask实现的，而不是该函数实现的
            // 后续这里会对状态进行操作，窗体状态可以用来判定UI的动效，粒子，是否播放
            this.gameObject.SetActive(true);

            //Debug.Log(this.gameObject.name + "冻结");
        }
        #endregion

        #region 封装UI框架的方法，供子类使用

        protected void ShowText()
        {
            LauguageMgr.GetInstance().ShowText("");
        }

        /// <summary>
        /// 通过编辑器设置窗体类型
        /// </summary>
        protected void SettingUIFormTypeByEditors()
        {
            // 仅仅测试使用，脚本设置通过SettingUIFormType()
            // 且脚本设置Awake,这里Start可以覆盖设置
            CurrentUIType.UIForm_Type = UITypeByEditors.UIForm_Type;
            CurrentUIType.UIForm_ShowMode = UITypeByEditors.UIForm_ShowMode;
            CurrentUIType.UIForm_LucencyType = UITypeByEditors.UIForm_LucencyType;
        }

        /// <summary>
        /// 设置窗体类型
        /// </summary>
        /// <param name="positionType">位置类型</param>
        /// <param name="showType">显示类型</param>
        /// <param name="lucenyType">透明度</param>
        protected void SettingUIFormType(UIFormType positionType,UIFormShowMode showType,UIFormLucenyType lucenyType)
        {
            CurrentUIType.UIForm_Type = positionType;
            CurrentUIType.UIForm_ShowMode = showType;
            CurrentUIType.UIForm_LucencyType = lucenyType;
        }

        /// <summary>
        /// 注册按钮事件
        /// </summary>
        /// <param name="buttonName"></param>
        /// <param name="buttonEvent"></param>
        protected void RegisterButtonEvent(string buttonName, UnityAction buttonEvent)
        {
            GameObject goButton = UnityHelper.FindTheChildNode(gameObject, buttonName).gameObject;

            if (goButton != null)
            {
                goButton.GetComponent<Button>().onClick.AddListener(buttonEvent);
            }
            else
            {
                Debug.Log(buttonName + "为空！");
            }
        }

        /// <summary>
        /// 打开窗体
        /// </summary>
        /// <param name="uiFromName"></param>
        protected void OpenUIForm(string uiFromName)
        {
            UIManager.GetInstance().ShowUIForms(uiFromName);
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="uiFromName"></param>
        protected void CloseUIForm(string uiFromName)
        {
            UIManager.GetInstance().HideUIForms(uiFromName);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="msgName">消息名称</param>
        /// <param name="msgContent">消息内容</param>
        protected void SendMessage(string msgType, string msgName, object msgContent)
        {
            KeyValueUpdate kv = new KeyValueUpdate(msgName, msgContent);
            MessageCenter.SendMessage(msgType, kv);
        }

        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="handler">消息相应函数</param>
        protected void ReceiveMessage(string msgType, MessageCenter.DelMessageDelivery handler)
        {
            MessageCenter.AddMsgListener(msgType, handler);
        }

        #endregion
    }
}
