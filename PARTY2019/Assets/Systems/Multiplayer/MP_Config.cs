using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class _CustomPlayerProps
{
    [Header("Social")]
    public string Nickname;
    public string IP;
    [Space]
    public string Email;
    public string Password;
}

public class MP_Config : MonoBehaviourPunCallbacks
{
    public static string _PlayerName { get; set; }
    static string _PlayerJSON { get; set; }

    public MenuManager Menu;

    [Header("Player Config")]
    [SerializeField]
    public _CustomPlayerProps CustomPlayerProps = new _CustomPlayerProps();

    [Header("UI")]
    public GameObject Register_Button;

    //Static var
    public static MP_Config Singleton;

    void Awake()
    {
        CreateSingleton();
        DontDestroyOnLoad(this.gameObject);
    }

    void CreateSingleton()
    {
        if (MP_Config.Singleton == null)
        {
            MP_Config.Singleton = this;
        } else
        {
            if(MP_Config.Singleton != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerData"))
        {
            LoadPlayerInfosLocal(out CustomPlayerProps);
        }
        ConnectToMaster();
    }

    public void ConnectToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        AuthenticationValues auth = new AuthenticationValues();
        auth.UserId = _PlayerJSON;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Master Server!");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Connected to lobby!");
        if (PlayerPrefs.HasKey("PlayerData"))
        {
            if (PhotonNetwork.IsConnectedAndReady)
            {
                Menu.GoToMenu(2);
                Debug.Log("IsConnectedAndReady!");
            }
        } else
        {
            Menu.GoToMenu(1);
        }
    }
    public void SetNickname(string nick)
    {
        _CustomPlayerProps CPp = new _CustomPlayerProps();
        CPp.Nickname = nick.ToLower();
        CPp.IP = NetUltility.GetIPV4();
        SavePlayerInfos(CPp);
        PhotonNetwork.NickName = nick;
        MP_Login.Singleton.UserName = nick;

        _PlayerName = nick;
        CustomPlayerProps.Nickname = nick;

        Register_Button.SetActive(nick != null && Regex.IsMatch(nick, @"^[a-zA-Z0-9_]+$") && MP_Login.Singleton.CheckEmail() &&
        MP_Login.Singleton.CheckPass());
        LoadPlayerInfosLocal(out CustomPlayerProps);
    }

    public void SaveGameData()
    {
        SavePlayerInfos(CustomPlayerProps);
    }

    public static void SavePlayerInfos(_CustomPlayerProps infos)
    {
        Singleton.CustomPlayerProps = infos;
        _PlayerJSON = JsonUtility.ToJson(Singleton.CustomPlayerProps);

        PlayerPrefs.SetString("PlayerData", _PlayerJSON);
    }


    public static void LoadPlayerInfosLocal(out _CustomPlayerProps infos)
    {
        _PlayerJSON = PlayerPrefs.GetString("PlayerData");
        infos = new _CustomPlayerProps();
        infos = JsonUtility.FromJson<_CustomPlayerProps>(_PlayerJSON);
        //PhotonNetwork.NickName = infos.Nickname;
    }

    public static void LoadPlayerInfos(out _CustomPlayerProps infos)
    {
        _PlayerJSON = PlayerPrefs.GetString("PlayerData");
        infos = JsonUtility.FromJson<_CustomPlayerProps>(_PlayerJSON);
        //PhotonNetwork.NickName = infos.Nickname;
        MP_Login.Singleton.SetEmail(Singleton.CustomPlayerProps.Email);
        MP_Login.Singleton.SetPass(Singleton.CustomPlayerProps.Password);
    }

    public void CreateRoom()
    {
        Debug.Log("Creating Room!");
        RoomOptions ops = new RoomOptions();
        ops.MaxPlayers = 8;
        LoadPlayerInfosLocal(out CustomPlayerProps);
        PhotonNetwork.CreateRoom(CustomPlayerProps.Nickname + "|" + CustomPlayerProps.IP,
    ops, TypedLobby.Default);
        Menu.EnableLoadScreens(0);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("Room ''" + PhotonNetwork.CurrentRoom.Name + "'' Created!");
        Menu.DisableAllLoadScreens();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Join On ''" + PhotonNetwork.CurrentRoom.Name + "'' Created!");
        Menu.GoToMenu("MP_Invite");
        Menu.DisableAllLoadScreens();
    }
}
