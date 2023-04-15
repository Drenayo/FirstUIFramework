using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    /// <summary>
    /// Unity UI��ܰ����ű�
    /// </summary>
    public class UnityHelper : MonoBehaviour
    {
        /// <summary>
        /// �����Ӷ���
        /// </summary>
        /// <param name="goParent">������</param>
        /// <param name="childName">�Ӷ�������</param>
        /// <returns></returns>
        public static Transform FindTheChildNode(GameObject goParent, string childName)
        {
            Transform searchTrans = null;

            searchTrans = goParent.transform.Find(childName);

            // ���û�ҵ��������
            if (searchTrans == null)
            {
                foreach (Transform trans in goParent.transform)
                {
                    searchTrans = FindTheChildNode(trans.gameObject, childName);

                    // �ݹ���ң��ӽڵ���ӽڵ㣩
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
        /// ��ȡ�Ӷ����ϵ����
        /// </summary>
        /// <typeparam name="T">�������</typeparam>
        /// <param name="goParent">������</param>
        /// <param name="childName">�Ӷ�������</param>
        /// <returns></returns>
        public static T GetTheChildNodeComponent<T>(GameObject goParent, string childName) where T : Component
        {
            Transform searchTranformNode = null;

            // �����ӽڵ�
            searchTranformNode = FindTheChildNode(goParent, childName);

            if (searchTranformNode != null)
            {
                // �����ӽڵ��Ϲ��صĽű�
                return searchTranformNode.gameObject.GetComponent<T>();
            }
            return null;
        }

        /// <summary>
        /// ���Ӷ���������
        /// </summary>
        /// <typeparam name="T">�������</typeparam>
        /// <param name="goParent">������</param>
        /// <param name="childName">�Ӷ�������</param>
        /// <returns></returns>
        public static T AddTheChildNodeComponent<T>(GameObject goParent, string childName) where T : Component
        {
            Transform searchTranformNode = null;

            // �����ӽڵ�
            searchTranformNode = FindTheChildNode(goParent, childName);

            if (searchTranformNode != null)
            {
                // �ж��Ƿ�����ͬ����Ѿ�����ӣ�������ɾ�������
                T[] componentArray = searchTranformNode.GetComponents<T>();
                for (int i = 0; i < componentArray.Length; i++)
                {
                    if (componentArray[i] != null)
                        Destroy(componentArray[i]);
                }

                // ������������
                return searchTranformNode.gameObject.AddComponent<T>();
            }
            return null;
        }

        /// <summary>
        /// ���Ӷ�����Ӹ�����
        /// </summary>
        /// <param name="parents">������</param>
        /// <param name="child">�Ӷ���</param>
        public static void AddChildNodeToParentNode(Transform parents, Transform child)
        {
            child.SetParent(parents, false);
            child.localPosition = Vector3.zero;
            child.localScale = Vector3.one;
            child.localEulerAngles = Vector3.zero;
        }
    }
}
