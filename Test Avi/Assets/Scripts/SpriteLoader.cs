using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

public class SpriteLoader : MonoBehaviour
{
    #region Fields

    [SerializeField] private SpriteRenderer[] spriteRenderers;

    private const string DEFAULT_REMOTE_SPRITE_PATH = "https://zlengos.github.io/Test_Web_build4/Addressables/WebGL";
    private const string DEFAULT_LOCAL_SPRITE_PATH = "Assets/AddressableResources/Sprites";

    [SerializeField] private AddressableAtlasConfig atlasNamesConfig;

    #region Runtime

    private AsyncOperationHandle<SpriteAtlas> _handle;
    private string _atlasPath;

    #endregion

    #endregion

    private void Awake()
    {
        InitializeHandle();
        ConcatAtlasPath();
    }

    private void InitializeHandle()
    {
        if (atlasNamesConfig.AllAtlases.Length > 0)
        {
            _handle = new AsyncOperationHandle<SpriteAtlas>();
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogError($"{nameof(atlasNamesConfig)} is not initialized!");
        }
#endif
    }

    private void ConcatAtlasPath()
    {
#if UNITY_EDITOR
        _atlasPath = $"{DEFAULT_LOCAL_SPRITE_PATH}/{atlasNamesConfig.AllAtlases[0].Name}";
#else
        _atlasPath = $"{DEFAULT_REMOTE_SPRITE_PATH}/{atlasNamesConfig.AllAtlases[0].Name.Replace(" ", "%20")}";
#endif
    }

    public void LoadAll()
    {
        StartCoroutine(LoadAtlasCoroutine());
    }

    private IEnumerator LoadAtlasCoroutine()
    {
        _handle = Addressables.LoadAssetAsync<SpriteAtlas>(_atlasPath);
        yield return _handle;

        if (_handle.Status == AsyncOperationStatus.Succeeded)
        {
            SetupSpritesFromAtlas(_handle.Result);
        }
        else
        {
            Debug.LogError($"Can't load atlas. Path: {_atlasPath}");
        }
    }

    private void SetupSpritesFromAtlas(SpriteAtlas atlas)
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            Sprite[] sprites = new Sprite[atlas.spriteCount];
            atlas.GetSprites(sprites);

            if (i < sprites.Length)
            {
                spriteRenderers[i].sprite = sprites[i];
            }
            else
            {
                Debug.LogWarning($"Not enough sprites in atlas to fill renderer at index {i}.");
            }
        }
    }

    public void UnloadAtlas()
    {
        if (_handle.IsValid())
        {
            Addressables.Release(_handle);
            _handle = default;
        }
        else
        {
            Debug.LogWarning("Atlas is not loaded.");
        }
    }

    private void OnDestroy()
    {
        UnloadAtlas();
    }
}
