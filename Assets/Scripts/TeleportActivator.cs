using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportActivator : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("tp zone")) {
            Debug.Log("entering teleport zone, can now teleport");
            transform.Find("Locomotion").Find("Teleportation").gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("tp zone")) {
            Debug.Log("exiting teleport zone, can no longer teleport");
            transform.Find("Locomotion").Find("Teleportation").gameObject.SetActive(false);
        }
    }
}
