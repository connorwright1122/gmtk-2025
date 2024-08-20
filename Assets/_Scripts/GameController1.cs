using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController1 : MonoBehaviour
{
    public static GameController1 Instance { get; private set; }

    public SlowMotionController slowMotionController;
    public TMP_Text _winText;
    public GameObject _menuButton;

    public CinemachineVirtualCamera _playerCloseUpCam;
    public CinemachineVirtualCamera _EnemyCloseUpCam;

    private bool gameOver;

    private GameObject player;
    private GameObject enemy;

    private void Start()
    {
        slowMotionController = GetComponent<SlowMotionController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
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
        if (!gameOver) { 
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            if (string.Compare(winner, "Player") == 0)
            {
                _winText.enabled = true;
                _winText.text = "Moneke wins!";
                _EnemyCloseUpCam.Priority = 20;
                slowMotionController.StartTimescaleChange();
                gameOver = true;
                _menuButton.SetActive(true);
            } else if (string.Compare(winner, "Enemy") == 0)
            {
                _winText.enabled = true;
                _winText.text = "Gocera wins!";
                _playerCloseUpCam.Priority = 20;
                slowMotionController.StartTimescaleChange();
                gameOver = true;
                _menuButton.SetActive(true);
            }
        }
    }
}
