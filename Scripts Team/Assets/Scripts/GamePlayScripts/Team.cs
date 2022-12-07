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
        if (txtCounterRed != null)
        {
            currentScoreRed = 0;
            for (int i = 0; i < playersRed.Count; i++)
            {
                currentScoreRed += (int)playersRed[i].GetComponent<PhotonView>().Owner.CustomProperties["kills"];
            }            

            GetComponent<PhotonView>().RPC("setCounterRed",RpcTarget.AllBuffered);
        }


        if (txtCounterBlue != null)
        {
            currentScoreBlue = 0;
            for (int i = 0; i < playersBlue.Count; i++)
            {
                currentScoreBlue += (int)playersBlue[i].GetComponent<PhotonView>().Owner.CustomProperties["kills"];
            }            

            GetComponent<PhotonView>().RPC("setCounterBlue",RpcTarget.AllBuffered);
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
