public struct Star : IAstronomicalObject
{
    public enum HeatLevel { Fiery, Hot, Temperate, Cold, Frozen }
    public HeatLevel Heat { get; set; }
    public float Brightness { get; set; }
    public float Radiactivity { get; set; }

    public string Name { get; set; }

    public void GenerateName() => Name = "default";
}

interface IAstronomicalObject
{
    string Name { get; set; }
    void GenerateName();
}