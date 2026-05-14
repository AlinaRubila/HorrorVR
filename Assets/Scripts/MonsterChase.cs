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
    [SerializeField] Key[] keys;
    [SerializeField] Transform _face;
    Vector3 startPoint;
    float _timer = 0f;
    Vector3 _lastPlayerPos;
    int _currentPoint = 0;
    bool _isChasing = false;
    float _repathTimer = 0f;
    float _chaseSpeed = 0f;
    private void Awake()
    {
        _player = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        _sanityManager = GameObject.FindWithTag("SanityManager").GetComponent<SanityManager>();
        _soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        _gBManager = GameObject.FindWithTag("GBManager").GetComponent<GBManager>();
        DynamicMoveProvider moveProvider = GameObject.FindWithTag("Player").GetComponentInChildren<DynamicMoveProvider>();
        _chaseSpeed = moveProvider.moveSpeed / 2;
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
        if (_timer < 0.5f) return;
        _timer = 0;
        MoveAgent();
    }
    private void LateUpdate()
    {
        RotateFace();
    }
    public void GoToPlayer()
    {
        _isChasing = true;
        _soundManager.PlaySound(_steps, _stepsSounds[1]);
        _agent.speed = _chaseSpeed;
        _agent.SetDestination(_player.position);
        _lastPlayerPos = _player.position;
        _repathTimer = 0f;
    }
    public void ThrowRespawn()
    {
        if (_isChasing) Respawn();
    }
    void MoveAgent() 
    {
        Vector3 toPlayer = _player.position - transform.position;
        float sqrDist = toPlayer.sqrMagnitude;
        if (sqrDist <= 2f * 2f)
        { 
            Respawn();
            return;
        }
        if (sqrDist <= 6.5f * 6.5f)
        {
            if (!_isChasing)
            {
                GoToPlayer();
            }
            if (_repathTimer >= 0.5f)
            {
                float moveDelta = (_player.position - _lastPlayerPos).sqrMagnitude;
                if (moveDelta > 0.25f)
                {
                    _agent.SetDestination(_player.position);
                    _lastPlayerPos = _player.position;
                }
                _repathTimer = 0f;
            }
            float t = 1f - (Mathf.Sqrt(sqrDist) / 6.5f);
            _sanityManager.ChangeValue(-t);
            _gBManager.SetVignette(t/4);
            _gBManager.ChangeEffects(t / 4);
        }
        else
        {
            if (_isChasing)
            {
                _isChasing = false;
                _agent.speed = 0.5f;
                _gBManager.SetVignette(0);
                _soundManager.PlaySound(_steps, _stepsSounds[0]);
                _agent.ResetPath();
            }
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                _currentPoint = (_currentPoint + 1) % _patrolPoints.Length;
                _agent.SetDestination(_patrolPoints[_currentPoint].position);
            }
        }
    }
    private void Respawn() 
    {
        foreach (Key k in keys) k.BackToStart();
        float maxDistance = 0;
        int farPoint = 0;
        for (int i=0; i < _patrolPoints.Length; i++)
        {
            float dist = (_player.position - _patrolPoints[i].position).sqrMagnitude;
            if (dist > maxDistance)
            {
                maxDistance = dist;
                farPoint = i;
            }
        }
        _agent.enabled = false;
        //_agent.Warp(startPoint);
        _agent.Warp(_patrolPoints[farPoint].position);
        _currentPoint = farPoint;
        _agent.enabled = true;
        _agent.speed = 0.5f;
        _soundManager.PlaySound(_steps, _stepsSounds[0]);
        _gBManager.SetVignette(0);
        _gBManager.ChangeEffects(-10);
        _isChasing = false;
        _agent.SetDestination(_patrolPoints[0].position);
    }
    void RotateFace()
    {
        Vector3 direction = _agent.velocity;
        direction.y = 0;
        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180, 0);
            _face.rotation = Quaternion.Slerp(_face.rotation, lookRotation, Time.deltaTime * 2f);
        }
    }
}
