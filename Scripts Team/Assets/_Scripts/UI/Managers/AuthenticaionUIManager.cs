using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PlayFab;
using System;
using Heros.Backend.Authentication;

namespace Heros.UI.Managers
{
    public class AuthenticaionUIManager : MonoBehaviour
    {
        [Header("Authentication Manager Scriptable Object")]
        [SerializeField] private AuthenticationServiceSO _authenticationServiceSO;




        [Header("UI Canvas and main elemnts")]
        [SerializeField] GameObject registerCanvas;
        [SerializeField] GameObject loggedInCanvas;
        [SerializeField] GameObject loginCanvas;

        [Header("Register UI Elements")]
        [SerializeField] TMP_InputField emailInput;
        [SerializeField] TMP_InputField usernameInput;
        [SerializeField] TMP_InputField passwordInput;
        [SerializeField] Button registerButton;
        [SerializeField] TMP_Text registerCallBackMessage;

        [Header("Login UI Elements")]
        [SerializeField] TMP_InputField loginEmailInput;
        [SerializeField] TMP_InputField loginPasswordInput;
        [SerializeField] Button loginButton;
        [SerializeField] TMP_Text loginCallBackMessage;


        [Header("LoggedIn UI Elements")]
        [SerializeField] Button logOutButton;
        [SerializeField] TMP_Text loggedInCallbackMessage;







        private void OnEnable()
        {
            _authenticationServiceSO.OnRegisterSuccessEvent += OnRegisterSuccessEvent_OnAuthSuccess;
            _authenticationServiceSO.OnRegisterFailedEvent += OnRegisterFailedEvent_OnStringEvent;
            _authenticationServiceSO.OnLogoutEvent += OnLogout_onActionEvent;
            _authenticationServiceSO.OnLoginFailedEvent += OnLoginFailedEvent_OnStringEvent;
            _authenticationServiceSO.OnLoginSuccessEvent += OnLoginSuccessEvent_OnAuthSuccess;

            registerButton.onClick.AddListener(OnClickRegister);
            logOutButton.onClick.AddListener(Logout);
            loginButton.onClick.AddListener(OnClickLogin);
        }



        private void OnLoginSuccessEvent_OnAuthSuccess(string userId, string username)
        {
            loginCanvas.SetActive(false);
            loggedInCanvas.SetActive(true);
            loggedInCallbackMessage.text = $"Welcome {username}";



        }

        private void OnLoginFailedEvent_OnStringEvent(string message)
        {
            Debug.Log("login failed");
            loginCallBackMessage.text = message;
        }

        private void OnLogout_onActionEvent()
        {
            loggedInCanvas.SetActive(false);
            loginCanvas.SetActive(true);
            Debug.Log("User has logged out");
        }

        private void OnDisable()
        {
            _authenticationServiceSO.OnRegisterSuccessEvent -= OnRegisterSuccessEvent_OnAuthSuccess;
            _authenticationServiceSO.OnRegisterFailedEvent -= OnRegisterFailedEvent_OnStringEvent;
            _authenticationServiceSO.OnLogoutEvent -= OnLogout_onActionEvent;
            _authenticationServiceSO.OnLoginFailedEvent -= OnLoginFailedEvent_OnStringEvent;
            _authenticationServiceSO.OnLoginSuccessEvent -= OnLoginSuccessEvent_OnAuthSuccess;

            registerButton.onClick.RemoveAllListeners();
            logOutButton.onClick.RemoveAllListeners();
            loginButton.onClick.RemoveAllListeners();
        }

        private void Awake()
        {

            _authenticationServiceSO.CheckIfLoggedIn();

        }

        private void OnClickLogin()
        {
            Debug.Log("Login Click");
            var loginParams = new LoginParams { Email = loginEmailInput.text, Password = loginPasswordInput.text };
            _authenticationServiceSO.LoginWithEmail(loginParams);
        }

        private void Logout()
        {
            _authenticationServiceSO.Logout();
        }

        private void OnRegisterFailedEvent_OnStringEvent(string message)
        {
            registerCallBackMessage.text = message;
        }

        private void OnRegisterSuccessEvent_OnAuthSuccess(string playerId, string username)
        {
            loggedInCallbackMessage.text = $"Welcome {username}";
            registerCanvas.SetActive(false);
            loggedInCanvas.SetActive(true);
        }


        public void OnClickRegister()
        {
            Debug.Log("Register button clicked");
            var registerParams = new RegisterParams
            {
                Email = emailInput.text,
                Username = usernameInput.text,
                Password = passwordInput.text
            };

            _authenticationServiceSO.RegisterWithEmail(registerParams);
        }


    }

}