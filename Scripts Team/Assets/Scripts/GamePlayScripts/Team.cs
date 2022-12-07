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
                currentScoreRed += playersRed[i].GetComponent<Player>().numOfKills;
            }
            txtCounterRed.text = currentScoreRed + "";

        }

        if (txtCounterBlue != null)
        {
            currentScoreBlue = 0;
            for (int i = 0; i < playersBlue.Count; i++)
            {
                currentScoreBlue += playersBlue[i].GetComponent<Player>().numOfKills;
            }
            txtCounterBlue.text = currentScoreBlue + "";

        }
        
    }
}
