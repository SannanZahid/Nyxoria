using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <Class-Purpose>
//   Class to manage Downloading of assets using addressables
///

public class AddressableDownloader : MonoBehaviour
{
    public void LoadSpritesByGroupLabel(string addressableGroupLabel) 
    { 
        Addressables.LoadAssetsAsync<Sprite>(addressableGroupLabel, null).Completed += OnSpritesLoaded; 
    }

    private void OnSpritesLoaded(AsyncOperationHandle<IList<Sprite>> handle) 
    { 
        if (handle.Status == AsyncOperationStatus.Succeeded) 
        {
            List<Sprite> loadedSprites = new List<Sprite>();
            loadedSprites.AddRange(handle.Result);
            GameController.GameStartEvent.Invoke(loadedSprites);
        } 
        else 
        { 
            Debug.LogError("Failed to load sprites."); 
        } 
    }
}