using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

public class MemoryGame : MonoBehaviour
{
    [SerializeField] private SpriteAtlas _gamePadsAtlas;
    [SerializeField] private MemoryButton _memoryButtonGO;
    
    private List<MemoryButton> _memoryButtons = new List<MemoryButton>(); //TODO: Needed?
    private Vector3 _buttonPosition = new Vector3(0,-0.5f,0);
    private MemoryButton _currentMemoryButton;
    
    private const int Lines = 3;
    private const int Columns = 4;
    private const float VerticalSpace = 0.75f;
    private const float HorizontalSpace = 0.75f;
    private const float AddedTime = 0.1f;

    private void Start()
    {
        var sprites = new Sprite[_gamePadsAtlas.spriteCount];
        _gamePadsAtlas.GetSprites(sprites);

        var usedNumbers = new List<int>();
        
        for (int j = 0; j < Lines; j++)
        {
            for (int i = 0; i < Columns; i++)
            {
                var button = Instantiate(_memoryButtonGO, _buttonPosition, Quaternion.identity, transform);
                var randomNumber = GetRandomNumber(sprites, usedNumbers);
                button.Renderer.sprite = sprites[randomNumber];
                button.PreInit(this, randomNumber);

                _memoryButtons.Add(button);

                _buttonPosition.x += HorizontalSpace;
            }
            
            _buttonPosition.z += VerticalSpace;
            _buttonPosition.x = 0;
        }
    }

    [ContextMenu("Init")]
    public void Init()
    {
        float timeToWaitForAnimOut = 0;
        
        _memoryButtons.ForEach(x => x.Init(timeToWaitForAnimOut += AddedTime));
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

    public void CheckMatch(MemoryButton button)
    {
        if (_currentMemoryButton == null)
        {
            _currentMemoryButton = button;
            return;
        }

        if (_currentMemoryButton.ButtonNumber == button.ButtonNumber)
        {
            Debug.Log("Match!");
            _currentMemoryButton = null;
        }
    }
}
