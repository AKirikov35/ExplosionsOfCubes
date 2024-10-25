using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Creator : MonoBehaviour
{
    [SerializeField] private Transform _prefab;
    [SerializeField] private Finder _finder;

    private List<ExplosionCube> _explosionCubes;

    public Action HasCreate;

    private void Start()
    {
        Vector3 startPosition = new Vector3(0, 0, 0);
        Vector3 startScale = new Vector3(4, 4, 4);
        Create(startPosition, startScale);

        _explosionCubes = _finder.ExplosionCubes;
    }

    private void OnEnable()
    {
        if (_explosionCubes != null && _explosionCubes.Count > 0)
            for (int i = 0; i < _explosionCubes.Count; i++)
                if (_explosionCubes[i] != null)
                    _explosionCubes[i].HasExplode += Create;
    }

    private void OnDisable()
    {
        if (_explosionCubes != null && _explosionCubes.Count > 0)
            for (int i = 0; i < _explosionCubes.Count; i++)
                if (_explosionCubes[i] != null)
                    _explosionCubes[i].HasExplode -= Create;
    }

    private void Create(Vector3 position, Vector3 scale)
    {
        if (_prefab != null)
        {
            for (int i = 0; i < Random.Range(2, 7); i++)
            {
                Vector3 newPosition = CreateNewPosition(position);
                Vector3 newScale = CreateNewScale(scale);

                Transform newCube = Instantiate(_prefab, newPosition, Quaternion.identity);
                newCube.localScale = newScale;

                Color randomColor = GetRandomColor();
                Renderer cubeRenderer = newCube.GetComponent<Renderer>();

                if (cubeRenderer != null)
                {
                    cubeRenderer.material.color = randomColor;
                }

                ExplosionCube newExplosionCube = newCube.GetComponent<ExplosionCube>();

                if (newExplosionCube != null)
                    newExplosionCube.HasExplode += Create;

                HasCreate?.Invoke();
            } 
        }
    }

    private Vector3 CreateNewPosition(Vector3 position)
    {
        Vector3 newPosition;
        float modifierPosition = 4f;

        newPosition.x = Random.Range(position.x - modifierPosition, position.x + modifierPosition);
        newPosition.y = Random.Range(position.y - modifierPosition, position.y + modifierPosition);
        newPosition.z = Random.Range(position.z - modifierPosition, position.z + modifierPosition);

        return newPosition;
    }

    private Vector3 CreateNewScale(Vector3 scale)
    {
        Vector3 newScale;
        float modifierScale = 2f;

        newScale = scale / modifierScale;

        return newScale;
    }
    
    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
