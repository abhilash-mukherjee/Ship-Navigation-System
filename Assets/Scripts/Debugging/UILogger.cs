using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILogger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textField;
    
    private void OnNewMessegeLogged(string str)
    {
        textField.text = str;
    }
}
