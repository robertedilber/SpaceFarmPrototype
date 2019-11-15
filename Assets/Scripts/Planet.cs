using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Planet : MonoBehaviour, IAstronomicalObject
{
    [Flags]
    public enum Type
    {
        None = 0,
        A = 1,
        B = 2,
        C = 4,
        D = 8,
        E = 16,
        F = 32
    }

    public CircleCollider2D Collider { get; private set; }

    public string Name { get; set; }
    public void GenerateName() => Name = "Planet";

    public float TimeFlow { get => _timeFlow; }

    // Private serialized fields
    [SerializeField, Range(0, 1)] private float _timeFlow;

    // Private fields
    private List<PlantationBox> _activeSeedBoxes = new List<PlantationBox>();

    private void Awake()
    {
        Collider = GetComponent<CircleCollider2D>();
        WorldGenerator.GeneratePlanet(Type.A, transform);
    }

    public void PlaceSeedBox(PlantationBox seedBox, Vector2 position)
    {
        if (IsSpaceAvailableAtPosition(seedBox.RequiredSpace, position))
        {
            _activeSeedBoxes.Add(seedBox);
            Vector3 vectorToTarget = (Vector2)transform.position - position;
            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90;
            seedBox.Create(position, Quaternion.Euler(0, 0, angle));
        }
    }

    public bool IsSpaceAvailableAtPosition(float requiredSpace, Vector2 position)
    {
        for (int i = 0; i < _activeSeedBoxes.Count; i++)
        {
            var cx = Mathf.Pow(position.x - _activeSeedBoxes[i].Position.x, 2);
            var cy = Mathf.Pow(position.y - _activeSeedBoxes[i].Position.y, 2);
            var dist = Mathf.Sqrt(cx + cy);

            if (dist <= requiredSpace + _activeSeedBoxes[i].RequiredSpace)
                return false;
        }

        return true;
    }

    public PlantationBox GetPlantationBoxAtPosition(Vector2 position)
    {
        if (_activeSeedBoxes.Count == 0)
            return null;

        PlantationBox closestPlantationBox = _activeSeedBoxes[0];
        foreach (var box in _activeSeedBoxes)
        {
            float closestDistance = Vector2.Distance(closestPlantationBox.Position, position);
            float currentDistance = Vector2.Distance(box.Position, position);
            if (currentDistance <= closestDistance)
                closestPlantationBox = box;
        }

        return closestPlantationBox;
    }
}
