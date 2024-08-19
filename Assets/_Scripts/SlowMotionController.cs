using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

public class SlowMotionController : MonoBehaviour
{
    public float normalTimeScale = 1f;
    public float slowTimeScale = 0.01f;
    public float transitionDuration = 3f; // Duration of the transition in seconds

    private float targetTimeScale;
    private float timeElapsed;

    //public event Action<float> OnTimeScaleChange;
    public UnityEvent m_OnTimeScaleChange;

    public PlayerCombatController _playerCombatController;

    

    void Start()
    {
        targetTimeScale = normalTimeScale;
        targetTimeScale = slowTimeScale;


        if (m_OnTimeScaleChange == null)
            m_OnTimeScaleChange = new UnityEvent();

        //m_OnTimeScaleChange.AddListener(_playerCombatController.TimescaleChange);


        //StartCoroutine(SwitchTimeScale());


    }

    public void StartTimescaleChange()
    {
        StartCoroutine(SwitchTimeScale());
    }

    IEnumerator SwitchTimeScale()
    {
        //while (true)
        //{
            float currentTimeScale = Time.timeScale;
            timeElapsed = 0f;

            while (timeElapsed < transitionDuration)
            {
                Time.timeScale = Mathf.Lerp(currentTimeScale, targetTimeScale, timeElapsed / transitionDuration);
                timeElapsed += Time.unscaledDeltaTime;
                yield return null;
            }

            Time.timeScale = targetTimeScale;
            //OnTimeScaleChange?.Invoke(Time.timeScale);
            m_OnTimeScaleChange.Invoke();
            //yield return new WaitForSecondsRealtime(6f - transitionDuration);

            //targetTimeScale = (targetTimeScale == normalTimeScale) ? slowTimeScale : normalTimeScale;
        //}
    }
}
