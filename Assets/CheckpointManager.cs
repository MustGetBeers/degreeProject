using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckpointManager : MonoBehaviour
{

    public List<GameObject> checkpointsList;
    public int missedCheckpoints;
    public float timer;
    private bool hasFinished;
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFinished)
        {
            timer += Time.deltaTime;
            timerText.text = timer.ToString("n2");
        }
    }

    public void CheckpointPast(GameObject checkpoint)
    {
        if (checkpointsList.Contains(checkpoint))
        {
            checkpointsList.Remove(checkpoint);
        }
        
    }
    public void FinishLinePast()
    {
        missedCheckpoints = checkpointsList.Count;
        hasFinished = true;
    }
}
