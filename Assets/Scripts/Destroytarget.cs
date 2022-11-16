using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroytarget : MonoBehaviour
{
    [SerializeField] GameObject destructionFx;
    public void Destruction()
    {


        var Impact = Instantiate(destructionFx, gameObject.transform.position, Quaternion.identity) as GameObject;
        Destroy(Impact, 2);



        Destroy(gameObject);
    }

}
