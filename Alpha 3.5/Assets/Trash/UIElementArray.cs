//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//
//public class UIElementArray : MonoBehaviour 
//{
//	public RectTransform _transform;
//	public Game _game;
//	//public LevelButton[] _buttons;
//	public GameObject[] _pages;
//	public int _buttonsPerPage;
//	public int _buttonSpacing;
//	public GameObject _buttonPrefab;
//	public GameObject _pagePrefab;
//	private int _currentPageIndex;
//	private	int _numbeOfPages;
//	
//
//	public void Start ()
//	{
//		int numberOfButtons = _game._levels.Length;
//		_numbeOfPages = (int)(numberOfButtons / _buttonsPerPage) + 1;
//		_pages = new GameObject[_numbeOfPages];
//		for (int p = 0, b = 0; p < _numbeOfPages; p++)
//		{
//			GameObject page = Instantiate(_pagePrefab) as GameObject;
//			page.transform.SetParent(_transform, false);
//			_pages[p] = page;
//			if (p != 0)
//			{
//				page.SetActive(false);
//			}
//			for (int i = 0; i < _buttonsPerPage && b < numberOfButtons; i++, b++)
//			{
//				LevelButton button = (Instantiate(_buttonPrefab) as GameObject).GetComponent<LevelButton>();
//				button._transform.SetParent(page.transform, false);
//				button._index = i + p * _buttonsPerPage;
//				button.OnEnable();
//				button._transform.Translate(new Vector3(0f, -i * _buttonSpacing, 0f));
//			}
//		}
//	}
//
//	public void GoToNextPage ()
//	{
//		if (_currentPageIndex < _numbeOfPages - 1)
//		{
//			_pages[_currentPageIndex].SetActive(false);
//			_pages[++_currentPageIndex].SetActive(true);
//		}
//	}
//
//	public void GoToPreviousPage ()
//	{
//		if (_currentPageIndex > 0)
//		{
//			_pages[_currentPageIndex].SetActive(false);
//			_pages[--_currentPageIndex].SetActive(true);
//		}
//	}
//
//	public void OnEnable () 
//	{
//	}
//}
