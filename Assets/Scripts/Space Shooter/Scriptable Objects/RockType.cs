using UnityEngine;

[CreateAssetMenu(fileName = "RockData", menuName = "Scriptable Objects/RockData")]
public class RockData : ScriptableObject
{
    public enum Type
    {
        Asteroid,
        Regen,
        Power,
    }

    public Type type;

    [Range(0f, 1f)]
    public float spawnChance = 0.5f;

    public Vector2 initialForce = new Vector2(1f, 5f);
}
