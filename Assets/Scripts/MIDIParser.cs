using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;
using MK.Glow.Legacy;
//using NAudio.Midi;
using System.IO;
using System.Linq;

public class MIDIParser : MonoBehaviour
{
    int numBeats = 0;
    string phase = "start";
    //public MidiFile midi;
    public MidiFilePlayer midiFilePlayer;
    public GameObject enemy;

    //Glow pulse parameter. Starts at start, then goes to peak, then goes back to start
    public float startScatter = 1;
    public float peakScatter = 5;
    public float duration = .1f;

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
            if (numBeats == 48)
            {
                phase = "start Vocals";
                print(note.Midi);
                if (note.Midi == 72)
                {
                    enemy.GetComponent<RiffBulletPattern>().StartCoroutine("fireRiff");
                }
                if (note.Midi == 84)
                {
                    print("fire @");
                    enemy.GetComponent<FireAtPlayerPattern>().fireAtPlayer();
                }
            }
            if (phase == "start")
            {
                if (note.Midi == 72)
                {
                    enemy.GetComponent<LaunchAsteroid>().launchAsteroid(true, transform.position, 20);
                    StartCoroutine(PulseMkGlow(Camera.main.GetComponent<MKGlow>(), startScatter, peakScatter, duration));
                    numBeats++;
                }
            }
            else if (phase == "start Vocals")
            {
                print(numBeats);
                //new behaviour
            }
        }
    }

    public static IEnumerator PulseMkGlow(MKGlow glow, float startAlpha, float endAlpha, float duration)
    {
        // keep track of when the fading started, when it should finish, and how long it has been running&lt;/p&gt; &lt;p&gt;&a
        var startTime = Time.time;
        var endTime = Time.time + duration;
        var elapsedTime = 0f;

        // set the canvas to the start alpha – this ensures that the canvas is ‘reset’ if you fade it multiple times
        glow.bloomScattering = startAlpha;
        // loop repeatedly until the previously calculated end time
        while (Time.time <= endTime)
        {
            elapsedTime = Time.time - startTime; // update the elapsed time
            var percentage = 1 / (duration / elapsedTime); // calculate how far along the timeline we are
            if (Time.time <= (endTime / 2)) // if we are fading up 
            {
                glow.bloomScattering = startAlpha + (endAlpha - startAlpha) * percentage; // calculate the new alpha
                Debug.Log(endTime / 2);
            }
            else // if we are fading down
            {
                glow.bloomScattering = endAlpha - (endAlpha - startAlpha) * percentage; // calculate the new alpha
            }

            yield return new WaitForEndOfFrame(); // wait for the next frame before continuing the loop
        }
        Debug.Log(endTime / 2);
        glow.bloomScattering = startAlpha; // force the alpha to the end alpha before finishing – this is here to mitigate any rounding errors, e.g. leaving the alpha at 0.01 instead of 0
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
        if (Input.GetKeyDown("g"))
        {
            StartCoroutine(PulseMkGlow(Camera.main.GetComponent<MKGlow>(), startScatter, peakScatter, duration));
        }
    }
}
