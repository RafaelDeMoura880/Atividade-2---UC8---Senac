using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public Text ammoText;

    private void Update()
    {
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
