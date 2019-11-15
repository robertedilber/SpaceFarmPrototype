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

        // Get a random planet shape
        GameObject[] availableCores = Resources.LoadAll<GameObject>("Art/Planet Elements/Core");
        GameObject planetCore = availableCores[Random.Range(0, availableCores.Length)];
        GameObject planetInstance = Object.Instantiate(planetCore, planetTransform.position, Quaternion.identity, planetTransform);

        planetInstance.GetComponent<MeshRenderer>().material = GetMaterialForType(type);

        while (totalSize <= 360.0f)
        {
            // Get a random planet element from the available list
            PlanetElement e = _availablePlanetElements[Random.Range(0, _availablePlanetElements.Count)];

            if (totalSize + e.angularSize > 360.0f)
                return;

            PlanetElement.InstanceInfos objectInstance = new PlanetElement.InstanceInfos(totalSize, e.angularSize, e.Visual, e.materialType, e.color1, e.color2);

            // Instantiate planetObject
            GameObject instance = Object.Instantiate(objectInstance.Prop, planetTransform.position, objectInstance.GetOrientation(), planetInstance.transform);

            //string materialType = e.materialType == PlanetElement.MaterialType.Musgrave ? "Mat_Musgrave" : "Mat_Wave";
            instance.GetComponent<MeshRenderer>().material = e.mat;

            _elements.Add(objectInstance);
            totalSize += e.angularSize;
        }
    }

    private static List<PlanetElement> GetElementsForType(Planet.Type type)
    {
        PlanetElement[] e = Resources.LoadAll<PlanetElement>("Art/Planet Elements/Forest Planet");
        List<PlanetElement> finalElementList = new List<PlanetElement>();
        foreach (PlanetElement p in e.Where((a) => a.AvailableOnPlanetType.HasFlag(type)))
            finalElementList.Add(p);

        return finalElementList;
    }

    private static Material GetMaterialForType(Planet.Type type)
    {
        // hack for now
        return Resources.Load<Material>("Materials/Core/Mat_Musgrave");
    }
}