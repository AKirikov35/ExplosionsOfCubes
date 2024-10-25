using System;
using UnityEngine;

public class ExplosionCube : MonoBehaviour
{
    public event Action<Vector3, Vector3> HasExplode;

    public void Explode()
    {
        HasExplode?.Invoke(transform.position, transform.localScale);
        Destroy(gameObject);
    }
}