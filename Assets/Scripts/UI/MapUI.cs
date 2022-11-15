using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered");
        var img = other.gameObject.GetComponent<Image>();
        SetOpacity(img, 1f);
        
    }
    
    
    private void OnTriggerExit2D(Collider2D other)
    {
        var img = other.gameObject.GetComponent<Image>();
        SetOpacity(img, 0f);
        
    }

    private void SetOpacity(Image img, float v)
    {
        if (img != null)
        {
            var tempColor = img.color;
            tempColor.a = v;
            img.color = tempColor;
        }
    }
}


