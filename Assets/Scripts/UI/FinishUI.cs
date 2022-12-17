using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishUI : MonoBehaviour
{
    [SerializeField] private Text m_ScoreText;
    [SerializeField] private Button m_CompleteButton;

    private CanvasGroup m_CG;

    private void Awake()
    {
        m_CG = GetComponent<CanvasGroup>();
        m_CompleteButton.onClick.AddListener(CompleteButtonClick);
    }

    private void OnEnable()
    {
        GameController.Instance.Action_GameEnd += ShowUI;
    }

    private void OnDisable()
    {
        GameController.Instance.Action_GameEnd -= ShowUI;
    }

    private void ShowUI()
    {
        m_ScoreText.text = ScoreManager.Instance.GetScore.ToString();
        m_CG.alpha = 1;
        m_CG.blocksRaycasts = true;
    }

    private void CompleteButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
