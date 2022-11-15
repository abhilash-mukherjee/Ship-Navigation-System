using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SeaMap : MonoBehaviour
{
    [SerializeField] private Transform seaCenter;
    [SerializeField] private float seaSquareSide;
    [SerializeField] private RectTransform mapBG, portImg;
    [SerializeField] private List<MapElement> mapElements;
    [SerializeField] private GameObject lineImg;
    [SerializeField] private List<PortVertex> PortVertices;
    [SerializeField] private float lineThickness = 0.01f;
    private Vector3[] corners;
    private float m_scaleReductionFactor = 1;
    private void Start()
    {
        corners = new Vector3[4];
        mapBG.GetWorldCorners(corners);
        var imgSide = Vector3.Magnitude(corners[0] - corners[1]);
        m_scaleReductionFactor = imgSide / seaSquareSide;
        DrawPort();

    }

    private void DrawPort()
    {
        List<GameObject> mapAnchorPoints = new List<GameObject>();
        for(int i = 0; i < PortVertices.Count; i++)
        {
            var mapPos = MapHelper.GetScaledTopViewPosition(PortVertices[i].transform, seaCenter.position, m_scaleReductionFactor);
            var anchorPoint = Instantiate(new GameObject());
            anchorPoint.transform.SetParent(portImg);
          
            var rect = anchorPoint.GetComponent<RectTransform>();
            if (rect == null) rect = anchorPoint.AddComponent<RectTransform>();
            rect.localPosition = Vector3.zero;
            rect.localRotation = Quaternion.identity;
            rect.anchoredPosition = new Vector2(mapPos.x, mapPos.z);
            mapAnchorPoints.Add(anchorPoint);
        }
        for(int i = 0; i < mapAnchorPoints.Count; i++)
        {
            var start = mapAnchorPoints[i].transform.position;
            var end = i == mapAnchorPoints.Count - 1 ? mapAnchorPoints[0].transform.position : mapAnchorPoints[i + 1].transform.position;
            var pos = (start + end) / 2;
            var size = (start - end).magnitude;
            var img = Instantiate(lineImg);
            img.transform.SetParent(portImg);
            img.transform.localRotation = Quaternion.identity;
            img.transform.localScale = Vector3.one;
            img.transform.position = pos;
            img.transform.up = (end - start).normalized;
            img.GetComponent<RectTransform>().sizeDelta = new Vector2(lineThickness, size);
        }
    }

    private void Update()
    {

        foreach (var element in mapElements)
        {
            element.UpdateTransform(seaCenter.position, m_scaleReductionFactor);
            element.UpdateRotation(seaCenter.position, transform);
        }
    }
    
}

[System.Serializable]
public class MapElement
{
    public Sprite sprite;
    public Transform GlobalTransform;
    public RectTransform mapElementTransform;

    public void UpdateTransform(Vector3 globalCentre, float scaleReductionFactor)
    {
        var locaPos = MapHelper.GetScaledTopViewPosition(GlobalTransform, globalCentre, scaleReductionFactor);
        mapElementTransform.anchoredPosition = new Vector2(locaPos.x, locaPos.z);
        
    }

    internal void UpdateRotation(Vector3 globalCentre, Transform transform)
    {
        var centreDir = Vector3.Scale(globalCentre - GlobalTransform.position, new Vector3(1, 0, 1));
        var forward = Vector3.Scale(GlobalTransform.forward, new Vector3(1, 0, 1));
        var angle = Vector3.SignedAngle(centreDir, forward, Vector3.up);
        var mapElementUp = Quaternion.AngleAxis(angle, -transform.forward) * Vector3.up;
        mapElementTransform.up = mapElementUp;
    }

    
}

public class MapHelper
{
    public static Vector3 GetScaledTopViewPosition(Transform _globalTransform, Vector3 globalMapCentre, float scaleReductionFactor)
    {
        var _globalRelativePositionVector = (_globalTransform.position - globalMapCentre);
        var _scalingVector = new Vector3(scaleReductionFactor, 0, scaleReductionFactor);
        var relPos = Vector3.Scale(_globalRelativePositionVector, _scalingVector);
        return relPos;
    }
}
