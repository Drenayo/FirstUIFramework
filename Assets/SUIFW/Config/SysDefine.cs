using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    /// <summary>
    /// 系统定义信息
    /// </summary>
    public class SysDefine : MonoBehaviour
    {


    }

    #region 枚举
    /// <summary>
    /// UI窗体类型(位置)
    /// Normal 普通窗体 全屏的
    /// Fixed 固定窗体 部分的，但又是固定的，比如右上角金币信息之类的
    /// PopUp 弹出窗体 
    /// </summary>
    public enum UIFormType
    {
        /// <summary>
        /// 普通窗体
        /// </summary>
        Normal,
        /// <summary>
        /// 固定窗体
        /// </summary>
        Fixed,
        /// <summary>
        /// 弹出窗体
        /// </summary>
        PopUp
    }

    /// <summary>
    /// UI窗体显示类型
    /// Normal 普通窗体，可以点击父窗体
    /// ReverseChange 按照顺序来回切换 
    /// HideOther 窗体加载后，会隐藏掉其他所有窗体
    /// </summary>
    public enum UIFormShowMode
    {
        /// <summary>
        /// 普通窗体，切换时父窗体不隐藏
        /// </summary>
        Normal,
        /// <summary>
        /// 只能按打开顺序反切换回去
        /// </summary>
        ReverseChange,
        /// <summary>
        /// 隐藏其他窗体
        /// </summary>
        HideOther
    }

    /// <summary>
    /// UI窗体的透明度类型
    /// </summary>
    public enum UIFormLucenyType
    {
        /// <summary>
        /// 完全透明，无法穿透
        /// </summary>
        Lucency,
        /// <summary>
        /// 半透明，无法穿透
        /// </summary>
        Translucence,
        /// <summary>
        /// 低透明的，无法穿透
        /// </summary>
        ImPenetrable,
        /// <summary>
        /// 可以穿透
        /// </summary>
        Pentrate
    }
    #endregion
}
