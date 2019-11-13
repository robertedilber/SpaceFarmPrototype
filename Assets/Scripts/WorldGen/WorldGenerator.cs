using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    private struct PlanetElement
    {
        public float angularSize;

        public PlanetElement(float angularSize) => this.angularSize = angularSize;

        public static float operator +(PlanetElement e1, PlanetElement e2) => e1.angularSize + e2.angularSize;
    }


    private float _minSpaceBetweenElements = 22.5f;

    private PlanetElement[] _availablePlanetElements = new PlanetElement[] {
        new PlanetElement(20),
        new PlanetElement(30),
        new PlanetElement(180),
        new PlanetElement(60),
        new PlanetElement(10),
        new PlanetElement(50)
    };


    List<PlanetElement> d = new List<PlanetElement>();

    private void Awake()
    {


        float getCurrentSum()
        {
            float v = 0;

            for (int i = 0; i < d.Count; i++)
            {
                v += d[i].angularSize;
            }

            return v;
        }
    }

    private void Update()
    {
    }
}
