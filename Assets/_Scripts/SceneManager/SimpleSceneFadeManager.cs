using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleSceneFadeManager : MonoBehaviour
{
    public static SimpleSceneFadeManager Instance;

    [SerializeField] private Image _fadeOutImage;
    [Range(0.1f, 10f), SerializeField] private float _fadeOutSpeed = 5f;
    [Range(0.1f, 10f), SerializeField] private float _fadeInSpeed = 5f;

    [SerializeField] private Color _fadeOutStartColor;
    [SerializeField] private Color _fadeOutInstantColor;

    public bool IsFadingOut { get; set; }
    public bool IsFadingIn { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _fadeOutStartColor.a = 0f;
        _fadeOutInstantColor.a = 1f;
    }



    // Update is called once per frame
    void Update()
    {
        if (IsFadingOut)
        {
            if (_fadeOutImage.color.a < 1f)
            {
                _fadeOutStartColor.a += Time.deltaTime * _fadeOutSpeed;
                _fadeOutImage.color = _fadeOutStartColor;
            }
            else
            {
                IsFadingOut = false;
            }
        }

        if (IsFadingIn)
        {
            if (_fadeOutImage.color.a > 0f)
            {
                _fadeOutStartColor.a -= Time.deltaTime * _fadeInSpeed;
                _fadeOutImage.color = _fadeOutStartColor;
            }
            else
            {
                IsFadingIn = false;
            }
        }
    }

    public void StartFadeOut()
    {
        _fadeOutImage.color = _fadeOutStartColor;
        IsFadingOut = true;
    }

    public void InstantFadeOut()
    {
        _fadeOutImage.color = _fadeOutInstantColor;
        IsFadingIn = true;
        //_fadeOutImage.color.a = 1f;
    }

    public void StartFadeIn()
    {
        if (_fadeOutImage.color.a >= 1f)
        {
            _fadeOutImage.color = _fadeOutStartColor;
            IsFadingIn = true;
        }
    }
}
