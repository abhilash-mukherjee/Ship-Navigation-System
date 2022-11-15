using UnityEngine;

[CreateAssetMenu(fileName = "New Mode Name Container", menuName = "Variables/ Ship Mode Name Container")]
public class ShipModeNameContainer : ScriptableObject
{
    private ShipModeName m_modeName;

    public ShipModeName ModeName { get => m_modeName; set => m_modeName = value; }
}
