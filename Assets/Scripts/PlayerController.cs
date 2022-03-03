using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    float speedMov;
    float speedTurn;
    float inputH;
    float inputV;

    Rigidbody playerRb;
    Animator playerAnim;

    private void Awake()
    {
        speedMov = 7;
        speedTurn = 3;
    }

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        if (photonView.IsMine)
            GetComponent<CameraWork>().OnStartFollowing();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
            return;

        Vector3 newSpeed = transform.forward * inputV * speedMov;
        newSpeed.y = playerRb.velocity.y;
        playerRb.velocity = newSpeed;

        playerRb.angularVelocity = new Vector3(0, inputH * speedTurn, 0);
    }
        
    
}
