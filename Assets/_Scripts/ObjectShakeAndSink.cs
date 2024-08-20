using System.Collections;
using UnityEngine;

public class ObjectShakeAndSink : MonoBehaviour
{
    public float shakeDuration = 1f; // Duration of the shaking effect
    public float shakeMagnitude = 0.1f; // Magnitude of the shake
    public float sinkDuration = 1f; // Duration of the sinking effect

    private Vector3 originalPosition;
    private BoxCollider boxCollider;
    private AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        originalPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    public void StartShakeAndSink()
    {
        StartCoroutine(ShakeAndSink());
    }

    private IEnumerator ShakeAndSink()
    {
        float elapsedTime = 0f;

        /*
        // Step 1: Shake the object
        while (elapsedTime < shakeDuration)
        {
            float x = originalPosition.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = originalPosition.y + Random.Range(-shakeMagnitude, shakeMagnitude);
            float z = originalPosition.z + Random.Range(-shakeMagnitude, shakeMagnitude);

            transform.position = new Vector3(x, y, z);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the object is back at its original position after shaking
        transform.position = originalPosition;
        */

        // Step 2: Sink the object beneath the ground based on its BoxCollider size
        float sinkHeight = boxCollider.size.y * transform.localScale.y; // Total height of the object
        Vector3 targetPosition = originalPosition - new Vector3(0, sinkHeight, 0);
        boxCollider.enabled = false;

        audioSource.clip = audioClip;
        audioSource.Play();
        //SoundFXManager.Instance.PlaySoundFXClip(_audioClips[0], this.transform, 1);

        //elapsedTime = 0f;

        while (elapsedTime < sinkDuration)
        {

            float x = originalPosition.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float z = originalPosition.z + Random.Range(-shakeMagnitude, shakeMagnitude);
            targetPosition.x = x;
            targetPosition.z = z;

            //transform.position = new Vector3(x, y, z);

            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / sinkDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the object is at the target position after sinking
        transform.position = targetPosition;
        gameObject.SetActive(false);
    }
}
