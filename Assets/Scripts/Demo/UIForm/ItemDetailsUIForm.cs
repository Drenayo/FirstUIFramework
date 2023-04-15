using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SUIFW;
namespace Demo
{
    public class ItemDetailsUIForm : BaseUIForm
    {
        private Text textName; 
        private Text textInfo;
        private Text textPrice;

        private void Awake()
        {
            textName = UnityHelper.GetTheChildNodeComponent<Text>(gameObject, "textName");
            textInfo = UnityHelper.GetTheChildNodeComponent<Text>(gameObject, "textInfo");
            textPrice = UnityHelper.GetTheChildNodeComponent<Text>(gameObject, "textPrice");

            SettingUIFormTypeByEditors();

            RegisterButtonEvent("Btn_Close", ·µ»Ø);

            ReceiveMessage("Item", kv => {
                string[] str = kv.Value as string[];
                textName.text = str[0];
                textInfo.text = str[1];
                textPrice.text = str[2];
            });
            
        }


        public void ·µ»Ø()
        {
            CloseUIForm(ProConst.UI_ITEMDETAILS);
        }
    }
}
