using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour, IClickable
{
    private MeshRenderer m_MR;
    private float m_Speed, m_BorderToDestroy;
    private int m_Score;

    private void Awake()
    {
        m_MR = GetComponent<MeshRenderer>();
    }

    public void Init(float _size, float _speed, int _score, float _topBorder)
    {
        m_Speed = _speed;
        m_Score = _score;
        transform.localScale = Vector3.one *_size;
        m_BorderToDestroy = _topBorder + 2f;
        m_MR.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * m_Speed, Space.World);
        m_MR.material.SetFloat("_Brightness", 0.5f + transform.position.y / m_BorderToDestroy);

        if (transform.position.y > m_BorderToDestroy)
        {
            Destroy(gameObject);
        }
    }

    public void ClickObject()
    {
        ScoreManager.Instance.AddScore(m_Score);
        Destroy(gameObject);
    }
}

public interface IClickable
{
    public void ClickObject();
}
