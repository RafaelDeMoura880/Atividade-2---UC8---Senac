using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviourPun
{
    public Text ammoText;
    Slider healthBar;

    private void Start()
    {
        healthBar = this.transform.GetChild(5).transform.GetChild(0).
            gameObject.GetComponent<Slider>();
        healthBar.maxValue = this.gameObject.GetComponent<FireScript>().health;
    }

    private void Update()
    {
        healthBar.value = this.gameObject.GetComponent<FireScript>().health;
        
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
