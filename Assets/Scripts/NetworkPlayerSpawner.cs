using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks {

    private GameObject spawnedPlayerPrefab;

    //Roomに入った時に呼び出される
    public override void OnJoinedRoom () {
        base.OnJoinedRoom ();
        spawnedPlayerPrefab = PhotonNetwork.Instantiate ("Network Player", transform.position, transform.rotation);
    }

    //Roomを出た時に呼び出される
    public override void OnLeftRoom () {
        base.OnLeftRoom ();
        PhotonNetwork.Destroy (spawnedPlayerPrefab);
    }
}