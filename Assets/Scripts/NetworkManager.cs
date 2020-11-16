using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks {
    // Start is called before the first frame update
    void Start () {
        ConnectToServer ();
    }

    //サーバーに接続する
    void ConnectToServer () {
        PhotonNetwork.ConnectUsingSettings ();
        Debug.Log ("Try To Server...");
    }

    //マスター権限のユーザーによるRoomの作成及び設定
    public override void OnConnectedToMaster () {
        Debug.Log ("Connected To Server...");
        base.OnConnectedToMaster ();
        RoomOptions roomOptions = new RoomOptions ();
        roomOptions.MaxPlayers = 10;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom ("Room 1", roomOptions, TypedLobby.Default);
    }

    //Roomへの参加した時の処理
    public override void OnJoinedRoom () {
        Debug.Log ("Joined a Room");
        base.OnJoinedRoom ();
    }

    //別のプレイヤーがRoomに参加したかどうかの確認
    public override void OnPlayerEnteredRoom (Player newPlayer) {
        Debug.Log ("A new player join the room");
        base.OnPlayerEnteredRoom (newPlayer);
    }
}