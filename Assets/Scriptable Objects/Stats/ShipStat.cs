using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Ship Stat", menuName ="Variables/ Ship Stat")]
public class ShipStat : ScriptableObject
{
    [SerializeField] private string statID;
    [SerializeField] private float minValue, maxValue;
    private float value;
    public string StatID { get => statID;  }
    public float MinValue { get => minValue;}
    public float MaxValue { get => maxValue; }
    public float Value { get => value; set => this.value = value; }
}
