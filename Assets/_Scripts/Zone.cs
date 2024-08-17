using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    private bool _isShrinking;
    public float _timeBeforeShrink;
    public float _shrinkDuration;
    public float _initialSize;
    public float _finalSize;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(_initialSize, 20f, _initialSize);
        Invoke("StartShrinking", _timeBeforeShrink);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartShrinking()
    {
        Vector3 newScale = new Vector3(_finalSize, 20f, _finalSize);
        StartCoroutine(LerpScale(newScale, _shrinkDuration));

    }

    private IEnumerator LerpScale(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        transform.localScale = targetScale; // Ensure the final scale is set

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name + " exited");
    }
}
