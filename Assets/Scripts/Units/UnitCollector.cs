using System;
using UnityEngine;

public class UnitCollector : MonoBehaviour
{
    [SerializeField] private ResourceTrigger _trigger;

    private Resource _resource;
    private ResourceCell _cell;

    public bool IsEmpty => _resource == null;

    public event Action<Resource> ResourceCollected;
    public event Action ResourceSent;

    private void OnEnable()
    {
        _trigger.ResourceCellFound += OnResourceCellFound;
    }

    private void OnDisable()
    {
        _trigger.ResourceCellFound -= OnResourceCellFound;
    }

    public void SetResourceCell(ResourceCell resourceCell)
    {
        _cell = resourceCell;
    }

    public void ClearStorage()
    {
        Destroy(_resource.gameObject);
        _resource = null;
        ResourceSent?.Invoke();
    }

    private void OnResourceCellFound(ResourceCell cell)
    {
        if(cell == _cell)
        {
            _resource = cell.GetResource();
            _resource.transform.SetParent(transform);
            ResourceCollected?.Invoke(_resource);
        }
    }
}