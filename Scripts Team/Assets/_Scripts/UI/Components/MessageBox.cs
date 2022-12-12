using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace Heros.UI.Components
{
    public class MessageBox : MonoBehaviour
    {
        [SerializeField] TMP_Text messageText;
        [SerializeField] Button messageButton;


        public void SetMessageText(string text)
        {
            messageText.text = text;
        }

        private void OnEnable()
        {
            messageButton.onClick.AddListener(OnClickMessageButton);
        }

        private void OnClickMessageButton()
        {
            gameObject.SetActive(false);

        }

        private void OnDisable()
        {
            messageButton.onClick.RemoveListener(OnClickMessageButton);

        }



    }

}