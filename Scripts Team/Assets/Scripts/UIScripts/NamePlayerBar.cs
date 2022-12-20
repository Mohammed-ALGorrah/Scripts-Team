using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class NamePlayerBar : MonoBehaviourPunCallbacks
{
    private Camera cam;
    CheckPhoton checkPhoton;
    public TextMeshProUGUI textName;
    private void Awake()
    {
        checkPhoton = FindObjectOfType<CheckPhoton>();
    }

    void Start()
    {
        if (checkPhoton.team == 1)
        {
            textName.color = Color.blue;
        }
        else if (checkPhoton.team == 2)
        {
            textName.color = Color.red;
        }

        if (photonView.IsMine)
        {
            textName.color = Color.green;
        }

        cam = Camera.main;

        GameObject.Find("Name").GetComponentInChildren<TextMeshProUGUI>().text = photonView.Owner.NickName;
        textName.text = GameObject.Find("Name").GetComponentInChildren<TextMeshProUGUI>().text;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
}
