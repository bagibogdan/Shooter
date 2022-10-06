using Managers;
using TMPro;
using UnityEngine;

public class UILevel : MonoBehaviour
{
    private const string LEVEL_TEXT = "Level:\n";
    private TextMeshProUGUI _levelText;
    private GameManager _gameManager;

    private void Awake()
    {
        _levelText = GetComponent<TextMeshProUGUI>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        _levelText.text = LEVEL_TEXT + _gameManager.Level;
    }
}
