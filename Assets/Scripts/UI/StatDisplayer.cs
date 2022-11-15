using UnityEngine;

public abstract class StatDisplayer : MonoBehaviour
{
    [SerializeField] private ShipStat stat;

    protected ShipStat Stat { get => stat;}

    private void Update()
    {
        DisplayStat();
    }

    protected abstract void DisplayStat();
}
