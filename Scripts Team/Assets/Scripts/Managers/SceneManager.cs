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
            GameObject player = (GameObject)PhotonNetwork.Instantiate("Prefab/PlayersPrefabs/"+playerPrefab1.name, 
            SpwanPoints[((int)PhotonNetwork.LocalPlayer.CustomProperties["position"])].position, Quaternion.identity);
            GetComponent<PhotonView>().RPC("addToTeam",RpcTarget.AllBuffered,
            player.GetComponentInChildren<PhotonView>().ViewID,true);
        
          }else if((int)PhotonNetwork.LocalPlayer.CustomProperties["team"] == 2){
            Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["position"]);
            GameObject player = (GameObject)PhotonNetwork.Instantiate("Prefab/PlayersPrefabs/"+playerPrefab1.name,
            SpwanPoints[((int)PhotonNetwork.LocalPlayer.CustomProperties["position"])].position, Quaternion.identity);
            GetComponent<PhotonView>().RPC("addToTeam",RpcTarget.AllBuffered,
            player.GetComponentInChildren<PhotonView>().ViewID,false);

          }

        }

    }

    [PunRPC]
    private void addToTeam(int id,bool first){

        if(first){
            GameObject.Find("FirstTeam").GetComponent<Team>().players.Add(id);
        }else{
            GameObject.Find("SecondTeam").GetComponent<Team>().players.Add(id);
        }
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
