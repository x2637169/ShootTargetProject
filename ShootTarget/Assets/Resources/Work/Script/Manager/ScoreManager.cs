using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text deadScoreText;
    public Text winScoreText;
    public Text hightScoreText;
    public Text stopMenuHighScoreText;

    public Animator scorePopup_m;

    public int score;
    public int highScore;

    void Awake()
    {
        highScore = PlayerPrefs.GetInt("HightScore");
        scoreText.text = "Score:" + score;
        hightScoreText.text = "High Score" + "\n" + PlayerPrefs.GetInt("HightScore", 0).ToString();
        stopMenuHighScoreText.text = "High Score" + "\n" + PlayerPrefs.GetInt("HightScore", 0).ToString();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void GetScore(int getScore)
    {
        score += getScore;

        SetScore();

        scorePopup_m.SetTrigger("ScorePopup");

        scoreText.text = "Score:" + score;
        deadScoreText.text = "Score:" + score;
        winScoreText.text = "Score:" + score;
    }

    public void SetScore()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HightScore", score);

            hightScoreText.text = score.ToString();
            stopMenuHighScoreText.text = score.ToString();
        }
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();

        hightScoreText.text = "High Score" + "\n" + PlayerPrefs.GetInt("HightScore", 0).ToString();
        stopMenuHighScoreText.text = "High Score" + "\n" + PlayerPrefs.GetInt("HightScore", 0).ToString();
    }
}
