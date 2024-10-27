using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class ExplosionCube : MonoBehaviour
{
    public event Action<EventParameters> HasExploded;

    private Renderer _renderer;
    private float _splitChance = 1f;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Init(float initialSplitChance)
    {
        _splitChance = initialSplitChance;
    }

    public void Explode()
    {
        if (ShouldSplit())
        {
            var parameters = new EventParameters(transform.position, transform.localScale, _splitChance);
            HasExploded?.Invoke(parameters);
        }

        Destroy(gameObject);
    }

    public void SetColor(Color color)
    {
        if (_renderer != null)
            _renderer.material.color = color;
    }

    private bool ShouldSplit()
    {
        return Random.value <= _splitChance;
    }
}
