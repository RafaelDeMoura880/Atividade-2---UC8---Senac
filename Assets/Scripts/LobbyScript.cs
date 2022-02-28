using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScript : MonoBehaviourPunCallbacks
{
    public int maxPlayers = 2;
    public Text statusTxt;

    int countDown;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if(PhotonNetwork.InRoom)
            statusTxt.text = PhotonNetwork.CurrentRoom.PlayerCount + " of " + maxPlayers;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        string TxtStats =
            PhotonNetwork.CurrentRoom.PlayerCount + " of " + maxPlayers;

        photonView.RPC("UpdateStatusTxtRPC", RpcTarget.All, TxtStats);

        if(PhotonNetwork.CurrentRoom.PlayerCount >= maxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            photonView.RPC("StartCountDownRPC", RpcTarget.All);
            StartCoroutine(StartGame());
        }
    }

    [PunRPC]
    void UpdateStatusTxtRPC(string message)
    {
        statusTxt.text = message;
    }
    
    [PunRPC]
    void StartCountDownRPC()
    {
        countDown = 5;
        StartCoroutine(CountDownCoroutine());
    }

    IEnumerator CountDownCoroutine()
    {
        while(countDown > 0)
        {
            statusTxt.text = "Starting in: " + countDown;
            yield return new WaitForSeconds(1);
            countDown--;
        }
    }
    
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.LoadLevel("GameScene");
    }
}
