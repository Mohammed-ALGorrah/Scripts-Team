using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomPanel : MonoBehaviour
{
    [Header("Room Name ")]
    public TMP_InputField  roomName;
    [Header("Button Create")]
    public Button Create;
    [Header("Button Create Room")]
    public Button createRoom;
    [Header("Button Find Room")]
    public Button findRoom;
    [Header("Room panel")]
    public GameObject Panel;
    [Header(" Create Room panel")]
    public GameObject createPanel;
    [Header(" find Room panel")]
    public GameObject findPanel;

    

    public void OpenPanel()
    {
        //SceneManager.LoadScene(2);
        Panel.SetActive(true);
    }
    public void closePanel()
    {
        Panel.SetActive(false);
    }
    public void opencreatePanel()
    {
        createPanel.SetActive(true);
        findPanel.SetActive(false);
    
    }
    public void openfindPanel()
    {
        createPanel.SetActive(false);
        findPanel.SetActive(true);

    }
}
