using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    public float speed,lifeTime;

    
    void Update()
    {
        
        transform.TransformDirection(Vector3.forward);
        transform.Translate(new Vector3(0,0,speed) * Time.deltaTime);

        Destroy(gameObject,lifeTime);
    }
}
