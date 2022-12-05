using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;
using Photon.Pun.UtilityScripts;

public class SceneManager : MonoBehaviourPunCallbacks
{

    #region Variables

    [Header("Player Resources")]
    public GameObject playerPrefab1;
    public GameObject playerPrefab2;
    public GameObject playerPrefab3;
    int playerNum = 0;
    [SerializeField]
    public Transform [] SpwanPointsTeam1;
    public Transform [] SpwanPointsTeam2;
    GameObject thisPlayer;
    Photon.Realtime.Player thisPlayerPun;

    void Start()
    {
        GameObject player = (GameObject)PhotonNetwork.Instantiate("Prefab/PlayersPrefabs/" + playerPrefab1.name,
        Vector3.zero , Quaternion.identity);

        thisPlayer = GameObject.FindObjectOfType<Heros.Players.Player>().gameObject;
        thisPlayerPun = thisPlayer.GetComponent<PhotonView>().Owner;

        if ((int)thisPlayerPun.CustomProperties["team"] == 1)
        {
            player.transform.position = SpwanPointsTeam1[(int)thisPlayerPun.CustomProperties["postion"]].position;
            player.GetComponent<CheckPhoton>().respawnPos = SpwanPointsTeam1[(int)thisPlayerPun.CustomProperties["postion"]].position;
        }
        else if ((int)thisPlayerPun.CustomProperties["team"] == 2)
        {
            player.transform.position = SpwanPointsTeam2[(int)thisPlayerPun.CustomProperties["postion"]].position;
            player.GetComponent<CheckPhoton>().respawnPos = SpwanPointsTeam2[(int)thisPlayerPun.CustomProperties["postion"]].position;
        }


        
        
        

    }
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("XXXXXXXXXXXXXXXXX " + thisPlayerPun.ActorNumber);
            foreach (KeyValuePair<int, Player> kvp in PhotonNetwork.CurrentRoom.Players)
            {
                /*Debug.Log(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
                Debug.Log("avatar = " + kvp.Value.CustomProperties["avatar"]);
                Debug.Log("team = " + kvp.Value.CustomProperties["team"]);
                Debug.Log("position = " + kvp.Value.CustomProperties["team"]);*/
            }
        }
    }

    #endregion

}