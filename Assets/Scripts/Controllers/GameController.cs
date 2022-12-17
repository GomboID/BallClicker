using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : Singleton<GameController>
{
    public event Action Action_GameEnd;

    [SerializeField] private float m_GameTime = 10f;    

    public float GetGameTime => m_GameTime;

    public void GameEnd()
    {
        Action_GameEnd?.Invoke();
    }
}
