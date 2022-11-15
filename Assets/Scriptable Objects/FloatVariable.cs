using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Float Variable", menuName = "Variables/float/default")]
public class FloatVariable : ScriptableObject
{
    [SerializeField]
    protected float floatValue;
    public virtual float Value { get => floatValue; set => floatValue = value; }
}
