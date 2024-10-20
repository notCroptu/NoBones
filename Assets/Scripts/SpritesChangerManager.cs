using System.Collections;
using UnityEngine;

public class SpritesChangerManager : MonoBehaviour
{
    [SerializeField] private float changeSpeed = 0.1f;
    [SerializeField] private float waitSpeed = 1.0f;
    [SerializeField] private float waitSpeed2 = 2.0f;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    void Start()
    {
        _spriteRenderer = transform.Find("Square").GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeSprite());
    }

    private IEnumerator ChangeSprite()
    {
        while (true)
        {
            float rand = Random.Range(waitSpeed, waitSpeed2);
            yield return new WaitForSeconds(rand);
            int count = 0;
            while (true)
            {
                _spriteRenderer.sprite = sprites[count];
                count++;
                if (count >= sprites.Length) break;
                yield return new WaitForSeconds(changeSpeed);
            }
        }
    }
}
