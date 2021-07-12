using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Score
{
    public class Points : MonoBehaviour
    {
        public static Points Instance { get; set; }
        public TMP_Text txtScore;

        private int highScore;
        private int score = 0;

        private void Awake()
        {
            Instance = this;
            if (PlayerPrefs.HasKey("Highscore"))
                highScore = PlayerPrefs.GetInt("Highscore");
            else highScore = 0;
            txtScore.text = "Score: " + score;
        }

        public void addScore(int score)
        {
            this.score += score;
            if (this.score > highScore) { highScore = score; PlayerPrefs.SetInt("Highscore", this.score); }
            PlayerPrefs.SetInt("Score", this.score);
            txtScore.text = "Score: " + this.score;
        }

        public int getScore()
        {
            return score;
        }

        public int getHighScore()
        {
            return highScore;
        }
    }

}