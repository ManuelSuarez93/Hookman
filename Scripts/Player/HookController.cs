using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HookController : MonoBehaviour
{
    [Tooltip("Hook speed when it's thrown in units per second")]
    public float speed = 2;
    [Tooltip("Hook speed when it's returning in units per second")]
    public float returnSpeed = 3;
    [Tooltip("Maximum distance the hook can reach")]
    public float maxRange = 3;
    [Tooltip("Hook speed in units per second generated when it's pulling another object")]
    public float pullingForce = 50;
    [Tooltip("Start point of the hook")]
    public GameObject weapon;
    [Tooltip("Finish point of the hook")]
    public GameObject hook;
    [Tooltip("The rigidbody of the player that has the hook")]
    public Rigidbody2D player;
    [Tooltip("The line renderer between the start point and the finish point")]
    public LineRenderer line;
    public UnityEvent hookForceLevelUp;
    public UnityEvent hookFired;
    public UnityEvent hookPrepared;
    public UnityEvent playerPulled;
    public UnityEvent pullStoped;
    public UnityEvent connected;
    public UnityEvent pulling;
    //Cooldown properties
    [Tooltip("Tiempo de cooldown(tiempo minimo antes de usar la habilidad) de la habilidad")]
    public float CooldownTime;
    //Indica si el cooldown termino y el arma esta lista para usar
    public bool CooldownReady;
    //Variable para guardar el tiempo de cooldown
    public float TimeBetweenCooldown;
    public int forceLevel = 1;
    public bool controlPlayerMovement = true;

    private enum hookStates {Prepared, Shooting, Connected, Returning};
    private hookStates actualState = hookStates.Prepared;
    private float realFixedReturnSpeed;
    private float realReturnSpeed;
    private AudioSource source;
    public string state;
    private bool isStatic
    {
        get
        {
            if (anotherRbObject && anotherRbObject.bodyType == RigidbodyType2D.Static)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    private bool canPull = false;
    private bool isPulling = false;
    private PlayerMovement playerMovement;
    private Collision2D hookCollision;
    private Rigidbody2D hookRigidbody;
    private HingeJoint2D hookHingeJoint;
    private Rigidbody2D anotherRbObject;
    private Camera cam;


    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        hookRigidbody = hook.GetComponent<Rigidbody2D>();
        hookHingeJoint = hook.GetComponent<HingeJoint2D>();
        DistanceJoint2D hookDj = hook.GetComponent<DistanceJoint2D>();
        cam = Camera.main;
        hookDj.connectedBody = player;
        float hookAddedDistance = .5f;
        hookDj.distance = maxRange + hookAddedDistance;
        line.enabled = false;
        source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        state = actualState.ToString();
        //LLAMO A LA FUNCION COOLDOWN PARA CALCULAR EL TIEMPO Y MARCAR SI ESTA LISTA EL ARMA
        Cooldown();
        if (player)
        {
            float distance = Vector2.Distance(player.transform.position, hook.transform.position);

            // Hook states control
            if (actualState == hookStates.Prepared && Input.GetButtonDown("ThrownHook") && CooldownReady && Time.timeScale > 0)//AÑADI COOLDOWN READY
            {
                FireHook();
            }
            else if (actualState == hookStates.Shooting && distance >= maxRange)
            {
                hookRigidbody.velocity = Vector3.zero;
                realFixedReturnSpeed = (1 / (Vector2.Distance(weapon.transform.position, hook.transform.position) / returnSpeed));
                realReturnSpeed = 0;
                actualState = hookStates.Returning;
            }
            else if (actualState == hookStates.Connected && Input.GetButton("ThrownHook") && Time.timeScale > 0)
            {
                PullHook();
            }
            else if (actualState == hookStates.Connected && Input.GetButtonDown("ReturnHook") && Time.timeScale > 0)
            {
                if (playerMovement && controlPlayerMovement)
                {
                    playerMovement.enabled = true;
                }
                hookHingeJoint.connectedBody = null;
                hookHingeJoint.enabled = false;
                hookRigidbody.velocity = Vector3.zero;
                realFixedReturnSpeed = (1 / (Vector2.Distance(weapon.transform.position, hook.transform.position) / returnSpeed));
                realReturnSpeed = 0;
                actualState = hookStates.Returning;
            }
            else if (actualState == hookStates.Connected)
            {
                hookRigidbody.velocity = new Vector2(0, 0);
                isPulling = false;
                pullStoped.Invoke();
            }
            else if (actualState == hookStates.Prepared)
            {
                hook.transform.position = weapon.transform.position;
            }

            if (actualState == hookStates.Connected && hookHingeJoint.connectedBody == null)
            {
                hookHingeJoint.enabled = false;
                hookRigidbody.velocity = Vector3.zero;
                realFixedReturnSpeed = (1 / (Vector2.Distance(weapon.transform.position, hook.transform.position) / returnSpeed));
                realReturnSpeed = 0;
                actualState = hookStates.Returning;
            }

            if (actualState == hookStates.Connected && !Input.GetButton("ThrownHook") && playerMovement && controlPlayerMovement)
            {
                playerMovement.enabled = true;
            }

            // Activate the ReturnHook function once per frame if the hook is returning
            if (actualState == hookStates.Returning)
            {
                ReturnHook();
                source.Stop();
            }

            // Update the start and finish points of the line renderer
            if (line.enabled)
            {
                line.SetPosition(0, weapon.transform.position);
                line.SetPosition(1, hook.transform.position);
            }
        }
    }

    private void FireHook()
    {
        line.enabled = true;
        hookRigidbody.velocity = weapon.transform.right * speed;
        actualState = hookStates.Shooting;
        hookFired.Invoke();
    }

    public void setConnectedState(Collider2D collision)
    {
        if (actualState == hookStates.Shooting)
        {
            anotherRbObject = collision.gameObject.GetComponent<Rigidbody2D>();
            ObjectConnected anotherObjectConnected = collision.gameObject.GetComponent<ObjectConnected>();
            if (anotherRbObject && anotherObjectConnected)
            {
                HingeJoint2D hookHingeJoint = hook.GetComponent<HingeJoint2D>();
                actualState = hookStates.Connected;
                connected.Invoke();
                hookRigidbody.velocity = Vector2.zero;
                hookHingeJoint.enabled = true;
                hookHingeJoint.connectedBody = anotherRbObject;
                canPull = (anotherObjectConnected.hookLevelRequired <= forceLevel) ? true : false;
                
            }
        }
    }

    private void ReturnHook()
    {
        realReturnSpeed += realFixedReturnSpeed * Time.deltaTime;
        hook.transform.position = Vector3.Lerp(hook.transform.position, weapon.transform.position, realReturnSpeed);
        if (realReturnSpeed >= 1)
        {
            line.enabled = false;
            actualState = hookStates.Prepared;
            TimeBetweenCooldown = CooldownTime;
            CooldownReady = false;
            hookPrepared.Invoke();
        }
    }

    private void PullHook()
    {
        if (isStatic)
        {
            if (playerMovement && controlPlayerMovement)
            {
                playerMovement.enabled = false;
            }
            player.velocity = (hook.transform.position - player.transform.position).normalized * speed;
            if (!isPulling)
            {
                playerPulled.Invoke();
                isPulling = true;
            }
        }
        else if (canPull)
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 weaponDirection = (weapon.transform.position - hook.transform.position);
            Vector2 mouseDirection = (mousePosition - hook.transform.position);
            weaponDirection = weaponDirection.normalized;
            mouseDirection = mouseDirection.normalized;
            float angle = Vector2.Angle(weaponDirection, mouseDirection);
            float weaponForcePercent = 0.15f;
            float mouseForcePercent = 0.85f;
            if (angle > 90)
            {
                mouseDirection = (Vector2.SignedAngle(weaponDirection, mouseDirection) > 0) ? new Vector2(-weaponDirection.y, weaponDirection.x) : new Vector2(weaponDirection.y, -weaponDirection.x);
            }
            weaponDirection *= (pullingForce * weaponForcePercent);
            mouseDirection *= (pullingForce * mouseForcePercent);
            hookRigidbody.velocity = weaponDirection + mouseDirection;
            isPulling = true;
            pulling.Invoke();
            if(!source.isPlaying)
            {
                source.Play();
            }
        }
    }

    public void ModifyMaxRange(float newRange)
    {
        maxRange += newRange;
        hook.GetComponent<DistanceJoint2D>().distance += newRange;
        MyGameManager.instance.SetRopeLevel((int) maxRange);
    }

    public void ModifyForceLevel(int levelModifier)
    {
        forceLevel += levelModifier;
        hookForceLevelUp.Invoke();
        MyGameManager.instance.SetHookLevel(forceLevel);
    }

    //COOLDOWN
    public void Cooldown()
    {
        if (actualState == hookStates.Prepared && !CooldownReady)
        {
            TimeBetweenCooldown -= Time.deltaTime;
            if (TimeBetweenCooldown <= 0)
            {
                CooldownReady = true;
                TimeBetweenCooldown = 0;
            }
        }
    }

    public void ReleaseHook()
    {
        if (playerMovement && controlPlayerMovement)
        {
            playerMovement.enabled = true;
        }
        hookHingeJoint.connectedBody = null;
        hookHingeJoint.enabled = false;
        hookRigidbody.velocity = Vector3.zero;
        realFixedReturnSpeed = (1 / (Vector2.Distance(weapon.transform.position, hook.transform.position) / returnSpeed));
        realReturnSpeed = 0;
        actualState = hookStates.Returning;
    }
}
