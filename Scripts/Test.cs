using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    public NavMeshSurface[] surfaces;
    private GameObject bluePrint;

    // Use this for initialization
    // void Update () 
    // {
    //     bluePrint = GameObject.FindGameObjectWithTag("BluePrint");
    //     if(Input.GetMouseButton(0) && bluePrint) {
    //         for (int i = 0; i < surfaces.Length; i++) 
    //         {
    //             surfaces [i].BuildNavMesh ();
    //         }
    //     }
    // }

    // public void UpadteNavmesh() {
    //     for (int i = 0; i < surfaces.Length; i++) 
    //     {
    //         surfaces [i].BuildNavMesh ();
    //     }
    // }
    
    void OnPreRender() {
        GL.wireframe = true;
    }
    void OnPostRender() {
        GL.wireframe = false;
    }

}
