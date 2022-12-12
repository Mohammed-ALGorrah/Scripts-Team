using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorCharacter : MonoBehaviour
{
    [Header("Change Color Character Panel")]
    public GameObject Panel;

    public void ClosePanel()
    {
        Panel.SetActive(false);
    }
}
