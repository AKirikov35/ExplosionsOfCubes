using System.Collections.Generic;
using UnityEngine;

public class Finder : MonoBehaviour
{
    [SerializeField] private List<ExplosionCube> _explosionCubes;
    [SerializeField] private Creator _creator;
    [SerializeField] private Destroyer _destroyer;

    public List<ExplosionCube> ExplosionCubes => _explosionCubes;

    private void Start()
    {
        _explosionCubes = new List<ExplosionCube>();
        ExplosionCube[] foundCubes = FindObjectsOfType<ExplosionCube>();
        _explosionCubes.AddRange(foundCubes);
    }

    private void OnEnable()
    {
        _creator.HasCreate += Add;
        _destroyer.HasDestroy += Remove;
    }

    private void OnDisable()
    {
        _creator.HasCreate -= Add;
        _destroyer.HasDestroy -= Remove;
    }

    private void Add()
    {
        ExplosionCube[] foundCubes = FindObjectsOfType<ExplosionCube>();
        _explosionCubes.AddRange(foundCubes);
    }

    private void Remove(ExplosionCube explosionCube)
    {
        _explosionCubes.Remove(explosionCube);
    }
}