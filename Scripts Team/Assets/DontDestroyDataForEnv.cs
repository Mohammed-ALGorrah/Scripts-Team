using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class DontDestroyDataForEnv : MonoBehaviour
{
    bool isSpring = true;

    public TMP_Text envTypeText;

    public Sprite springImage;
    public Sprite snowImage;

    public Image envImageSelected;

    public Image snowImageSelectedTrue;
    public Image springImageSelectedTrue;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            Destroy(this.gameObject);
        }
        

        if (level == 3)
        {
            Debug.Log("lvvvvvvvvlllllllllvvvvvvvvl :  " + level);
            GameObject envSnow = GameObject.FindWithTag("Env Snow");
            GameObject envSpring = GameObject.FindWithTag("Env Spring");
            if (isSpring)
            {
                if (envSpring != null)
                {
                    envSnow.SetActive(false);
                    envSpring.SetActive(true);
                }
            }else{
                if (envSnow != null)
                {
                    envSnow.SetActive(true);
                    envSpring.SetActive(false);
                }
            }
        }
           
    }


    [PunRPC]
    private void IsSpringRpc()
    {
        isSpring = true;
        
        envImageSelected.sprite = springImage;
        springImageSelectedTrue.gameObject.SetActive(true);
        snowImageSelectedTrue.gameObject.SetActive(false);
        envTypeText.text = "Spring";
        Debug.Log("Spreinggggggg");
    }
    
    [PunRPC]
    private void IsSnowgRpc()
    {

        isSpring = false;
        
        envImageSelected.sprite = snowImage;
        snowImageSelectedTrue.gameObject.SetActive(true);
        springImageSelectedTrue.gameObject.SetActive(false);

        envTypeText.text = "Snow";
        Debug.Log("Snowwwwwwwww");
    }

    public void OnClickSnow()
    {
        GetComponent<PhotonView>().RPC("IsSnowgRpc", RpcTarget.AllBuffered);
    }
    
    public void OnClickSpring()
    {
        GetComponent<PhotonView>().RPC("IsSpringRpc", RpcTarget.AllBuffered);
    }

}
