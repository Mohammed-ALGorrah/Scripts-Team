using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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

    [SerializeField]
    TextMeshProUGUI btnTxt;

    //we have to save hero on backend
    private void Awake() {
        Debug.Log(characters.Length + this.gameObject.name);
        Debug.Log(selectedCharacter + this.gameObject.name);
        if (PlayerPrefs.GetString("CH","Wizard") == characters[selectedCharacter].gameObject.name)
        {
            btnTxt.text = "Selected";
        }else{
            btnTxt.text = "Select";
        }
    }
   
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
        if (selectedCharacter + 1 >= characters.Length)
        {
            selectedCharacter = 0;
            characters[selectedCharacter].SetActive(true);

        }else{
            selectedCharacter += 1;
            characters[selectedCharacter].SetActive(true);
        }

        if (PlayerPrefs.GetString("CH","Wizard") == characters[selectedCharacter].gameObject.name)
        {
            btnTxt.text = "Selected";
        }else{
            btnTxt.text = "Select";
        }

    }

    public void PreviousCharater()
    {
        characters[selectedCharacter].SetActive(false);
        if (selectedCharacter - 1 < 0 )
        {
            selectedCharacter = characters.Length - 1;
            characters[selectedCharacter].SetActive(true);

        }else{
            selectedCharacter -= 1;
            characters[selectedCharacter].SetActive(true);
        }

        if (PlayerPrefs.GetString("CH","Wizard") == characters[selectedCharacter].gameObject.name)
        {
            btnTxt.text = "Selected";
        }else{
            btnTxt.text = "Select";
        }
    }

    public void SelectCharachter(){
        PlayerPrefs.SetString("CH",characters[selectedCharacter].gameObject.name);
        btnTxt.text = "Selected";
    }   
}
