using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEngine;
public class BuildingEditorTool : MonoBehaviour {
    [SerializeField] private Material glass;
    [SerializeField] private Material brick;
    [SerializeField] private Material chairMat;
    [SerializeField] private Material tableMat;
    
    [SerializeField] private string wallName;
    [SerializeField] private string windowName;
    [SerializeField] private string chairName;
    [SerializeField] private string tableName;
    [SerializeField] private List<string> colliderNames;
    
    //private variables
    private Transform[] _children;
    private List<Transform> _walls;
    private List<Transform> _windows;
    private List<Transform> _chairs;
    private List<Transform> _tables;
    private List<Transform> _colliders;
    
    
    private void GetChildReferences() {
        //gets references to all children of the Mub Model (every individual piece of building).
        _children = GetComponentsInChildren<Transform>();
        _walls = new List<Transform>();
        _windows = new List<Transform>();
        _colliders = new List<Transform>();
        _chairs = new List<Transform>();
        _tables = new List<Transform>();

        //adds the walls and windows to their respective lists.
        foreach (Transform child in _children) {
            if (child.name.Contains(wallName)) {
                _walls.Add(child);
            }
            else if (child.name.Contains(windowName)) {
                _windows.Add(child);
            }
            else if (child.name.Contains(chairName)){
                _chairs.Add(child);
            }
            else if (child.name.Contains(tableName)) {
                _tables.Add(child);
            }
            foreach (string childName in colliderNames) {
                if (child.name.Contains(childName)) {
                    _colliders.Add(child);
                }
            }
        }
    }
    
    
    //sets the materials of the walls and windows to brick and glass respectively.
    [ContextMenu("Set Materials")]
    public void SetMaterials() {
        GetChildReferences();
        
        /*
        //adds the wall material to all walls
        foreach (Transform wall in _walls) {
            wall.GetComponent<Renderer>().material = brick;
        }*/
        
        //adds the window material to all windows
        foreach (Transform window in _windows) {
            window.GetComponent<Renderer>().material = glass;
        }
        
        //adds the window material to all windows
        foreach (Transform chair in _chairs) {
            chair.GetComponent<Renderer>().material = chairMat;
        }
        
        //adds the window material to all windows
        foreach (Transform table in _tables) {
            table.GetComponent<Renderer>().material = tableMat;
        }
    }

    
    //adds mesh colliders to the walls and windows.
    [ContextMenu("Add Colliders")]
    public void AddColliders() {
        GetChildReferences();
        
        //adds a mesh collider to all objects designated for collision
        foreach (Transform colliderObject in _colliders) {
            AddMeshCollider(colliderObject);
        }
    }
    
    //erases colliders from every object
    [ContextMenu("Clear Colliders")]
    public void ClearColliders() {
        Collider meshCollider;
        foreach (Transform child in _children) {
            meshCollider = child.GetComponent<MeshCollider>();
            if (meshCollider != null){
                Debug.LogWarning("erasing collider of " + child.name);
                Destroy(meshCollider);
            }
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
    

