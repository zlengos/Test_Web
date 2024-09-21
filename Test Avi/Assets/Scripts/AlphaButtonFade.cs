using UnityEngine;

public class AlphaButtonFade : UIFader
{
    #region Fields

    [SerializeField] private SpriteLoader spriteLoader;

    #endregion

    public override void Fade()
    {
        base.Fade();
        
        spriteLoader.LoadAll();
    }
}