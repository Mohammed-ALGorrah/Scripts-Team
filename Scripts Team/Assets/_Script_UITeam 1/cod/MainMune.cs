using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMune : MonoBehaviour, IPointerClickHandler
{

    public UnityEvent onClick;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        Debug.Log(name + " Game Object Clicked!", this);

        // invoke your event
        onClick.Invoke();
    }


    public void Login()
    {

        SceneManager.LoadScene("LOGIN");
    }
    public void Signin()
    {
        SceneManager.LoadScene("SIGNIN");
    }
    public void Guest()
    {
        SceneManager.LoadScene("GUEST");
    }
    public void logGame()
    {
        SceneManager.LoadScene("Mune");
    }
    public void Bake()
    {
        SceneManager.LoadScene("FOR");
    }

}
