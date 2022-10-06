using System;
using Managers;
using TMPro;
using UnityEngine;

public class UICoins : MonoBehaviour
{
    private const string COINS_TEXT = "Coins:\n";
    private TextMeshProUGUI _coinsText;
    private GameManager _gameManager;

    private void Awake()
    {
        _coinsText = GetComponent<TextMeshProUGUI>();
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.OnCoinsChanged += CoinsUpdate;
    }

    private void Start()
    {
        CoinsUpdate(_gameManager.Coins);
    }

    private void OnDestroy()
    {
        _gameManager.OnCoinsChanged -= CoinsUpdate;
    }

    private void CoinsUpdate(int value)
    {
        _coinsText.text = COINS_TEXT + value;
    }
}
