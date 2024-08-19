using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameController1 : MonoBehaviour
{
    public static GameController1 Instance { get; private set; }

    public SlowMotionController slowMotionController;
    public TMP_Text _winText;

    public CinemachineVirtualCamera _playerCloseUpCam;
    public CinemachineVirtualCamera _EnemyCloseUpCam;

    private void Start()
    {
        slowMotionController = GetComponent<SlowMotionController>();
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }


    }

    public void EndGame(string winner)
    {
        if (string.Compare(winner, "Player") == 0)
        {
            _winText.enabled = true;
            _winText.text = "Moneke wins!";
            _EnemyCloseUpCam.Priority = 20;
            slowMotionController.StartTimescaleChange();
        } else if (string.Compare(winner, "Enemy") == 0)
        {
            _winText.enabled = true;
            _winText.text = "Gocera wins!";
            _playerCloseUpCam.Priority = 20;
            slowMotionController.StartTimescaleChange();
        }
    }
}
