using System;
using UnityEngine;

public class BaseMoneySystem : MonoBehaviour
{
    [SerializeField] private int _unitPrice;
    [SerializeField] private int _basePrice;

    public int ResourceCount { get; private set; }
    public bool CanCreateUnit => ResourceCount >= _unitPrice;
    public bool CanBuildBase => ResourceCount >= _basePrice;

    public event Action UnitAccumulated;
    public event Action BaseAccumulated;

    public void AddResource()
    {
        ResourceCount++;
    }

    public void RemoveResource()
    {
        ResourceCount--;
    }

    public void BuyBase()
    {
        if (CanBuildBase)
        {
            ResourceCount -= _basePrice;
            BaseAccumulated?.Invoke();
        }
    }

    public void BuyBot()
    {
        if (CanCreateUnit)
        {
            ResourceCount -= _unitPrice;
            UnitAccumulated?.Invoke();
        }
    }
}
