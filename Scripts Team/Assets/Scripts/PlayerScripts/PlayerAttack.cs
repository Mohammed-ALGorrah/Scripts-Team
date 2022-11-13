using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    PlayerType playerType;

    [SerializeField]
    Transform firePoint;

    public GameObject Bullet;


    void Start()
    {
        playerType = GetComponent<Player>().playerData.playerType;

        switch(playerType){

            case PlayerType.fighter:
                Bullet = Resources.Load<GameObject>("BulletsPrefs/Warrior_Ammo");
                break;

            case PlayerType.defneder:
            
                Bullet = Resources.Load<GameObject>("BulletsPrefs/Archer_Ammo");

                break;

            case PlayerType.healther:
            
                Bullet = Resources.Load<GameObject>("BulletsPrefs/Wizard_Ammo");

                break;
           }
    }
    
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            normalAttack();
        }else if(Input.GetKeyDown(KeyCode.G)){
            specialAttack();
        }
    }
    

    void normalAttack(){
        Instantiate(Bullet,firePoint.position, transform.rotation);
        
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
