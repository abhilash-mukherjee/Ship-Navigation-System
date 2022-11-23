using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerInstantiator : MonoBehaviour
{
    [Tooltip("The prefab to use for representing the player")]
    public GameObject initialOVR,sailorPrefab, operatorPrefab;
    [SerializeField] private string operatorView = "OPERATOR", sailorView = "SAILOR";
    [SerializeField] private Transform operatorTransform, sailorTransform;
    #region Monobehaviour Callbacks

    private void Start()
    {
        if (sailorPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            if (PlayerManager.LocalPlayerInstance == null)
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate

                if (ClientSideData.Instance.View == sailorView)
                {
                    var sailorOBJ = PhotonNetwork.Instantiate(this.sailorPrefab.name, sailorTransform.position, sailorTransform.rotation, 0);
                    sailorOBJ.transform.parent = sailorTransform;
                }
                else
                {
                    var operatorOBJ = PhotonNetwork.Instantiate(this.operatorPrefab.name, operatorTransform.position, operatorTransform.rotation, 0);
                    operatorOBJ.transform.parent = operatorTransform;
                }
                initialOVR.SetActive(false);
            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }
        }
    }

    #endregion

}
