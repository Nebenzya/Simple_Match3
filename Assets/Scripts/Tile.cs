using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool isSelect;
    public bool IsEmpty => spriteRenderer.sprite == null;
}
