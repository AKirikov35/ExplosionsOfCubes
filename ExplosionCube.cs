using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class ExplosionCube : MonoBehaviour
{
    public int Generation { get; private set; } = 1;

    public event Action<Vector3, Vector3, int> OnExplode;

    public void Explode()
    {
        if (ShouldSplit())
            OnExplode?.Invoke(transform.position, transform.localScale, Generation);

        Destroy(gameObject);
    }

    public void SetGeneration(int generation) => Generation = generation;

    private bool ShouldSplit() => Random.value <= GetSplitChance();

    private float GetSplitChance()
    {
        switch (Generation)
        {
            case 1:
                return 1.0f;
            case 2:
                return 0.5f;
            case 3:
                return 0.25f;
            default:
                return 0.0f;
        }
    }
}