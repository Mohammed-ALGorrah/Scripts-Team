using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendPanel : MonoBehaviour
{
    [Header("Panel Friend ")]
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
