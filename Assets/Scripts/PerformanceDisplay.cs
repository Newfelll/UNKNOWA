using UnityEngine;
using UnityEngine.UI;

public class PerformanceDisplay : MonoBehaviour
{
    public Text fpsText;
    public Text msText;
    public Text onePercentLowText;
    public Text lowFramesText;
    public Text avgFrameTimeText;  // Display for average frame time
    public Text cpuUsageText;      // Display for CPU usage
    public Text memoryUsageText;   // Display for memory usage
    public Text gcText;            // Display for GC time

    private float[] frameTimes;
    private int frameCount;
    private float deltaTime;
    private int lowFrameCount = 0;

    private const int FrameRange = 100;
    public float fpsThreshold = 30f;

    // Variables for average frame time
    private float totalFrameTime = 0f;
    private int totalFrames = 0;

    // Variables for CPU usage
    private float simulationTime = 0f;

    // Variables for garbage collection tracking
    private float lastGCCollectionTime = 0f;

    void Start()
    {
        frameTimes = new float[FrameRange];

        if (fpsText == null || msText == null || onePercentLowText == null || lowFramesText == null || avgFrameTimeText == null ||
            cpuUsageText == null || memoryUsageText == null || gcText == null)
        {
            Debug.LogWarning("Some UI Text components are not assigned in the Inspector.");
        }
    }

    void Update()
    {
        // Calculate FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        // Display FPS and Frame Time (ms)
        if (fpsText != null) fpsText.text = string.Format("FPS: {0:0.}", fps);
        if (msText != null) msText.text = string.Format("Frame Time: {0:0.0} ms", deltaTime * 1000f);

        // Detect low FPS and increment counter
        if (fps < fpsThreshold)
        {
            lowFrameCount++;
            if (lowFramesText != null) lowFramesText.text = string.Format("Low FPS Frames: {0}", lowFrameCount);
        }

        // Store frame time for 1% low FPS calculation
        frameTimes[frameCount % FrameRange] = Time.unscaledDeltaTime;
        frameCount++;

        if (frameCount >= FrameRange)
        {
            if (frameCount % 10 == 0)
            {
                float[] sortedFrameTimes = (float[])frameTimes.Clone();
                System.Array.Sort(sortedFrameTimes);

                int index = Mathf.FloorToInt(FrameRange * 0.01f);
                float onePercentLow = 1.0f / sortedFrameTimes[index];

                if (onePercentLowText != null) onePercentLowText.text = string.Format("1% Low FPS: {0:0.}", onePercentLow);
            }
        }

        // -------------------- Additional Performance Metrics --------------------

        // Average Frame Time
        totalFrameTime += Time.unscaledDeltaTime;
        totalFrames++;
        float averageFrameTime = (totalFrameTime / totalFrames) * 1000f; // in milliseconds
        if (avgFrameTimeText != null) avgFrameTimeText.text = string.Format("Avg Frame Time: {0:0.0} ms", averageFrameTime);

        // CPU Usage (Simulation Time)
        simulationTime += Time.deltaTime; // Total time spent in Update() calls
        float cpuUsage = (simulationTime / Time.realtimeSinceStartup) * 100f; // CPU usage percentage
        if (cpuUsageText != null) cpuUsageText.text = string.Format("CPU Usage: {0:0.0}%", cpuUsage);

        // Memory Usage
        long memoryUsage = System.GC.GetTotalMemory(false) / (1024 * 1024); // Convert bytes to MB
        if (memoryUsageText != null) memoryUsageText.text = string.Format("Memory: {0} MB", memoryUsage);

        // Garbage Collection Time
        if (System.GC.GetTotalMemory(false) > 100 * 1024 * 1024) // Check if more than 100MB is allocated
        {
            float currentTime = Time.time;
            float gcInterval = currentTime - lastGCCollectionTime;

            lastGCCollectionTime = currentTime;

            if (gcText != null) gcText.text = string.Format("GC Interval: {0:0.0} sec", gcInterval);
        }
    }
}
