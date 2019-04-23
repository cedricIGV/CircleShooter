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
    int numRings = 0;
    string phase = "start";
    //public MidiFile midi;
    public MidiFilePlayer midiFilePlayer;
    public GameObject enemy;

    //Glow pulse parameter. Starts at start, then goes to peak, then goes back to start
    public float startScatter = 1;
    public float peakScatter = 5;
    public float duration = .1f;

    float riffTime;
    float riffCount;
    GameObject grid;
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
        riffTime = 100000;
        riffCount = 1;
        midiFilePlayer.OnEventNotesMidi = new MidiFilePlayer.ListNotesEvent();
        midiFilePlayer.OnEventNotesMidi.AddListener(NotesToPlay);
        GameObject[] background = GameObject.FindGameObjectsWithTag("Background");
        grid = background[0];

        //midiFilePlayer.OnEventStartPlayMidi = new UnityEngine.Events.UnityEvent();
        //midiFilePlayer.OnEventStartPlayMidi.AddListener(PlaySong);
    }

    public void NotesToPlay(List<MidiNote> notes)
    {
        //Debug.Log(notes.Count);
        foreach (MidiNote note in notes)
        {
            if (numBeats == 30)
            {
                enemy.GetComponent<LaunchAsteroid>().fade = true;
                enemy.GetComponent<LaunchAsteroid>().startTime = Time.time;
                grid.GetComponent<fade>().startFade = true;
                grid.GetComponent<fade>().startTime = Time.time;
                enemy.GetComponent<FireAtPlayerPattern>().fade = true;
                enemy.GetComponent<FireAtPlayerPattern>().startTime = Time.time;
            }
            if (phase == "start Vocals")
            {
                if (numBeats == 62)
                {
                    enemy.GetComponent<LaunchAsteroid>().fade = false;
                    grid.GetComponent<fade>().startFade = false;
                    grid.GetComponent<lightUp>().StartCoroutine("flareUp");
                }
                //print(numBeats);
                if (numBeats == 63)
                {
                    phase = "start";
                }
                if (numBeats == 110)
                {
                    phase = "rocking";
                }
                //print(note.Midi);
                
                if (Time.time - riffTime > .5 && riffCount < 2)
                {
                    enemy.GetComponent<RiffBulletPattern>().StartCoroutine("fireRiff"); //second call to coreroutine ovverides the first one!
                    riffTime = Time.time;
                    riffCount++;
                }
                
                if (note.Midi == 72)
                {
                    enemy.GetComponent<RiffBulletPattern>().StartCoroutine("fireRiff");
                    riffTime = Time.time;
                    grid.GetComponent<fade>().startFade = false;
                    riffCount = 1;
                }
                if (note.Midi == 84)
                {
                    numBeats++;
                }
            }
            if (phase == "start")
            {
                if (note.Midi == 72)
                {
                    enemy.GetComponent<LaunchAsteroid>().launchAsteroid(true, transform.position, 20, numRings);
                    numRings++;
                    StartCoroutine(PulseMkGlow(Camera.main.GetComponent<MKGlow>(), startScatter, peakScatter, duration));
                    numBeats++;
                    if ((numRings-1) % 4 == 0)
                    {
                        enemy.GetComponent<FireAtPlayerPattern>().fireAtPlayer();
                    }
                }
                if (numBeats == 48)
                {
                    phase = "start Vocals";
                }
                if (numBeats == 78)
                {
                    phase = "start Vocals";
                }
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
                //Debug.Log(endTime / 2);
            }
            else // if we are fading down
            {
                glow.bloomScattering = endAlpha - (endAlpha - startAlpha) * percentage; // calculate the new alpha
            }

            yield return new WaitForEndOfFrame(); // wait for the next frame before continuing the loop
        }
        //Debug.Log(endTime / 2);
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
