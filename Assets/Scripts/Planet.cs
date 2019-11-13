using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Planet : MonoBehaviour, IAstronomicalObject
{
    public float Mass { get => _mass; }
    [SerializeField] private float _mass;

    public Collider2D Collider { get; private set; }

    public string Name { get; set; }
    public void GenerateName() => Name = "Planet";

    public float TimeFlow { get => _timeFlow; }
    public float Temperature { get; private set; }

    public Atmosphere PlanetAtmosphere { get; private set; }
    public Star System { get; private set; }

    // Private serialized fields
    [SerializeField, Range(0, 1)] private float _timeFlow;

    // Private fields
    private List<Plant> _activePlantations = new List<Plant>();
    GameObject[] _atmosphereVisualLayers;
    Coroutine _atmosphereAnimationCoroutine;

    // Events
    public event EventHandler TickEvent;

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // Grow plants
        for (int i = 0; i < _activePlantations.Count; i++)
        {
            var plant = _activePlantations[i];
            if (plant.Active)
                plant.Grow(TimeFlow * Time.unscaledDeltaTime);
        }
    }

    public void Plant(Plant plant, Vector2 position)
    {
        if (IsSpaceAvailableAtPosition(plant.RequiredSpace, position))
        {
            _activePlantations.Add(plant);

            // Goes into plant subcategory
            plant.GrowTime = 3;

            plant.Create(position, this);
        }
    }

    public bool IsSpaceAvailableAtPosition(float requiredSpace, Vector2 position)
    {
        for (int i = 0; i < _activePlantations.Count; i++)
        {
            var cx = Mathf.Pow(position.x - _activePlantations[i].Position.x, 2);
            var cy = Mathf.Pow(position.y - _activePlantations[i].Position.y, 2);
            var dist = Mathf.Sqrt(cx + cy);

            if (dist <= requiredSpace + _activePlantations[i].RequiredSpace)
                return false;
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        foreach (Plant p in _activePlantations)
        {
            switch (p.EvolutionStage)
            {
                case 0:
                    Gizmos.color = Color.white;
                    break;
                case 1:
                    Gizmos.color = Color.green;
                    break;
                case 2:
                    Gizmos.color = Color.yellow;
                    break;
                case 3:
                    Gizmos.color = Color.red;
                    break;
            }
            Gizmos.DrawWireSphere(p.Position, p.RequiredSpace);
        }
    }

    public struct Atmosphere
    {
        // Composition
        public float Oxygen { get; set; }
        public float CarbonDioxide { get; set; }
        public float Neon { get; set; }
        public float Mercury { get; set; }

        // Density
        public float Density { get; set; }  // The more dense the less light there is (0 = no atmosphere, 1 = no light)
    }
}
