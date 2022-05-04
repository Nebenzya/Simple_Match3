using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool IsSelect { get; set; }
    public int ID { get; set; }
    public bool IsEmpty => spriteRenderer.sprite == null;
}
