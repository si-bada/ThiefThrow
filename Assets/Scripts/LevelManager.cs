using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	#region Static
	public static LevelManager Get { get { return sInstance; } }
	private static LevelManager sInstance;
	#endregion

	public LineDrawer[] LevelsPref;
	public Vector3 offset;

	private GameObject mBall;
	private Vector3 newPosition;
	private Vector3 startPosition;
	public List<Transform> Levels = new List<Transform>();
	private float buildLength = 49.7f;

	void Start()
	{
		GenerateLevels();
	}

	public void GenerateLevels()
	{
		startPosition = LevelsPref[0].PointsList[0].position;
		mBall = GameController.Get.Ball;
		mBall.transform.position = startPosition;
		for (int i = 0; i < LevelsPref.Length; i++)
		{
			newPosition.y = i * buildLength;
			//var level = Instantiate(LevelsPref[Random.Range(0, LevelsPref.Length)], newPosition + offset, Quaternion.identity) ;
			var level = Instantiate(LevelsPref[i], newPosition + offset, Quaternion.identity);
			Levels.Add(level.transform);
		}
		Levels[0].GetComponent<LineDrawer>().enabled = true;
	}

	public void NextLevel()
    {
		var currentLevel = GameController.Get.GetCurrentLevel();
		var level = Levels[currentLevel - 3].transform;
		level.gameObject.SetActive(false);
	}
}
