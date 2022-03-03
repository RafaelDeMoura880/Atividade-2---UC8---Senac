using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviourPun
{
    public Text ammoText;

    private void Start()
    {
        if (!photonView.IsMine)
            return;
    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        FireScript[] player = GameObject.FindObjectsOfType<FireScript>();
        string playerAmmo = "Ammo: ";
        foreach (FireScript fire in player)
            playerAmmo += fire.ammo + "; ";
        ammoText.text = playerAmmo;

        //FireScript players = GameObject.FindObjectOfType<FireScript>();
        //string TxtAmmo = "Ammo: ";
        //TxtAmmo += players.ammo;
        //ammoText.text = TxtAmmo;
    }
}
