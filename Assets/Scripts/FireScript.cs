using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FireScript : MonoBehaviourPun, IPunObservable
{
    public GameObject projectilePrefab;
    SliderJoint2D healthBar;

    public int ammo = 10;
    public int health = 5;

    [SerializeField] float projectileSpeed = 2f;
    bool hasFired = false;

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        if (Input.GetButtonDown("Fire1") && ammo > 0)
            hasFired = true;
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
            return;
        
        if (hasFired)
        {
            Fire();
        }
    }

    void Fire()
    {
        Vector3 pos = this.transform.GetChild(0).position; ; //gets position of Cano
        GameObject newProjectile = 
            PhotonNetwork.Instantiate(projectilePrefab.name, pos, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        ammo--;
        hasFired = false;
    }

    public void AddAmmo()
    {
        photonView.RPC("AddAmmoRPC", RpcTarget.All);
    }

    [PunRPC]
    void AddAmmoRPC()
    {
        ammo += 5;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(ammo);
        }
        else
        {
            ammo = (int)stream.ReceiveNext();
        }
    }
}
