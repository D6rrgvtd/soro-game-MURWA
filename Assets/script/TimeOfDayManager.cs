using UnityEngine;

public class TimeOfDayManager : MonoBehaviour
{
    public Light sun; // Directional LightÅiëæózÅj
    public Material skyboxMaterial; // SkyboxÅiîCà”Åj

    private int currentTimeIndex = 0;

    private enum TimeOfDay { Morning, Noon, Evening, Night,Midnight }
    private TimeOfDay[] times = { TimeOfDay.Morning, TimeOfDay.Noon, TimeOfDay.Evening, TimeOfDay.Night,TimeOfDay.Midnight };

    void Start()
    {
        ApplyTimeSettings();
    }

    public void NextTimeOfDay()
    {
        currentTimeIndex = (currentTimeIndex + 1) % times.Length;
        ApplyTimeSettings();
    }

    private void ApplyTimeSettings()
    {
        switch (times[currentTimeIndex])
        {
            case TimeOfDay.Morning:
                sun.color = new Color(1f, 0.85f, 0.6f);
                sun.transform.rotation = Quaternion.Euler(20f, 30f, 0f);
                RenderSettings.ambientLight = new Color(0.9f, 0.8f, 0.7f);
                break;

            case TimeOfDay.Noon:
                sun.color = Color.white;
                sun.transform.rotation = Quaternion.Euler(60f, 0f, 0f);
                RenderSettings.ambientLight = new Color(1f, 1f, 1f);
                break;

            case TimeOfDay.Evening:
                sun.color = new Color(1f, 0.6f, 0.4f);
                sun.transform.rotation = Quaternion.Euler(10f, 300f, 0f);
                RenderSettings.ambientLight = new Color(0.8f, 0.6f, 0.5f);
                break;

            case TimeOfDay.Night:
                sun.color = new Color(0.3f, 0.3f, 0.5f);
                sun.transform.rotation = Quaternion.Euler(-20f, 0f, 0f);
                RenderSettings.ambientLight = new Color(0.2f, 0.2f, 0.3f);
                break;

            case TimeOfDay.Midnight:
                sun.color = new Color(0.15f, 0.15f, 0, 25f);
                sun.transform.rotation = Quaternion.Euler(-30f,0f, 0f);
                RenderSettings.ambientLight = new Color(0.05f, 0.05f, 0.1f);
                break;
        }
    }
}
