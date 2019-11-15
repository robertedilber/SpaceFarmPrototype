using UnityEngine;

[CreateAssetMenu(fileName = "New Planet Element", menuName = "Planet Element")]
public class PlanetElement : ScriptableObject
{
    public struct InstanceInfos
    {
        public const float PLANET_RADIUS = 2.0f;

        public float AngularStartPosition { get; private set; }
        public float AngularSize { get; private set; }
        public GameObject Prop { get; private set; }
        public MaterialType MaterialType { get; private set; }
        public Color Color1 { get; private set; }
        public Color Color2 { get; private set; }

        public InstanceInfos(float angularStartPosition, float angularSize, GameObject prop, MaterialType materialType, Color c1, Color c2)
        {
            AngularStartPosition = angularStartPosition;
            AngularSize = angularSize;
            Prop = prop;
            MaterialType = materialType;
            Color1 = c1;
            Color2 = c2;
        }

        public Quaternion GetOrientation() => Quaternion.Euler(0, 0, AngularStartPosition + (AngularSize / 2));
    }

    public enum MaterialType
    {
        Wave,
        Musgrave
    }

    public MaterialType materialType;
    public Material mat;
    public Color color1, color2;
    public Planet.Type AvailableOnPlanetType;
    public GameObject Visual;
    public float angularSize;
}