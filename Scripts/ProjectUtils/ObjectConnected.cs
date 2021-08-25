using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectConnected : MonoBehaviour
{
    [Tooltip("The hook object name.")]
    public string hookName = "Hook";
    [Tooltip("Hook force level required to move this object.")]
    public int hookLevelRequired = 1;
    public UnityEvent connected;
    public UnityEvent disconnected;
    public UnityEvent onMovable;
    public UnityEvent enableDialogue;
    public UnityEvent disabledialogue;

        public Dialogue dialogue;
        public DialogueManager dialogueManager;
        public float dialoguetime;

    private bool objectIsConnected = false;
    private HingeJoint2D _hinge;
    private Rigidbody2D _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        if (!_rigid)
        {
            gameObject.AddComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_hinge)
        {
            if (_hinge.enabled && _hinge.connectedBody == _rigid && !objectIsConnected)
            {
                objectIsConnected = true;
                connected.Invoke();
                if (MyGameManager.instance.hookLevel >= hookLevelRequired)
                {
                    onMovable.Invoke();
                }
                else
                {
                    StartCoroutine(notMovable());
                }
            }
            else if (!_hinge.enabled && objectIsConnected)
            {
                objectIsConnected = false;
                disconnected.Invoke();
            }
        }
    }

    public bool isConnected()
    {
        return (objectIsConnected);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_hinge && collision.gameObject.name == hookName)
        {
            _hinge = collision.gameObject.GetComponent<HingeJoint2D>();
            if (!_hinge)
            {
                Debug.LogError("ObjectConnected error: The hook involved in the collision with " + gameObject.name + " hasn't a HingeJoint2D.");
            }
        }
    }

    private IEnumerator notMovable()
    {
        enableDialogue.Invoke();
        dialogueManager.StartDialogue(dialogue);
        yield return new WaitForSeconds(dialoguetime);
        disabledialogue.Invoke();

    }
}
