using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ToggleSpriteSwap : Toggle
{
    [SerializeField] private Image targetImage;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;

    protected override void Start()
    {
        base.Start();
        
        UpdateSprite(isOn);

        onValueChanged.AddListener(UpdateSprite);
    }

    private void UpdateSprite(bool isOn)
    {
        if (targetImage != null)
        {
            targetImage.sprite = isOn ? onSprite : offSprite;
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        UpdateSprite(isOn);
    }

    public override void OnSubmit(BaseEventData eventData) {}

    protected override void OnDestroy()
    {
        base.OnDestroy();
        onValueChanged.RemoveListener(UpdateSprite);
    }
}

