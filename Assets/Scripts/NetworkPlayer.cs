﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviour {

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    private PhotonView photonView;

    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;

    // Start is called before the first frame update
    void Start () {
        photonView = GetComponent<PhotonView> ();

        XRRig rig = FindObjectOfType<XRRig> (); //XRRigにアクセスする
        headRig = rig.transform.Find ("Camera Offset/Main Camera"); //XRRigのMain CameraにheadRig
        leftHandRig = rig.transform.Find ("Camera Offset/LeftHand Controller");
        rightHandRig = rig.transform.Find ("Camera Offset/RightHand Controller");
    }

    // Update is called once per frame
    void Update () {
        if (photonView.IsMine) {
            head.gameObject.SetActive (false);
            leftHand.gameObject.SetActive (false);
            rightHand.gameObject.SetActive (false);

            MapPosition (head, headRig);
            MapPosition (leftHand, leftHandRig);
            MapPosition (rightHand, rightHandRig);
        }
    }

    void MapPosition (Transform target, Transform rigTransform) {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}