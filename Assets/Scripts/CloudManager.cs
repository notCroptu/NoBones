using System.Collections;
using UnityEngine;

public class CloudManager : MonoBehaviour
{

    private RectTransform _rectTransform;
    [SerializeField] private float animeSpeed = 0.1f;
    
    
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        StartCoroutine(CloudAnime());
    }

    private IEnumerator CloudAnime()
    {
        while (true)
        {
            _rectTransform.position += new Vector3(1, 0, 0);
            if (_rectTransform.position.x > 0)
            {
                _rectTransform.position += new Vector3(-Screen.width, 0, 0);
            }
            yield return new WaitForSeconds(animeSpeed);
        }
    }
}
