using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    /// <summary>
    /// Unity UI框架帮助脚本
    /// </summary>
    public class UnityHelper : MonoBehaviour
    {
        /// <summary>
        /// 查找子对象
        /// </summary>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
        public static Transform FindTheChildNode(GameObject goParent, string childName)
        {
            Transform searchTrans = null;

            searchTrans = goParent.transform.Find(childName);

            // 如果没找到，则继续
            if (searchTrans == null)
            {
                foreach (Transform trans in goParent.transform)
                {
                    searchTrans = FindTheChildNode(trans.gameObject, childName);

                    // 递归查找（子节点的子节点）
                    if (searchTrans != null)
                        return searchTrans;
                }
            }
            else
            {
                return searchTrans;
            }

            return null;
        }

        /// <summary>
        /// 获取子对象上的组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
        public static T GetTheChildNodeComponent<T>(GameObject goParent, string childName) where T : Component
        {
            Transform searchTranformNode = null;

            // 查找子节点
            searchTranformNode = FindTheChildNode(goParent, childName);

            if (searchTranformNode != null)
            {
                // 返回子节点上挂载的脚本
                return searchTranformNode.gameObject.GetComponent<T>();
            }
            return null;
        }

        /// <summary>
        /// 给子对象添加组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
        public static T AddTheChildNodeComponent<T>(GameObject goParent, string childName) where T : Component
        {
            Transform searchTranformNode = null;

            // 查找子节点
            searchTranformNode = FindTheChildNode(goParent, childName);

            if (searchTranformNode != null)
            {
                // 判断是否有相同组件已经被添加，若有则删除再添加
                T[] componentArray = searchTranformNode.GetComponents<T>();
                for (int i = 0; i < componentArray.Length; i++)
                {
                    if (componentArray[i] != null)
                        Destroy(componentArray[i]);
                }

                // 添加组件并返回
                return searchTranformNode.gameObject.AddComponent<T>();
            }
            return null;
        }

        /// <summary>
        /// 给子对象添加父对象
        /// </summary>
        /// <param name="parents">父对象</param>
        /// <param name="child">子对象</param>
        public static void AddChildNodeToParentNode(Transform parents, Transform child)
        {
            child.SetParent(parents, false);
            child.localPosition = Vector3.zero;
            child.localScale = Vector3.one;
            child.localEulerAngles = Vector3.zero;
        }
    }
}
