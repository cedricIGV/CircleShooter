using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;
//using NAudio.Midi;
using System.IO;
using System.Linq;


public class MIDIParser : MonoBehaviour
{

    //public MidiFile midi;
    public MidiFilePlayer midiFilePlayer;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        //midi = new MidiFile("Assets/Scripts/JukeboxHero/notes.mid");
        //foreach (MidiEvent note in midi.Events[2])
        //{
        //    //If its the start of the note event
        //    if (note.CommandCode == MidiCommandCode.NoteOn)
        //    {
        //        Debug.Log(note.ToString());
        //    }
        //}

        midiFilePlayer.OnEventNotesMidi = new MidiFilePlayer.ListNotesEvent();
        midiFilePlayer.OnEventNotesMidi.AddListener(NotesToPlay);


        //midiFilePlayer.OnEventStartPlayMidi = new UnityEngine.Events.UnityEvent();
        //midiFilePlayer.OnEventStartPlayMidi.AddListener(PlaySong);
    }

    public void NotesToPlay(List<MidiNote> notes)
    {
        //Debug.Log(notes.Count);
        foreach (MidiNote note in notes)
        {
            if(note.Midi == 72)
            {
                enemy.GetComponent<LaunchAsteroid>().launchAsteroid(true, transform.position, 20);
            }
        }
    }

    void PlaySong()
    {
        AudioSource[] list = FindObjectsOfType<AudioSource>();
        //double x = AudioSettings.dspTime;
        foreach (AudioSource a in list)
        {
            a.Play();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            midiFilePlayer.MPTK_Play();
            PlaySong();

        }
    }
}
