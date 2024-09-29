using UnityEngine;

public class CharacterData
{
    public GameObject CharacterObject { get; private set; }
    public string Direction { get; private set; }

    public CharacterData(GameObject characterObject, string direction)
    {
        CharacterObject = characterObject;
        Direction = direction;
    }
}