using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerInstantiator : MonoBehaviour
{
    [Tooltip("The prefab to use for representing the player")]
    public GameObject oVRManager,sailorPrefab, operatorPrefab;
    [SerializeField] private string operatorView = "OPERATOR", sailorView = "SAILOR";
    [SerializeField] private Transform operatorTransform, sailorTransform;
    [SerializeField]private GameObject operatorInput, shipInut;
    #region Monobehaviour Callbacks
    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;

    }
    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex != 1) return;
        StartCoroutine(CheckSailorPrefabCoroutine(10f));

    }
    IEnumerator CheckSailorPrefabCoroutine(float sec)
    {
        yield return new WaitForSeconds(sec);
        Debug.Log("On Scene Loaded Called");
        UpdateLevel();

    }

    private void UpdateLevel()
    {
        if (PlayerManager.LocalPlayerInstance == null)
        {
            var currentView = ClientSideData.Instance.View;
            Transform parent = currentView == operatorView ? operatorTransform : sailorTransform;
            oVRManager.gameObject.transform.SetParent(parent);
            oVRManager.gameObject.transform.localPosition = Vector3.zero;
            oVRManager.gameObject.transform.localRotation = Quaternion.identity;
            shipInut.SetActive(currentView == sailorView);
            operatorInput.SetActive(currentView == operatorView);

            if(currentView == sailorView)
            {
                PhotonNetwork.Instantiate(sailorPrefab.name, Vector3.zero, Quaternion.identity);
            }
            else
            {

                PhotonNetwork.Instantiate(operatorPrefab.name, Vector3.zero, Quaternion.identity);
            }

        }
        else
        {
            Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
        }
    }

    #endregion

}
