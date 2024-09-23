using UnityEngine;

public class PlatformDetector : MonoBehaviour
{
    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalEval("DetectPlatform();");
#endif
    }
}