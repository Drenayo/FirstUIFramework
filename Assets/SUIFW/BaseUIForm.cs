using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SUIFW
{
    /// <summary>
    /// UI����
    /// </summary>
    public class BaseUIForm : MonoBehaviour
    {
        private UIType _CurrentUIType = new UIType();
        /// <summary>
        /// ��ǰUI��������
        /// </summary>
        public UIType CurrentUIType { get { return _CurrentUIType; } set { _CurrentUIType = CurrentUIType; } }

        public UIType UITypeByEditors;

        #region ���ִ���״̬
        /// <summary>
        /// ��ʾ״̬
        /// </summary>
        public virtual void Display()
        {
            this.gameObject.SetActive(true);

            // ��������ǵ���ģʽ ������ģ̬
            if (_CurrentUIType.UIForm_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().SetMaskWindow(gameObject, _CurrentUIType.UIForm_LucencyType);
            }

            //Debug.Log(this.gameObject.name + "��ʾ");
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        public virtual void Hiding()
        {
            this.gameObject.SetActive(false);

            // ��������ǵ���ģʽ ��ȡ��ģ̬
            if (_CurrentUIType.UIForm_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().CancelMaskWindow();
            }

            //Debug.Log(this.gameObject.name + "����");
        }

        /// <summary>
        /// �ٴ���ʾ״̬
        /// </summary>
        public virtual void Redisplay()
        {
            this.gameObject.SetActive(true);

            // ��������ǵ���ģʽ ������ģ̬
            if (_CurrentUIType.UIForm_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().SetMaskWindow(gameObject, _CurrentUIType.UIForm_LucencyType);
            }


            //Debug.Log(this.gameObject.name + "�ٴ���ʾ");
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        public virtual void Freeze()
        {
            // ����״̬��ʱ����Ҫ̫���������Ϊ�²㴰���޷��������Maskʵ�ֵģ������Ǹú���ʵ�ֵ�
            // ����������״̬���в���������״̬���������ж�UI�Ķ�Ч�����ӣ��Ƿ񲥷�
            this.gameObject.SetActive(true);

            //Debug.Log(this.gameObject.name + "����");
        }
        #endregion

        #region ��װUI��ܵķ�����������ʹ��

        protected void ShowText()
        {
            LauguageMgr.GetInstance().ShowText("");
        }

        /// <summary>
        /// ͨ���༭�����ô�������
        /// </summary>
        protected void SettingUIFormTypeByEditors()
        {
            // ��������ʹ�ã��ű�����ͨ��SettingUIFormType()
            // �ҽű�����Awake,����Start���Ը�������
            CurrentUIType.UIForm_Type = UITypeByEditors.UIForm_Type;
            CurrentUIType.UIForm_ShowMode = UITypeByEditors.UIForm_ShowMode;
            CurrentUIType.UIForm_LucencyType = UITypeByEditors.UIForm_LucencyType;
        }

        /// <summary>
        /// ���ô�������
        /// </summary>
        /// <param name="positionType">λ������</param>
        /// <param name="showType">��ʾ����</param>
        /// <param name="lucenyType">͸����</param>
        protected void SettingUIFormType(UIFormType positionType,UIFormShowMode showType,UIFormLucenyType lucenyType)
        {
            CurrentUIType.UIForm_Type = positionType;
            CurrentUIType.UIForm_ShowMode = showType;
            CurrentUIType.UIForm_LucencyType = lucenyType;
        }

        /// <summary>
        /// ע�ᰴť�¼�
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
                Debug.Log(buttonName + "Ϊ�գ�");
            }
        }

        /// <summary>
        /// �򿪴���
        /// </summary>
        /// <param name="uiFromName"></param>
        protected void OpenUIForm(string uiFromName)
        {
            UIManager.GetInstance().ShowUIForms(uiFromName);
        }

        /// <summary>
        /// �رմ���
        /// </summary>
        /// <param name="uiFromName"></param>
        protected void CloseUIForm(string uiFromName)
        {
            UIManager.GetInstance().HideUIForms(uiFromName);
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="msgType">��Ϣ����</param>
        /// <param name="msgName">��Ϣ����</param>
        /// <param name="msgContent">��Ϣ����</param>
        protected void SendMessage(string msgType, string msgName, object msgContent)
        {
            KeyValueUpdate kv = new KeyValueUpdate(msgName, msgContent);
            MessageCenter.SendMessage(msgType, kv);
        }

        /// <summary>
        /// ע����Ϣ
        /// </summary>
        /// <param name="msgType">��Ϣ����</param>
        /// <param name="handler">��Ϣ��Ӧ����</param>
        protected void ReceiveMessage(string msgType, MessageCenter.DelMessageDelivery handler)
        {
            MessageCenter.AddMsgListener(msgType, handler);
        }

        #endregion
    }
}
