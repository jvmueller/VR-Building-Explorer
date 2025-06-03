using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoActivator : MonoBehaviour {
    [SerializeField] private GameObject videoSphere;
    public VideoPlayer videoPlayer;
    public Transform playerTransform;
    [SerializeField] Vector3 startPosition = new Vector3(0.5f,1,25f);
    //public GameObject building;
    
    void Start() {
        videoSphere.SetActive(false);
        videoPlayer.Stop();
        SetMovementActive(true);
    }

    public void ToggleVideo() {
        if (videoSphere.activeSelf){
            DeactivateVideo();
        }else{
            ActivateVideo();
        }
    }

    void ActivateVideo() {
        //building.SetActive(false);
        videoSphere.SetActive(true);
        videoPlayer.Play();
        transform.position = playerTransform.position + new Vector3(0,1,1.5f);
        videoSphere.transform.position = playerTransform.position + new Vector3(0, 1.5f, 0);
        SetMovementActive(false);

    }

    void DeactivateVideo() {
        //building.SetActive(true);
        videoSphere.SetActive(false);
        videoPlayer.Stop();
        videoSphere.transform.position = startPosition;
        SetMovementActive(true);
    }

    void SetMovementActive(bool active) {
        playerTransform.Find("Locomotion").Find("Move").gameObject.SetActive(active);
        playerTransform.Find("Locomotion").Find("Teleportation").gameObject.SetActive(active);
        playerTransform.Find("Locomotion").Find("Climb").gameObject.SetActive(active);
        playerTransform.Find("Locomotion").Find("Gravity").gameObject.SetActive(active);
        playerTransform.Find("Locomotion").Find("Jump").gameObject.SetActive(active);
    }

}

