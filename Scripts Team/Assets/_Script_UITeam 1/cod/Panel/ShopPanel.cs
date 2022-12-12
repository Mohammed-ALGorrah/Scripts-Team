using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [Header("button skin")]
    public Button Skin;
    [Header("button Daymond")]
    public Button Daymond;
    [Header("button Gold")]
    public Button Gold;
    [Header("Shop panel")]
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

