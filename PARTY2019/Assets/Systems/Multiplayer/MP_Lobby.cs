using System.Linq;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MP_Lobby : MonoBehaviourPunCallbacks
{
    public static MP_Lobby Lobby;
    [Header("Interface")]
    public Transform PlayersRect;
    public MP_PlayerProfileButton PlayerLabel;
    [Space]
    public List<MP_PlayerProfileButton> PlayersLabel = new List<MP_PlayerProfileButton>();

    void Start()
    {
        Lobby = this;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        PlayersLabel = FindObjectsOfType<MP_PlayerProfileButton>().ToList();
        for (int i = 0; i < PlayersLabel.Count; i++)
        {
            PlayersLabel[i].transform.parent = PlayersRect;
            PlayersLabel[i].RectParent = PlayersRect;

        }
        //UpdatePlayersList(PlayersRect, PlayerLabel);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        //UpdatePlayersList(PlayersRect, PlayerLabel);
    }

    public override void OnConnected()
    {
        base.OnConnected();
        //UpdatePlayersList(PlayersRect, PlayerLabel);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PlayersLabel = GetComponents<MP_PlayerProfileButton>().ToList();
        for (int i = 0; i < PlayersLabel.Count; i++)
        {
            PlayersLabel[i].transform.parent = PlayersRect;
        }
        UpdatePlayersList(PlayersRect, PlayerLabel);
    }

    public static void UpdatePlayersList(Transform RectLocation, MP_PlayerProfileButton Button)
    {
        Debug.Log("Players on Room:" + PhotonNetwork.PlayerList.Length);
        MP_PlayerProfileButton _Button = PhotonNetwork.Instantiate(Button.name, Vector3.zero, Quaternion.identity).
            GetComponent<MP_PlayerProfileButton>();
        _Button.transform.parent = RectLocation;
        _Button.RectParent = RectLocation;
        _Button.transform.localScale = Vector3.one;
        _Button.transform.localPosition = Vector3.zero;

        _Button.PhotonPlayer = PhotonNetwork.LocalPlayer;
        //PlayersLabel.Add(_Button);



        /*
        Debug.Log("Players on Room:" + PhotonNetwork.PlayerList.Length);
        for (int i = 0; i < PlayersLabel.Count; i++)
        {
            Destroy(PlayersLabel[i].gameObject);
        }

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            MP_PlayerProfileButton _Button = Instantiate(Button);
            _Button.transform.parent = RectLocation;
            _Button.PhotonPlayer = PhotonNetwork.PlayerList[i];
            _Button.GetInfo(_Button.Title, MP_PlayerProfileButton._PlayerInfo.Nickname);
            _Button.GetInfo("Lvl</lvl>", "</lvl>",_Button.SubTitle, MP_PlayerProfileButton._PlayerInfo.Level);
            PlayersLabel.Add(_Button);
        }*/
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        ChangeHost();
    }

    public void ChangeHost()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.PlayerListOthers.Length > 1)
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerListOthers[0]);
            }
        }
}
}
