using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class MonsterChase : MonoBehaviour
{
    SanityManager _sanityManager;
    SoundManager _soundManager;
    GBManager _gBManager;
    Transform _player;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] AudioSource _breath;
    [SerializeField] AudioClip[] _breathSounds;
    [SerializeField] AudioSource _steps;
    [SerializeField] AudioClip[] _stepsSounds;
    [SerializeField] Transform[] _patrolPoints;
    Vector3 startPoint;
    float _timer = 0f;
    float _updateRate = 0.5f;
    Vector3 _lastPlayerPos;
    float _detectDistance = 7f;
    float disappearDistance = 2f;
    int _currentPoint = 0;
    bool _isChasing = false;
    float _repathTimer = 0f;
    float _repathRate = 0.5f;
    float _chaseSpeed = 0f;
    private void Awake()
    {
        _player = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        _sanityManager = GameObject.FindWithTag("SanityManager").GetComponent<SanityManager>();
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        _gBManager = GameObject.FindWithTag("GBManager").GetComponent<GBManager>();
        DynamicMoveProvider moveProvider = GameObject.FindWithTag("Player").GetComponentInChildren<DynamicMoveProvider>();
        _chaseSpeed = moveProvider.moveSpeed - 1f;
        startPoint = transform.position;
    }
    private void Start()
    {
        if (_patrolPoints.Length > 0)
        {
            _agent.SetDestination(_patrolPoints[0].position);
        }
        _lastPlayerPos = _player.position;
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        _repathTimer += Time.deltaTime;
        if (_timer < _updateRate) return;
        _timer = 0;
        MoveAgent();
    }
    void MoveAgent() 
    {
        Vector3 toPlayer = _player.position - transform.position;
        float sqrDist = toPlayer.sqrMagnitude;
        if (sqrDist <= disappearDistance * disappearDistance)
        { 
            Respawn();
            return;
        }
        if (sqrDist <= _detectDistance * _detectDistance)
        {
            if (!_isChasing)
            {
                _isChasing = true;
                //_soundManager.PlaySound(_steps, _stepsSounds[1]);
                _agent.speed = _chaseSpeed;
                _agent.SetDestination(_player.position);
                _lastPlayerPos = _player.position;
                _repathTimer = 0f;
            }
            if (_repathTimer >= _repathRate)
            {
                float moveDelta = (_player.position - _lastPlayerPos).sqrMagnitude;
                if (moveDelta > 0.25f)
                {
                    _agent.SetDestination(_player.position);
                    _lastPlayerPos = _player.position;
                }
                _repathTimer = 0f;
            }
            float t = 3f - (Mathf.Sqrt(sqrDist) / _detectDistance);
            _sanityManager.ChangeValue(-t);
            _gBManager.SetVignette(t/2);
        }
        else
        {
            if (_isChasing)
            {
                _isChasing = false;
                _agent.speed = 0.5f;
                _gBManager.SetVignette(0);
                //_soundManager.PlaySound(_steps, _stepsSounds[0]);
                _agent.ResetPath();
            }
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                _agent.SetDestination(_patrolPoints[_currentPoint].position);
                _currentPoint = (_currentPoint + 1) % _patrolPoints.Length;
            }
        }
    }
    void Respawn() 
    {
        //_soundManager.PlaySound(_breath, _breathSounds[currentPoint]);
        _agent.enabled = false;
        _agent.Warp(startPoint);
        _agent.enabled = true;
        _agent.speed = 0.5f;
        //_soundManager.PlaySound(_steps, _stepsSounds[0]);
        _gBManager.SetVignette(0);
        _isChasing = false;
        _agent.SetDestination(_patrolPoints[0].position);
    }
}
