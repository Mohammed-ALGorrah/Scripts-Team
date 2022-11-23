using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class PlayerSetup : MonoBehaviourPunCallbacks
{
    #region Variables
    
    public GameObject PlayerCanvas;
    public GameObject PlayerCamera;
    public GameObject ShootScript;
    public GameObject assaultScript;
    public GameObject pistolScript;

    [Header("Test data")]
    public Text numText;
    int num = 0;

    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            GetComponent<polygon_fps_controller>().enabled = true;
            ShootScript.GetComponent<shoot_handle>().enabled = true;
            assaultScript.GetComponent<assault57>().enabled = true;
            pistolScript.GetComponent<old_pistol>().enabled = true;
            PlayerCanvas.SetActive(true);
            PlayerCamera.SetActive(true);
        }
        else
        {
            GetComponent<polygon_fps_controller>().enabled = false;
            ShootScript.GetComponent<shoot_handle>().enabled = false;
            assaultScript.GetComponent<assault57>().enabled = false;
            pistolScript.GetComponent<old_pistol>().enabled = false;
            PlayerCanvas.SetActive(false);
            PlayerCamera.SetActive(false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // change value on all clients
        if (Input.GetKeyDown(KeyCode.Z))
        {
            num++;
            numText.text = "Number = " + num;
            photonView.RPC("changeText",RpcTarget.All, num);
        }
    }

    #endregion

    #region Public_Methods

    [PunRPC]
    public void changeText(int x)
    {
        num = x;
        numText.text = "Number = " + num;

    }

    #endregion
}
