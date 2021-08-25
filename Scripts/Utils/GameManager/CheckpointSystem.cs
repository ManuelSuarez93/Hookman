using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CheckpointSystem : MonoBehaviour
{
    [Tooltip("Player GameObject that has the principal logic.")]
    public GameObject player;
    [Tooltip("Delay in seconds to save the checkpoint.")]
    public float saveDelay = 0;
    [Tooltip("Camera transform, if you are using Cinemachine, this will be the VirtualCamera transform.")]
    public Transform cam;

    public UnityEvent onSave;
    public UnityEvent onLoad;

    private ScriptHealth sh;
    private HookController hc;
    private PlayerSample ps;

    // Start is called before the first frame update
    void Start()
    {
        if (player)
        {
            sh = player.GetComponent<ScriptHealth>();
            if (!sh)
            {
                Debug.LogError("CheckpointSystem Script error: player object doesn't have the component ScriptHealth.");
            }

            hc = player.GetComponentInChildren<HookController>();
            if (!hc)
            {
                Debug.LogError("CheckpointSystem Script error: player object children doesn't have the component HookController.");
            }
            ps = player.GetComponent<PlayerSample>();
            if (!ps)
            {
                Debug.LogError("CheckpointSystem Script error: player object doesn't have the component PlayerSample.");
            }

            MyGameManager.instance.SetCheckpointSystem(this);

        if(SceneManager.GetActiveScene().buildIndex == 2)
            {
                PlayerPrefs.SetInt("PlayerHealth", 3);
                PlayerPrefs.SetInt("PlayerMaxHealth", 3);
                PlayerPrefs.SetInt("HookForceLevel", 1);
                PlayerPrefs.SetInt("SampleAmount", 0);
                PlayerPrefs.SetInt("MaxSampleAmount", 5);
                PlayerPrefs.SetFloat("HookMaxRange", 3);

                sh.currentHealth = PlayerPrefs.GetInt("PlayerHealth");
                sh.maxhealth = PlayerPrefs.GetInt("PlayerMaxHealth");
                hc.forceLevel = PlayerPrefs.GetInt("HookForceLevel");
                hc.maxRange = PlayerPrefs.GetFloat("HookMaxRange");
                ps.currentSampleAmount = PlayerPrefs.GetInt("SampleAmount");
                ps.maxSampleAmount = PlayerPrefs.GetInt("MaxSampleAmount");
            }
            else
            {
                sh.currentHealth = PlayerPrefs.GetInt("PlayerHealth");
                sh.maxhealth = PlayerPrefs.GetInt("PlayerMaxHealth");
                hc.forceLevel = PlayerPrefs.GetInt("HookForceLevel");
                hc.maxRange = PlayerPrefs.GetFloat("HookMaxRange");
                ps.currentSampleAmount = PlayerPrefs.GetInt("SampleAmount");
                ps.maxSampleAmount = PlayerPrefs.GetInt("MaxSampleAmount");
            }
        }
        else
        {
            Debug.LogError("CheckpointSystem Script error: missing Player component in " + gameObject.name + ".");
        }
    }

    public void Save()
    {
        StartCoroutine(SaveCkeckpoint());
    }

    public void Load()
    {
        sh.currentHealth = PlayerPrefs.GetInt("PlayerHealth");
        gameObject.SetActive(true);
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerXPosition"), PlayerPrefs.GetFloat("PlayerYPosition"), 0);
        cam.position = new Vector3(PlayerPrefs.GetFloat("CameraXPosition"), PlayerPrefs.GetFloat("CameraYPosition"), 0);
        CinemachineVirtualCamera vCam = cam.GetComponent<CinemachineVirtualCamera>();
        if (vCam)
        {
            vCam.ForceCameraPosition(cam.position, cam.rotation);
        }
        
        onLoad.Invoke();
        StartCoroutine(LoadCheckpoint());
    }

    private IEnumerator LoadCheckpoint()
    {
        yield return new WaitForSeconds(saveDelay);
        
        
    }

    private IEnumerator SaveCkeckpoint()
    {
        yield return new WaitForSeconds(saveDelay);
        PlayerPrefs.SetInt("PlayerHealth", sh.currentHealth);
        PlayerPrefs.SetInt("PlayerMaxHealth", sh.maxhealth);
        PlayerPrefs.SetInt("HookForceLevel", hc.forceLevel);
        PlayerPrefs.SetInt("SampleAmount", ps.currentSampleAmount);
        PlayerPrefs.SetInt("MaxSampleAmount", ps.maxSampleAmount);
        PlayerPrefs.SetFloat("HookMaxRange", hc.maxRange);
        PlayerPrefs.SetFloat("PlayerXPosition", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerYPosition", player.transform.position.y);
        PlayerPrefs.SetFloat("CameraXPosition", cam.position.x);
        PlayerPrefs.SetFloat("CameraYPosition", cam.position.y);
        onSave.Invoke();
    }
}
