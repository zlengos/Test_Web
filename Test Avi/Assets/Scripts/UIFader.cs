using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIFader : MonoBehaviour
{
    #region Fields

    [SerializeField] private Button alphaButton;

    #region Runtime

    private Image _alphaButtonImage;

    #endregion

    #endregion

    protected virtual void Awake()
    {
        _alphaButtonImage = alphaButton.image;
    }

    public virtual void Fade()
    {
        DOTween.Kill(alphaButton);
        
#if UNITY_EDITOR
        Debug.Log("Alpha button clicked!");
#endif

        _alphaButtonImage.DOFade(0, 1f)
            .OnComplete(() =>
            {
                _alphaButtonImage.DOFade(1, 1f);
            });
    }
}