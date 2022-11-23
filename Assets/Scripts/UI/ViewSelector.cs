using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSelector : MonoBehaviour
{
    [SerializeField] private string view0, view1;
    [SerializeField] private GameObject view0Selected, view1Selected;
    private string currentView;
    public delegate void ViewSelectHandler(string view);
    public static event ViewSelectHandler OnViewSelected;
    private void Start()
    {
        currentView = view0;
        OnViewSelected?.Invoke(currentView);
    }
    public void ToggleViews()
    {
        currentView = currentView == view0 ? view1 : view0;
        OnViewSelected?.Invoke(currentView);
        UpdateUI(); 
        Debug.Log($"View Updated. {currentView} view");
    }

    private void UpdateUI()
    {
        view0Selected.SetActive(currentView == view0);
        view1Selected.SetActive(currentView == view1);
    }
}