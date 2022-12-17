using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public event Action<int> Action_UpdateScore;

    private int m_Score;

    public int GetScore => m_Score;

    public void AddScore(int _value)
    {
        m_Score += _value;
        Action_UpdateScore?.Invoke(m_Score);
    }
}
