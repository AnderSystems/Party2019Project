using TMPro;
using Photon.Pun;
using Photon.Chat;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP_PlayerProfileButton : MonoBehaviourPunCallbacks
{
    public PhotonView PV { get; set; }

    public enum _PlayerInfo
    {
        Nickname, Level, Score
    }

    SimpleButton SimpleButton;
    public Player PhotonPlayer;

    [Header("Interface")]
    public TextMeshProUGUI Title;
    public TextMeshProUGUI SubTitle;

    public static int Score;
    public Transform RectParent;

    void Start()
    {
        SimpleButton = GetComponent<SimpleButton>();
        PV = GetComponent<PhotonView>();
        this.transform.parent = MP_Lobby.Lobby.PlayersRect;
        Title.text = "......";
        SubTitle.text = ".";
        Invoke("GetLocalInformation", 1);
        if (PhotonNetwork.IsMasterClient)
        {
            PV.TransferOwnership(PhotonPlayer);
        }
    }
    void GetLocalInformation()
    {
        PhotonPlayer = PV.Owner;
        GetInfo(Title, MP_PlayerProfileButton._PlayerInfo.Nickname);
        GetInfo("Lvl</lvl>", "</lvl>", SubTitle, MP_PlayerProfileButton._PlayerInfo.Level);
    }

    public void GetInfo(TextMeshProUGUI Output, _PlayerInfo playerInfo)
    {
        GetInfo("</Info>", "</Info>", Output, playerInfo);
    }
    public void GetInfo(string Text, string ToReplace,TextMeshProUGUI Output, _PlayerInfo playerInfo)
    {
        if (playerInfo == _PlayerInfo.Level)
        {
            Output.text = Text.Replace(ToReplace, "0");
        }

        if (playerInfo == _PlayerInfo.Nickname)
        {
            Output.text = Text.Replace(ToReplace, PhotonPlayer.NickName);
        }

        if (playerInfo == _PlayerInfo.Score)
        {
            Output.text = Text.Replace(ToReplace, Score.ToString());
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        if(otherPlayer == PhotonPlayer)
        {
            Destroy(this);
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Destroy(this.gameObject);
    }
}
