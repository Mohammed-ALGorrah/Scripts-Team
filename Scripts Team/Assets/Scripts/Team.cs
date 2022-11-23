using Heros.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public List <Player> players;
    public int currentScore;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //currentScore  = players[0].numOfKills + players[1].numOfKills + players[2].numOfKills;
    }
}
