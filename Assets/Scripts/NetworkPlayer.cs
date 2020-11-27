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

    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    private PhotonView photonView;

    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;

    void Start () {
        photonView = GetComponent<PhotonView> ();

        //各部位の位置の取得
        XRRig rig = FindObjectOfType<XRRig> (); //XRRigにアクセスする
        headRig = rig.transform.Find ("Camera Offset/Main Camera");
        leftHandRig = rig.transform.Find ("Camera Offset/LeftHand Controller");
        rightHandRig = rig.transform.Find ("Camera Offset/RightHand Controller");

        if (photonView.IsMine) {
            //Rendererを無効化
            foreach (var item in GetComponentsInChildren<Renderer> ()) {
                item.enabled = false;
            }
        }
    }

    void Update () {
        //自分の頭・両手を自分から見えなくする
        if (photonView.IsMine) {

            //自分の動きをグローバルポジションで同期させる
            MapPosition (head, headRig);
            MapPosition (leftHand, leftHandRig);
            MapPosition (rightHand, rightHandRig);

            UpdateHandAnimation (InputDevices.GetDeviceAtXRNode (XRNode.LeftHand), leftHandAnimator);
            UpdateHandAnimation (InputDevices.GetDeviceAtXRNode (XRNode.RightHand), rightHandAnimator);
        }
    }

    //HandAnimationの検出及び同期
    void UpdateHandAnimation (InputDevice targetDevice, Animator handAnimator) {
        if (targetDevice.TryGetFeatureValue (CommonUsages.trigger, out float triggerValue)) {
            handAnimator.SetFloat ("Trigger", triggerValue);
        } else {
            handAnimator.SetFloat ("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue (CommonUsages.grip, out float gripValue)) {
            handAnimator.SetFloat ("Grip", gripValue);
        } else {
            handAnimator.SetFloat ("Grip", 0);
        }
    }

    //targetの位置を同期させる更新処理
    void MapPosition (Transform target, Transform rigTransform) {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}