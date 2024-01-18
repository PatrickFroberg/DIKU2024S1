using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //TODO: lav til timer, hvis der er gået 4 sekunder destroy
        if (transform.position.magnitude > 1000.0f)
            Destroy(gameObject);
    }

    public void Launch(Vector2 direction, float force)
    {
        _rigidbody.AddForce(direction * force);   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<EnemyController>(out var e))
            e.Fix();

        Destroy(gameObject);
    }
}
