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
            collision.gameObject.GetComponent<FireScript>().AddAmmo();
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
