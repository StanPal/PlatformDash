using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatLog : MonoBehaviour
{
    private TwitchCommands twitchCommands;
    public Queue<string> messageQueue;
    [SerializeField] Scrollbar _verticalScrollBar;

    private void Awake()
    {
        twitchCommands = FindObjectOfType<TwitchCommands>();
    }

    private void Start()
    {
        messageQueue = new Queue<string>();
        twitchCommands.OnChatCalled += ChatLogChange;
    }

    private void Update()
    {
        CheckNewMessage();
    }

    private void CheckNewMessage()
    {
        if(messageQueue.Count > 0)
        {
            foreach (string line in messageQueue)
            {
                Messenger.Instance.CreateMessage(line, Color.black);
            }
            messageQueue.Dequeue();
        }
    }

    public void ChatLogChange(string chatInput)
    {
        messageQueue.Enqueue(chatInput);
        _verticalScrollBar.value = 0;

    }
}
