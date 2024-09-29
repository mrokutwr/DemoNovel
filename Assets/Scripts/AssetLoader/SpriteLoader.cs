using UnityEngine;
using Zenject;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteLoader : MonoBehaviour
{
    [Inject] private AssetLoader assetLoader;
    [SerializeField] private string spriteName;
    private SpriteRenderer spriteRenderer {get {return GetComponent<SpriteRenderer>();}}

    private async void Start()
    {        
        Sprite loadedSprite = await assetLoader.LoadAsset<Sprite>(spriteName);
        
        if (loadedSprite != null)
        {
            spriteRenderer.sprite = loadedSprite;
            Debug.Log($"Sprite loaded and assigned from key: {spriteName}");
        }
        else
        {
            Debug.LogError($"Failed to load sprite with key: {spriteName}");
        }

    }
}