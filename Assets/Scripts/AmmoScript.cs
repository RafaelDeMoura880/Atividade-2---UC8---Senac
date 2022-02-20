using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AmmoScript : MonoBehaviourPun
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if(collision.gameObject.tag == "Player")
        {
            collision.transform.GetChild(0).transform.GetChild(0).GetComponent<FireScript>().ammo += 5;
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
