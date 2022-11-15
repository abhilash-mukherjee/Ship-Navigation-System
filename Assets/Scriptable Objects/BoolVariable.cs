using UnityEngine;

[CreateAssetMenu(fileName = "New Float Variable", menuName = "Variables/bool")]
public class BoolVariable : ScriptableObject
{
    [SerializeField]
    protected bool boolValue;
    public virtual bool Value { get => boolValue; set => boolValue = value; }
}
