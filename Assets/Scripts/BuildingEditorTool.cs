using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEngine;
public class BuildingEditorTool : MonoBehaviour {
    [SerializeField] private Material glass;
    [SerializeField] private Material brick;
    [SerializeField] private string wallName;
    [SerializeField] private string windowName;
    
    //private variables
    private Transform[] _children;
    private List<Transform> _walls;
    private List<Transform> _windows;
    
    
    private void GetChildReferences() {
        //gets references to all children of the Mub Model (every individual piece of building).
        _children = GetComponentsInChildren<Transform>();
        _walls = new List<Transform>();
        _windows = new List<Transform>();

        //adds the walls and windows to their respective lists.
        foreach (Transform child in _children) {
            if (child.name.Contains(wallName)) {
                _walls.Add(child);
            } else if(child.name.Contains(windowName)){
                _windows.Add(child);
            }
        }
    }
    
    
    //sets the materials of the walls and windows to brick and glass respectively.
    [ContextMenu("Set Materials")]
    public void SetMaterials() {
        GetChildReferences();
        
        //adds the wall material to all walls
        foreach (Transform wall in _walls) {
            wall.GetComponent<Renderer>().material = brick;
        }
        
        //adds the window material to all windows
        foreach (Transform window in _windows) {
            window.GetComponent<Renderer>().material = glass;
        }
    }

    
    //adds mesh colliders to the walls and windows.
    [ContextMenu("Add Colliders")]
    public void AddColliders() {
        GetChildReferences();
        
        //adds a mesh collider to all walls
        foreach (Transform wall in _walls) {
            AddMeshCollider(wall);
        }
        
        //adds the window material to all windows
        foreach (Transform window in _windows) {
            AddMeshCollider(window);
        }
        
    }

    //helper method for AddColliders()
    void AddMeshCollider(Transform wall) {
        // Check if mesh collider already exists
        if (wall.GetComponent<MeshCollider>() != null){
            Debug.LogWarning("MeshCollider already exists on " + wall.name);
            return;
        }

        // Get the mesh
        MeshFilter meshFilter = wall.GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.mesh == null) {
            Debug.LogError("No mesh found on " + wall.name);
            return;
        }

        // Add and configure
        MeshCollider meshCollider = wall.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = meshFilter.mesh;
        
    }
    
}

#endif
    

