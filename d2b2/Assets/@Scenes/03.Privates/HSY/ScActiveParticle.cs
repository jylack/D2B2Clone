using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScActiveParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] SkinnedMeshRenderer modelMesh;
    private void OnEnable()
    {
        modelMesh.enabled = false;
        particleSystem.Play();
        modelMesh.enabled = true;
    }

    private void OnDisable()
    {
        modelMesh.enabled = false;
        particleSystem.Play();
        modelMesh.enabled = true;
    }
}
