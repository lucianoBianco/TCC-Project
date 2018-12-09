using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;
using System;

namespace MidiPlayerTK
{
    public class TestMidiPlayerScripting : MonoBehaviour
    {

        public MidiFilePlayer midiFilePlayer;
        public float LastTimeChange;
        public float DelayTimeChange = 5;
        public bool CheckPosition = false;
        public bool CheckSpeed = false;
        public bool CheckTranspose = false;
        public static Color ButtonColor = new Color(.7f, .9f, .7f, 1f);
        public bool RandomPLay = false;

        private void Awake()
        {
            if (MidiPlayerGlobal.OnEventPresetLoaded != null)
                MidiPlayerGlobal.OnEventPresetLoaded.AddListener(() => EndLoadingSF());
        }

        private void EndLoadingSF()
        {
            Debug.Log("End loading SF, MPTK is ready to play");
            Debug.Log("   Time To Load SoundFont: " + Math.Round(MidiPlayerGlobal.MPTK_TimeToLoadSoundFont.TotalSeconds, 3).ToString() + " second");
            Debug.Log("   Time To Load Waves: " + Math.Round(MidiPlayerGlobal.MPTK_TimeToLoadWave.TotalSeconds, 3).ToString() + " second");
            Debug.Log("   Presets Loaded: " + MidiPlayerGlobal.MPTK_CountPresetLoaded);
            Debug.Log("   Waves Loaded: " + MidiPlayerGlobal.MPTK_CountWaveLoaded);
        }
        // Use this for initialization
        void Start()
        {
            InitPlay();
        }
        public void InitPlay()
        {
            if (MidiPlayerGlobal.MPTK_ListMidi != null && MidiPlayerGlobal.MPTK_ListMidi.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, MidiPlayerGlobal.MPTK_ListMidi.Count);
                midiFilePlayer.MPTK_MidiIndex = index;
                midiFilePlayer.MPTK_Play();
            }
        }


        void OnGUI()
        {
            float startx = 25;
            float starty = 30;
            float maxwidth = Screen.width / 2;
            if (maxwidth < 300) maxwidth = 300;

            starty = GUISelectSoundFont.SelectSoundFont(startx, starty);

            if (midiFilePlayer != null)
            {
                GUILayout.BeginArea(new Rect(startx, starty, maxwidth, 600));

                GUILayout.Space(20);
                GUILayout.Label("Current midi '" + midiFilePlayer.MPTK_MidiName + "'" + (midiFilePlayer.MPTK_IsPlaying ? " is playing" : "is not playing"));
                float currentposition = midiFilePlayer.MPTK_Position / 1000f;
                float position = GUILayout.HorizontalSlider(currentposition, 0f, (float)midiFilePlayer.MPTK_Duration.TotalSeconds);
                if (position != currentposition)
                    midiFilePlayer.MPTK_Position = position * 1000f;

                GUILayout.Space(20);
                RandomPLay = GUILayout.Toggle(RandomPLay, "  Random Play Midi");
                GUILayout.Space(20);
                GUILayout.BeginHorizontal();
                if (midiFilePlayer.MPTK_IsPlaying && !midiFilePlayer.MPTK__IsPaused)
                    GUI.color = ButtonColor;
                if (GUILayout.Button(new GUIContent("Play", "")))
                    midiFilePlayer.MPTK_Play();
                GUI.color = Color.white;

                if (midiFilePlayer.MPTK__IsPaused)
                    GUI.color = ButtonColor;
                if (GUILayout.Button(new GUIContent("Pause", "")))
                    if (midiFilePlayer.MPTK__IsPaused)
                        midiFilePlayer.MPTK_Play();
                    else
                        midiFilePlayer.MPTK_Pause();
                GUI.color = Color.white;

                if (GUILayout.Button(new GUIContent("Stop", "")))
                    midiFilePlayer.MPTK_Stop();
                if (GUILayout.Button(new GUIContent("Restart", "")))
                    midiFilePlayer.MPTK_RePlay();
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button(new GUIContent("Previous", "")))
                    midiFilePlayer.MPTK_Previous();
                if (GUILayout.Button(new GUIContent("Next", "")))
                    midiFilePlayer.MPTK_Next();
                GUILayout.EndHorizontal();

                GUILayout.Label("Go to your Hierarchy, select GameObject MidiFilePlayer, inspector contains more parameters to control your Midi player.");

                GUILayout.EndArea();
            }

        }

        /// <summary>
        /// Triggered at end of each midi file 
        /// </summary>
        public void RandomPlay()
        {
            if (RandomPLay)
            {
                //Debug.Log("Is playing : " + midiFilePlayer.MPTK_IsPlaying);
                int index = UnityEngine.Random.Range(0, MidiPlayerGlobal.MPTK_ListMidi.Count);
                midiFilePlayer.MPTK_MidiIndex = index;
                midiFilePlayer.MPTK_Play();
            }
            else
                midiFilePlayer.MPTK_RePlay();
        }

        // Update is called once per frame
        void Update()
        {
            if (midiFilePlayer != null && midiFilePlayer.MPTK_IsPlaying)
            {
                float time = Time.realtimeSinceStartup - LastTimeChange;
                if (time > DelayTimeChange)
                {
                    LastTimeChange = Time.realtimeSinceStartup;
                    if (CheckPosition)
                    {
                        midiFilePlayer.MPTK_Position = UnityEngine.Random.Range(0f, (float)midiFilePlayer.MPTK_Duration.TotalMilliseconds);
                    }
                    if (CheckSpeed)
                    {
                        midiFilePlayer.MPTK_Speed = UnityEngine.Random.Range(0.1f, 5f);
                    }
                    if (CheckTranspose)
                    {
                        midiFilePlayer.MPTK_Transpose = UnityEngine.Random.Range(-12, 12);
                    }
                }
            }
        }
    }
}