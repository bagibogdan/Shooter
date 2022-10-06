using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRewardedButton : MonoBehaviour
{
    public TextMeshProUGUI ButtonText => _buttonText;
    
    private TextMeshProUGUI _buttonText;
    private Button _button;
    private IronSourceManager _ironSourceManager;
    
    private void Awake()
    {
        _buttonText = GetComponent<TextMeshProUGUI>();
        _button = GetComponent<Button>();
        _ironSourceManager = FindObjectOfType<IronSourceManager>();
    }

    private void FixedUpdate()
    {
        _button.interactable = _ironSourceManager.IsRewardedReady;
    }
}
