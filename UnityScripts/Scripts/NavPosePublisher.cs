using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;
using RosMessageTypes.Std;
using RosMessageTypes.BuiltinInterfaces;

public class NavPosePublisher : MonoBehaviour
{
    ROSConnection _ros;
    RaycastHit hit;
    LineRenderer line;
    bool isMouseButtonDown = false;
    Vector3 mousePositionStart = new Vector3(0, 0, 0);
    Vector3 mousePositionEnd = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        _ros = ROSConnection.GetOrCreateInstance();
        _ros.RegisterPublisher<PoseWithCovarianceStampedMsg>("/initialpose");
        _ros.RegisterPublisher<PoseStampedMsg>("/goal_pose");
        SendInitialPose();

        // 線を引くための設定
        line = gameObject.AddComponent<LineRenderer>();
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.SetPosition(0, mousePositionStart);
        line.SetPosition(1, mousePositionEnd);
    }

    // Update is called once per frame
    void Update()
    {
        // マウスを左クリックした瞬間
        if (Input.GetMouseButtonDown(0)) {
            mousePositionStart = GetMousePosition();
            isMouseButtonDown = true;
        }

        // クリック中
        if (isMouseButtonDown) {
            mousePositionEnd = GetMousePosition();
            line.SetPosition(0, mousePositionStart);
            line.SetPosition(1, mousePositionEnd);
        }

        // クリックを離した瞬間
        if (Input.GetMouseButtonUp(0)) {
            isMouseButtonDown = false;
            SendGoalPose(mousePositionStart, mousePositionEnd);
        }
    }

    public void SendInitialPose() {
        HeaderMsg header = new HeaderMsg();
        header.stamp.sec = 0;
        header.stamp.nanosec = 0;
        header.frame_id = "map";

        PoseWithCovarianceMsg pose = new PoseWithCovarianceMsg();
        pose.pose.position.x = 0.0;
        pose.pose.position.y = 0.0;
        pose.pose.position.z = 0.0;
        pose.pose.orientation.x = 0.0;
        pose.pose.orientation.y = 0.0;
        pose.pose.orientation.z = 0.0;
        pose.pose.orientation.w = 1.0;

        PoseWithCovarianceStampedMsg msg = new PoseWithCovarianceStampedMsg(header, pose); 
        _ros.Publish("/initialpose", msg);
    }

    public Vector3 GetMousePosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            var hitPos = hit.point;
            hitPos.y = 0f;
            return hitPos;
        }
        return new Vector3(0f, -100f, 0f);
    }

    public void SendGoalPose(Vector3 mouseStart, Vector3 mouseEnd) {
        // 角度の計算
        Vector3 diff = mouseEnd - mouseStart;
        diff.y = 0;
        var mouseRotation = Quaternion.LookRotation(diff, Vector3.up);

        HeaderMsg header = new HeaderMsg();
        header.stamp.sec = 0;
        header.stamp.nanosec = 0;
        header.frame_id = "map";

        PoseMsg pose = new PoseMsg();
        pose.position.x = mouseStart.z;
        pose.position.y = -mouseStart.x;
        pose.position.z = 0.0;
        pose.orientation.x = mouseRotation.z;
        pose.orientation.y = -mouseRotation.x;
        pose.orientation.z = mouseRotation.y;
        pose.orientation.w = -mouseRotation.w;

        Debug.Log(pose);

        PoseStampedMsg msg = new PoseStampedMsg(header, pose); 
        _ros.Publish("/goal_pose", msg);
    }
}
