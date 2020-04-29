using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

public class OnMemoryGameWon : ASignal { }

public class MemoryGame : MonoBehaviour
{
    [SerializeField] private SpriteAtlas _gamePadsAtlas;
    [SerializeField] private MemoryButton _memoryButtonGo;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private AudioClip _gameWonAudioClip;

    #region Fields

    private readonly List<MemoryButton> _memoryButtons = new List<MemoryButton>(); //TODO: Needed?
    private Vector3 _buttonPosition;// = new Vector3(0, -0.5f, 0);
    private MemoryButton _currentMemoryButton;
    private int _matches;

    private const int Lines = 3;
    private const int Columns = 4;
    private const float VerticalSpace = 0.75f;
    private const float HorizontalSpace = 0.75f;
    private const float AddedTime = 0.1f;

    #endregion

    private void Start()
    {
        _buttonPosition = transform.position;
        
        var sprites = new Sprite[_gamePadsAtlas.spriteCount];
        _gamePadsAtlas.GetSprites(sprites);

        var usedNumbers = new List<int>();

        for (int j = 0; j < Lines; j++)
        {
            for (int i = 0; i < Columns; i++)
            {
                var button = Instantiate(_memoryButtonGo, _buttonPosition, Quaternion.identity, transform);
                var randomNumber = GetRandomNumber(sprites, usedNumbers);
                button.Renderer.sprite = sprites[randomNumber];
                button.PreInit(this, randomNumber);

                _memoryButtons.Add(button);

                _buttonPosition.x += HorizontalSpace;
            }

            _buttonPosition.z += VerticalSpace;
            _buttonPosition.x = transform.position.x;
        }
    }

    [ContextMenu("Init")]
    public void Init()
    {
        float timeToWaitForAnimIn = 0;

        _memoryButtons.ForEach(x => x.Init(timeToWaitForAnimIn += AddedTime));
    }

    private static int GetRandomNumber(IReadOnlyCollection<Sprite> sprites, List<int> usedNumbers)
    {
        int GetRandom() => Random.Range(0, sprites.Count);

        var random = GetRandom();
        var result = usedNumbers.Exists(x => x == random);

        while (result)
        {
            random = GetRandom();
            result = usedNumbers.Exists(x => x == random);
        }

        usedNumbers.Add(random);

        if (usedNumbers.Count == sprites.Count) usedNumbers.Clear();

        return random;
    }

    /// <summary>
    /// Rotate chosen button, and check if the last 2 chosen buttons match
    /// </summary>
    /// <param name="button">The button to check</param>
    public void CheckMatch(MemoryButton button)
    {
        button.RotateButton(false);
        
        //If there is no chosen button, then this is the _currentButton
        if (_currentMemoryButton == null)
        {
            _currentMemoryButton = button;
        }
        //If there is a chosen button - Does his index and the new button's the same?
        else if (_currentMemoryButton.ButtonNumber == button.ButtonNumber)
        {
            if ((_matches += 2) >= _memoryButtons.Count) MemoryGameWon();
            
            _currentMemoryButton = null;
        }
        //The indices of the chosen buttons don't match, rotate them and return.
        else
        {
            const float delay = 0.8f;
            
            button.RotateButton(false, 0.4f);
            _currentMemoryButton.RotateButton(false, 0.4f);
            _currentMemoryButton = null;

            StartCoroutine(DisableButtonColliders(delay));
        }
    }

    private void MemoryGameWon()
    {
        _memoryButtons.ForEach(x => x.GameWon());
        _particles.Play(true);
     
        AudioManager.Instance.PlaySound(_gameWonAudioClip, 0.6f);
        Signals.Get<OnMemoryGameWon>().Dispatch();
    }

    /// <summary>
    /// Disable colliders on all buttons for a set amount of time
    /// </summary>
    /// <param name="waitAmount">Delay between disable and reenable each button collider</param>
    /// <returns></returns>
    private IEnumerator DisableButtonColliders(float waitAmount)
    {
        _memoryButtons.ForEach(x => x.Enabled = false);
        yield return new WaitForSeconds(waitAmount);
        _memoryButtons.ForEach(x => x.Enabled = true);
    }
}