
public class DialogFrame
{
    public string CharacterName { get; }
    public string Direction { get; }
    public string Key { get; }

    public DialogFrame(string characterName, string direction, string key)
    {
        CharacterName = characterName;
        Direction = direction;
        Key = key;
    }
}