using Heros.Backend.Authentication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanel : MonoBehaviour
{
    [Header("Setting Panel")]
    public GameObject Panel;


    [SerializeField]
    AuthenticationServiceSO authenticationServiceSO;

    private void OnEnable()
    {
        authenticationServiceSO.OnLogoutEvent += AuthenticationServiceSO_OnLogoutEvent;
    }
    private void OnDisable()
    {
        authenticationServiceSO.OnLogoutEvent -= AuthenticationServiceSO_OnLogoutEvent;

    }
    private void AuthenticationServiceSO_OnLogoutEvent()
    {
        Panel.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void OpenPanel()
    {
        Panel.SetActive(true);
    }
    public void closePanel()
    {
        Panel.SetActive(false);
    }
    public void LogOut()
    {
        authenticationServiceSO.Logout();
    }

}
