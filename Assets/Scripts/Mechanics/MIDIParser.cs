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

    float gridAlpha;

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
        gridAlpha = grid.GetComponent<SpriteRenderer>().color.a;


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
                enemy.GetComponent<FireAtPlayerPattern>().fade = true;
                enemy.GetComponent<FireAtPlayerPattern>().startTime = Time.time;


                StartCoroutine(fade.Fade(grid.GetComponent<SpriteRenderer>(), gridAlpha, 0, 7f));
                //grid.GetComponent<fade>().startFade = true;
                //grid.GetComponent<fade>().startTime = Time.time;
            }
            if (phase == "start Vocals")
            {
                if (numBeats == 62)
                {
                    //return asteroids and grid to their original opacity
                    enemy.GetComponent<LaunchAsteroid>().fade = false;


                    //grid.GetComponent<fade>().startFade = false;
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
                    StartCoroutine(GlowTechniques.PulseMkGlow(Camera.main.GetComponent<MKGlow>(), startScatter, peakScatter, duration));


                    numBeats++;
                    if ((numRings-1) % 4 == 0)
                    {
                        enemy.GetComponent<FireAtPlayerPattern>().fireAtPlayer();
                    }
                }
                if (numBeats == 48)
                {
                    phase = "start Vocals";
                    StartCoroutine(Peak(grid.GetComponent<SpriteRenderer>(), 0, .7f, gridAlpha, 2f));
                }
                if (numBeats == 78)
                {
                    phase = "start Vocals";
                }
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
        if (Input.GetKeyDown("f"))
        {
            StartCoroutine(fade.Fade(grid.GetComponent<SpriteRenderer>(),0, 1, 5f));
        }
        if (Input.GetKeyDown("g"))
        {
            StartCoroutine(Peak(grid.GetComponent<SpriteRenderer>(), 0, .7f, gridAlpha, .5f));
        }
    }


    public  IEnumerator Peak(SpriteRenderer sprite, float startAlpha, float peakAlpha, float endAlpha, float duration)
    {
        yield return StartCoroutine(fade.Fade(sprite, startAlpha, peakAlpha, duration * .8f));
        StartCoroutine(fade.Fade(sprite, peakAlpha, endAlpha, duration * .2f));
    }
}
