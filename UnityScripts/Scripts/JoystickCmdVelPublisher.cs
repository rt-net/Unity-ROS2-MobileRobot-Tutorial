/* CmdVelPublisher.csを参考 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using TwistMsg = RosMessageTypes.Geometry.TwistMsg;

public class JoystickCmdVelPublisher : MonoBehaviour
{
    [SerializeField] string topicName = "cmd_vel";
    private ROSConnection ros;
    private TwistMsg cmdVelMessage = new TwistMsg();
    public VariableJoystick variableJoystick;
    private float timeSum = 0.0f;
    private const float timeBase = 0.1f;

    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<TwistMsg>(topicName);
        cmdVelMessage.linear.x = 0.0f;
        cmdVelMessage.angular.z = 0.0f;
        Publish();
    }

    void Update()
    {
        timeSum += Time.deltaTime;
        if(timeSum >= timeBase) {
            timeSum = 0.0f;
            cmdVelMessage.linear.x = variableJoystick.Vertical * 0.2f;    
            cmdVelMessage.angular.z = variableJoystick.Horizontal * -1.0f;
            Publish();
        }
    }

    public void Publish()
    {
        ros.Publish(topicName, cmdVelMessage);
    }
}