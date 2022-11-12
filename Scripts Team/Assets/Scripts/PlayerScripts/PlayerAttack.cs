using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    PlayerType playerType;

    void Start()
    {
        playerType = GetComponent<Player>().playerData.playerType;
    }
    
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            normalAttack();
        }else if(Input.GetKeyDown(KeyCode.G)){
            specialAttack();
        }
    }

    void normalAttack(){
        switch(playerType){

            case PlayerType.fighter:
                Debug.Log(playerType);
                break;

            case PlayerType.defneder:
                Debug.Log(playerType);

                break;

            case PlayerType.healther:
                Debug.Log(playerType);

                break;
           }
    }

    void specialAttack(){
        switch(playerType){

            case PlayerType.fighter:
                Debug.Log(playerType);
                break;

            case PlayerType.defneder:
                Debug.Log(playerType);

                break;

            case PlayerType.healther:
                Debug.Log(playerType);

                break;
           }
    }


}
