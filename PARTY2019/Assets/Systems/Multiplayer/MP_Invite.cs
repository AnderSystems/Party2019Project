using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Photon.Chat;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using ExitGames.Client.Photon;

public class MP_Invite : MonoBehaviourPunCallbacks, IChatClientListener
{
    ChatClient chatClient;
    [SerializeField]
    public string PlayerToInvite;
    [Header("Interface")]
    public Transform Rect;
    public MP_InviteButton InviteButton;

    Photon.Chat.AuthenticationValues authValues = new Photon.Chat.AuthenticationValues();
    void Start()
    {
        MP_Config.Singleton.CustomPlayerProps = JsonUtility.FromJson<_CustomPlayerProps>(PlayerPrefs.GetString("PlayerData"));
        PhotonNetwork.NickName = MP_Config.Singleton.CustomPlayerProps.Nickname;
        chatClient = new ChatClient(this,ConnectionProtocol.Udp);
        chatClient.ChatRegion = "US";
        authValues.UserId = PhotonNetwork.NickName;
        authValues.AuthType = Photon.Chat.CustomAuthenticationType.None;
    }

    void LateUpdate()
    {
        if (chatClient != null)
        {
            chatClient.Service();
        }
    }

    public void ConnectToChat()
    {
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, "0", authValues);
    }

    public void SearchPlayer(string _search)
    {
        PlayerToInvite = _search;
    }

    public void InvitePlayer()
    {
        //chatClient.PublishMessage("Master", PhotonNetwork.CurrentRoom.Name);
        chatClient.SendPrivateMessage(PlayerToInvite, "</Invited>" + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Player ''" + PlayerToInvite + "'' Invited to ''" + PhotonNetwork.CurrentRoom.Name + "''");
    }
    public void InvitePlayer(string Player)
    {
        chatClient.SendPrivateMessage(Player, "</Invited>" + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Player ''" + PlayerToInvite + "'' Invited to ''" + PhotonNetwork.CurrentRoom.Name + "''");
    }


    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnChatStateChange(ChatState state)
    {
        
    }

    public void OnConnected()
    {
        Debug.Log("Connected to chat using: ''" + chatClient.UserId + "'' Id");
        chatClient.Subscribe(new string[] { "Master" });
    }

    public override void OnConnectedToMaster()
    {
        ConnectToChat();
        base.OnConnectedToMaster();
    }

    public void OnDisconnected()
    {
        
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int s = 0; s < senders.Length; s++)
        {
            for (int m = 0; m < messages.Length; m++)
            {
                Debug.Log(senders[s] + " says: " + messages[m]);
            }
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        if (sender == chatClient.UserId)
            return;
        Debug.Log("''" + sender + "'' send a message to you:" + message);
        if (message.ToString().StartsWith("</Invited>"))
        {
            MP_InviteButton button = Instantiate(InviteButton.gameObject, Rect).GetComponent<MP_InviteButton>();
            button.Title.SetTitle(sender.ToUpper() + " Convidou vc para uma Party",
                sender.ToUpper() + " Invited you to a Party",
                sender.ToUpper() + " te invitó a una Party");
            button.RoomInvite(message.ToString().Replace("</Invited>", ""));
        }
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        Debug.Log("Subscribed on " + channels[0]);
    }

    public void OnUnsubscribed(string[] channels)
    {
        
    }

    public void OnUserSubscribed(string channel, string user)
    {
        
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        
    }

    void OnApplicationQuit()
    {
        if(chatClient != null)
        {
            chatClient.Disconnect();
        }
    }
}
