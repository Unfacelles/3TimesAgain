using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public GameObject pastWorld;
    public GameObject presentWorld;
    public GameObject futureWorld;
    public TimeFlash flashEffect;


    public enum TimeState { Past, Present, Future }
    public TimeState currentState = TimeState.Present;

    void Start()
    {
        UpdateWorldVisibility();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TrySwitchTo(TimeState.Past);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            TrySwitchTo(TimeState.Present);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            TrySwitchTo(TimeState.Future);
        }
    }

    void TrySwitchTo(TimeState newState)
    {
        if (newState == currentState) return; // Already in that state

        currentState = newState;
        UpdateWorldVisibility();
        if (flashEffect != null)
            flashEffect.TriggerFlash();
        Debug.Log("Switched to: " + currentState);
    }

    void UpdateWorldVisibility()
    {
        pastWorld.SetActive(currentState == TimeState.Past);
        presentWorld.SetActive(currentState == TimeState.Present);
        futureWorld.SetActive(currentState == TimeState.Future);
    }
}
