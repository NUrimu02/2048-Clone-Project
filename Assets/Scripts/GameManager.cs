using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;

    public TMP_Text scoreText;
    public TMP_Text bestText;
    public TMP_Text movesText;

    private int score;
    private int moves;

    private void Start()
    {
        board = FindAnyObjectByType<TileBoard>();
        NewGame();
    }

    public void NewGame()
    {
        board.enabled = true;
        SetScore(0);
        bestText.text = LoadBestScore().ToString();

        gameOver.alpha = 0f;
        gameOver.interactable = false;
        
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
    }

    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;

        Fade(gameOver, 1f, 1f);
    }
    

    public void UpdateScore(int points)
    {
        SetScore(score + points);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
        movesText.text = $"Scored {score} points in {moves} moves";

        SaveBestScore();
    }

    private void SaveBestScore()
    {
        int bestScore = LoadBestScore();

        if(score > bestScore)
            PlayerPrefs.SetInt("bestScore", score);
    }

    private int LoadBestScore()
    {
        return PlayerPrefs.GetInt("bestScore", 0);
    }
    
    public void UpdateMoves(int points)
    {
        moves = moves + points;
    }

    private void Fade(CanvasGroup canvasGroup, float targetValue, float delay)
    {
        canvasGroup.DOKill();

        canvasGroup.DOFade(targetValue, 0.5f)
               .SetDelay(delay);
    }
}
