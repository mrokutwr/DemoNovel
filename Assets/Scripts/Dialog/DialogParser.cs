using UnityEngine;
using System.Collections.Generic;

public class DialogParser
{
    public List<DialogFrame> Parse(TextAsset dialogFile)
    {
        var dialogFrames = new List<DialogFrame>();

        if (dialogFile == null)
        {
            Debug.LogWarning("Dialog file is null!");
            return dialogFrames;
        }

        string[] lines = dialogFile.text.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (string line in lines)
        {
            string[] parts = line.Split(':');
            if (parts.Length == 2)
            {
                string characterAndDirection = parts[0].Trim();
                string key = parts[1].Trim();

                string[] characterParts = characterAndDirection.Split('(');
                string characterName = characterParts[0].Trim();
                string directionStr = characterParts[1].Replace(")", "").Trim();

                dialogFrames.Add(new DialogFrame(characterName, directionStr, key));
            }
            else
            {
                Debug.LogWarning($"Invalid line format: {line}");
            }
        }

        return dialogFrames;
    }
}