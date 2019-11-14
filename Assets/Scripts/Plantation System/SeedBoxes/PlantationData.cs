using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlantationData
{
    public float EvolutionStageTimer { get; private set; }
    public GameObject[] EvolutionStageModels { get; private set; }

    public PlantationData(float evolutionStageTimer, GameObject[] evolutionStageModels)
    {
        if (evolutionStageModels.Length != 4)
            throw new Exception("Size should be 4");

        EvolutionStageTimer = evolutionStageTimer;
        EvolutionStageModels = evolutionStageModels;
    }
}