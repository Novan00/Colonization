using UnityEngine;

[RequireComponent(typeof(UnitMover), typeof(UnitCollector))]

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitMover _mover;
    [SerializeField] private UnitCollector _collector;
    [SerializeField] private UnitBaseBuilder _unitBaseBuilder;

    private Base _base;

    public bool IsBusy { get; private set; }

    private void OnEnable()
    {
        _collector.ResourceCollected += OnResourceCollected;
        _collector.ResourceSent += OnResourceSent;
        _unitBaseBuilder.BaseBuilt += OnBaseBuilt;
    }

    private void OnDisable()
    {
        _collector.ResourceCollected -= OnResourceCollected;
        _collector.ResourceSent -= OnResourceSent;
        _unitBaseBuilder.BaseBuilt -= OnBaseBuilt;
    }

    public void Init(Base currentBase)
    {
        _base = currentBase;
    }

    private void MoveToBase()
    {
        _mover.SetTarget(_base.transform);
    }

    public void Mine(ResourceCell resourceCell)
    {
        IsBusy = true;
        _mover.SetTarget(resourceCell.transform);
        _collector.SetResourceCell(resourceCell);
    }

    public void BuildBase(BaseFlag flagPosition)
    {
        IsBusy = true;
        _mover.SetTarget(flagPosition.transform);
        _unitBaseBuilder.SetFlagPosition(flagPosition);
    }

    private void OnBaseBuilt(Base newBase)
    {
        _base = newBase;
        newBase.AddUnit(this);
        IsBusy = false;
    }

    private void OnResourceSent()
    {
        IsBusy = false;
    }

    private void OnResourceCollected(Resource resource) => MoveToBase();
}
