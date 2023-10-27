using System;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private int _startUnitsCount = 3;

    public event Action<Unit> UnitSpawned;

    private void Start()
    {
        for (int i = 0; i < _startUnitsCount; i++)
        {
            SpawnUnit();
        }
    }

    public void SpawnUnit()
    {
        Unit unit = Instantiate(_unit, transform.position, Quaternion.identity);
        UnitSpawned?.Invoke(unit);
    }
}
