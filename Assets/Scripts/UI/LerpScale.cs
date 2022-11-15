using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpScale : MonoBehaviour
{
    [SerializeField] private Transform scalableElement;
    [SerializeField] private AudioSource UIOn, UIOff;
    public float targetScale;
    public float timeToLerp = 0.25f;
    float scaleModifier = 1;
    private bool m_isUIActive = false;
    private void Start()
    {
        m_isUIActive = false;
        scaleModifier = 0f;
    }
    public void ToogleUI()
    {
        if (m_isUIActive) HideUI();
        else ShowUI();
        m_isUIActive = !m_isUIActive;
    }
    void ShowUI()
    {
        UIOn.Play();
        scalableElement.gameObject.SetActive(true);
        //StartCoroutine(LerpFunction(targetScale, timeToLerp));
    }
    void HideUI()
    {
        UIOff.Play();
        scalableElement.gameObject.SetActive(false);
        //StartCoroutine(LerpFunction(0, timeToLerp));
    } 
    IEnumerator LerpFunction(float endValue, float duration)
    {
        float time = 0;
        float startValue = scaleModifier;
        Vector3 startScale = scalableElement.localScale;
        while (time < duration)
        {
            scaleModifier = Mathf.Lerp(startValue, endValue, time / duration);
            scalableElement.localScale = startScale * scaleModifier;
            time += Time.deltaTime;
            yield return null;
        }

        scalableElement.localScale = startScale * endValue;
        scaleModifier = endValue;
    }
}
