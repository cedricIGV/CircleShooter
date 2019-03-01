using System.Collections;
using System.Collections.Generic;
using RhythmosEngine;
using UnityEngine;

public class RhythmPlayer : MonoBehaviour
{
    public RhythmosDatabase m_rhythmosDatabase;
    public TextAsset m_rhythmosFile;
    public GameObject enemy;
    private RhythmosPlayer player;
    private int noteCount;

    private float lastTime, deltaTime, timer;
    public float bpm;

    // Use this for initialization
    void Awake()
    {
        m_rhythmosDatabase = new RhythmosDatabase(m_rhythmosFile);
       
    }

    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log("Total of Rhythms: " + m_rhythmosDatabase.RhythmsCount.ToString());
        //Debug.Log("Total of NoteLayout: " + m_rhythmosDatabase.NoteLayoutCount.ToString());
        PlayRhythm();
        ////m_rhythmosDatabase.GetNoteLayoutList().;
        lastTime = 0f;
        deltaTime = 0f;
        timer = 0f;
    }

    void PlayRhythm()
    {
        // play a rhythm from a loaded RhythmosDatabase
        player = m_rhythmosDatabase.PlayRhythm("MainBeat", 1f); // set playback loop
        Debug.Log(player.volume);
        player.loop = true;
         //should the GameObject of RhythmosPlayer destroy on end of playback?
        player.destroyOnEnd = false;
        noteCount = player.transform.childCount;
        //player.Play();
    }


    // Update is called once per frame
    void Update()
    {
        if (noteCount != player.transform.childCount)
        {
            //Debug.Log(noteCount);
            noteCount = player.transform.childCount;
            //if()
            if(player.GetCurrentNote().layoutIndex == 0)
            {
                enemy.GetComponent<LaunchAsteroid>().launchAsteroid(true, transform.position, 20);
            }
            else if(player.GetCurrentNote().layoutIndex == 1)
            {
                enemy.GetComponent<CircleBulletPattern>().fireCircle(20,false);
            }

        }


        //Broken code. Tried to get bpm stuff working.
        //deltaTime = GetComponent<AudioSource>().timeSamples - lastTime;
        //timer += deltaTime;

        //if (timer >= (60f / bpm))
        //{
        //    enemy.GetComponent<CircleBulletPattern>().fireCircle(20, false);
        //    timer -= (60f / bpm);
        //}
        //lastTime = GetComponent<AudioSource>().timeSamples;

    }
}
