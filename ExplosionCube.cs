using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class ExplosionCube : MonoBehaviour
{
    public event Action<EventParameters> HasExploded;

    public int Generation { get; private set; } = 1;

    public void Explode()
    {
        if (ShouldSplit())
        {
            var parameters = new EventParameters(transform.position, transform.localScale, Generation);
            HasExploded?.Invoke(parameters);
        }           

        Destroy(gameObject);
    }

    public void SetGeneration(int generation) => Generation = generation;

    private bool ShouldSplit() => Random.value <= GetSplitChance();

    private float GetSplitChance() => 1.0f / (1 << (Generation - 1));
}
