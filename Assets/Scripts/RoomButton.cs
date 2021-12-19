using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class RoomButton : MonoBehaviour
{
    public TextMeshProUGUI nameText, sizeText;
    public string roomName;
    public int roomSize;


    #region Custom Functions
    internal void SetRoom()
    {
        nameText.SetText(roomName);
        sizeText.SetText(roomSize.ToString());
    }

    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    #endregion
}
