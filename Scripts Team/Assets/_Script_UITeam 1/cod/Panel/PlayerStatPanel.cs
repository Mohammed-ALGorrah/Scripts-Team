using Heros.Backend.PlayfabData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatPanel : MonoBehaviour
{
    [Header("Player Stat panel")]
    public GameObject Panel;
    public TextMeshProUGUI playerName;
    public PlayFabConstants userPlayfab;
    private void Start()
    {
        playerName.text = userPlayfab.savedUsername;
    }
    
    public void OpenPanel()
    {
        Panel.SetActive(true);
    }
    public void closePanel()
    {
        Panel.SetActive(false);
    }
}
