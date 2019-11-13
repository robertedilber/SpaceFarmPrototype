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
        public Transform PlanetTransform { get; private set; }

        public InstanceInfos(float angularStartPosition, float angularSize, GameObject prop, Transform planetTransform)
        {
            AngularStartPosition = angularStartPosition;
            AngularSize = angularSize;
            Prop = prop;
            PlanetTransform = planetTransform;
        }

        public Vector2 GetObjectPosition()
        {
            return PlanetTransform.position + Quaternion.Euler(0, 0, AngularStartPosition + (AngularSize / 2)) * new Vector2(0, PLANET_RADIUS);
        }

        public Quaternion GetObjectRotation()
        {
            Vector3 vectorToTarget = (Vector2)PlanetTransform.position - GetObjectPosition();
            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90;
            return Quaternion.Euler(0, 0, angle);
        }
    }

    public Planet.Type AvailableOnPlanetType;
    public GameObject Visual;
    public float angularSize;
}