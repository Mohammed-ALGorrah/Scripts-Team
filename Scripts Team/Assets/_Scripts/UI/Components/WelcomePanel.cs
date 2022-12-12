using Heros.Backend.Authentication;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Heros.UI.Components
{
    public class WelcomePanel : MonoBehaviour
    {
        [Header("Panels")]
        public GameObject loginPanel;
        public GameObject registerPanel;
        
        [Header("Buttons")]
        public Button loginButton;
        public Button registerButton;

        [Header("Services")]
        [SerializeField] private AuthenticationServiceSO _authenticationServiceSO;

        private void Awake()
        {
          
            
        }

        private void Start()
        {
            _authenticationServiceSO.CheckIfLoggedIn();

        }

        private void OnEnable()
        {
            loginButton.onClick.AddListener(OnLoginButtonClick);
            registerButton.onClick.AddListener(OnRegisterButtonClick);
            _authenticationServiceSO.OnLoginSuccessEvent += _authenticationServiceSO_OnLoginSuccessEvent;
        }

        private void _authenticationServiceSO_OnLoginSuccessEvent(string arg1, string arg2)
        {
            Debug.Log("Logging using saved custom id");
            SceneManager.LoadScene(1);
        }

        private void OnRegisterButtonClick()
        {
            registerPanel.SetActive(true);
            gameObject.SetActive(false);
        }

        private void OnLoginButtonClick()
        {
            loginPanel.SetActive(true);
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {

            loginButton.onClick.RemoveListener(OnLoginButtonClick);
            registerButton.onClick.RemoveListener(OnRegisterButtonClick);
            _authenticationServiceSO.OnLoginSuccessEvent -= _authenticationServiceSO_OnLoginSuccessEvent;



        }
    }

}