using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Static
    public static GameController Get { get { return sInstance; } }
    private static GameController sInstance;
    #endregion

    public Camera Camera;
    public DashedCurve DashedCurve;
    public int speed = 0;
    public GameObject Ball;
    public LevelManager LevelsManager;

    private int mCurrentPoint=0;
    private LTSpline splines;
    private List<Vector3> points = new List<Vector3>();
    private int mCurrentLevel=0;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        sInstance = this;
    }

    private void Start()
    {
        UpdateTarget();
    }
    void LateUpdate()
    {
        if (60 <= Application.targetFrameRate + 5)
            Application.targetFrameRate = 60;
    }
    public void UpdateTarget()
    {
        if (mCurrentPoint + 1 >= LevelsManager.LevelsPref[mCurrentLevel].PointsList.Count)
        {
            NextLevel();
        }
        else
        {
            DashedCurve.start = LevelsManager.LevelsPref[mCurrentLevel].PointsList[mCurrentPoint].position;
            DashedCurve.end = LevelsManager.LevelsPref[mCurrentLevel].PointsList[mCurrentPoint + 1].position;
        }
    }

    private void NextLevel()
    {
        Debug.LogWarning("next level");
        DashedCurve.start = LevelsManager.LevelsPref[mCurrentLevel].PointsList[mCurrentPoint].position;
        DashedCurve.end = LevelsManager.LevelsPref[mCurrentLevel+1].PointsList[0].position;
        mCurrentLevel++;
        mCurrentPoint = 0;
        LevelsManager.Levels[mCurrentLevel].GetComponent<LineDrawer>().enabled = true;
        LevelsManager.Levels[mCurrentLevel - 1].GetComponent<LineDrawer>().enabled = false;

        if (mCurrentLevel >= 3)
        {
            LevelManager.Get.NextLevel();
        }
    }

    private void MoveCamera()
    {
        Camera.transform.LeanMove(new Vector3(Camera.transform.position.x, Camera.transform.position.y + 15, Camera.transform.position.z), 0.5f);
        UpdateTarget();
    }

    public void LaunchBall()
    {
        UpdatePoints();
        StartCoroutine(LaunchBallCoroutine());
    }
    void UpdatePoints()
    {
        points = DashedCurve.GetPoints();
        splines = new LTSpline(points.ToArray());
    }
    IEnumerator LaunchBallCoroutine()
    {
        LeanTween.move(Ball, splines, speed * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        mCurrentPoint++;
        Ball.transform.position = LevelsManager.LevelsPref[mCurrentLevel].PointsList[mCurrentPoint].position;
        MoveCamera();
    }

    public int GetCurrentLevel()
    {
        return mCurrentLevel;
    }

}
