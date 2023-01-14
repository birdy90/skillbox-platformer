using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour
{
    public List<Sprite> Sprites;

    void Awake()
    {
        if (Sprites.Count > 0)
        {
            SetSprite(PeekRandomSprite());
        }
    }

    Sprite PeekRandomSprite()
    {
        int randomIndex = Random.Range(0, Sprites.Count);
        return Sprites[randomIndex];
    }

    void SetSprite(Sprite sprite)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
}
