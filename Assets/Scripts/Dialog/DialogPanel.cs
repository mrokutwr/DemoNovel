using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;
using System.Collections.Generic;
using DG.Tweening;
using Zenject;
using UnityEngine.Localization.Settings;

public class DialogPanel : MonoBehaviour
{
    [Inject] private DialogManager dialogManager;
    [Inject] private LocalizationManager localizationManager;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private TMP_Text characterNameText;
    private string dialog;
    private string characterName;

    private void Start()
    {
        DialogFrame dialogFrame = new DialogFrame(characterName = "Autor",null, dialog = "startGame");
        ShowDialogFrame(dialogFrame);
    }
    private void OnEnable()
    {
       localizationManager.OnLanguageSwitcher += SetText;
       dialogManager.OnShowDialogFrame += ShowDialogFrame;
    }

    private void OnDisable()
    {
        localizationManager.OnLanguageSwitcher -= SetText;
        dialogManager.OnShowDialogFrame -= ShowDialogFrame;
    }
    
    public void ShowDialogFrame(DialogFrame dialogFrame)
    {

        characterName = dialogFrame.CharacterName;
        dialog = dialogFrame.Key;
        SetText();
    }

    public async void SetText()
    {
        if (string.IsNullOrEmpty(characterName) || string.IsNullOrEmpty(dialog))
        {
            return;
        }

        List<UniTask> fadeOutTasks = new List<UniTask>();

        if (!string.IsNullOrEmpty(dialogText.text) || !string.IsNullOrEmpty(characterNameText.text))
        {
            fadeOutTasks.Add(FadeText(dialogText, 0f));
            fadeOutTasks.Add(FadeText(characterNameText, 0f));
        }
        await UniTask.WhenAll(fadeOutTasks);

        characterNameText.text = localizationManager.LocalizedString(characterName);
        dialogText.text = localizationManager.LocalizedString(dialog);

        List<UniTask> fadeInTasks = new List<UniTask>
        {
            FadeText(dialogText, 1f),
            FadeText(characterNameText, 1f)
        };
        await UniTask.WhenAll(fadeInTasks);
    }

    private async UniTask FadeText(TMP_Text textComponent, float targetAlpha, float duration = 1f)
    {
        await textComponent.DOFade(targetAlpha, duration).AsyncWaitForCompletion();
    }
}