using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public LayerMask hitLayer;
    public BoxCollider hitCollider;
    public float moveSpeed = 50;
    public float rotationSpeed = 500;
    public float minChargeForce = 50, maxChargeForce = 500;
    public float hitMultiplier = 2f;
    public float stunDuration = 1f;
    public float miniStunDuration = 0.1f;

    [SerializeField]
    Animator _animator;
    PlayerInput playerInput;
    Rigidbody _rigidbody;
    Collider _collider;
    float _chargeForceTimer;
    bool _isCharging, _chargeBlocked, _isAttacking;
    IEnumerator _courutine;

    public Rigidbody Rigidbody { get { { return _rigidbody; } } }
    public Collider Collider { get { { return _collider; } } }

    public GameObject hitParticleSpawnPoint;
    public GameObject hitParticle;

    public GameObject stunParticleSpawnPoint;

	private bool miniStunState = false;
    private bool isFalling = false;


	public float fallY = -1;
	public AudioSource attackSound;
	public AudioSource stunSound;
	public AudioSource fallSound;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = transform.GetChild(0).GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
		if(!isFalling && this.transform.position.y < fallY)
		{
			OnFall();

		}

		if (_animator.GetBool("Stun") || isFalling)
            return;

        Move();
        Rotate();
        Attack();
    }

   

    void Move()
    {
        var move = new Vector3(playerInput.GetAxis(PlayerInput.InputActions.Horizontal), 0, playerInput.GetAxis(PlayerInput.InputActions.Vertical));
        var lerp = 1 - _chargeForceTimer;
        _rigidbody.AddForce(move * moveSpeed * lerp);

        var magnitude = move.magnitude;
        _animator.SetFloat("Move", magnitude);
    }

    void Rotate()
    {
        var move = new Vector3(playerInput.GetAxis(PlayerInput.InputActions.Horizontal), 0, playerInput.GetAxis(PlayerInput.InputActions.Vertical));
        if (move == Vector3.zero) return;

        Quaternion wantedRotation = Quaternion.LookRotation(move);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, wantedRotation, rotationSpeed * Time.deltaTime);
    }


    void Attack()

    {

        Debug.Log(_animator.GetCurrentAnimatorStateInfo(1).IsName("Attack_2"));
        if (_animator.GetCurrentAnimatorStateInfo(1).IsName("Attack_2"))
        {
            if (_isAttacking)
            {
                var hit = Physics.OverlapBox(hitCollider.transform.position, hitCollider.size * 0.5f, hitCollider.transform.rotation);
                for (int i = 0; i < hit.Length; i++)
                {

                    var rb = hit[i].GetComponent<Rigidbody>();
                    if (rb && rb.gameObject != this.gameObject)
                    {
                        rb.AddForce(this.transform.forward * hitMultiplier);
                        var player = rb.GetComponent<PlayerController>();
                        if (player)
                        {
                            player.MiniStun();
                        }
                        _isAttacking = false;
                    }
                }
            }
            
        }
        else
        if (playerInput.GetButtonDown(PlayerInput.InputActions.Attack))
        {
            _isAttacking = true;
            _animator.SetTrigger("Attack");


			if (this.attackSound)
				this.attackSound.Play();
        }
            
       

    }
    void Charge()
    {
        if (_isCharging)
        {
            var hit = Physics.OverlapBox(hitCollider.transform.position, hitCollider.size * 0.5f, hitCollider.transform.rotation, hitLayer);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i] == _collider) continue;
                if (hit[i] == _collider) continue;

                var force = _rigidbody.velocity * hitMultiplier;
                hit[i].transform.parent.GetComponent<PlayerController>().Push(force);
                Push(transform.forward * -5);
                _chargeForceTimer = 0;
                _isCharging = false;
               

             //   hit[i].transform.parent.GetComponent<PlayerController>().Push(force);

             
            }
            return;
        }

        if (_chargeBlocked) return;

        if (playerInput.GetButtonUp(PlayerInput.InputActions.Attack))
        {
            _animator.SetBool("Attack", false);

            _isCharging = true;

            var lerp = _chargeForceTimer;
            var force = Mathf.Lerp(minChargeForce, maxChargeForce, lerp);
            _rigidbody.velocity = transform.forward * force;

            StartCoroutine(BlockCharge());

            return;
        }

        if (playerInput.GetButton(PlayerInput.InputActions.Attack))
        {
            if (!_animator.GetBool("Attack")) _animator.SetBool("Attack", true);
            _chargeForceTimer += Time.deltaTime;
            _chargeForceTimer = Mathf.Clamp(_chargeForceTimer, 0, 1);
        }
    }

    public void Push(Vector3 force)
    {
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = false;

        _rigidbody.velocity = force;
    }

    public void Stun()
    {
        if (_courutine != null) StopCoroutine(_courutine);
        _courutine = StunPlyer(stunDuration);
        StartCoroutine(_courutine);
    }
    public void MiniStun()
    {
        if (_courutine != null) StopCoroutine(_courutine);
        {
            miniStunState = true;
            _courutine = StunPlyer(miniStunDuration);
            Instantiate(hitParticle, hitParticleSpawnPoint.transform);
            
        }
            StartCoroutine(_courutine);
    }
    public void AddAttackForce()
    {
        Debug.Log("XX");
    }

    IEnumerator StunPlyer(float  duration)
    {
        _animator.SetBool("Stun", true);

		if (this.stunSound)
			this.stunSound.Play();
		
        if (miniStunState != true)
        stunParticleSpawnPoint.SetActive(true);
        yield return new WaitForSeconds(duration);
        miniStunState = false;
        stunParticleSpawnPoint.SetActive(false);
        _animator.SetBool("Stun", false);
    }

    void OnFall()
	{
		if (this.fallSound)
			this.fallSound.Play();
    
		_animator.SetFloat("Move", 0);
	}

    IEnumerator BlockCharge()
    {
        _chargeBlocked = true;
        yield return new WaitForSeconds(1f);
        _chargeBlocked = false;
    }
}
