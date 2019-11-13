using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class WorldGenerator
{
    private static List<PlanetElement.InstanceInfos> _elements = new List<PlanetElement.InstanceInfos>();

    public static void GeneratePlanet(Planet.Type type, Transform planetTransform)
    {
        List<PlanetElement> _availablePlanetElements = GetElementsForType(type);

        float totalSize = 0;

        while (totalSize <= 360.0f)
        {
            // Get a random planet element from the available list
            PlanetElement e = _availablePlanetElements[Random.Range(0, _availablePlanetElements.Count)];

            if (totalSize + e.angularSize > 360.0f)
                return;

            PlanetElement.InstanceInfos instance = new PlanetElement.InstanceInfos(totalSize, e.angularSize, e.Visual, planetTransform);

            // Instantiate planetObject
            Object.Instantiate(instance.Prop, instance.GetObjectPosition(), instance.GetObjectRotation());

            _elements.Add(instance);
            totalSize += e.angularSize;
        }
    }

    private static List<PlanetElement> GetElementsForType(Planet.Type type)
    {
        PlanetElement[] e = Resources.LoadAll<PlanetElement>("PlanetElements");
        List<PlanetElement> finalElementList = new List<PlanetElement>();
        foreach (PlanetElement p in e.Where((a) => a.AvailableOnPlanetType.HasFlag(type)))
            finalElementList.Add(p);

        return finalElementList;
    }
}