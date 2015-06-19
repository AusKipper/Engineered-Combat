////#if UNITY_EDITOR
//using UnityEngine;
//using UnityEditor;
//using UnityEditor.Callbacks;
//using System.Collections;
//
//[InitializeOnLoad]
//[CustomEditor(typeof(Game))]
//public class GameInspector : Editor
//{
//	//[SerializeField] private LevelData[] _levels;
//	//[SerializeField] private Game _game;
//	static Game _game;
//
//	private void OnEnable ()
//	{
//		_game = (Game)target;
//	}
//
////	[PostProcessScene]
////	public static void OnPostprocessScene ()//BuildTarget btarget, string pathToBuiltProject) 
////	{
////		//Resources.
////		//Game game = GameObject.Find("#Game").GetComponent<Game>();
////		//Game game = _game;
////		Debug.Log(GameObject.Find("#Game"));
//////		foreach (LevelData data in game.GetLevels())
//////		{
//////			Debug.Log(data._name);
//////		}
////		//Debug.Log( pathToBuiltProject );
////	}
////	public void OnEnable ()
////	{
////		if (Application.isEditor)
////		{
////			foreach (LevelData data in _game.GetLevels())
////			{
////
////			}
////		}
////	}
////
////	public void OnDisable ()
////	{
////
////	}
//}
////#endif
