using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Heros.Backend.Authentication;
using System;
using Heros.UI.Components;
using UnityEngine.SceneManagement;

namespace Heros.UI.Managers
{
    public class LoginUIManager : MonoBehaviour
    {
        [Header("Input fields")]
        public InputField  EmailInputField;
        public InputField PasswordInputField;

        [Header("Buttons")]
        public Button loginButton;
        public Button facebookLoginButton;
        public Button googleLoginButton;
        public Button signUpLinkButton;

        [Header("Panels")]
        public GameObject loginPanel;
        public GameObject registerPanel;
        public MessageBox messagePanel;

        

        [Header("Authentication Services")]
        [SerializeField] private AuthenticationServiceSO _authenticationServiceSO;

        private void OnEnable()
        {
            signUpLinkButton.onClick.AddListener(OnClickRegisterButton);
            loginButton.onClick.AddListener(OnClickLoginButton);
            _authenticationServiceSO.OnLoginSuccessEvent += _authenticationServiceSO_OnLoginSuccessEvent;
            _authenticationServiceSO.OnLoginFailedEvent += _authenticationServiceSO_OnLoginFailedEvent;
        }

       

        private void OnDisable()
        {
            signUpLinkButton.onClick.RemoveListener(OnClickRegisterButton);
            loginButton.onClick.RemoveListener(OnClickLoginButton);
            _authenticationServiceSO.OnLoginSuccessEvent -= _authenticationServiceSO_OnLoginSuccessEvent;
            _authenticationServiceSO.OnLoginFailedEvent -= _authenticationServiceSO_OnLoginFailedEvent;

        }



        public void OnClickRegisterButton()
        {
            loginPanel.SetActive(false);
            registerPanel.SetActive(true);
        }


        private void OnClickLoginButton()
        {
            Debug.Log("clicked");
            var loginParams = new LoginParams { Email = EmailInputField.text, Password = PasswordInputField.text };
            _authenticationServiceSO.LoginWithEmail(loginParams);

        }


        private void _authenticationServiceSO_OnLoginFailedEvent(string error)
        {
            messagePanel.gameObject.SetActive(true);
            messagePanel.SetMessageText(error);
        }

        private void _authenticationServiceSO_OnLoginSuccessEvent(string id, string username)
        {

            Debug.Log($"Login Success for {id} - {username}");
            SceneManager.LoadScene(1);
        }


    }

}