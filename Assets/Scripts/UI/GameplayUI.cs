using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Text m_TimerText, m_ScoreText;
    private float m_Timer = 10f;

    private CanvasGroup m_CG;

    private void Awake()
    {
        m_CG = GetComponent<CanvasGroup>();
        m_TimerText.text = m_Timer.ToString();
        StartCoroutine(TimerCoroutine());
    }

    private void Start()
    {
        m_Timer = GameController.Instance.GetGameTime;
    }

    private void OnEnable()
    {
        ScoreManager.Instance.Action_UpdateScore += UpdateScore;
        GameController.Instance.Action_GameEnd += HideUI;
    }

    private void OnDisable()
    {
        ScoreManager.Instance.Action_UpdateScore -= UpdateScore;
        GameController.Instance.Action_GameEnd -= HideUI;
    }

    private void UpdateScore(int _newScore)
    {
        m_ScoreText.text = _newScore.ToString();
    }

    private void HideUI()
    {
        m_CG.alpha = 0f;
        m_CG.blocksRaycasts = false;
    }

    private IEnumerator TimerCoroutine()
    {
        while (m_Timer > 0)
        {
            yield return new WaitForSeconds(1f);
            m_Timer -= 1f;
            m_TimerText.text = m_Timer.ToString();
            BallSpawner.Instance.CalculateBallSpeed(m_Timer);
        }

        GameController.Instance.GameEnd();
    }    
}
