using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
    /// <summary>
    /// JsonΩ‚Œˆ“Ï≥£¿‡
    /// </summary>
    public class JsonAnlysisException : Exception
    {
        public JsonAnlysisException() : base() { }

        public JsonAnlysisException(string exceptionMessage) : base(exceptionMessage)
        {

        }
    }
}
