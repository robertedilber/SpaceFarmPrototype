using UnityEngine;

public class Plant
{
    // Constants
    public const string PLANT_RESOURCE_PATH = "Art/";

    public float RequiredSpace { get; } = 0.15f;
    public Vector2 Position { get; set; }
    public Planet Home { get; set; }
    public string Name { get; private set; }
    public bool Active { get; private set; }

    private float growTime;
    public float GrowTime
    {
        get => growTime;
        set => growTime = value + ((value / 10) * Random.Range(-1, 1));
    }

    private int _evolutionStage;
    public int EvolutionStage
    {
        get => _evolutionStage;
        private set
        {
            // Only execute if value is changed
            if (_evolutionStage != value)
            {
                Evolve(value);
                Active = value < 4;
                _evolutionStage = value;
            }
        }
    }

    private Sprite[] _sprites;
    private SpriteRenderer _instance;

    private float currentGrowth;

    public Plant(string inGameName) : this(inGameName, inGameName) { }
    public Plant(string inGameName, string spriteResourceName)
    {
        _sprites = new Sprite[4];
        for (int i = 0; i < 4; i++)
            _sprites[i] = Resources.Load<Sprite>($"{PLANT_RESOURCE_PATH + spriteResourceName}_{i}");
        Name = inGameName;
    }

    public void Create(Vector2 position, Planet home)
    {
        GameObject g = new GameObject(Name, typeof(SpriteRenderer));
        g.transform.position = position;

        Vector3 vectorToTarget = home.transform.position - g.transform.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90;
        g.transform.rotation = Quaternion.Euler(0, 0, angle);

        _instance = g.GetComponent<SpriteRenderer>();
        _instance.sprite = _sprites[0]; // Set default sprite

        Position = position;
        Home = home;

        Active = true;
    }

    public void Destroy()
    {
        // Destroy (insert atmosphere mods)
        Object.Destroy(_instance.gameObject);
    }

    public void Grow(float value)
    {
        float stageTime = GrowTime / 3;
        value += currentGrowth;

        if (value >= stageTime * 3)
            EvolutionStage = 3;
        else if (value > stageTime * 2)
            EvolutionStage = 2;
        else if (value > stageTime)
            EvolutionStage = 1;
        else
            EvolutionStage = 0;

        value = Mathf.Clamp(value, 0, GrowTime);
        currentGrowth = value;
    }

    protected virtual void Evolve(int stage) => _instance.sprite = _sprites[stage];
}