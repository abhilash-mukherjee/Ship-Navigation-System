
using UnityEngine;


public class ClientSideData : MonoBehaviour
{
    [SerializeField] private string initialView;
    private string m_view;
    public static ClientSideData Instance;
    public string View { get => m_view; set => m_view = value; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        m_view = initialView;
    }

    private void OnEnable()
    {
        ViewSelector.OnViewSelected += UpdateView;
    }
    private void OnDisable()
    {
        ViewSelector.OnViewSelected -= UpdateView;
    }

    private void UpdateView(string view)
    {

        m_view = view;
    }
}