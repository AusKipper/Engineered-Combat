using UnityEngine;
using System.Collections;

public class UICrosshair : MonoBehaviour 
{
	[SerializeField]
	private GameObject _gameObject;
	[SerializeField]
	private Transform _left;
	[SerializeField]
	private Transform _right;
	[SerializeField]
	private Transform _upper;
	[SerializeField]
	private Transform _lower;
	[SerializeField]
	private float _spreadPerUnit;
	[SerializeField]
	private float _offset;

	public void OnEnable ()
	{
	}

	public void SetSpread (int spread)
	{
		_left.localPosition = new Vector3(-(_offset + (spread * _spreadPerUnit)), 0f, 0f);
		_right.localPosition = new Vector3((_offset + (spread * _spreadPerUnit)), 0f, 0f);
		_upper.localPosition = new Vector3(0f, (_offset + (spread * _spreadPerUnit)), 0f);
		_lower.localPosition = new Vector3(0f, -(_offset + (spread * _spreadPerUnit)), 0f);
		//_left.position = new Vector3(0f, -(_offset + (spread * _spreadPerUnit)), 0f);
		//_right.position = (_offset + (spread * _spreadPerUnit));
		//_right.position = (_offset + (spread * _spreadPerUnit));
	}

	public void Disable ()
	{
		_gameObject.SetActive(false);
	}

	public void Enable ()
	{
		_gameObject.SetActive(true);
	}
}
