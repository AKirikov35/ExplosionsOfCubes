using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    [SerializeField] private List<ExplosionCube> _explosionCubes;

    private void OnEnable()
    {
        SubscribeToExplodeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromExplodeEvents();
    }

    private void SubscribeToExplodeEvents()
    {
        if (_explosionCubes == null) return;

        foreach (var cube in _explosionCubes)
            if (cube != null)
                cube.HasExploded += Create;
    }

    private void UnsubscribeFromExplodeEvents()
    {
        if (_explosionCubes == null) return;

        foreach (var cube in _explosionCubes)
            if (cube != null)
                cube.HasExploded -= Create;
    }

    private void Create(EventParameters parameters)
    {
        _explosionCubes.RemoveAll(cube => cube == null);

        int minCount = 2;
        int maxCount = 8;
        int cubeCount = Random.Range(minCount, maxCount);

        for (int i = 0; i < cubeCount; i++)
        {
            Vector3 newPosition = CreateNewPosition(parameters.Position);
            Vector3 newScale = CreateNewScale(parameters.Scale);

            ExplosionCube newCube = Instantiate(_explosionCubes[0], newPosition, Quaternion.identity);
            newCube.transform.localScale = newScale;

            newCube.Init(ReduceSplitChance(parameters.SplitChance));

            SetRandomColor(newCube);

            if (newCube.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                rigidbody.isKinematic = false;

                Vector3 explosionForce = new Vector3
                (
                    Random.Range(-3f, 3f),
                    Random.Range(-3f, 3f),
                    Random.Range(-3f, 3f)
                ) * 5f;

                rigidbody.AddForce(explosionForce, ForceMode.Impulse);
                newCube.HasExploded += Create;
            }

            _explosionCubes.Add(newCube);
        }
    }

    private Vector3 CreateNewPosition(Vector3 position)
    {
        float modifierPosition = 6f;
        return new Vector3
        (
            Random.Range(position.x - modifierPosition, position.x + modifierPosition),
            Random.Range(position.y - modifierPosition, position.y + modifierPosition),
            Random.Range(position.z - modifierPosition, position.z + modifierPosition)
        );
    }

    private Vector3 CreateNewScale(Vector3 scale)
    {
        float modifierScale = 2f;
        return scale / modifierScale;
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }

    private void SetRandomColor(ExplosionCube cube)
    {
        Color randomColor = GetRandomColor();
        cube.SetColor(randomColor);
    }

    private float ReduceSplitChance(float splitChance)
    {
        float modifierChance = 2f;
        return splitChance / modifierChance;
    }
}
