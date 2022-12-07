using Heros.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Team : MonoBehaviour
{
    public List <GameObject> playersRed,playersBlue;
    public Text txtCounterRed,txtCounterBlue;
    public int currentScoreRed,currentScoreBlue;  

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            /*for (int i = 0; i < playersRed.Count; i++)
            {
                Debug.Log("Reeeeeeeed kill " + playersRed[i].GetComponent<PhotonView>().Owner.CustomProperties["team"] );

                Debug.Log("reddddddddd dead " + playersRed[i].GetComponent<PhotonView>().Owner.CustomProperties["dead"]);
            }
            for (int i = 0; i < playersBlue.Count; i++)
            {
                Debug.Log("Blueeeee kill " +playersBlue[i].GetComponent<PhotonView>().Owner.CustomProperties["team"]);

                Debug.Log("Blueeeee dead " + playersBlue[i].GetComponent<PhotonView>().Owner.CustomProperties["dead"]);
            }*/
            foreach (KeyValuePair<int, Photon.Realtime.Player> kvp in PhotonNetwork.CurrentRoom.Players)
            {
                Debug.Log("Team = " + kvp.Value.CustomProperties["team"] + "kills = " + kvp.Value.CustomProperties["kills"] 
                    + "dead = " + kvp.Value.CustomProperties["dead"]);
            }
        }

        if (txtCounterRed != null)
        {
            currentScoreRed = 0;
            for (int i = 0; i < playersRed.Count; i++)
            {
                currentScoreRed += (int)playersRed[i].GetComponent<PhotonView>().Owner.CustomProperties["kills"];
            }
            txtCounterRed.text = currentScoreRed + "";

            //GetComponent<PhotonView>().RPC("setCounterRed",RpcTarget.AllBuffered);
        }


        if (txtCounterBlue != null)
        {
            currentScoreBlue = 0;
            for (int i = 0; i < playersBlue.Count; i++)
            {
                currentScoreBlue += (int)playersBlue[i].GetComponent<PhotonView>().Owner.CustomProperties["kills"];
            }
            txtCounterBlue.text = currentScoreBlue + "";

            //GetComponent<PhotonView>().RPC("setCounterBlue",RpcTarget.AllBuffered);
        }
        
    }

    [PunRPC]
    public void setCounterRed(){
        txtCounterRed.text = currentScoreRed + "";
    }

    [PunRPC]
    public void setCounterBlue(){
        txtCounterBlue.text = currentScoreBlue + "";
    }
}
