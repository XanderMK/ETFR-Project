using System.IO;
using UnityEngine;

public class WritePerformanceStats : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private int framesToWaitBeforeCapture = 8;
    private string path;

    private void OnEnable()
    {
        path = Application.persistentDataPath + '/' + fileName;

        if (!File.Exists(path))
        {
            File.WriteAllText(path, string.Empty);
        }

        Unity.XR.Oculus.Stats.PerfMetrics.EnablePerfMetrics(true);
    }

    void OnDisable()
    {
        Unity.XR.Oculus.Stats.PerfMetrics.EnablePerfMetrics(false);
    }


    int elapsedFrames = 0;
    private void Update()
    {
        if (elapsedFrames > framesToWaitBeforeCapture)
        {
            float appGPUTime = Unity.XR.Oculus.Stats.PerfMetrics.AppGPUTime;
            File.AppendAllText(path, Time.time + ":" + appGPUTime * 1000f + "\n");
        }
        else
        {
            elapsedFrames++;
        }
    }
}
