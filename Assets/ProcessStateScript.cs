using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessStateScript : MonoBehaviour
{
    public enum State { New, Ready, Running, Waiting, Terminated}
    public State currentState;
    private SpriteRenderer renderer;
    public AudioSource audioSource;
    public Text stateText;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        renderer = GetComponent<SpriteRenderer>();
        currentState = State.New;
        UpdateColorAndDisplayState();
        StartCoroutine(SimulateProcess());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { SetState(State.New); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { SetState(State.Ready); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { SetState(State.Running); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { SetState(State.Waiting); }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { SetState(State.Terminated); }

    }
    void SetState(State newState)
    {
        currentState = newState;
        UpdateColorAndDisplayState();
    }
    void UpdateColorAndDisplayState()
    {
        Color newColor = Color.white;
        switch (currentState)
        {
            case State.New:
                newColor = Color.blue;
                renderer.color = newColor;
                break;
            case State.Ready:
                newColor = Color.yellow;
                renderer.color = newColor;
                break;
            case State.Running:
                newColor = Color.green;
                renderer.color = newColor;
                break;
            case State.Waiting:
                newColor = Color.gray;
                renderer.color = newColor;
                break;
            case State.Terminated:
                newColor = Color.red;
                renderer.color = newColor;
                break;

        }
        audioSource.Play();
        if(stateText != null)
        {
            stateText.text = "Current State: " + currentState.ToString();
            stateText.color = newColor;
        }
    }
    void OnMouseDown()
    {
        // Allow the user to select a process and transition its state
        SetState(State.Running);  // Example: move the process to the running state when clicked
    }
    IEnumerator SimulateProcess()
    {
        SetState(State.New);
        yield return new WaitForSeconds(2f);
        SetState(State.Ready);
        yield return new WaitForSeconds(2f);
        SetState(State.Running);
        yield return new WaitForSeconds(5f);
        SetState(State.Waiting);
        yield return new WaitForSeconds(3f);
        SetState(State.Terminated);
    }

     

}
