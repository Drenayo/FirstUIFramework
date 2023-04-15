using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    /// <summary>
    /// ��Ϣ����(������Ϣ����ȡ��Ϣ(����ȷǰ��ִ��ʱ���˳�򣬾Ϳ���ֱ�ӻ�ȡ�������Ǽ�����ִ��)��������Ϣ(����UI�࣬��Ҫʵʱ�ı䣬��Ҫ������ȡ��������ֱ�ӻ�ȡ))
    /// </summary>
    public class MessageCenter
    {
        // ί�У���Ϣ����
        public delegate void DelMessageDelivery(KeyValueUpdate kv);
        // ��Ϣ���Ļ��漯��
        public static Dictionary<string, DelMessageDelivery> _dicMessages = new Dictionary<string, DelMessageDelivery>();

        /// <summary>
        /// �����Ϣ�ļ���
        /// </summary>
        /// <param name="messageType">��Ϣ����</param>
        /// <param name="handler">��Ϣί��</param>
        public static void AddMsgListener(string messageType, DelMessageDelivery handler)
        {
            // �жϸ���Ϣ�Ƿ���ڣ����������ֱ��ע�ᣬ������������������ע��
            if (!_dicMessages.ContainsKey(messageType))
            {
                _dicMessages.Add(messageType, null);
            }

            // ע��
            _dicMessages[messageType] += handler;
        }

        /// <summary>
        /// ���ָ���������Ϣ����
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="handle"></param>
        public static void RemoveMsgListener(string messageType,DelMessageDelivery handle)
        {
            if (_dicMessages.ContainsKey(messageType))
            {
                _dicMessages[messageType] -= handle;
            }
        }

        /// <summary>
        /// ȡ��������Ϣ����
        /// </summary>
        public static void ClearAllMsgListener()
        {
            if (_dicMessages != null)
            {
                _dicMessages.Clear();
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="messageType">��Ϣ����</param>
        /// <param name="kv">��Ҫ���͵�ֵ���Զ����ֵ�Խṹ��</param>
        public static void SendMessage(string messageType, KeyValueUpdate kv)
        {
            DelMessageDelivery del;
            if (_dicMessages.TryGetValue(messageType, out del))
            {
                del(kv);
            }
        }

    }

    public class KeyValueUpdate
    {
        private string _Key;
        private object _Value;
        public string Key { get { return _Key; } }
        public object Value { get { return _Value; } }

        public KeyValueUpdate(string key, object value)
        {
            _Key = key;
            _Value = value;
        }
    }
}
