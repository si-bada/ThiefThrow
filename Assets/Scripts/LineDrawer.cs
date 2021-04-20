using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LineDrawer : MonoBehaviour
{
    public List<Transform> PointsList = new List<Transform>();
    public GameObject Thiefs;

    private DashedCurve mDashedCurve;
    private Camera mCamera;
    void Start()
    {
        mDashedCurve = GameController.Get.DashedCurve;
        mCamera = GameController.Get.Camera;
        mDashedCurve.DrawFirstLine();
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            var angle = Mathf.Clamp01((Input.mousePosition.x / Screen.width)) - 0.5f;
            if(Mathf.Abs(angle) >= 0.005)
            {
                if ((mDashedCurve.handle1.x + angle) <= 4 && (mDashedCurve.handle1.x + angle) >= -4)
                {
                   mDashedCurve.handle1 = new Vector3(10 * angle, Thiefs.transform.position.y + Camera.main.transform.position.y, 0);
                }
            }
            mDashedCurve.DrawLine(new Vector3(angle, 0, 0));
        }
        else if (Input.GetMouseButtonUp(0))
        {
            GameController.Get.LaunchBall();
        }
    }
}
