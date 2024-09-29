using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationManager: MonoBehaviour
{
    [SerializeField]private string tableName = "Story";
    public delegate void LocalizationEventHandler();
    public event LocalizationEventHandler OnLanguageSwitcher;

    private async void Start()
    {
        await LocalizationSettings.InitializationOperation.Task;
    }

    public void SwitchLocale()
    {
        OnLanguageSwitcher?.Invoke();
    }


    public string LocalizedString(string entryKey)
    {
        return LocalizationSettings.StringDatabase.GetLocalizedString(tableName, entryKey);
    }
}