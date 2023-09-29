using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using RosMessageTypes.Geometry;
using RosMessageTypes.Std;
using RosMessageTypes.BuiltinInterfaces;
using PointMsg = RosMessageTypes.Geometry.PointMsg;
using QuaternionMsg = RosMessageTypes.Geometry.QuaternionMsg;

public class AMCLSubscriber : MonoBehaviour
{
    [SerializeField] string rosTopicName = "amcl_pose";

    private Vector3 robotAMCLPosition = new Vector3();
    private Quaternion robotAMCLPosture = new Quaternion();

    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<PoseWithCovarianceStampedMsg>(rosTopicName, AMCLPoseUpdate);
    }

    void AMCLPoseUpdate(PoseWithCovarianceStampedMsg AMCLPoseMessage)
    {
        PointMsg rosOdomPositionMsg = AMCLPoseMessage.pose.pose.position;
        QuaternionMsg rosOdomPostureMsg = AMCLPoseMessage.pose.pose.orientation;

        // 座標変換
        robotAMCLPosition = rosOdomPositionMsg.From<FLU>();
        robotAMCLPosture = rosOdomPostureMsg.From<FLU>();
    }

    public Vector3 GetRobotAMCLPosition()
    {
        return robotAMCLPosition;
    }

    public Quaternion GetRobotAMCLPosture()
    {
        return robotAMCLPosture;
    }
}
