using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ParallaxEffect : MonoBehaviour
{
    [FormerlySerializedAs("Layers")] [SerializeField] private List<Sprite> Sprites;
    [SerializeField] private Vector2 ImageInitialShift = new Vector2(0, 0);
    [SerializeField] private float ScaleFactor = 1f;
    [SerializeField]private GameObject Player;

    private List<Transform> _renderedLayers = new List<Transform>();
    private List<Tuple<Transform, float>> _renderedSprites = new List<Tuple<Transform, float>>();
    private Camera _mainCamera;
    private Vector2 _playerInitialPosition;
    
    void Awake()
    {
        _playerInitialPosition = Player.transform.position;
        
        _mainCamera = Camera.main;
        if (!_mainCamera) return;
        Vector2 cameraPosition = _mainCamera.transform.position;

        for (int spriteIndex = 0; spriteIndex < Sprites.Count; spriteIndex++)
        {
            Sprite sprite = Sprites[spriteIndex];
                
            GameObject parallaxLayer = new GameObject();
            parallaxLayer.name = $"Layer {sprite.name}";
            _renderedLayers.Add(parallaxLayer.transform);
            
            for (int i = -1; i < 7; i++)
            {
                float spriteWidth = sprite.bounds.size.x;

                float shiftX = cameraPosition.x + ImageInitialShift.x + spriteWidth * i * ScaleFactor;
                float shiftY = cameraPosition.y + ImageInitialShift.y;

                GameObject spriteRenderObject = new GameObject();
                spriteRenderObject.transform.parent = parallaxLayer.transform;
                spriteRenderObject.transform.position = new Vector3(shiftX, shiftY, 0);
                spriteRenderObject.transform.localScale = new Vector3(ScaleFactor, ScaleFactor, ScaleFactor);

                SpriteRenderer spriteRenderer = spriteRenderObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;
                spriteRenderer.sortingOrder = spriteIndex;

                _renderedSprites.Add(new Tuple<Transform, float>(spriteRenderObject.transform, spriteWidth));
            }
        }
    }

    void LateUpdate()
    {
        if (!_mainCamera) return;

        Vector2 playerOffset = (Vector2)Player.transform.position - _playerInitialPosition;
        for (int i = 0; i < _renderedLayers.Count; i++)
        {
            Transform layer = _renderedLayers[i];
            float layerMultiplierX = -1f / (_renderedLayers.Count - i + 1);
            float layerMultiplierY = layerMultiplierX / 5f;
            Vector3 layerOffset = new Vector3(playerOffset.x * layerMultiplierX, playerOffset.y * layerMultiplierY, 0);
            layer.position = layerOffset;
            layer.gameObject.name = $"Layer {i}";
        }
    }
}
