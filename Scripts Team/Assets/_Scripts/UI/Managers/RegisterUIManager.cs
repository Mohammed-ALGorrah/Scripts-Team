using Heros.Backend.Authentication;
using Heros.Backend.PlayerData;
using Heros.UI.Components;
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
            _playerDataServiceSO.OnGetPlayerDataSuccess += _playerDataServiceSO_OnGetPlayerDataSuccess;
            _playerDataServiceSO.OnGetPlayerDataError += _playerDataServiceSO_OnGetPlayerDataError;
        }

        private void _playerDataServiceSO_OnGetPlayerDataError(string obj)
        {
            Debug.Log("Registerd Error!" + obj);
        }

        private void _playerDataServiceSO_OnGetPlayerDataSuccess(PlayerDataInfo obj)
        {
            Debug.Log("Registerd successfully !");
            SceneManager.LoadScene(1);
        }

        private void _authenticationServiceSO_OnRegisterFailedEvent(string error)
        {
            messagePanel.SetMessageText(error);
            messagePanel.gameObject.SetActive(true);
        }

        private void _authenticationServiceSO_OnRegisterSuccessEvent(string arg1, string arg2)
        {
            _playerDataServiceSO.GetAllPlayerData();
           // SceneManager.LoadScene(1);
            Debug.Log("Registerd successfully !");
            //SceneManager.LoadScene(1);
            //StartCoroutine("enumerator");
            
        }

        private void OnClickLoginLink()
        {
            loginPanel.SetActive(true);
            registerPanel.SetActive(false);
        }

        IEnumerator enumerator()
        {
            yield return new WaitForSeconds(20f);
            
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
            _playerDataServiceSO.OnGetPlayerDataSuccess -= _playerDataServiceSO_OnGetPlayerDataSuccess;
            _playerDataServiceSO.OnGetPlayerDataError -= _playerDataServiceSO_OnGetPlayerDataError;


        }
    }

}