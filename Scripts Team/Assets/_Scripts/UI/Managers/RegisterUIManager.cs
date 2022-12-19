using Heros.Backend.Authentication;
using Heros.Backend.PlayerData;
using Heros.UI.Components;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Heros.UI.Managers
{
    public class RegisterUIManager : MonoBehaviour
    {
        [Header("Input fields")]
        public InputField emailInputField;
        public InputField usernameInputField;
        public InputField passwordInputField;
        public InputField confirmPasswordInputField;

        [Header("Buttons")]
        public Button loginLinkButton;
        public Button facebookLoginButton;
        public Button googleLoginButton;
        public Button signUpButton;

        [Header("Panels")]
        public GameObject loginPanel;
        public GameObject registerPanel;
        public MessageBox messagePanel;

        [Header("Authentication Services")]
        [SerializeField] private AuthenticationServiceSO _authenticationServiceSO;

        [Header("Services")]
        [SerializeField] PlayerDataServiceSO _playerDataServiceSO;

        private void OnEnable()
        {
            signUpButton.onClick.AddListener(OnClickRegisterButton);
            loginLinkButton.onClick.AddListener(OnClickLoginLink);
            _authenticationServiceSO.OnRegisterSuccessEvent += _authenticationServiceSO_OnRegisterSuccessEvent;
            _authenticationServiceSO.OnRegisterFailedEvent += _authenticationServiceSO_OnRegisterFailedEvent;
        }

        private void _authenticationServiceSO_OnRegisterFailedEvent(string error)
        {
            messagePanel.gameObject.SetActive(true);
            messagePanel.SetMessageText(error);
            
        }

        private void _authenticationServiceSO_OnRegisterSuccessEvent(string arg1, string arg2)
        {
         //  _playerDataServiceSO.GetAllPlayerData();
            Debug.Log("Registerd successfully !");
            SceneManager.LoadScene(1);              
        }

        private void OnClickLoginLink()
        {
            loginPanel.SetActive(true);
            registerPanel.SetActive(false);
        }

        private void OnClickRegisterButton()
        {
            
            Debug.Log("Register button clicked");

            if(passwordInputField.text != confirmPasswordInputField.text)
            {
                messagePanel.SetMessageText("Password and confirm password is not the same !");
                messagePanel.gameObject.SetActive(true);
                return;
            }

            _authenticationServiceSO.RegisterWithEmail(new RegisterParams 
            {Email= emailInputField.text,Password = passwordInputField.text,Username=usernameInputField.text });
        }



        private void OnDisable()
        {
            signUpButton.onClick.RemoveListener(OnClickRegisterButton);
            loginLinkButton.onClick.RemoveListener(OnClickLoginLink);
            _authenticationServiceSO.OnRegisterSuccessEvent -= _authenticationServiceSO_OnRegisterSuccessEvent;
        }
    }

}