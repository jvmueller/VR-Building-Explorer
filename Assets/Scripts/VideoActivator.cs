using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoActivator : MonoBehaviour {

    public GameObject[] objectsInScene;
    //public FadeCanvas fadeCanvas;
    public VideoPlayer videoPlayer;
    public Material videoMaterial;
    public Transform playerTransform;
    [SerializeField] private float fadeDuration = 1f;

    private Material _skyboxMaterial;

    void Start(){
        _skyboxMaterial = RenderSettings.skybox;
    }

    public void ActivateVideo() {
        StartCoroutine(FadeAndSwitchVideo(videoMaterial, videoPlayer.Play));
        SetMovementActive(false);
    }

    public void DeactivateVideo(){
        StartCoroutine(FadeAndSwitchVideo(_skyboxMaterial, videoPlayer.Stop));
        SetMovementActive(true);
    }

    private IEnumerator FadeAndSwitchVideo(Material targetMaterial, Action onCompleteAction){
        //fadeCanvas.QuickFadeIn();
        yield return new WaitForSeconds(fadeDuration);
        
        //perform actions after fading in
        ToggleObjectVisibility(targetMaterial);
        //fadeCanvas.QuickFadeOut();
        
        //perform actions after fading out
        RenderSettings.skybox = _skyboxMaterial;
        onCompleteAction.Invoke();

    }

    private void ToggleObjectVisibility(Material targetMaterial) {
        foreach (GameObject obj in objectsInScene) {
            obj.SetActive(targetMaterial.Equals(_skyboxMaterial));
        }
    }
    void SetMovementActive(bool active){
        playerTransform.Find("Locomotion").Find("Move").gameObject.SetActive(active);
        playerTransform.Find("Locomotion").Find("Teleportation").gameObject.SetActive(active);
        playerTransform.Find("Locomotion").Find("Climb").gameObject.SetActive(active);
        playerTransform.Find("Locomotion").Find("Gravity").gameObject.SetActive(active);
        playerTransform.Find("Locomotion").Find("Jump").gameObject.SetActive(active);
    }
}

