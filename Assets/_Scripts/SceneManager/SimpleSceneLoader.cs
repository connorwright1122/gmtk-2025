using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneLoader : MonoBehaviour
{
    public static SimpleSceneLoader Instance;

    //private static bool _loadFromDoor;

    /*
    private GameObject _player;
    private Collider _playerCollider;
    private Collider _doorCollider;
    private Vector3 _playerSpawnPosition;
    */

    //private InteractableDoor.DoorToSpawnAt _doorToSpawnTo;
    [SerializeField] private SceneField _sceneToLoad;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        SimpleSceneFadeManager.Instance.StartFadeIn();
    }



    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /*
    public static void SwapSceneFromDoorUse(SceneField myScene, InteractableDoor.DoorToSpawnAt doorToSpawnAt)
    {
        _loadFromDoor = true;
        StartCoroutine(FadeOutThenChangeScene(myScene, doorToSpawnAt));
    }
    */

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        SwapSceneFromButtonUse(_sceneToLoad);
        Debug.Log("Swapping to scene");
    }

    public static void SwapSceneFromButtonUse(SceneField myScene)
    {
        //_loadFromDoor = true;
        Instance.StartCoroutine(Instance.FadeOutThenChangeScene(myScene));
    }

    private IEnumerator FadeOutThenChangeScene(SceneField myScene)
    {
        SimpleSceneFadeManager.Instance.StartFadeOut();
        Debug.Log("started fade");

        while (SimpleSceneFadeManager.Instance.IsFadingOut)
        {
            Debug.Log("fading");
            yield return null;
        }

        //_doorToSpawnTo = doorToSpawnAt;
        Debug.Log("Done fading");
        SceneManager.LoadScene(myScene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        SimpleSceneFadeManager.Instance.StartFadeIn();
        /*
        if (_loadFromDoor)
        {
            FindDoor(_doorToSpawnTo);
            _player.transform.position = _playerSpawnPosition;
            _loadFromDoor = false;
        }
        */
    }

    /*
    private void FindDoor(InteractableDoor.DoorToSpawnAt doorSpawnNumber)
    {
        InteractableDoor[] doors = FindObjectsOfType<InteractableDoor>();

        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i]._currentDoorPosition == doorSpawnNumber)
            {
                _doorCollider = doors[i].gameObject.GetComponent<Collider>();
                CalculateSpawnPosition();
                return;
            }
        }
    }

    private void CalculateSpawnPosition()
    {
        float colliderHeight = _playerCollider.bounds.extents.y;
        _playerSpawnPosition = _doorCollider.transform.position - new Vector3(0f, colliderHeight, 0f);
    }
    */
}
