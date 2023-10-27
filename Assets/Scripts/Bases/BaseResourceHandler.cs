using System;
using UnityEngine;

public class BaseResourceHandler : MonoBehaviour
{
    public event Action ResourceSent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out UnitCollector unitCollector))
        {
            if (!unitCollector.IsEmpty)
            {
                unitCollector.ClearStorage();
                ResourceSent?.Invoke();
            }
        }
    }
}
