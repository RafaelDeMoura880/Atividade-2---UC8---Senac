using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManagement : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    private void Start()
    {
        if (playerPrefab != null)
        {
            Vector3 pos = new Vector3(0, 2, 0);
            PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity);
        }
    }
}
