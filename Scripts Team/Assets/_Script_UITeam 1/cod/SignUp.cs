using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SignUp : MonoBehaviour, IPointerClickHandler
{
    public GameObject Accountnameed;
    public GameObject textAccountname;
    public GameObject Password;
    public GameObject textPassword;
    public GameObject AccountEmail;
    public GameObject textAccountEmail;
    public GameObject ConfirmPassword;
    public GameObject textConfirmPassword;

    public UnityEvent onClick;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        Debug.Log(name + " Game Object Clicked!", this);

        // invoke your event
        onClick.Invoke();
    }
    public GameObject mainPanel;

    public GameObject Panel;

    public void OpenPanel()
    {
        Panel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void SignUP()
    {
        SceneManager.LoadScene("Menu");
    }
}
