using System;
using System.Collections;
using UnityEngine;

public abstract class PlantationBox
{
    public Planet Home { get; private set; }
    public GameObject Model { get; private set; }
    public float RequiredSpace { get; private set; }
    public bool ReadyToHarvest { get; private set; }
    public bool NeedsWater { get; private set; }
    public PlantationData ActivePlantation { get; private set; }
    public Vector2 Position { get; private set; }

    private int _currentEvolutionStage;
    public int CurrentEvolutionStage
    {
        get => _currentEvolutionStage;
        private set
        {
            value = Mathf.Clamp(value, 0, 4);
            ReadyToHarvest = value == 4;
            _currentEvolutionStage = value;
        }
    }

    private GameObject _instance;
    private GameObject _plantation;
    private Coroutine _growCoroutine;

    public PlantationBox(Planet home, GameObject model, float requiredSpace)
    {
        Home = home;
        Model = model;
        RequiredSpace = requiredSpace;

        // Set default values
        NeedsWater = false;
        CurrentEvolutionStage = 0;
        ReadyToHarvest = false;
    }

    #region Display Related Methods

    public void Create(Vector3 relativePosition, Quaternion relativeRotation)
    {
        _instance = UnityEngine.Object.Instantiate(Model, relativePosition, relativeRotation);
        _instance.transform.SetParent(Home.transform);
        Position = relativePosition;
    }

    public void Destroy()
    {
        UnityEngine.Object.Destroy(_instance);
    }

    #endregion

    public virtual void PlantSeed(PlantationData plantationData)
    {
        if (ActivePlantation != null)
            return;

        // Instantiate the seed model
        ActivePlantation = plantationData;

        _plantation = UnityEngine.Object.Instantiate(plantationData.EvolutionStageModels[0], _instance.transform);

        // Needs water right after planting a new seed
        NeedsWater = true;
    }

    public virtual void Water()
    {
        if (ReadyToHarvest)
            return;

        NeedsWater = false;
        StartGrowing();
    }

    public virtual void Harvest()
    {

    }

    protected virtual void OnEvolved()
    {
        NeedsWater = true;
        CurrentEvolutionStage++;
        UnityEngine.Object.Destroy(_plantation);
        _plantation = UnityEngine.Object.Instantiate(ActivePlantation.EvolutionStageModels[CurrentEvolutionStage], _instance.transform);
    }

    public void StartGrowing() => CoroutineTemplates.ExecuteAfter(ref _growCoroutine, Home, OnEvolved, ActivePlantation.EvolutionStageTimer);
}