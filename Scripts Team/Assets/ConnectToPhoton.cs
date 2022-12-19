using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Heros.Backend.PlayfabData;
using Heros.Backend.Authentication;
using UnityEngine.SceneManagement;

public class ConnectToPhoton : MonoBehaviourPunCallbacks
{

    public override void OnConnected()
    {
        Debug.Log("Connected");
     //   SceneManager.LoadScene(1);
    }

    
}
