using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    private void Update()
    {
        StartCoroutine("ParticleDestroy");

    }
    IEnumerator ParticleDestroy()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
