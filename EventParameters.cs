using UnityEngine;

public class EventParameters
{
    public Vector3 Position { get; private set; }
    public Vector3 Scale { get; private set; }
    public float SplitChance { get; private set; }
    public ExplosionCube Cube { get; private set; }

    public EventParameters(Vector3 position, Vector3 scale, float splitChance, ExplosionCube cube)
    {
        Position = position;
        Scale = scale;
        SplitChance = splitChance;
        Cube = cube;
    }
}
