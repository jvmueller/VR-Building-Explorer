using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class HighlightController : MonoBehaviour {
    public Material[] materialsToHighlight;
    public Color emissionColor;
    public float emissionIntensity = 0.02f;
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float fadeOutDuration = 0.5f;
    [SerializeField] private AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private Coroutine _currentFadeCoroutine;

    void Start() {
        foreach (Material material in materialsToHighlight) {
            material.SetColor("_EmissionColor", emissionColor * 0f);
        }
    }
    
    public void HighlightOn() {
        if (_currentFadeCoroutine != null) {
            StopCoroutine(_currentFadeCoroutine);
        }

        _currentFadeCoroutine = StartCoroutine(FadeCoroutine(true, fadeInDuration));
    }

    public void HighlightOff() {
        if (_currentFadeCoroutine != null) {
            StopCoroutine(_currentFadeCoroutine);
        }

        _currentFadeCoroutine = StartCoroutine(FadeCoroutine(false, fadeOutDuration));
    }
    
    private IEnumerator FadeCoroutine(bool fadeIn, float duration) {
        float startIntensity = fadeIn ? 0f : emissionIntensity;
        float endIntensity = fadeIn ? emissionIntensity : 0f;
        float elapsedTime = 0f;
        
        //sets the emission to the starting intensity
        foreach (Material material in materialsToHighlight) {
            material.SetColor("_EmissionColor", emissionColor * startIntensity);
        }

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / duration;

            //applies animation curve with lerp for smooth transition
            float curveValue = fadeCurve.Evaluate(normalizedTime);
            float currentIntensity = Mathf.Lerp(startIntensity, endIntensity, curveValue);
            
            foreach (Material material in materialsToHighlight) {
                material.SetColor("_EmissionColor", emissionColor * currentIntensity);
            }

            yield return null;
        }

        //ensures the final emission intensity is set
        foreach (Material material in materialsToHighlight) {
            material.SetColor("_EmissionColor", emissionColor * endIntensity);
        }
        
        _currentFadeCoroutine = null;
    }
}
