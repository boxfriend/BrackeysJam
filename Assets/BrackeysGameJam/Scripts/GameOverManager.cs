using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Boxfriend.Player;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText, _reasonText;

    void Start()
    {
        try
        {
            _scoreText.text = $"SCORE: {PlayerController.Instance.Score:000}";
            _reasonText.text = $"{PlayerController.Instance.DeathString}";
        } catch (System.NullReferenceException)
        {
            Debug.LogWarning("you done fucked up a a ron");
            _scoreText.text = "it means you broke something";
            _reasonText.text = "This is an easter egg";
        }
    }

}
