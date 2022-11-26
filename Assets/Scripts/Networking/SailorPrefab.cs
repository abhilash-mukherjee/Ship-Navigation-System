using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class SailorPrefab : MonoBehaviourPunCallbacks
{
    [SerializeField] private string sailorView = "SAILOR";
    private Transform ShipController;
    private bool isSailor;
    private void Start()
    {
        var SPMObject = GameObject.FindObjectOfType<ShipPathManager>();
        if(SPMObject != null) ShipController = SPMObject.transform;
        isSailor = ClientSideData.Instance.View == sailorView;
    }

    private void Update()
    {
        if (!isSailor) return;
        transform.SetPositionAndRotation(ShipController.position, ShipController.rotation);
    }

}
