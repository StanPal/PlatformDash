using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchCommands : MonoBehaviour
{

    private float[] xPositions = { -5.0f, 5.0f };

    [SerializeField]
    string twitchMessageToSpawn = "enemy";

    private string twitchMessageHeal = "heal";
    private string twitchMessageMoveUp = "up";
    private string twitchMessageMoveDown = "down";
    private string twitchMessageJump = "jump";
    private string twitchMessageSpeed = "speed";
    private string twitchMessageSlow = "slow";
    private string twitchMessageInvert = "invert";


    [SerializeField]
    GameObject enemyPrefab;

    private PlayerStats playerStats;
    private PlayerController playerController;
    private PlatFormManager platform;
    private EnemySpawnManager SpawnManager;

    private void Awake()
    {
        platform = FindObjectOfType<PlatFormManager>();
        playerStats = FindObjectOfType<PlayerStats>();
        playerController = FindObjectOfType<PlayerController>();
        SpawnManager = FindObjectOfType<EnemySpawnManager>();
        TwitchChat twitchChat = FindObjectOfType<TwitchChat>();
        twitchChat.OnMessageReceived += (x) => twitchCommands(x);
        
    }
    
    private void twitchCommands(string twitchMessage)
    {
        if (twitchMessage.ToLower() == twitchMessageHeal.ToLower())
        {
            playerStats.playerHeal();
        }
        if (twitchMessage.ToLower() == twitchMessageJump.ToLower())
        {
            playerController.Jump();
        }
        if (twitchMessage.ToLower() == twitchMessageSlow.ToLower())
        {
            playerController.Slow();
        }
        if (twitchMessage.ToLower() == twitchMessageSpeed.ToLower())
        {
            playerController.IncreaseSpeed();
        }

        if(twitchMessage.ToLower() == twitchMessageInvert.ToLower())
        {
            if (playerController.Inverted)
                playerController.Inverted = false;
            else
                playerController.Inverted = true;
        }

        if (twitchMessage.ToLower() == twitchMessageToSpawn.ToLower())
        {
            int xPosIndex = Random.Range(0,2);
            Vector3 spawn = new Vector3
                (playerController.transform.position.x + xPositions[xPosIndex],
                playerController.transform.position.y);
            if (SpawnManager.canSpawn())
            {
                SpawnManager.Enemies.Enqueue(Instantiate(enemyPrefab, spawn, Quaternion.identity));
            }
            else
            {
                Destroy(SpawnManager.Enemies.Dequeue());
                SpawnManager.Enemies.Enqueue(Instantiate(enemyPrefab, spawn, Quaternion.identity));
            }
        }

        if(twitchMessage.ToLower() == twitchMessageMoveUp.ToLower())
        {
            platform.setMovementDir = true;
        }

        if(twitchMessage.ToLower() == twitchMessageMoveDown.ToLower())
        {
            platform.setMovementDir = false;
        }
    }

}
