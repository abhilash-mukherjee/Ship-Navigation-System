using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderStatDisplayer : StatDisplayer
{
    [SerializeField] private Image slider;
    protected override void DisplayStat()
    {
        var val = Stat.Value / (Stat.MaxValue - Stat.MinValue);
    }
}