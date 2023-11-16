using System.Collections;
using System.Text;
using System.IO;
using UnityEngine.Serialization;
using System.Security.Permissions;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using UnityEngine;
using System.Globalization;

public class OutputHandPosition : MonoBehaviour, IMixedRealityTouchHandler
{
    private string appStartTime;
    private string dataFolderPath;
    private string logFilePath;
    private StreamWriter logStreamWriter;
    private float timeSinceLastPrint = 0f;  // ONLY ADDED TO DELAY PRINTING TO CONSOLE
    public float printDelay = 2f; // Delay in seconds between prints  ONLY ADDED TO DELAY PRINTING TO CONSOLE

    public TrackedHandJoint ReferenceJoint { get; set; } = TrackedHandJoint.IndexTip;
    private Vector3 offset = Vector3.zero;
    private Handedness recordingHand = Handedness.None;
    private float touchEventCooldown = 1.0f; // Cooldown period in seconds
    private float lastTouchEventTime = -1.0f; // The time at which the last event was handled


    private void Start()
    {
        // Store the start time when the script is first run as a DateTime object
        appStartTime = DateTime.Now.ToString("HH:mm:ss.fff");
        dataFolderPath = UnityEngine.Application.persistentDataPath;

        logFilePath = dataFolderPath + "/" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "_log.txt";
        logStreamWriter = new StreamWriter(logFilePath, false);


        Debug.Log("Testing the file");
        Debug.Log($"Filepath: {logFilePath}");
        logStreamWriter.WriteLine("Filepath: " + logFilePath);
        logStreamWriter.Flush();
        logStreamWriter.WriteLine("Start Time: " + appStartTime);
        logStreamWriter.Flush();

        //////// GameObject roundbuttons = GameObject.Find("MixedRealitySceneContent/PressExamples/RoundButtons");  //// USE THIS TO FIND OUR HAND OBJECTS VALUES

    }

    private void Update()
    {
        timeSinceLastPrint += Time.deltaTime; //  ONLY ADDED TO DELAY PRINTING TO CONSOLE
        if (timeSinceLastPrint >= printDelay) // ONLY ADDED TO DELAY PRINTING TO CONSOLE
        {
            timeSinceLastPrint = 0f; //  ONLY ADDED TO DELAY PRINTING TO CONSOLE

            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            float difference = CalculateTimeDifference(appStartTime, timestamp);

            logStreamWriter.WriteLine($"Update Time: {difference}");
            logStreamWriter.Flush();

            RecordMugPosition();
            RecordLeftHandStart();
            RecordHandStop("Left");
            RecordRightHandStart();
            RecordHandStop("Right");
        }
    }

    public void RecordLeftHandStart()
    {
        RecordHandStart(Handedness.Left);
    }

    public void RecordRightHandStart()
    {
        RecordHandStart(Handedness.Right);
    }

    private void RecordHandStart(Handedness handedness)
    {
        HandJointUtils.TryGetJointPose(ReferenceJoint, handedness, out MixedRealityPose joint);
        offset = joint.Position;
        recordingHand = handedness;
    }

    public void RecordHandStop(string handedness)
    {
        MixedRealityPose[] jointPoses = new MixedRealityPose[ArticulatedHandPose.JointCount];
        for (int i = 0; i < ArticulatedHandPose.JointCount; ++i)
        {
            HandJointUtils.TryGetJointPose((TrackedHandJoint)i, recordingHand, out jointPoses[i]);

            string jointName = ((TrackedHandJoint)i).ToString();

            logStreamWriter.WriteLine($"\t\t\t{handedness} {jointName} Position: {jointPoses[i].Position}");
            logStreamWriter.Flush();
            logStreamWriter.WriteLine($"\t\t\t{handedness} {jointName} Rotation: {jointPoses[i].Rotation.eulerAngles}");
            logStreamWriter.Flush();
        }

        ArticulatedHandPose pose = new ArticulatedHandPose();
        pose.ParseFromJointPoses(jointPoses, recordingHand, Quaternion.identity, offset);

        recordingHand = Handedness.None;

       // var filename = String.Format("{0}-{1}.json", OutputFileName, DateTime.UtcNow.ToString("yyyyMMdd-HHmmss"));
       // StoreRecordedHandPose(pose.ToJson(), filename);
    }

    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        if (Time.time >= lastTouchEventTime + touchEventCooldown)
        {
            lastTouchEventTime = Time.time; // Update the last event time

            if (eventData.Controller is IMixedRealityHand hand)
            {
                if (hand.TryGetJoint(TrackedHandJoint.IndexTip, out MixedRealityPose pose))
                {
                    // Get the collider of the coffee cup
                    Collider collider = GetComponent<Collider>();
                    Vector3 touchPoint = collider.ClosestPoint(pose.Position);

                    // Log the touch point
                    LogTouchPoint(touchPoint);
                }
            }
        }
    }

    public void OnTouchUpdated(HandTrackingInputEventData eventData)
    {
        if (Time.time >= lastTouchEventTime + touchEventCooldown)
        {
            lastTouchEventTime = Time.time; // Update the last event time

            if (eventData.Controller is IMixedRealityHand hand)
            {
                if (hand.TryGetJoint(TrackedHandJoint.IndexTip, out MixedRealityPose pose))
                {
                    // Get the collider of the coffee cup
                    Collider collider = GetComponent<Collider>();
                    Vector3 touchPoint = collider.ClosestPoint(pose.Position);

                    // Log the touch point
                    LogTouchPoint(touchPoint);
                }
            }
        }
    }

    public void OnTouchCompleted(HandTrackingInputEventData eventData)
    {
        if (Time.time >= lastTouchEventTime + touchEventCooldown)
        {
            lastTouchEventTime = Time.time; // Update the last event time

            // Handle the touch completion
            Debug.Log("Touch completed");
            logStreamWriter.WriteLine($"Touch completed at {DateTime.Now:HH:mm:ss.fff}");
            logStreamWriter.Flush();
        }
    }

    public void RecordMugPosition()
    {
        Vector3 cupPosition = transform.position; // This is the global position
        logStreamWriter.WriteLine($"\tCoffee Cup Position: {cupPosition}");
        logStreamWriter.Flush();

    }

    private void LogTouchPoint(Vector3 touchPoint)
    {
        string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
        logStreamWriter.WriteLine($"\t\tTouch Point at {timestamp}: {touchPoint}");
        logStreamWriter.Flush();
    }

    public static float CalculateTimeDifference(string timestamp1Str, string timestamp2Str)
    {

        DateTime timestamp1 = DateTime.ParseExact(timestamp1Str, "HH:mm:ss.fff", CultureInfo.InvariantCulture);
        DateTime timestamp2 = DateTime.ParseExact(timestamp2Str, "HH:mm:ss.fff", CultureInfo.InvariantCulture);

        TimeSpan timeDifference = timestamp2 - timestamp1;
        float timeDifferenceInSeconds = (float)timeDifference.TotalMilliseconds / 1000.0f;

        return timeDifferenceInSeconds;
    }

}
