using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Credits : MonoBehaviour
{
    [SerializeField] private CanvasGroup menuCanvasRenderer;
    [SerializeField] private CanvasGroup creditsCanvasRenderer;
    [SerializeField] private GameObject Panel;
    [SerializeField] private List<string> textList;
    [SerializeField] private TMP_Text placer;

    void OnEnable()
    {
        StartCoroutine(Everything());
    }

    private IEnumerator Everything()
    {
        // Fade in credits canvas
        yield return StartCoroutine(CreditsFadeIn(creditsCanvasRenderer, menuCanvasRenderer));

        Panel.SetActive(false);

        // Show names
        yield return ShowNames();

        // Fade back to menu canvas
        yield return StartCoroutine(CreditsFadeIn(menuCanvasRenderer, creditsCanvasRenderer));
        
        Panel.SetActive(true);
        gameObject.SetActive(false);
    }

    private IEnumerator CreditsFadeIn(CanvasGroup cr1, CanvasGroup cr2)
    {
        float timer = 0f;
        float duration = 0.4f;  // Duration for fade

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            float alpha1 = Mathf.Lerp(0f, 1f, timer / duration);
            float alpha2 = Mathf.Lerp(1f, 0f, timer / duration);
            cr1.alpha = alpha1;
            cr2.alpha = alpha2;
            yield return null;
        }
    }

    private IEnumerator ShowNames()
    {
        yield return new WaitForSeconds(2f);
        placer.enabled = true;

        foreach (string txt in textList)
        {
            placer.text = txt.Replace("NEWLINE", "\n");
            
            // Fade in the text
            yield return FadeText(placer, 1f, 0.5f); // Fade in

            yield return new WaitForSeconds(1f); // Display time

            // Fade out the text
            yield return FadeText(placer, 0f, 0.5f); // Fade out

            yield return new WaitForSeconds(0.5f); // Wait before next text
        }
        placer.enabled = false;
    }

    private IEnumerator FadeText(TMP_Text text, float targetAlpha, float duration)
    {
        float timer = 0f;
        float startAlpha = text.color.a;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, timer / duration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, newAlpha);
            yield return null;
        }
    }
}
