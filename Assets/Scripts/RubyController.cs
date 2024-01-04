using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class RubyController : MonoBehaviour
{
    public float Speed = 3.0f;
 
    public int MaxHealth = 5;
    public float TimeInvincible = 2.0f;

    public int Health { get; private set; }

    bool _isInvincible;
    float _invincibleTimer;

    Rigidbody2D _rigidbody;
    float _horizontal;
    float _vertical;

    Animator _animator;
    Vector2 _lookDirection = new(1,0);
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        ChangeHealth(MaxHealth);
    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(_horizontal, _vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            _lookDirection.Set(move.x, move.y);
            _lookDirection.Normalize();
        }

        _animator.SetFloat("Look X", _lookDirection.x);
        _animator.SetFloat("Look Y", _lookDirection.y);
        _animator.SetFloat("Speed", move.magnitude);

        if (_isInvincible)//TODO: make countdown into func for more readability
        {
            _invincibleTimer -= Time.deltaTime;

            if(_invincibleTimer < 0 )
                _isInvincible = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = _rigidbody.position;

        position.x += Speed * _horizontal * Time.deltaTime;
        position.y += Speed * _vertical * Time.deltaTime;

        _rigidbody.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)//TODO: Should be a take dmg func insted to avoid if
        {
            //TODO: Sender should not be firing on a Invincible target
            //Debug.Log("TOO MANY CALLS !! MOVE LOGIC TO SENDER");
            if (_isInvincible)
                return;

            _isInvincible = true;
            _invincibleTimer = TimeInvincible;
        }

        Health = Mathf.Clamp(Health + amount, 0, MaxHealth);
        Debug.Log(Health + "/" + MaxHealth);
    }
}