using UnityEngine;
using System.Collections;

public class LevelsController : MonoBehaviour
{

	public static Level[] levels = new Level[] {
		new Level (1, "level1", true),
		new Level (2, "level2", true),
		new Level (3, "level3", true),
		new Level (4, "level4", true),
		new Level (5, "level5", true),
		new Level (6, "level6", true)
	};

	public static void OpenLevel(int levelNumber){

		if (levelNumber <= levels.Length) {

			levels [levelNumber - 1].isOpened = true;

		}

	}

	public static string GetLevelName(int levelnumber){

		return levels [levelnumber - 1].levelName;

	}

	public static void StartLevel(int levelNumber){

		SimpleSceneFader.ChangeSceneWithFade (LevelsController.GetLevelName (levelNumber), 0.5f);
		AudioManager.instancia.ChangeToLevelAudio (levelNumber);
	
	}

	public static void LoadMenu(){

		SimpleSceneFader.ChangeSceneWithFade ("mainMenu", 1.5f);
		AudioManager.instancia.ChangeToMenuBMG ();

	}

	public static void LoadCreditos(){

		SimpleSceneFader.ChangeSceneWithFade ("creditos", 0.5f);

	}

}

public class Level{

	public int levelNumber;
	public string levelName;
	public bool isOpened;

	public Level(int _levelNumber, string _levelName, bool _isOpened){
		levelNumber = _levelNumber;
		levelName = _levelName;
		isOpened = _isOpened;
	}

}

