using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private ExplosionCube _explosionCube;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryExplodeCube();
    }

    private void TryExplodeCube()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            _explosionCube = hit.transform.GetComponent<ExplosionCube>();

            if (_explosionCube != null)
                Explode();
        }
    }

    private void Explode()
    {
        _explosionCube.Explode();
        _explosionCube = null;
    }
}