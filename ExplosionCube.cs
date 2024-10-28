using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class ExplosionCube : MonoBehaviour
{
    public event Action<EventParameters> HasExploded;

    private readonly float _baseDetonationRadius = 100f;
    private readonly float _baseDetonationForce = 1000f;

    private readonly float _minDetonationRadius = 30f;
    private readonly float _minDetonationForce = 100f;

    private readonly float _forceModifier = 100f;

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
        else
        {
            Detonate();
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

    private void Detonate()
    {
        List<Rigidbody> detonateObjects = GetDetonateObjects(_baseDetonationRadius);

        foreach (Rigidbody rigidbody in detonateObjects)
        {
            float distance = Vector3.Distance(transform.position, rigidbody.transform.position);
            float size = GetObjectSize(rigidbody);

            float radius = Mathf.Max(_baseDetonationRadius * (1 / size), _minDetonationRadius);
            float force = Mathf.Max(_baseDetonationForce * (1 / size), _minDetonationForce);

            force *= _forceModifier;

            float effectiveForce = force * (1 - (distance / radius));
            effectiveForce = Mathf.Max(effectiveForce, _minDetonationForce);

            rigidbody.AddExplosionForce(effectiveForce, transform.position, radius);

            Debug.Log($"Взрыв на позиции {transform.position.normalized}\n" +
                      $"Радиус: {radius}, " +
                      $"Сила: {force}");
        }
    }

    private List<Rigidbody> GetDetonateObjects(float radius)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        List<Rigidbody> explosionCubes = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                explosionCubes.Add(hit.attachedRigidbody);

        return explosionCubes;
    }

    private float GetObjectSize(Rigidbody rigidbody)
    {
        if (rigidbody.TryGetComponent<Collider>(out var collider))
            return collider.bounds.size.magnitude;

        return 1f;
    }
}
