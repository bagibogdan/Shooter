using Cysharp.Threading.Tasks;
using Managers;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Without Zenject
    
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject blackPanel;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Button startButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextButton;
    
    private GameObject _currentPanel;
    private UIEffects _effects;

    public void Initialize()
    {
        startButton.onClick.AddListener(gameManager.StartGame);
        restartButton.onClick.AddListener(gameManager.OnRestart);
        nextButton.onClick.AddListener(gameManager.StartGame);
    }
    
    private async UniTask EnableCurrentPanel()
    {
        if (_currentPanel == null) return;
        
        _currentPanel.SetActive(true);
        await _effects.PanelFadeInEffect(_currentPanel);
    }
    
    private async UniTask DisableCurrentPanel()
    {
        if (_currentPanel == null) return;

        await _effects.PanelFadeOutEffect(_currentPanel);
        _currentPanel.SetActive(false);
        _currentPanel = null;
    }

    private async UniTask ShowPanel(GameObject panel)
    {
        await DisableCurrentPanel();
        _currentPanel = panel;
        await EnableCurrentPanel();
    }
    
    public void ShowBlackPanel()
    {
        _effects = new UIEffects();
        _currentPanel = blackPanel;
        _currentPanel.SetActive(true);
    }
    
    public async UniTask ShowStartPanel()
    {
        await ShowPanel(startPanel);
    }
    
    public async UniTask ShowInfoPanel()
    {
        infoPanel.SetActive(true);
        await _effects.PanelFadeInEffect(infoPanel);
    }
    
    public async UniTask ShowGamePanel()
    {
        await ShowPanel(gamePanel);
    }
    
    public async UniTask ShowWinPanel()
    {
        await ShowPanel(winPanel);
    }
    
    public async UniTask ShowLosePanel()
    {
        await ShowPanel(losePanel);
    }
}
