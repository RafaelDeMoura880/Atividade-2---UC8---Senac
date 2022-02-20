using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FireScript : MonoBehaviourPun
{
    public GameObject projectilePrefab;

    Rigidbody projectileRb;
    public int ammo = 10;
    [SerializeField] float projectileSpeed = 100f;
    bool hasFired = false;

    private void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
    }

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
        Vector3 pos = this.transform.position; //gets position of Cano
        GameObject newProjectile = 
            PhotonNetwork.Instantiate(projectilePrefab.name, pos, Quaternion.identity);
        Vector3 direction = this.transform.forward;
        newProjectile.GetComponent<Rigidbody>().AddForce(direction * projectileSpeed, ForceMode.Force);
        ammo--;
        hasFired = false;
    }
}
