using UnityEngine;

public class CharacterPositions : MonoBehaviour
{
    [SerializeField] private RectTransform leftStartPosition;
    [SerializeField] private RectTransform rightStartPosition;
    [SerializeField] private RectTransform leftTargetPosition;
    [SerializeField] private RectTransform rightTargetPosition;
    public RectTransform LeftStartPosition => leftStartPosition;
    public RectTransform RightStartPosition => rightStartPosition;
    public RectTransform LeftTargetPosition => leftTargetPosition;
    public RectTransform RightTargetPosition => rightTargetPosition;

    public Vector2 GetStartPosition(string direction)
    {
        return direction == "L" ? leftStartPosition.anchoredPosition : rightStartPosition.anchoredPosition;
    }

    public Vector2 GetTargetPosition(string direction)
    {
        return direction == "L" ? leftTargetPosition.anchoredPosition : rightTargetPosition.anchoredPosition;
    }
}