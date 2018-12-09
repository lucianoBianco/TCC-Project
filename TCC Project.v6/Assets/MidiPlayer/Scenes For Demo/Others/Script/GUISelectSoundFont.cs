using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;
using System;

namespace MidiPlayerTK
{
    public class GUISelectSoundFont : MonoBehaviour
    {
        static public List<string> SoundFonts = null;


        static public float SelectSoundFont(float startx, float starty)
        {
            int btwidth = 400;
            int btheight = 30;
            int lbheight = 20;
            int espace = 5;

            if (SoundFonts == null)
                SoundFonts = MidiPlayerGlobal.MPTK_ListSoundFont;

            if (SoundFonts != null)
            {
                GUI.Label(new Rect(startx, starty, btwidth, lbheight), "Select a soundfont");
                starty += lbheight + espace;
                foreach (string sfName in SoundFonts)
                {
                    GUI.color = Color.white;
                    if (sfName == MidiPlayerGlobal.CurrentMidiSet.ActiveSounFontInfo.Name)
                    {
                        GUI.color = new Color(.7f, .9f, .7f, 1f);
                    }
                    if (GUI.Button(new Rect(startx , starty, btwidth, btheight), sfName))
                    {
                        MidiPlayerGlobal.MPTK_SelectSoundFont(sfName);
                    }
                    starty += btheight + espace;
                }
                GUI.color = Color.white;
            }
            else
            {
                GUI.Label(new Rect(startx, starty, btwidth, lbheight), "No Soundfont found");
                starty += lbheight + espace;
            }

            return starty;
        }
    }
}