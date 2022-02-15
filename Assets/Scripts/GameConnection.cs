using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class GameConnection : MonoBehaviourPunCallbacks
{
    string version = "1";
    bool isConnecting = false;
    GameObject txtConnectingObj;
    GameObject btnConnectObj;

    private void Start()
    {
        btnConnectObj = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        txtConnectingObj = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
    }

    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = version;
        }
        else
            PhotonNetwork.JoinRandomRoom();

        txtConnectingObj.gameObject.SetActive(true);
        btnConnectObj.gameObject.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            txtConnectingObj.gameObject.GetComponent<Text>().text = "Connected!";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected. Cause: " + cause);
        txtConnectingObj.gameObject.SetActive(false);
        btnConnectObj.gameObject.SetActive(true);
        isConnecting = false;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined game room");
        PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join. Code: " + returnCode + ". Message: " + message +
            ". Creating room...");
        PhotonNetwork.CreateRoom("game-room1", new RoomOptions() { MaxPlayers = 4 });
    }
}
