using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectCharacter : MonoBehaviour
{
    [Header("Select Character Panel")]
    public GameObject Panel;
    [Header("change_color_Character_Panel_")]
    public GameObject mainPanel;
    [Header("Array of Characters")]
    public GameObject[] characters;
    public int selectedCharacter = 0;
   
    public void OpenPanel()
    {
        Panel.SetActive(true);
    }
    public void OpenPanelColor()
    {
        mainPanel.SetActive(true);
        Panel.SetActive(false);
    }

    public void closePanel()
    {
        Panel.SetActive(false);
    }
    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }
    public void PreviousCharater()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }   
}
