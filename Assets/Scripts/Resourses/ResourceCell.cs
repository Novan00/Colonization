using UnityEngine;

public class ResourceCell : MonoBehaviour
{
    private Resource _resource;

    public bool IsReserve { get; private set; } = false;

    public bool IsEmpty => _resource == null;

    public void SetResource(Resource resource)
    {
        _resource = resource;
    }

    public Resource GetResource()
    {
        var resource = _resource;

        Clear();

        return resource;
    }

    public void Reserve()
    {
        IsReserve = true;
    }

    private void Clear()
    {
        IsReserve = false;
        _resource = null;
    }
}
