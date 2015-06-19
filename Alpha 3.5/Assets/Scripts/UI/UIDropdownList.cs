using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIDropdownList : MonoBehaviour  
{
	[SerializeField] private RectTransform _dropdownFieldTransform;
	[SerializeField] private GameObject _dropdownFieldObject;
	[SerializeField] private GameObject _dropdownButtonPrefab;
	[SerializeField] private Selectable _dropdownFieldSelecable;
	[SerializeField] private float _buttonHeight;
	[SerializeField] private Slider _slider;
	[SerializeField] private int _valueIndex;
	[SerializeField] private int _buttonsPerPage;
	[SerializeField] private Text _mainButtonLabel;
	
	private Text[] _labels;
	private bool _cursorOverSlider;
	private int _buttonsCount;
	protected object[] _values;

	private void Awake ()
	{
		if (_values == null)
		{
			InitiateValues();
			GenerateButtons();
			Select(_valueIndex);
		}
	}

	public void OnMainButtonClick ()
	{
		if (_dropdownFieldObject.activeSelf == false)
		{
			Unfold();
			_dropdownFieldSelecable.Select();
		}
		else if (_dropdownFieldObject.activeSelf == true)
		{
			Fold();
		}
	}
	
	public void OnSliderMove ()
	{
		UpdateList();
	}

	public void OnDropdownButtonSelect (BaseEventData eventData)
	{
		Select(int.Parse(eventData.selectedObject.name));
	}

	public void OnCursorEnterSlider ()
	{
		_cursorOverSlider = true;
	}

	public void OnCursorExitSlider ()
	{
		_cursorOverSlider = false;
	}

	public void Select (int valueIndex)
	{
		int firstValueIndex = (int)_slider.value;
		_valueIndex = valueIndex;
		_mainButtonLabel.text = _values[valueIndex].ToString();
		UpdateList();
		Fold();
	}

	public void UpdateList ()
	{
		int firstValueIndex = (int)_slider.value;
		if (_valueIndex > (int)_slider.maxValue && firstValueIndex > 0)
		{
			firstValueIndex--;
		}
		for (int v = firstValueIndex, l = 0; l < _buttonsCount; v++)
		{
			if (v != _valueIndex)
			{
				_labels[l].text = _values[v].ToString();
				_labels[l].name = v.ToString();
				l++;
			}
		}
	}

	public void OnDeselect ()
	{
		StartCoroutine("DeselectCoroutine");
	}

	public IEnumerator DeselectCoroutine ()
	{
		yield return null;
		if (!_cursorOverSlider)
		{
			Fold();
		}
		else 
		{
			_dropdownFieldSelecable.Select();
		}
	}

	public void Unfold ()
	{
		_dropdownFieldObject.SetActive(true);
	}

	public void Fold ()
	{
		_dropdownFieldObject.SetActive(false);
	}

	public object GetValue ()
	{
		if (_values == null)
		{
			Awake();
		}
		return _values[_valueIndex];
	}

	public void SetValueFromString (string valueName)
	{
		for (int i = 0; i < _values.Length; i++)
		{
			if (_values[i].ToString() == valueName)
			{
				Select(i);
			}
		}
	}

	private void GenerateButtons ()
	{
		_buttonsCount = Mathf.Min(_values.Length - 1, _buttonsPerPage);
		_labels = new Text[_buttonsCount];

		//Find range of the slider.
		int firstValueIndexRange = _values.Length - _buttonsPerPage;

		//Enabling slider if needed.
		if (firstValueIndexRange > 0)
		{
			_slider.gameObject.SetActive(true);
			_slider.maxValue = firstValueIndexRange;
		}
		else
		{
			_slider.gameObject.SetActive(false);
		}

		//Generating buttons.
		for (int i = 0; i < _buttonsCount; i++)
		{
			GameObject buttonObject = Instantiate(_dropdownButtonPrefab, _dropdownButtonPrefab.transform.position, _dropdownButtonPrefab.transform.rotation) as GameObject;
			Transform buttonTransform = buttonObject.transform;
			EventTrigger trigger = buttonObject.GetComponent<EventTrigger>();
			
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.Select;
			UnityEngine.Events.UnityAction<BaseEventData> callback = new UnityEngine.Events.UnityAction<BaseEventData>(OnDropdownButtonSelect);
			entry.callback.AddListener( callback );
			trigger.delegates.Add(entry);
			
			_labels[i] = buttonObject.GetComponent<Text>();
			buttonTransform.Translate(0f, -_buttonHeight * i, 0f, Space.Self);
			buttonTransform.SetParent(_dropdownFieldTransform, true);
			buttonObject.SetActive(true);
		}

		//All this code is for changing single height value of dropdown field.
		Vector2 newSize =  new Vector2(_dropdownFieldTransform.rect.size.x, _buttonHeight * _buttonsPerPage);
		Vector2 oldSize = _dropdownFieldTransform.rect.size;
		Vector2 deltaSize = newSize - oldSize;
		_dropdownFieldTransform.offsetMin = _dropdownFieldTransform.offsetMin - new Vector2(deltaSize.x * _dropdownFieldTransform.pivot.x, deltaSize.y * _dropdownFieldTransform.pivot.y) * 2f;
	}

	protected virtual void InitiateValues ()
	{

	}
}
