using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    // UI Information
    // text mesh pro variable
    private int score;
    public TextMeshProUGUI scoreText;
    private float textFontSize;

    [SerializeField] TextMeshProUGUI restartText;

    // ════════════════════════════
    //      Start and Update
    // ════════════════════════════
    
    // Start is called before the first frame update
    void Start()
    {
        // Null guard the scoreText
        if (scoreText == null) return;

        scoreText.text = "0";
        textFontSize = scoreText.fontSize;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseScore(int score) {
        // Null guard the scoreText
        if (scoreText == null) return;

        this.score += score;
        StartCoroutine(ScaleText(score));
        scoreText.text = this.score.ToString();
    }

    public void IncreasePassiveScore(int score) {
        // Null guard the scoreText
        if (scoreText == null) return;

        this.score += score;
        scoreText.text = this.score.ToString();
    }

    // Scale the transform of the textmeshpro up and down
    IEnumerator ScaleText(int score)
    {
        float targetFontSize = textFontSize * (1 + (float)(score / 5f));

        // Scale up
        float elapsedTime = 0f;
        float duration = 0.20f;
        float startFontSize = scoreText.fontSize;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            scoreText.fontSize = Mathf.Lerp(startFontSize, targetFontSize, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        scoreText.fontSize = targetFontSize;

        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.1f);

        // Scale down
        elapsedTime = 0f;
        duration = 0.25f;
        startFontSize = targetFontSize;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            scoreText.fontSize = Mathf.Lerp(startFontSize, textFontSize, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        scoreText.fontSize = textFontSize;
    }

    public void ShowRestartButton() {
        // Null guard the restartText
        if (restartText == null) return;
        restartText.gameObject.SetActive(true);
    }

}