using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private BaseFlag _baseFlag;
    [SerializeField] private int _maxUnitCount = 7;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private ResourceScaner _scaner;
    [SerializeField] private BaseMoneySystem _moneySystem;
    [SerializeField] private BaseResourceHandler _baseResourceHandler;

    private List<Unit> _units = new List<Unit>();
    private float _time = 0f;
    private float _delay = 0.5f;
    private bool _isTouch = false;
    private bool _canBuildBase = false;

    private void OnEnable()
    {
        _unitSpawner.UnitSpawned += OnUnitSpawned;
        _scaner.ResourceCellFound += OnResourceCellFound;
        _baseResourceHandler.ResourceSent += OnResourceSent;
        _moneySystem.UnitAccumulated += OnUnitAccumulated;
        _moneySystem.BaseAccumulated += OnBaseAccumulated;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time > _delay)
        {
            _scaner.Scan();
            _time = 0;
        }
    }

    private void OnDisable()
    {
        _unitSpawner.UnitSpawned -= OnUnitSpawned;
        _scaner.ResourceCellFound -= OnResourceCellFound;
        _baseResourceHandler.ResourceSent -= OnResourceSent;
        _moneySystem.UnitAccumulated -= OnUnitAccumulated;
        _moneySystem.BaseAccumulated -= OnBaseAccumulated;
    }

    public void MoveFlag(Vector3 newPosition)
    {
        if (!_isTouch)
        {
            return;
        }

        _baseFlag.MovePosition(newPosition);
    }

    public void SetIsTouch()
    {
        _isTouch = !_isTouch;
    }

    public void AddUnit(Unit unit)
    {
        _units.Add(unit);
    }

    private void OnResourceSent()
    {
        _moneySystem.AddResource();
        
        if (!_baseFlag.gameObject.activeSelf)
        {
            _moneySystem.BuyBot();
        }
        else
        {
            _moneySystem.BuyBase();
        }
    }

    private void OnResourceCellFound(ResourceCell cell)
    {
        foreach (var unit in _units)
        {
            if (!unit.IsBusy)
            {
                if (_canBuildBase && _baseFlag.gameObject.activeSelf && !_baseFlag.IsReserve)
                {
                    _baseFlag.Reserve();
                    unit.BuildBase(_baseFlag);
                    _units.Remove(unit);
                    return;
                }

                cell.Reserve();
                unit.Mine(cell);
                return;
            }
        }
    }

    private void OnBaseAccumulated()
    {
        _canBuildBase = true;
    }   

    private void OnUnitAccumulated()
    {
        if (_units.Count < _maxUnitCount)
        {
            _unitSpawner.SpawnUnit();
        }
    }

    private void OnUnitSpawned(Unit unit)
    {
        unit.Init(this);
        AddUnit(unit);
    }
}
