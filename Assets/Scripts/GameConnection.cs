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
    GameObject connectingTextObj;
    GameObject connectButtonObj;

    private void Start()
    {
        connectButtonObj = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        connectingTextObj = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
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

        connectingTextObj.gameObject.SetActive(true);
        connectButtonObj.gameObject.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            connectingTextObj.gameObject.GetComponent<Text>().text = "Connected!";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected. Cause: " + cause);
        connectingTextObj.gameObject.SetActive(false);
        connectButtonObj.gameObject.SetActive(true);
        isConnecting = false;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed attempt at connecting to a game room. Code: "
            + returnCode + "; Message: " + message + ". Let's create a game room!");
        PhotonNetwork.CreateRoom("game-room", new RoomOptions() { MaxPlayers = 2 });
    }
}
