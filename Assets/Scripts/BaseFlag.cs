using System;
using UnityEngine;

public class BaseFlag : MonoBehaviour
{
    public bool IsReserve { get; private set; }

    public event Action BaseFlagActivated;

    private void OnEnable()
    {
        BaseFlagActivated?.Invoke();
    }

    public void MovePosition(Vector3 newPosition)
    {
        gameObject.SetActive(true);
        transform.position = newPosition;
    }

    public void Reserve()
    {
        gameObject.SetActive(false);
        IsReserve = true;
    }

    public void Clear()
    {
        IsReserve = false;
    }
}
