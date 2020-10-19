using System;
using System.Collections;
using System.Collections.Generic;
using Colyseus.Schema;
using TMPro;
using UnityEngine;

public class Gems : MonoBehaviour
{
    // Start is called before the first frame update
    Client client;
    public TextMeshProUGUI gemsText;
    private AudioSource audioSource;

    bool registred = false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }
    void Start()
    {
        Client.Instance.addReadyListener(init);

    }

    private void init()
    {
        //
        client = Client.Instance;
        client.room.State.turnState.players.OnChange += onPlayersChange;

    }

    private void onPlayerChange(List<DataChange> changes)
    {
        changes.ForEach(val =>
        {
            if (val.Field == "gems")
            {
                if (audioSource != null)
                {
                    audioSource.Play();
                }
                setGems(Int16.Parse(val.Value.ToString()));
            }

        });

    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>

    private void onPlayersChange(TurnPlayerState value, string key)
    {
        if (value.user.sessionId == client.room.SessionId && !registred)
        {
            setGems((int)value.gems);
            value.OnChange += onPlayerChange;
            registred = true;
        }
    }
    public void setGems(int gems)
    {
        gemsText.text = "" + gems;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
