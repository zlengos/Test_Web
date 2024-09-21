using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpriteLoader : MonoBehaviour
{
    #region Fields

    [SerializeField] private SpriteRenderer[] spriteRenderers;

    private const string DEFAULT_SPRITE_PATH = "Assets/AddressableResources/Sprites";

    [SerializeField] private AddressableSpritesConfig spriteNamesConfig;

    #region Runtime

    private AsyncOperationHandle<Sprite>[] _handles;

    private string[] _spritePathes;

    #endregion

    #endregion

    private void Awake()
    {
        InitializeHandles();
        ConcatSpritePaths();
    }

    private void InitializeHandles()
    {
        if (spriteNamesConfig.AllSprites.Length > 0)
        {
            _handles = new AsyncOperationHandle<Sprite>[spriteNamesConfig.AllSprites.Length];
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogError($"{nameof(spriteNamesConfig)} is not initialized!");
        }
#endif
    }

    private void ConcatSpritePaths()
    {
        _spritePathes = new string[spriteNamesConfig.AllSprites.Length];
        
        for (int i = 0; i < spriteNamesConfig.AllSprites.Length; i++)
        {
            
            _spritePathes[i] = $"{DEFAULT_SPRITE_PATH}/{spriteNamesConfig.AllSprites[i].Name}";
        }
    }

    public void LoadAll()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            LoadByIndex(i);
        }
    }

    public void LoadByIndex(int index)
    {
        if (index < 0 || index >= spriteRenderers.Length)
        {
            Debug.LogError($"Index {index} out of {nameof(spriteRenderers)}.");
            return;
        }

        if (_handles[index].IsValid())
        {
            Debug.LogWarning($"Sprite index {index} is already loaded.");
            return;
        }

        StartCoroutine(LoadSpriteCoroutine(spriteRenderers[index], index));
    }

    private IEnumerator LoadSpriteCoroutine(SpriteRenderer renderer, int index)
    {
        _handles[index] = Addressables.LoadAssetAsync<Sprite>(_spritePathes[index]);
        yield return _handles[index];

        if (_handles[index].Status == AsyncOperationStatus.Succeeded)
        {
            renderer.sprite = _handles[index].Result;
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogError($"Can't load file. path: {_spritePathes[index]}");
        }
#endif
    }

    public void UnloadSprite(int index)
    {
        if (index < 0 || index >= _handles.Length)
        {
            Debug.LogError($"Index {index} out of {nameof(_handles)}.");
            return;
        }

        if (_handles[index].IsValid())
        {
            Addressables.Release(_handles[index]);
            _handles[index] = default;
        }
        else
        {
            Debug.LogWarning($"There is no loaded sprite by index {index}.");
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _handles.Length; i++)
        {
            if (_handles[i].IsValid())
            {
                Addressables.Release(_handles[i]);
                _handles[i] = default;
            }
        }
    }
}
