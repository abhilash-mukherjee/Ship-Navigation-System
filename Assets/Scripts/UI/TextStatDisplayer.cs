using TMPro;
using UnityEngine;
public class TextStatDisplayer : StatDisplayer
{
    [SerializeField] private TextMeshProUGUI statText;
    protected override void DisplayStat()
    {
        statText.text = Stat.Value.ToString();
    }
}
