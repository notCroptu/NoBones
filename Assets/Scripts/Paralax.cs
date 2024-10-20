using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float speed;

    private Material _material;
    private Vector2 offset;

    void Start()
    {
        // Get the material from the SpriteRenderer
        _material = _spriteRenderer.material;
    }

    void Update()
    {
        // Calculate the texture movement based on speed and time
        float moveIdleDistance = Time.deltaTime * speed;

        // Accumulate the offset over time
        offset.x += moveIdleDistance;

        // Apply the new offset to the material
        _material.SetTextureOffset("_MainTex", offset);
    }
}
