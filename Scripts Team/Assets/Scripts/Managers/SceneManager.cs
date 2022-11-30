using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;

public class SceneManager : MonoBehaviour
{

    #region Variables

    [Header("Player Resources")]
    public GameObject playerPrefab1;
    public GameObject playerPrefab2;
    public GameObject playerPrefab3;

    [SerializeField]
    Transform [] SpwanPoints;

    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
          if ((int)PhotonNetwork.LocalPlayer.CustomProperties["team"] == 1){
            Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["position"]);
            PhotonNetwork.Instantiate("Prefab/PlayersPrefabs/"+playerPrefab1.name, 
            SpwanPoints[((int)PhotonNetwork.LocalPlayer.CustomProperties["position"])].position, Quaternion.identity);

          }else if((int)PhotonNetwork.LocalPlayer.CustomProperties["team"] == 2){
            Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["position"]);
            PhotonNetwork.Instantiate("Prefab/PlayersPrefabs/"+playerPrefab1.name,
            SpwanPoints[((int)PhotonNetwork.LocalPlayer.CustomProperties["position"])].position, Quaternion.identity);

          }

        }

/*
            int prefab = (int)PhotonNetwork.LocalPlayer.CustomProperties["avatar"];
            switch (prefab)
            {
                case 1:
                    PhotonNetwork.Instantiate("Prefab/PlayersPrefabs/"+playerPrefab1.name, new Vector3(randomNumber1, 5f, randomNumber2), Quaternion.identity);
                    break;
                case 2:
                    PhotonNetwork.Instantiate("Prefab/PlayersPrefabs/"+playerPrefab2.name, new Vector3(randomNumber1, 5f, randomNumber2), Quaternion.identity);
                    break;
                case 3:
                    PhotonNetwork.Instantiate("Prefab/PlayersPrefabs/"+playerPrefab3.name, new Vector3(randomNumber1, 5f, randomNumber2), Quaternion.identity);
                    break;
                default:
                    PhotonNetwork.Instantiate("Prefab/PlayersPrefabs/"+playerPrefab1.name, new Vector3(randomNumber1, 5f, randomNumber2), Quaternion.identity);
                    break;
            }
            
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (KeyValuePair<int, Player> kvp in PhotonNetwork.CurrentRoom.Players)
            {
                Debug.Log(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
                Debug.Log("avatar = " + kvp.Value.CustomProperties["avatar"]);
                Debug.Log("team = "+kvp.Value.CustomProperties["team"]);
                Debug.Log("position = "+kvp.Value.CustomProperties["team"]);
            }
        }
    }

    #endregion

}
