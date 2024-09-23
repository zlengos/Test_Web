using System;
using UnityEngine;

[CreateAssetMenu(order = 51, fileName = "SO_Atlases", menuName = "Avi/Addressable_AtlasNames")]
public class AddressableAtlasConfig : ScriptableObject
{
    #region Fields

    [Header("All atlases")] 
    [SerializeField] private AddressableAtlas[] allAtlases;

    #region Properties
    public AddressableAtlas[] AllAtlases => allAtlases;

    #endregion

    #endregion
}

[Serializable]
public class AddressableAtlas
{
    #region Fields

    [Header("Atlas Properties")] 
    [SerializeField] private string name;

    #region Properties

    public string Name => name;

    #endregion

    #endregion
}