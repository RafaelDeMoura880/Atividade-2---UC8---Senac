using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class FireScript : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject projectilePrefab;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") && photonView.IsMine)
        {
            health--;
            if (health <= 0)
                GameOver();
        }
    }

    public void GameOver()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(3);
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
            stream.SendNext(health);
        }
        else
        {
            ammo = (int)stream.ReceiveNext();
            health = (int)stream.ReceiveNext();
        }
    }
}
