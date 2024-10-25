using System;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private ExplosionCube _explosionCube;
    private RaycastHit _hit;

    public Action<ExplosionCube> HasDestroy;

    private void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit))
            _explosionCube = _hit.transform.GetComponent<ExplosionCube>();

        if (_explosionCube == null)
            return;

        if (Input.GetMouseButtonDown(0))
            Explode();
    }

    private void Explode()
    {
        _explosionCube.Explode();
        HasDestroy?.Invoke(_explosionCube);
    }
}