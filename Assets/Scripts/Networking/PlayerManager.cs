using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun;

using System.Collections;


    /// <summary>
    /// Player manager.
    /// Handles fire Input and Beams.
    /// </summary>
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;
        #endregion

        #region Private Fields

        [Tooltip("The Input Script")]
        [SerializeField]
        private GameObject playerInputModule;

        #endregion

        #region MonoBehaviour CallBacks

        private void Awake()
        {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);

        }

        void Start()
        {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                playerInputModule.SetActive(false);
            }

        }



        #endregion

        #region Custom


        #endregion
    }
