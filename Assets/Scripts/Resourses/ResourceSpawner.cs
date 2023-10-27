using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource _resourcePrefab;
    [SerializeField] private List<ResourceCell> _cells = new List<ResourceCell>();
    [SerializeField] private float _delay;

    private Random _random = new Random();

    private float _timer = 0f;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _delay)
        {
            Spawn();
            _timer = 0f;
        }
    }

    private void Spawn()
    {
        var spawnCell = GetRandomSpawnCell();

        if (spawnCell == null )
        {
            return;
        }

        Resource newResourse = Instantiate(_resourcePrefab, spawnCell.transform.position, Quaternion.identity);
        spawnCell.SetResource(newResourse);
    }

    private ResourceCell GetRandomSpawnCell()
    {
        List<ResourceCell> cells = new List<ResourceCell>();

        foreach (var cell in _cells)
        {
            if (cell.IsEmpty)
            {
                cells.Add(cell);
            }
        }

        if (cells.Count == 0)
        {
            return null;
        }

        int ramdomIndex = _random.Next(cells.Count);

        return cells[ramdomIndex];
    }
}
    