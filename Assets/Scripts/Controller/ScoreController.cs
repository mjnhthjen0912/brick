using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    int m_score = 0;
    int m_lines;
    public int m_level = 1;

    public Text m_lineText;
    public Text m_levelText;
    public Text m_scoreText;
    public bool m_isLevelUp = false;

    public int m_linesPerLevel = 5;

    const int m_minLines = 1;
    const int m_maxLines = 4;

    public Text m_hightScore;

    public Text m_currentScore;

    private void SetCurrentScore()
    {
        m_currentScore.text = "Current " + m_score;
    }

    public void UpdateHightScore()
    {
        SetCurrentScore();
        if (m_score > PlayerPrefs.GetInt("HightScore", 0))
        {
            PlayerPrefs.SetInt("HightScore", m_score);
            m_hightScore.text = "Best " + m_score.ToString();
        }
    }

    public void ScoreLines(int n)
    {
        m_isLevelUp = false;
        n = Mathf.Clamp(n, m_minLines, m_maxLines);

        switch (n)
        {
            case 1:
                m_score += 40 ;
                break;
            case 2:
                m_score += 100 ;
                break;
            case 3:
                m_score += 300 ;
                break;
            case 4:
                m_score += 600 ;
                break;
        }
        m_lines -= n;

        if(m_lines <= 0)
        {
            LevelUp();
        }

        ShowText();
    }

    private void ShowText()
    {
        m_levelText.text = "lv:" + m_level.ToString();
        m_lineText.text = "line:" + m_lines.ToString();
        m_scoreText.text = FormatScore(m_score, 5);
        m_hightScore.text = "Best " + PlayerPrefs.GetInt("HightScore", 0).ToString();
    }

    private string FormatScore(int score, int numberDigit)
    {
        string str = score.ToString();
        while(str.Length < numberDigit)
        {
            str = "0" + str;
        }
        return str;
    }

    public void ResetLevel()
    {
        m_level = 1;
        m_lines = m_linesPerLevel * m_level;
        ShowText();
    }

    public void LevelUp()
    {
        m_level++;
        m_lines = m_linesPerLevel * m_level;
        m_isLevelUp = true;
    }
    
	void Start () {
        ResetLevel();
    }
	
}
