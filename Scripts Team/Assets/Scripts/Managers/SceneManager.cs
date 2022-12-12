using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;
using Photon.Pun.UtilityScripts;
using TMPro;
namespace Heros.Manager
{
    public class SceneManager : MonoBehaviourPunCallbacks
    {

        #region Variables

        [Header("Player Resources")]
        public GameObject playerPrefab1;
        public GameObject playerPrefab2;
        public GameObject playerPrefab3;
        int playerNum = 0;
        [SerializeField]
        public Transform[] SpwanPointsTeam1;
        public Transform[] SpwanPointsTeam2;
        GameObject thisPlayer;
        Photon.Realtime.Player thisPlayerPun;
        public TextMeshProUGUI playerName;



        void Start()
        {
            GameObject player = (GameObject)PhotonNetwork.Instantiate("Prefab/PlayersPrefabs/" + playerPrefab1.name,
            Vector3.zero, Quaternion.identity);

            thisPlayer = GameObject.FindObjectOfType<Heros.Players.Player>().gameObject;
            thisPlayerPun = thisPlayer.GetComponent<PhotonView>().Owner;

            playerName.text = thisPlayerPun.NickName;

            if ((int)thisPlayerPun.CustomProperties["team"] == 1)
            {
                player.transform.position = SpwanPointsTeam1[(int)thisPlayerPun.CustomProperties["postion"]].position;
                player.GetComponent<CheckPhoton>().respawnPos = SpwanPointsTeam1[(int)thisPlayerPun.CustomProperties["postion"]].position;
                GetComponent<PhotonView>().RPC("addTOTeam", RpcTarget.AllBuffered, true);

            }
            else if ((int)thisPlayerPun.CustomProperties["team"] == 2)
            {
                player.transform.position = SpwanPointsTeam2[(int)thisPlayerPun.CustomProperties["postion"]].position;
                player.GetComponent<CheckPhoton>().respawnPos = SpwanPointsTeam2[(int)thisPlayerPun.CustomProperties["postion"]].position;
                GetComponent<PhotonView>().RPC("addTOTeam", RpcTarget.AllBuffered, false);
            }
        }

        [PunRPC]
        public void addTOTeam(bool first)
        {
            if (first)
            {
                GameObject.Find("Teams").GetComponent<Team>().playersBlue.Add(GameObject.FindObjectOfType<Heros.Players.Player>().gameObject);
            }
            else
            {
                GameObject.Find("Teams").GetComponent<Team>().playersRed.Add(GameObject.FindObjectOfType<Heros.Players.Player>().gameObject);
            }
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                foreach (KeyValuePair<int, Player> kvp in PhotonNetwork.CurrentRoom.Players)
                {
                    Debug.Log(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
                    Debug.Log("avatar = " + kvp.Value.CustomProperties["avatar"]);
                    Debug.Log("team = " + kvp.Value.CustomProperties["team"]);
                    Debug.Log("position = " + kvp.Value.CustomProperties["team"]);
                    Debug.Log("kills = " + kvp.Value.CustomProperties["kills"]);
                    Debug.Log("dead = " + kvp.Value.CustomProperties["dead"]);
                }
            }
        }

        #endregion

    }
}