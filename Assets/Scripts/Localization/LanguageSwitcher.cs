using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Toggle))]
public class LanguageSwitcher : MonoBehaviour
{
    [Inject] private LocalizationManager localizationManager;
    private Toggle toggle {get {return GetComponent<Toggle>();}}

    private void Start()
    {
        var activeLocale = LocalizationSettings.SelectedLocale;
        if (activeLocale != null)
        {
            toggle.isOn = activeLocale.Identifier.Code == "ru"? true : false;
            Debug.Log($"Active Locale: {activeLocale.LocaleName} ({activeLocale.Identifier.Code})");
        }
        else
        {
            Debug.LogWarning("Active Locale not setup");
        }
    }

    public void OnSwith(bool language)
    {
        var locale = LocalizationSettings.AvailableLocales.Locales
                       .Find(l => l.Identifier.Code == (language? "ru": "en"));

        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
            localizationManager.SwitchLocale();
            Debug.Log($"Active Locale: {locale.name}");
        }
        else
        {
            Debug.LogWarning("Locale not found: " + language);
        }
    }
}
