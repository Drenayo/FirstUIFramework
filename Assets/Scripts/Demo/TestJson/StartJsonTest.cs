using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{
    public class StartJsonTest : MonoBehaviour
    {
        public TextAsset jsonObj;
        void Start()
        {
            Debug.Log("s");
            PeopleInfo peoInfo = JsonUtility.FromJson<PeopleInfo>(jsonObj.text);
            Debug.Log("sss");
            foreach (People p in peoInfo.People)
            {
                Debug.Log("");
                Debug.Log(string.Format("[{0}],[{1}]",p.Name,p.Age));
            }
            Debug.Log("ssss");
        }


        void Update()
        {
            
        }
    }
}
