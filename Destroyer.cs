using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryExplodeCube();
    }

    private void TryExplodeCube()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            ExplosionCube explosionCube = hit.transform.GetComponent<ExplosionCube>();

            if (explosionCube != null)
                explosionCube.Explode();
        }
    }
}
