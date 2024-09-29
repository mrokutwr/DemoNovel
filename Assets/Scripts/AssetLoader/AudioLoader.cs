using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class AudioLoader : MonoBehaviour
{
    [Inject] private AssetLoader assetLoader;
    [SerializeField] private string audioName;
    private AudioSource audioSource { get { return GetComponent<AudioSource>(); } }

    private async void Start()
    {
        AudioClip loadedAudio = await assetLoader.LoadAsset<AudioClip>(audioName);
        
        if (loadedAudio != null)
        {
            audioSource.clip = loadedAudio;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log($"Audio loaded and assigned from key: {audioName}");
        }
        else
        {
            Debug.LogError($"Failed to load audio with key: {audioName}");
        }
    }
}
