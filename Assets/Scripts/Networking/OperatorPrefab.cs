using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class OperatorPrefab : MonoBehaviourPunCallbacks
{
    [SerializeField] private string operatorView = "OPERATOR";
    private Transform SailorPositionTracker;
    private bool isOperator,isSailorPrefabFound;
    private void Start()
    {
        isOperator = ClientSideData.Instance.View == operatorView;
        if (!isOperator) return;
        isSailorPrefabFound = false;
        var SPTObject = GameObject.FindObjectOfType<SailorPrefab>();
        if (SPTObject != null)
        {
            SailorPositionTracker = SPTObject.transform;
            isSailorPrefabFound = true;
        }
        else Debug.Log("Not yet found sailor prefab");
    }


    public override void OnEnable()
    {
        base.OnEnable();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        StartCoroutine(CheckSailorPrefabCoroutine(10));

    }
    public override void OnDisable()
    {
        base.OnDisable();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex != 1) return;
        StartCoroutine(CheckSailorPrefabCoroutine(5));

    }

    IEnumerator CheckSailorPrefabCoroutine(float sec)
    {
        yield return new WaitForSeconds(sec);
        Debug.Log("On Scene Loaded Called");
                    CheckAndSetSailorPrefab();

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        CheckAndSetSailorPrefab();        
        
    }

    private void CheckAndSetSailorPrefab()
    {
        isOperator = ClientSideData.Instance.View == operatorView;
        if (!isOperator) return;
        var SPTObject = GameObject.FindObjectOfType<SailorPrefab>();
        if (SPTObject != null)
        {
            SailorPositionTracker = SPTObject.transform;
            isSailorPrefabFound = true;
            Debug.Log("Found sailor prefab");
        }
        else Debug.Log("Not yet found sailor prefab");
    }

    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        isSailorPrefabFound = false;
        SailorPositionTracker = null;
    }
    private void Update()
    {
        if (!isOperator || !isSailorPrefabFound || SailorPositionTracker == null) return;
        transform.position = SailorPositionTracker.position; 
        transform.rotation = SailorPositionTracker.rotation;
    }

}
