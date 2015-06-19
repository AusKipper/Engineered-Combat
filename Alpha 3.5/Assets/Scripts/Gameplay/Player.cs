using UnityEngine;

public class Player : MonoBehaviour 
{
	[SerializeField] private Transform _transform;
	[SerializeField] private Rigidbody _rigidbody;
	[SerializeField] private Transform _headTransform;
	[SerializeField] private Transform _cameraTransform;
	[SerializeField] private Transform _feetTransform;
	[SerializeField] private GameObject _cameraObject;
	[SerializeField] private Weapon _weapon;
	[SerializeField] private Camera _camera;
	[SerializeField] private float _speed;
	[SerializeField] private float _speedAir;
	[SerializeField] private float _sprintSpeedMultiplyer;
	[SerializeField] private float _lookSpeed;
	[SerializeField] private float _jumpSpeed;
	[SerializeField] private float _maxVelocity;
	[SerializeField] private float _velocityDamping;
	[SerializeField] private float _velocityDampingAir;
	[SerializeField] private LayerMask _feetLayermask;

	private bool _landed;
	private bool _jump;
	private float _cameraRotation;
	private GameObject _instance;

	private void Update ()
	{
		if (Time.timeScale != 0)
		{
			//SLOW MOTION
			if (Input.GetKeyDown(KeyCode.F))
			{
				if (Time.timeScale == 1f)
				{
					Time.timeScale = 0.5f;
				}
				else if (Time.timeScale == 0.5f)
				{
					Time.timeScale = 1f;
				}
			}
			//CAMERA MOVEMENT
			if ((Input.GetAxis("Mouse Y") > 0 && _cameraRotation >= -90) || (Input.GetAxis("Mouse Y") < 0 && _cameraRotation <= 90))
			{
				_cameraRotation += Input.GetAxis("Mouse Y") * -_lookSpeed * Time.deltaTime;
				if (_cameraRotation > 90f)
					_cameraRotation = 90;
				if (_cameraRotation < -90f)
					_cameraRotation = -90;
				_headTransform.localEulerAngles = new Vector3(_cameraRotation, 0f, 0f);
			}
			if (Input.GetAxis("Mouse X") != 0)
			{
				_transform.Rotate(0f, Input.GetAxis("Mouse X") * _lookSpeed * Time.deltaTime, 0f, Space.Self);
			}
			//MOUSE INPUT
			if (Input.GetKeyDown(KeyCode.Mouse0)) 
			{
				_weapon.PullTrigger();
			}
			if (Input.GetKeyUp(KeyCode.Mouse0)) 
			{
				_weapon.ReleaseTrigger();
			}
			if (Input.GetKeyDown(KeyCode.Mouse1)) 
			{
				_weapon.ToggleAim();
			}
			//JUMP
			if (Input.GetKeyDown(KeyCode.Space) && _landed)
			{
				_jump = true;
				_landed = false;
			}
			//RELOAD
			if (Input.GetKeyDown(KeyCode.R)) 
			{
				_weapon.Reload();
			}
		}
	}

	public void OnLevelWasLoaded ()
	{
		Spawn();
	}

	public void Spawn ()
	{
		Transform _spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
		//_instance = Instantiate(_spawnPoint.position, _spawnPoint.rotation);TODO: Consider instantiation.
		_transform.position = _spawnPoint.position;
		_transform.rotation = _spawnPoint.rotation;
		_weapon.Reset();
	}

	private void FixedUpdate ()
	{
		//GROUND TOUCH CHECK
		if (Physics.CheckSphere(_feetTransform.position, 0.3f, _feetLayermask))
		{
			_landed = true;
		}
		else
		{
			_landed = false;	
		}
		//MOVEMENT INPUT
		Vector3 moveVector = new Vector3(0f, 0f, 0f);
		if (Input.GetKey(KeyCode.W))
		{
			moveVector += Vector3.forward;
		}		
		if (Input.GetKey(KeyCode.S))
		{
			moveVector += Vector3.back;
		}
		if (Input.GetKey(KeyCode.A))
		{
			moveVector += Vector3.left;
		}
		if (Input.GetKey(KeyCode.D))
		{
			moveVector += Vector3.right;
		}
		moveVector = _transform.TransformDirection(moveVector).normalized;
		float sprint = 1f;
		if (Input.GetKey(KeyCode.LeftShift))
		{
			sprint = _sprintSpeedMultiplyer;
		}
		//MOVE
		Vector3 localVelocity = _rigidbody.velocity;
		//float localVelocityMagnitude = localVelocity.sqrMagnitude;
		Vector3 localTransformedVelocity = _transform.InverseTransformDirection(localVelocity);
		Vector3 horizontalVelocity = _transform.TransformDirection(new Vector3(localTransformedVelocity.x, 0f, localTransformedVelocity.z));

		if (_landed)
		{
			_rigidbody.velocity -= horizontalVelocity * (_velocityDamping * Time.deltaTime);
			_rigidbody.AddForce(moveVector * (_speed * sprint * Time.deltaTime), ForceMode.Acceleration);

			if (_jump)
			{
				//print("jump");
				_rigidbody.AddForce(_transform.up * (_jumpSpeed), ForceMode.Acceleration);
				_jump = false;
			}
		}
		else
		{
			_rigidbody.velocity -= horizontalVelocity * (_velocityDampingAir * Time.deltaTime);
			_rigidbody.AddForce(moveVector * (_speedAir * sprint * Time.deltaTime), ForceMode.Acceleration);
		}
	}

	private Weapon GetWeapon ()
	{
		return _weapon;
	}
}
