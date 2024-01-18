using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int Damage = -1;
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D _rigidbody2D;
    float _timer;
    Animator _animator;
    int _direction = 1;
    bool _broken;


    void Start()
    {
        _timer = changeTime;
        _broken = true;

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!_broken)
            return;

        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            _direction = -_direction;
            _timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        if (!_broken)
            return;

        Vector2 position = _rigidbody2D.position;

        if (vertical)
        {
            _animator.SetFloat("MoveX", 0);
            _animator.SetFloat("MoveY", _direction);

            position.y += Time.deltaTime * speed * _direction;
        }
        else
        {
            _animator.SetFloat("MoveX", _direction);
            _animator.SetFloat("MoveY", 0);
            position.x += Time.deltaTime * speed * _direction;
        }

        _rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<RubyController>(out var rubyController))
            rubyController.ChangeHealth(Damage);
    }

    public void Fix()
    {
        _broken = false;
        _rigidbody2D.simulated = false;

        _animator.SetTrigger("Fixed");
    }
}