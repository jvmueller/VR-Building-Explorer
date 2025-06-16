using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObfuscationManager : MonoBehaviour
{
    //objects in the scene you want to be deactivated
    public GameObject[] objectsToDeactivate;
    public GameObject[] objectsToHide;

    public static ObfuscationManager instance;

    private void Awake() {
        if (instance == null)
            instance = this;
    }
    
    public void HideAll(){
        //hides all objects by deactivating them
        foreach (GameObject obj in objectsToDeactivate) {
            obj.SetActive(false);
        }
    }

    public void ShowAll() {
        //shows all objects just by setting them active
        foreach (GameObject obj in objectsToDeactivate) {
            obj.SetActive(true);
        }
    }
}
