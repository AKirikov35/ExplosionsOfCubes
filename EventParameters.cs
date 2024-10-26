using UnityEngine;

public class EventParameters
{
	public Vector3 Position { get; private set; }
	public Vector3 Scale { get; private set; }
	public int Generation { get; private set; }

	public EventParameters(Vector3 position, Vector3 scale, int generation)
	{
		Position = position;
		Scale = scale;
		Generation = generation;
	}
}