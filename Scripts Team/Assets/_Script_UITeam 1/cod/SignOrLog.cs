using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignOrLog : MonoBehaviour
{
    public GameObject mainPanel;

    public GameObject Panel;

    public void OpenPanel()
    {
        Panel.SetActive(true);
        mainPanel.SetActive(false);
    }
}
