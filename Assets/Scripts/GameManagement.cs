using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    private void Start()
    {
        if (playerPrefab != null)
        {
            Vector3 pos = new Vector3(0, 2, 0);
            pos.x = Random.Range(-4f, 4f);
            pos.z = Random.Range(-4f, 4f);
            PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(4);
        }
    }
}
