using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    /// <summary>
    /// 消息中心(发送消息，获取消息(有明确前后执行时间和顺序，就可以直接获取，而不是监听再执行)，监听消息(对于UI类，想要实时改变，就要监听获取，而不是直接获取))
    /// </summary>
    public class MessageCenter
    {
        // 委托，消息传递
        public delegate void DelMessageDelivery(KeyValueUpdate kv);
        // 消息中心缓存集合
        public static Dictionary<string, DelMessageDelivery> _dicMessages = new Dictionary<string, DelMessageDelivery>();

        /// <summary>
        /// 添加消息的监听
        /// </summary>
        /// <param name="messageType">消息分类</param>
        /// <param name="handler">消息委托</param>
        public static void AddMsgListener(string messageType, DelMessageDelivery handler)
        {
            // 判断该消息是否存在，如果存在则直接注册，如果不存在则现添加在注册
            if (!_dicMessages.ContainsKey(messageType))
            {
                _dicMessages.Add(messageType, null);
            }

            // 注册
            _dicMessages[messageType] += handler;
        }

        /// <summary>
        /// 清除指定分类的消息监听
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
        /// 取消所有消息监听
        /// </summary>
        public static void ClearAllMsgListener()
        {
            if (_dicMessages != null)
            {
                _dicMessages.Clear();
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageType">消息分类</param>
        /// <param name="kv">需要发送的值（自定义键值对结构）</param>
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
