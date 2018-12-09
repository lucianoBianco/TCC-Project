using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;
using System;
using InfinityMusic;

namespace MidiPlayerTK
{
    public class TestMidiStreamScripting : MonoBehaviour
    {

        public MidiStreamPlayer midiStreamPlayer;

        [Range(0.1f, 10f)]
        public float DelayTimeChange = 1;

        public bool RandomPlay = true;
        public bool DrumKit = false;

        [Range(0, 127)]
        public int StartNote = 50;

        [Range(0, 127)]
        public int EndNote = 60;

        [Range(0, 127)]
        public int CurrentNote;

        [Range(0, 127)]
        public int CurrentPatch;

        private MPTKNote NotePlaying;
        private float LastTimeChange;

        // Use this for initialization
        void Start()
        {
            InitPlay();
            CurrentNote = StartNote;
        }

        /// <summary>
        /// Call defined from MidiPlayerGlobal event inspector
        /// </summary>
        public void EndLoadingSF()
        {
            Debug.Log("End loading SF, MPTK is ready to play");

            Debug.Log("List of presets available");
            int i = 0;
            foreach (string preset in MidiPlayerGlobal.MPTK_ListPreset)
                Debug.Log("   " + string.Format("[{0,3:000}] - {1}", i++, preset));
            i = 0;
            Debug.Log("List of drums available");
            foreach (string drum in MidiPlayerGlobal.MPTK_ListDrum)
                Debug.Log("   " + string.Format("[{0,3:000}] - {1}", i++, drum));

            Debug.Log("Load statistique");
            Debug.Log("   Time To Load SoundFont: " + Math.Round(MidiPlayerGlobal.MPTK_TimeToLoadSoundFont.TotalSeconds, 3).ToString() + " second");
            Debug.Log("   Time To Load Waves: " + Math.Round(MidiPlayerGlobal.MPTK_TimeToLoadWave.TotalSeconds, 3).ToString() + " second");
            Debug.Log("   Presets Loaded: " + MidiPlayerGlobal.MPTK_CountPresetLoaded);
            Debug.Log("   Waves Loaded: " + MidiPlayerGlobal.MPTK_CountWaveLoaded);

        }

        public void InitPlay()
        {

            LastTimeChange = Time.realtimeSinceStartup;
        }
        void OnGUI()
        {
            float startx = 25;
            float starty = 30;
            float maxwidth = Screen.width / 2;
            if (maxwidth < 300) maxwidth = 300;

            starty = GUISelectSoundFont.SelectSoundFont(startx, starty);

            if (midiStreamPlayer != null)
            {
                GUILayout.BeginArea(new Rect(startx, starty, maxwidth, 600));

                GUILayout.Space(20);
                midiStreamPlayer.MPTK_Volume = GUILayout.HorizontalSlider(midiStreamPlayer.MPTK_Volume * 100f, 0f, 100f) / 100f;

                GUILayout.Space(20);
                GUILayout.BeginHorizontal();
                RandomPlay = GUILayout.Toggle(RandomPlay, "   Random Play", GUILayout.Width(200));
                DrumKit = GUILayout.Toggle(DrumKit, "   Drum Kit", GUILayout.Width(200));
                GUILayout.EndHorizontal();
                GUILayout.Space(20);


                if (MidiPlayerGlobal.MPTK_ListPreset != null && MidiPlayerGlobal.MPTK_ListPreset.Count > 0)
                {
                    if (!RandomPlay && DrumKit)
                        if (MidiPlayerGlobal.MPTK_ListDrum != null && CurrentNote < MidiPlayerGlobal.MPTK_ListDrum.Count)
                            GUILayout.Label("Drum:" + MidiPlayerGlobal.MPTK_ListDrum[CurrentNote]);
                        else
                            GUILayout.Label("No Drumkit found in your SoundFont for key " + CurrentNote);
                    else
                    {
                        if (CurrentPatch >= 0 && CurrentPatch < MidiPlayerGlobal.MPTK_ListPreset.Count)
                            GUILayout.Label("Patch: " + MidiPlayerGlobal.MPTK_ListPreset[CurrentPatch]);
                        else
                            GUILayout.Label("Patch: " + CurrentPatch + " Nothing found");
                    }
                }

                GUILayout.Label("Go to your Hierarchy, select GameObject NotesGenerator, inspector contains some parameters to control your Midi player.");

                GUILayout.EndArea();
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (midiStreamPlayer != null)
            {
                float time = Time.realtimeSinceStartup - LastTimeChange;
                if (time > DelayTimeChange)
                {
                    LastTimeChange = Time.realtimeSinceStartup;

                    if (NotePlaying != null)
                    {
                        // Stop the note in case of second method
                        NotePlaying.Stop();
                        NotePlaying = null;
                    }

                    if (RandomPlay)
                    {
                        //
                        // First method to play notes: send a list of notes directly to the MidiStreamPlayer
                        // Useful for a long list of notes.
                        //
                        List<MPTKNote> notes = new List<MPTKNote>();
                        int rnd = UnityEngine.Random.Range(-4, 4) * 2;
                        if (!DrumKit)
                        {
                            notes.Add(CreateNote(60 + rnd, 0));
                            notes.Add(CreateNote(64 + rnd, 0));
                            notes.Add(CreateNote(67 + rnd, 0));
                        }
                        else
                        {
                            notes.Add(CreateDrum(54 + rnd, 0));
                            notes.Add(CreateDrum(54 + rnd, 150));
                            notes.Add(CreateDrum(54 + rnd, 300));
                        }
                        midiStreamPlayer.MPTK_Play(notes);
                    }
                    else
                    {
                        //
                        // Second methods to play notes: Play note from the Play method of MPTKNote
                        // Useful if you want to stop playing the note by script.
                        //
                        if (++CurrentNote > EndNote) CurrentNote = StartNote;
                        NotePlaying = new MPTKNote() { Note = CurrentNote, Delay = 0, Drum = DrumKit, Duration = 99999, Patch = CurrentPatch, Velocity = 100 };
                        NotePlaying.Play(midiStreamPlayer);
                    }
                }
            }
        }
        private MPTKNote CreateRandomNote()
        {
            MPTKNote note = new MPTKNote()
            {
                Note = 50 + UnityEngine.Random.Range(0, 4) * 2,
                Drum = false,
                Duration = UnityEngine.Random.Range(100, 1000),
                Patch = CurrentPatch,
                Velocity = 100,
                Delay = UnityEngine.Random.Range(0, 200),
            };
            return note;
        }
        private MPTKNote CreateNote(int key, float delay)
        {
            MPTKNote note = new MPTKNote()
            {
                Note = key,
                Drum = false,
                Duration = DelayTimeChange * 1000f,
                Patch = CurrentPatch,
                Velocity = 100,
                Delay = delay,
            };
            return note;
        }
        private MPTKNote CreateDrum(int key, float delay)
        {
            MPTKNote note = new MPTKNote()
            {
                Note = key,
                Drum = true,
                Duration = 0,
                Patch = 0,
                Velocity = 100,
                Delay = delay,
            };
            return note;
        }
    }
}