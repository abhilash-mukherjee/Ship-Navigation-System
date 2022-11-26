using UnityEngine;

public class LauncherUI : MonoBehaviour
{
    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject controlPanel;
    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private GameObject progressLabel;
    private void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);

    }

    //private void OnEnable()
    //{
    //    Launcher.OnLobbyJoined += OnLobbyJoined;
    //}
    //private void OnDisable()
    //{
    //    Launcher.OnLobbyJoined -= OnLobbyJoined;
    //}

    private void OnLobbyJoined()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    private void Update()
    {

    }

    
    public void OnConnectionStarted()
    {
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);
    }
}
