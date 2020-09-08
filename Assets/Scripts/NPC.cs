using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCConversation myConversation;
    public GameObject NPCAlert;

    public bool hit;
    // Start is called before the first frame update
    void Start()
    {
        NPCAlert.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && hit)
        {
            ConversationManager.Instance.StartConversation(myConversation);
        }

        if (ConversationManager.Instance != null)
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    ConversationManager.Instance.SelectPreviousOption();
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    ConversationManager.Instance.SelectNextOption();
                else if (Input.GetKeyDown(KeyCode.L))
                    ConversationManager.Instance.PressSelectedOption();
            }
        }
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NPCAlert.SetActive(true);
            hit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NPCAlert.SetActive(false);
            hit = false;
        }
    }
}
