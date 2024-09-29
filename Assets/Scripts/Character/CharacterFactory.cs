using UnityEngine;
using UnityEngine.UI;

public class CharacterFactory
{
    private RectTransform leftStartPosition;
    private RectTransform rightStartPosition;

    public CharacterFactory(RectTransform leftStart, RectTransform rightStart)
    {
        leftStartPosition = leftStart;
        rightStartPosition = rightStart;
    }

    public GameObject CreateCharacter(string characterName, Sprite characterSprite, string direction)
    {
        var characterObject = new GameObject(characterName);
        var characterImage = characterObject.AddComponent<Image>();
        characterImage.color = new Color(characterImage.color.r, characterImage.color.g, characterImage.color.b, 0f);
        characterImage.sprite = characterSprite;


        Vector2 newSize = new Vector2(leftStartPosition.sizeDelta.x, leftStartPosition.sizeDelta.y);
        characterImage.rectTransform.sizeDelta = newSize;
        characterObject.transform.SetParent(leftStartPosition.parent, false);
        characterObject.GetComponent<RectTransform>().anchoredPosition = direction == "L"
            ? leftStartPosition.localPosition
            : rightStartPosition.localPosition;

        return characterObject;
    }
}
