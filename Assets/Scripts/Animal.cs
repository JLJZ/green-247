using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SpriteRenderer))]
public class Animal : MonoBehaviour
{

    [Min(0.0f)]
    [SerializeField] float _wanderingSpeed = 1.0f;

    [Min(0.0f)]
    [SerializeField] float _maxIdleTime = 5.0f;

    [field: SerializeField] public Bounds WanderingBounds { get; set; }

    SpriteRenderer SpriteRenderer;
    Vector3 TargetPosition;
    float IdleTime;
    State CurrentState = State.Idle;

    public float WanderingSpeed
    {
        get => _wanderingSpeed;
        set => _wanderingSpeed = Mathf.Max(value, 0);
    }

    public float MaxIdleTime
    {
        get => _maxIdleTime;
        set => _maxIdleTime = Mathf.Max(value, 0);
    }

    public Sprite Sprite
    {
        get => SpriteRenderer.sprite;
        set => SpriteRenderer.sprite = value;
    }

    void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        switch (CurrentState)
        {
            case State.Idle:
                Idle(Time.fixedDeltaTime);
                break;
            case State.Wandering:
                Wander(Time.fixedDeltaTime);
                break;
        }
    }

    static Vector3 ChooseRandomTargetPosition(Bounds bounds, float z)
    {
        var x = Random.Range(bounds.min.x, bounds.max.x);
        var y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(x, y, z);
    }

    void Wander(float deltaTime)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            TargetPosition,
            WanderingSpeed * deltaTime);

        if (Vector3.Distance(transform.position, TargetPosition) < float.Epsilon)
        {
            TargetPosition = ChooseRandomTargetPosition(
                WanderingBounds,
                transform.position.z);

            var xDirection = TargetPosition.x - transform.position.x;
            SpriteRenderer.flipX = xDirection > 0;

            CurrentState = State.Idle;
        }
    }

    void Idle(float deltaTime)
    {
        IdleTime -= deltaTime;

        if (IdleTime < 0.0f)
        {
            IdleTime = Random.Range(0.0f, MaxIdleTime);
            CurrentState = State.Wandering;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(WanderingBounds.center, WanderingBounds.size);
    }

    enum State
    {
        Idle,
        Wandering,
    }
}
