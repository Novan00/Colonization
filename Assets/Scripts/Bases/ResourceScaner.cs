using System;
using UnityEngine;

public class ResourceScaner : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layerMask;

    private Collider[] overlappedColliders = new Collider[20];

    public event Action<ResourceCell> ResourceCellFound;

    public void Scan()
    {
        Vector3 center = transform.position;
        Physics.OverlapSphereNonAlloc(center, _radius, overlappedColliders, _layerMask);

        foreach (var collider in overlappedColliders)
        {
            if (collider == null)
            {
                continue;
            }

            if (collider.TryGetComponent(out ResourceCell resourceCell))
            {
                if (!resourceCell.IsEmpty && !resourceCell.IsReserve)
                {
                    ResourceCellFound?.Invoke(resourceCell);                    
                    return;
                }
            }
        }
    }
}
