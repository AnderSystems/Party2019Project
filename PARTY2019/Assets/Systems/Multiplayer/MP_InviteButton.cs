using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MP_InviteButton : MonoBehaviour
{
    public SimpleButton Title;
    public string Room { get; set; }

    public void RoomInvite(string _Title)
    {
        Room = _Title;
    }

    public void JoinOnRoom()
    {
        PhotonNetwork.JoinRoom(Room);
        MP_Config.Singleton.Menu.EnableLoadScreens(0);
        Destroy(this.gameObject);
    }
}
