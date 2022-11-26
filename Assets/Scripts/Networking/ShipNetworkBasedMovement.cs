using UnityEngine;

public class ShipNetworkBasedMovement : MonoBehaviour
{
    [SerializeField] private string operatorView = "OPERATOR";
    private bool isOperator, isOperatorPrefabFound;
    private Transform shipTransform;

    private void Start()
    {
        isOperatorPrefabFound = false;
        isOperator = ClientSideData.Instance.View == operatorView;
        if (!isOperator) return;
        var SPTObject = GameObject.FindObjectOfType<OperatorPrefab>();
        if (SPTObject != null)
        {
            shipTransform = SPTObject.transform;
            isOperatorPrefabFound = true;
        }
        else Debug.Log("Not yet found sailor prefab");
    }

    private void Update()
    {
        if (!isOperator || !isOperatorPrefabFound || shipTransform == null) return;
        transform.SetPositionAndRotation(shipTransform.position, shipTransform.rotation);

    }
}