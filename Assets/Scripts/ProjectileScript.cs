using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ProjectileScript : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider other)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if (other.CompareTag("Wall"))
            PhotonNetwork.Destroy(gameObject);
    }


    //...

    //private void //Update()
    //{
    //    if (photonView.IsMine)
    //        StartCoroutine(DestroyProjectile());
    //}

    //IEnumerator DestroyProjectile()
    //{
    //    yield return new WaitForSeconds(2f);
    //    PhotonNetwork.Destroy(gameObject);
    //}
}
