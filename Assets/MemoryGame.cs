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
    private Vector3 _buttonPosition = Vector3.zero;

    private void Start()
    {
        var sprites = new Sprite[_gamePadsAtlas.spriteCount];
        _gamePadsAtlas.GetSprites(sprites);

        var usedNumbers = new List<int>();
        
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                var button = Instantiate(_memoryButtonGO, _buttonPosition, Quaternion.identity, transform);
                button.Renderer.sprite = sprites[GetRandomNumber(sprites, usedNumbers)];
                
                _memoryButtons.Add(button);
                
                _buttonPosition.x += 0.75f;
            }
            
            _buttonPosition.y += 0.75f;
        }
    }

    private static int GetRandomNumber(IReadOnlyCollection<Sprite> sprites, List<int> usedNumbers)
    {
        var random = Random.Range(0, sprites.Count);
        var result = usedNumbers.Exists(x => x == random);

        while (result)
        {
            random = Random.Range(0, sprites.Count);
            result = usedNumbers.Exists(x => x == random);
        }

        usedNumbers.Add(random);
        
        if (usedNumbers.Count == sprites.Count) usedNumbers.Clear();

        return random;
    }
}
