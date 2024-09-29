using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;

public class AnimationHandler
{
    private float moveDuration;

    public AnimationHandler(float moveDuration)
    {
        this.moveDuration = moveDuration;
    }

    public async UniTask MoveCharacter(GameObject characterObject, Vector2 targetPosition)
    {
        var rectTransform = characterObject.GetComponent<RectTransform>();
        var imageComponent = characterObject.GetComponent<Image>();
        float startAlpha = imageComponent.color.a;

        List<UniTask> fadeInTasks = new List<UniTask>
        {
            Move(rectTransform, targetPosition),
            Fade(imageComponent, startAlpha)
        };

        await UniTask.WhenAll(fadeInTasks);
    }

    private async UniTask Move(RectTransform rectTransform, Vector2 targetPosition)
    {
        await rectTransform.DOLocalMove(targetPosition, moveDuration).AsyncWaitForCompletion();
    }

    private async UniTask Fade(Image imageComponent, float startAlpha)
    {
        await imageComponent.DOFade(startAlpha == 0f ? 1f : 0f, moveDuration).SetEase(Ease.Linear).AsyncWaitForCompletion();
    }
}