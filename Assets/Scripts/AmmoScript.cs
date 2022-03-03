using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AmmoScript : MonoBehaviourPun
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!photonView.IsMine)
            return;

        if(collision.gameObject.tag == "Player")
        {
            collision.transform.GetComponent<FireScript>().ammo += 5;
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
