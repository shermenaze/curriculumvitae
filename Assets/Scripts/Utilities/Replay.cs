using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Replay : MonoBehaviour
{
    [SerializeField] private Image _darkeningImage;
    [SerializeField] private Image _replayIconImage;
    [SerializeField] private TextMeshProUGUI _replayText;
    
    public void OnEnable()
    {
        _darkeningImage.DOFade(1, 0.8f).OnComplete(() =>
        {
            _replayText.DOFade(1, 1.5f);
            _replayIconImage.DOFillAmount(1, 0.8f);
        });
    }
    
    public void ReplayGame()
    {
        SceneManager.LoadScene(0);
    }
}
