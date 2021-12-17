using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    [SerializeField]
    private CameraMovement cameraMovement;
    [SerializeField]
    private Camera sceneCamera;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform bossRoomTransform;
    private float startCameraSize;
    [SerializeField]
    private float bigCameraSize;
    [SerializeField]
    private float cameraSizeChangeRate;
    [SerializeField]
    private BossRoomBlockMovement entryBlock;
    [SerializeField]
    private BossRoomBlockMovement exitBlock;
    private float cameraSizeTarget;
    [SerializeField]
    private AudioClip bossMusic;
    [SerializeField]
    private AudioClip victoryMusic;
    [SerializeField]
    private AudioSource musicManager;
    [SerializeField]
    private ParticleSystem victoryParticles;
    [SerializeField]
    private GameObject bossMonster;

    private void Start()
    {
        startCameraSize = sceneCamera.orthographicSize;
        cameraSizeTarget = startCameraSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            //tell walls to drop
            entryBlock.Block();
            exitBlock.Block();
            //update camera size and position
            cameraSizeTarget = bigCameraSize;
            cameraMovement.UpdateTargetTransform(bossRoomTransform, true);
            //update music
            musicManager.clip = bossMusic;
            musicManager.Play();
            bossMonster.SetActive(true);
        }
    }

    private void Update()
    {
        if (!Mathf.Approximately(sceneCamera.orthographicSize, cameraSizeTarget))
        {
            //change the camera size over time
            sceneCamera.orthographicSize = Mathf.Lerp(sceneCamera.orthographicSize, cameraSizeTarget, cameraSizeChangeRate);
        }

    }

    public void EndLevel()
    {
        //revert camera size and position
        cameraMovement.UpdateTargetTransform(playerTransform, false);
        cameraSizeTarget = startCameraSize;
        //update music again
        musicManager.clip = victoryMusic;
        musicManager.Play();
        victoryParticles.Play();
        exitBlock.UnBlock();
    }

}
