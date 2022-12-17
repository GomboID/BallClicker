using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : Singleton<BallSpawner>
{
    [SerializeField] private GameObject m_BallPrefab;
    [Range(0.01f, 10f)] [SerializeField] private float m_SpawnDelay = 0.5f;    
    [Range(0.75f, 2f)] [SerializeField] private float m_MaxBallSize = 1f;
    [Range(1f, 5f)] [SerializeField] private float m_BallBaseSpeed = 1f;
    [SerializeField] private int m_BallScore = 10;

    private Vector2 m_SpawnBorder;    
    private Coroutine m_SpawnCorotine = null;    
    private float m_CurrentBallSpeed, m_GameTime;
    

    private void Start()
    {
        m_CurrentBallSpeed = m_BallBaseSpeed;
        m_GameTime = GameController.Instance.GetGameTime;
        CalculateSpawnBorder();
        m_SpawnCorotine = StartCoroutine(SpawnCorotine());
    }

    private void OnEnable()
    {
        GameController.Instance.Action_GameEnd += StopSpawn;
    }

    private void OnDisable()
    {
        GameController.Instance.Action_GameEnd -= StopSpawn;
    }

    private void StopSpawn()
    {
        StopCoroutine(m_SpawnCorotine);
    }

    private void CalculateSpawnBorder()
    {
        Camera camera = Camera.main;
        var frustumHeight = 2.0f * Vector3.Distance(transform.position, camera.transform.position) * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        m_SpawnBorder.y = frustumHeight / 2f + m_MaxBallSize;
        m_SpawnBorder.x = frustumHeight * camera.aspect / 2f - m_MaxBallSize / 2 * 1.1f;
    }

    public void CalculateBallSpeed(float _currentTime)
    {
        m_CurrentBallSpeed = m_BallBaseSpeed + (1 - _currentTime / m_GameTime);
    }

    private IEnumerator SpawnCorotine()
    {
        while (true)
        {
            var newBall = Instantiate(m_BallPrefab, new Vector3(Random.Range(-m_SpawnBorder.x, m_SpawnBorder.x), -m_SpawnBorder.y, 0f), Quaternion.identity, transform).GetComponent<BallScript>();

            float size = Random.Range(m_MaxBallSize * 0.35f, m_MaxBallSize);

            newBall.Init(
                size,
                m_CurrentBallSpeed + (m_MaxBallSize - size),                
                (int)(m_BallScore + m_BallScore * (1 - size / m_MaxBallSize)),
                m_SpawnBorder.y);

            yield return new WaitForSeconds(m_SpawnDelay);
        }
    }
}
