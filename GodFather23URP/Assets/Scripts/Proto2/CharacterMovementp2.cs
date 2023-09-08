using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementp2 : MonoBehaviour
{
    public float _spiderSpeed;
    float _spiderSpeedBoost;
    Rigidbody2D _rb2d;
    Vector2 _position;
    Vector2 velocity = Vector2.zero;

    [SerializeField] float _distanceMin =.05f;

    private Player player;

    [SerializeField] private GameObject gfx;
    
    private Animator _animator;
    [SerializeField] AnimationCurve _dash;
    [SerializeField] float _dashDuration;
    [SerializeField] float _dashAmplifier;
    
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _position = new Vector2(Camera.main.transform.position.x - Camera.main.pixelWidth/2, Camera.main.transform.position.y - Camera.main.pixelHeight/2);

        _animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
       
        PlayerMovement();
        
    }
    public IEnumerator Dash()
    {
        float _duration = 0;
        while (_duration < _dashDuration)
        {
            _duration += Time.deltaTime;
            _spiderSpeedBoost = _dash.Evaluate(_duration / _dashDuration) * _dashAmplifier;
            yield return null;
        }
    }
    void PlayerMovement()
    {
        //renvoie la position de la souris en fonction du monde
        Vector3 _mousePosition = new Vector2(Input.mousePosition.x + Camera.main.transform.position.x - Camera.main.pixelWidth / 2, Input.mousePosition.y + Camera.main.transform.position.y - Camera.main.pixelHeight / 2);
        //on scale avec la size de la camera
        _mousePosition *= Camera.main.orthographicSize * 2 / Camera.main.pixelHeight;

        Vector3 _direction = (_mousePosition - transform.position).normalized;
        //Debug.Log(Mathf.Abs((_mousePosition - transform.position).magnitude));
        
        _rb2d.constraints = GameManager._instance.IsInBoss ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;

        bool isWalking = Mathf.Abs(_direction.x) >= 0.15f || Mathf.Abs(_direction.y) >= 0.15f;
        _animator.SetBool("IsWalking",isWalking);

        //Debug.Log(_spiderSpeed + _spiderSpeedBoost);
        if ((_mousePosition - transform.position).magnitude > _distanceMin)
            _rb2d.velocity = _direction * (_spiderSpeed + _spiderSpeedBoost);
        else
            _rb2d.velocity = Vector3.zero;
        
        if (_direction != Vector3.zero && !GameManager._instance.IsInBoss)
        {
            float angle = Mathf.Atan2(-_direction.x, _direction.y) * Mathf.Rad2Deg;
            gfx.transform.rotation = Quaternion.Euler(0,0,angle);
            
        }

        //transform.position = Vector2.SmoothDamp(transform.position, _mousePosition, ref velocity, 1/_spiderSpeed);
    }
}
