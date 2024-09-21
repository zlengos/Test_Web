using System;
using UnityEngine;

[CreateAssetMenu(order = 51, fileName = "SO_Sprites", menuName = "Avi/Addressable_SpriteNames")]
public class AddressableSpritesConfig : ScriptableObject 
{
    #region Fields

    [Header("All sprites")] 
    [SerializeField] private AddressableSprite[] allSprites;
    
    #region Properties
    public AddressableSprite[] AllSprites => allSprites;

    #endregion

    #endregion
}

[Serializable]
public class AddressableSprite
{
    #region Fields

    [Header("Sprite Properties")] 
    [SerializeField] private string name;
    
    #region Properties

    public string Name => name;

    #endregion

    #endregion
}