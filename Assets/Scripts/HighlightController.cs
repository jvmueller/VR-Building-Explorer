using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour {
    public Material[] materialsToHighlight;
    public Color emissionColor;
    public float emissionIntensity = 0.02f;

    public void HighlightOn() {
        foreach (Material material in materialsToHighlight) {
            material.SetColor("_EmissionColor", emissionColor * emissionIntensity);
        }
    }

    public void HighlightOff() {
        foreach (Material material in materialsToHighlight) {
            material.SetColor("_EmissionColor", new Color(0f,0f,0f,0f) * 0f);
        }
    }
}
