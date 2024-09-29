using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class AssetLoader
{
    List<string> LoadAssetKeys = new List<string>();

    public async UniTask<T> LoadAsset<T>(string assetKey) where T : UnityEngine.Object
    {
        var handle = Addressables.LoadAssetAsync<T>(assetKey);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            LoadAssetKeys.Add(assetKey);
            return handle.Result;
        }
        else
        {
            Debug.LogError($"Failed to load asset with key: {assetKey}");
            return null;
        }
    }

    public async UniTask<bool> UpdateAssetBundles(string catalogKey)
    {
        AsyncOperationHandle downloadHandle = Addressables.DownloadDependenciesAsync(catalogKey);

        await downloadHandle.Task;

        if (downloadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("AssetBundles updated successfully.");
            return true;
        }
        else
        {
            Debug.LogError("Failed to update AssetBundles.");
            return false;
        }
    }
    
    public async UniTask ClearAssetCache(string assetKey)
    {
        AsyncOperationHandle<long> sizeHandle = Addressables.GetDownloadSizeAsync(assetKey);
        await sizeHandle.Task;

        if (sizeHandle.Status == AsyncOperationStatus.Succeeded && sizeHandle.Result > 0)
        {

            Addressables.Release(sizeHandle);
            Debug.Log($"Cache cleared for asset with key: {assetKey}");
        }
        else
        {
            Debug.Log($"No cache to clear for asset with key: {assetKey}");
        }
    }

    public async UniTask ClearAllAssetCache()
    {
        foreach (var assetKey in LoadAssetKeys)
        {
            await ClearAssetCache(assetKey);
        }
    }
}