using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using RosMessageTypes.Geometry;
using RosMessageTypes.Std;
using RosMessageTypes.BuiltinInterfaces;
using PointMsg = RosMessageTypes.Geometry.PointMsg;
using QuaternionMsg = RosMessageTypes.Geometry.QuaternionMsg;

public class AMCLPoseViewer : MonoBehaviour
{
    [SerializeField] int lengthOfHistory = 10;
    [SerializeField] float AMCLPoseDistanceThreshold = 0.03f;
    [SerializeField] GameObject subscriberGameObject;

    [SerializeField] GameObject arrowPrefab;

    private List<GameObject> arrowList;

    private Vector3 previousRobotAMCLPosition = new Vector3();

    void Start()
    {
        arrowList = new List<GameObject>(lengthOfHistory);
    }

    void Update()
    {
        Vector3 currentRobotAMCLPosition = subscriberGameObject.GetComponent<AMCLSubscriber>().GetRobotAMCLPosition();
        Quaternion currentRobotAMCLPosture = subscriberGameObject.GetComponent<AMCLSubscriber>().GetRobotAMCLPosture();

        if (Vector3.Distance(currentRobotAMCLPosition, previousRobotAMCLPosition) > AMCLPoseDistanceThreshold)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.transform.parent = gameObject.transform;
            arrow.transform.position = currentRobotAMCLPosition;
            arrow.transform.rotation = currentRobotAMCLPosture;
            arrow.transform.Rotate(0, 180, 0); // ロボットの向きに合わせて矢印を回転
            if (arrowList.Count >= lengthOfHistory) {
                Destroy(arrowList[0]);
                arrowList.RemoveRange(0, 1);
            }
            arrowList.Add(arrow);
            previousRobotAMCLPosition = currentRobotAMCLPosition;
        }
    }
}
