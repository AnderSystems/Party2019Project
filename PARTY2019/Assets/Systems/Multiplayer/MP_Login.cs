using ExitGames.Client.Photon;
using Photon.Chat;
using PlayFab;
using PlayFab.ClientModels;
using System.Text.RegularExpressions;
using UnityEngine;

public class MP_Login : MonoBehaviour , IChatClientListener
{
    public static MP_Login Singleton;

    public string UserName;
    protected string Email;
    protected string Password;

    public bool CheckEmail()
    {
        return Email != null;
    }

    public bool CheckPass()
    {
        return Password != null;
    }

    void Awake()
    {
        if (!Singleton)
        {
            Singleton = this;
        } else
        {
            if(Singleton != this)
            {
                Destroy(this);
            }
        }
    }

    public void Start()
    {
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "1AB9A"; // Please change this value to your own titleId from PlayFab Game Manager
        }
        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);

        if (PlayerPrefs.HasKey("PlayerData"))
        {
            _CustomPlayerProps playerProps = new _CustomPlayerProps();
            playerProps = JsonUtility.FromJson<_CustomPlayerProps>(PlayerPrefs.GetString("PlayerData"));
            UserName = playerProps.Nickname;
            Email = playerProps.Email;
            Password = playerProps.Password;
            var Request = new LoginWithEmailAddressRequest { Email = Email, Password = Password };
            PlayFabClientAPI.LoginWithEmailAddress(Request, OnLoginSuccess, OnLoginFailure);
        }
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Success register!");

        //Photon.Chat.ChatClient.SendPrivateMessage()

    }

    private void OnLoginFailure(PlayFabError error)
    {
        if (error.Error != PlayFabErrorCode.AccountNotFound)
        {
            Debug.LogWarning("Something went wrong with your first API call.  :(");
            Debug.LogError("Here's some debug information:");
            Debug.LogError(error.GenerateErrorReport());
        }
        else
        {
            Debug.LogError("AccountNotFound, please register a new account");
            var RequestRegister = new RegisterPlayFabUserRequest { Email = Email, Password = Password , Username = UserName};
            PlayFabClientAPI.RegisterPlayFabUser(RequestRegister, OnRegisterSuccess, OnRegisterFail);
        }
    }

    void OnRegisterFail(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    public void SetPass(string _pass)
    {
        Password = _pass;
        MP_Config.Singleton.CustomPlayerProps.Password = _pass;

        MP_Config.Singleton.Register_Button.SetActive(UserName != null && Regex.IsMatch(UserName, @"^[a-zA-Z0-9_]+$") && MP_Login.Singleton.CheckEmail() &&
        MP_Login.Singleton.CheckPass());
    }

    public void SetEmail(string _email)
    {
        Email = _email;
        MP_Config.Singleton.CustomPlayerProps.Email = _email;


        MP_Config.Singleton.Register_Button.SetActive(UserName != null && Regex.IsMatch(UserName, @"^[a-zA-Z0-9_]+$") && MP_Login.Singleton.CheckEmail() &&
        MP_Login.Singleton.CheckPass());
    }

    public void LogIn()
    {
        var Request = new LoginWithEmailAddressRequest { Email = Email, Password = Password };
        PlayFabClientAPI.LoginWithEmailAddress(Request, OnLoginSuccess, OnLoginFailure);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    public void OnConnected()
    {
        throw new System.NotImplementedException();
    }

    public void OnChatStateChange(ChatState state)
    {
        throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        throw new System.NotImplementedException();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
}

