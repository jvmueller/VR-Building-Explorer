using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoActivator : MonoBehaviour {
    [SerializeField] private GameObject videoSphere;
    public VideoPlayer videoPlayer;
    public Transform playerTransform;
    [SerializeField] private Vector3 startPosition = new Vector3(0.5f,1,25f);
    //public GameObject building;
    [SerializeField] private Material videoMaterial;
    [SerializeField] private float fadeDuration = 1f;
    private bool _isActive;

    void Start(){
        _isActive = false;
        videoMaterial.SetFloat("_Alpha", 0);
        //videoSphere.SetActive(false);
        videoPlayer.Stop();
        SetMovementActive(true);
    }

    public void ToggleVideo(){
        if (videoSphere.activeSelf){
            DeactivateVideo();
        }else{
            ActivateVideo();
        }
    }

    public void ActivateVideo(){
        //building.SetActive(false);
        //videoMaterial.SetFloat("_Alpha", 0);
        //videoSphere.SetActive(true);
        if (_isActive){
            StartCoroutine(WaitForFade(!_isActive));
        }
        StartCoroutine(FadeCoroutine(0, 1, true));
        videoPlayer.Play();
        

        //transform.position = playerTransform.position + new Vector3(0,1,1f);
            videoSphere.transform.position = playerTransform.position + new Vector3(0, 1f, 0);
            SetMovementActive(false);
        

    }

    public void DeactivateVideo(){
        
        if (!_isActive) {
            StartCoroutine(WaitForFade(_isActive));
        }

        
        //building.SetActive(true);
        StartCoroutine(FadeCoroutine(1, 0, false));
        

        //videoSphere.SetActive(false);
            //videoPlayer.Stop();
            //videoSphere.transform.position = startPosition;
            SetMovementActive(true);
        
    }

    void SetMovementActive(bool active){
        playerTransform.Find("Locomotion").Find("Move").gameObject.SetActive(active);
        playerTransform.Find("Locomotion").Find("Teleportation").gameObject.SetActive(active);
        playerTransform.Find("Locomotion").Find("Climb").gameObject.SetActive(active);
        playerTransform.Find("Locomotion").Find("Gravity").gameObject.SetActive(active);
        playerTransform.Find("Locomotion").Find("Jump").gameObject.SetActive(active);
    }
    
    private IEnumerator FadeCoroutine(float startAlpha, float endAlpha, bool endState) {
        float elapsedTime = 0f;
        
        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            
            videoMaterial.SetFloat("_Alpha", alpha);
            yield return null;
        }
        videoMaterial.SetFloat("_Alpha", endAlpha);
        _isActive = endState;
    }
    
    private IEnumerator WaitForFade(bool endState) {
        // Wait until the current coroutine finishes
        while (_isActive != endState) {
            yield return null;
        }
    }
}

