using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftsPanel : MonoBehaviour
{
    [Header("Gift Panel")]
    public GameObject Panel;

    public void OpenPanel()
    {
        Panel.SetActive(true);
    }
    public void closePanel()
    {
        Panel.SetActive(false);
    }
}
