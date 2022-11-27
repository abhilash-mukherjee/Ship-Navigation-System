using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;


    public class Launcher : MonoBehaviour
    {
    [SerializeField] private int operatorScene, sailorScene;
        //public delegate void ConnectionHandler();
        //public static event ConnectionHandler OnConnectionLost, OnLobbyJoined;

        //#region Private Serializable Fields
        ///// <summary>
        ///// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
        ///// </summary>
        //[Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        //[SerializeField] private byte maxPlayersPerRoom = 4;
        //[SerializeField] private string Operator, Salior;
        //#endregion


    //    #region Private Fields


    //    /// <summary>
    //    /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
    //    /// </summary>
    //    string gameVersion = "1";
    //    bool isConnecting;
    //    private const string OPERATOR_EXIST = "OE";
    //    private const string SAILOR_EXISTS = "PE";
    //    private const string MAP_TYPE = "MAP_TYPE";
    //    private bool isMasterConnected = false;


    //#endregion


    //#region MonoBehaviour CallBacks


    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    //void Awake()
    //{
    //    // #Critical
    //    // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
    //    PhotonNetwork.AutomaticallySyncScene = true;
    //    if (!PhotonNetwork.IsConnected)
    //    {
    //        isConnecting = PhotonNetwork.ConnectUsingSettings();
    //        PhotonNetwork.GameVersion = gameVersion;
    //    }
    //}

    //public override void OnEnable()
    //{
    //    base.OnEnable();
    //    RoomUI.OnRoomLeft += LeaveRoom;
    //}
    //public override void OnDisable()
    //{
    //    base.OnDisable();
    //    RoomUI.OnRoomLeft -= LeaveRoom;

    //}


    //#endregion


    #region Public Methods

    public void Connect()
    {
        if (ClientSideData.Instance.View == "OPERATOR") SceneManager.LoadScene(operatorScene);
       else SceneManager.LoadScene(sailorScene);
    }


        #endregion


        //#region MonoBehaviourPunCallbacks Callbacks


        //public override void OnConnectedToMaster()
        //{
        //    isMasterConnected = true;
        //    Debug.Log("Master is connected");
        //    joinLobby();

        //}

        //private void joinLobby()
        //{
        //    PhotonNetwork.JoinLobby();
        //}


        //public override void OnDisconnected(DisconnectCause cause)
        //{
        //    isMasterConnected = false;
        //    Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        //    OnConnectionLost?.Invoke();

        //}

        //public override void OnJoinRandomFailed(short returnCode, string message)
        //{
        //    Debug.Log("Room failed " + message);
        //    Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
        //    CreateRoom();
        //}

        //public override void OnJoinedRoom()
        //{
        //    bool isOperatorView = ClientSideData.Instance.View == Operator;
        //    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable() { { "CurrentView", (isOperatorView ? "OPERATOR" : "SAILOR") } };
        //    PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        //    PhotonNetwork.LoadLevel("Room for 1");

        //}

        //public override void OnLeftRoom()
        //{
        //    SceneManager.LoadScene(0);

        //}


        //public override void OnJoinedLobby()
        //{
        //    Debug.Log("Lobby Joined");
        //    isConnecting = false;
        //    OnLobbyJoined?.Invoke();
        //}



        //#endregion

        //#region Private Methods
        //private void CreateRoom()
        //{
        //    RoomOptions roomOptions = new RoomOptions();
        //    string[] keys = { MAP_TYPE };
        //    roomOptions.CustomRoomPropertiesForLobby = keys;
        //    bool isOperatorView = ClientSideData.Instance.View == Operator;
        //    roomOptions.CustomRoomProperties = new Hashtable { { MAP_TYPE, isOperatorView ? "OPERATOR" : "SAILOR" } };
        //    Debug.Log("Creating room for " + (isOperatorView ? "OPERATOR" : "SAILOR"));
        //    PhotonNetwork.CreateRoom(null, roomOptions);
        //}

        //private void JoinRandomRoom()
        //{
        //    bool isOperatorView = ClientSideData.Instance.View == Operator;
        //    Debug.Log("Is operator view = " + isOperatorView);
        //    Hashtable expectedCustomRoomProperties = new Hashtable { { MAP_TYPE, isOperatorView ? "SAILOR" : "OPERATOR" } };
        //    Debug.Log("Checking room for " + (isOperatorView ? "SAILOR" : "OPERATOR"));
        //    PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
        //}
        //private void LeaveRoom()
        //{
        //    isMasterConnected = false;
        //    PhotonNetwork.LeaveRoom();
        //}
        //#endregion


    }


