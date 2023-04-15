using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    /// <summary>
    /// ϵͳ������Ϣ
    /// </summary>
    public class SysDefine : MonoBehaviour
    {


    }

    #region ö��
    /// <summary>
    /// UI��������(λ��)
    /// Normal ��ͨ���� ȫ����
    /// Fixed �̶����� ���ֵģ������ǹ̶��ģ��������Ͻǽ����Ϣ֮���
    /// PopUp �������� 
    /// </summary>
    public enum UIFormType
    {
        /// <summary>
        /// ��ͨ����
        /// </summary>
        Normal,
        /// <summary>
        /// �̶�����
        /// </summary>
        Fixed,
        /// <summary>
        /// ��������
        /// </summary>
        PopUp
    }

    /// <summary>
    /// UI������ʾ����
    /// Normal ��ͨ���壬���Ե��������
    /// ReverseChange ����˳�������л� 
    /// HideOther ������غ󣬻����ص��������д���
    /// </summary>
    public enum UIFormShowMode
    {
        /// <summary>
        /// ��ͨ���壬�л�ʱ�����岻����
        /// </summary>
        Normal,
        /// <summary>
        /// ֻ�ܰ���˳���л���ȥ
        /// </summary>
        ReverseChange,
        /// <summary>
        /// ������������
        /// </summary>
        HideOther
    }

    /// <summary>
    /// UI�����͸��������
    /// </summary>
    public enum UIFormLucenyType
    {
        /// <summary>
        /// ��ȫ͸�����޷���͸
        /// </summary>
        Lucency,
        /// <summary>
        /// ��͸�����޷���͸
        /// </summary>
        Translucence,
        /// <summary>
        /// ��͸���ģ��޷���͸
        /// </summary>
        ImPenetrable,
        /// <summary>
        /// ���Դ�͸
        /// </summary>
        Pentrate
    }
    #endregion
}
