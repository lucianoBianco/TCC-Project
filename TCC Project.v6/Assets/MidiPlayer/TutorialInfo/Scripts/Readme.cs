//
// Readme from the tower defense template 
// https://assetstore.unity.com/packages/essentials/tutorial-projects/tower-defense-template-107692
// Thank to the Unity Team !
//
using System;
using UnityEngine;

public class Readme : ScriptableObject {
	public Texture2D icon;
	public string title;
	public Section[] sections;
	//public bool loadedLayout;
	
	[Serializable]
	public class Section {
		public string heading, text, linkText, url;
	}
}
