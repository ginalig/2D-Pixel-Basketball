using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BasketballManager : MonoBehaviour
{
    [Header("HUD")]
    
    public TMP_Text scoreText;
    public TMP_Text coinCountText;
    public TMP_Text highScoreText;
    public TMP_Text multiplierText;
    
    [Header("Objects")] 
    
    public Ball ball;
    public Trajectory trajectory;

    [Header("Values")] 
    
    public float force;
    public int coinScore;
    public int score;
    public int highScore;
    public int coinCount;
    
    
    private int multiplier;
    
    [Header("Events")] 
    
    public UnityEvent onScoredEvent;

    [Header("Quests")]
    

    private InputMaster controls;
    private bool isDragging;
    
    [HideInInspector] public bool isAbleToDrag = true;
    [HideInInspector] public bool isScored = true; 

    private Vector2 mStartPos;
    private Vector2 mEndPos;
    private Vector2 mDirection;
    private Vector2 mForce;
    private float mDistance;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Click.started += _ => OnDragStart();
        controls.Player.Click.canceled += _ => OnDragEnd();
    }
    
    void Start()
    {
        highScore = ES3.Load("BasketballHighScore", 0);
        coinCount = ES3.Load("Coin", 0);
    }

    private void Update()
    {
        if (isDragging) OnDrag();

        scoreText.text = score.ToString();
        highScoreText.text = $"Highscore: {highScore}";
        coinCountText.text = coinCount.ToString();
        multiplier = score / coinScore + 1;
        multiplierText.text = $"{multiplier}x";
    }

    public void Score()
    {
        score++;
        if (score > highScore)
        {
            highScore = score;
            UpdateHighScore();
        }

        if (score % coinScore == 0)
        {
            coinCount += multiplier;
            ES3.Save("Coins", coinCount);
        }
        
        isScored = true;
        
        onScoredEvent.Invoke();
    }

    private void UpdateHighScore()
    {
        ES3.Save("BasketballHighScore", highScore);
    }

    public void LoseStreak()
    {
        score = 0;
    }
    
    private void OnDragStart()
    {
        if (!isAbleToDrag || EventSystem.current.IsPointerOverGameObject()) return;
        trajectory.Show();
        isDragging = true;
        ball.DeactivateRb();
        mStartPos = controls.Player.MousePosition.ReadValue<Vector2>();
    }

    private void OnDragEnd()
    {
        if (!isAbleToDrag || EventSystem.current.IsPointerOverGameObject()) return;
        trajectory.Hide();
        isAbleToDrag = false;
        isDragging = false;
        ball.ActivateRb();
        ball.Push(mForce);
    }

    private void OnDrag()
    {
        mEndPos = controls.Player.MousePosition.ReadValue<Vector2>();
        mDistance = Vector2.Distance(mStartPos, mEndPos);
        mDirection = (mStartPos - mEndPos).normalized;
        mForce = mDirection * (mDistance * force * 0.1f);
        trajectory.UpdateDots(ball.pos, mForce, ball.rb.gravityScale);
    }

    public void Reset()
    {
        mEndPos = Vector2.zero;
        mDistance = 0;
        mDirection = Vector2.zero;
        mForce = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
