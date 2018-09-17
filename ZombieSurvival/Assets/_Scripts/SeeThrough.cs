using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThrough : MonoBehaviour {
    public Material transparentMaterial;
    private Material normalMaterial;

    void OnTriggerEnter(Collider other)
    {
        normalMaterial = other.GetComponent<Renderer>().material;
        other.GetComponent<Renderer>().material = transparentMaterial; 
    }

    void OnTriggerExit(Collider other)
    {
        other.GetComponent<Renderer>().material = normalMaterial;
        normalMaterial = null;
    }

}
