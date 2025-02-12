using UnityEngine;
using System.Collections;
using UnityEditor; 
using System.IO;
using System;

[CustomEditor (typeof (TextureGenerator))]  
public class TextureGeneratorEditor : Editor {

	public override void OnInspectorGUI(){
		
		TextureGenerator textGen = (TextureGenerator)target;
	

		if (DrawDefaultInspector ()){			//if value changed

			if (textGen.autoUpdate){
				textGen.GenerateTexture();
			}
		}


		if (GUILayout.Button("Generate")){
			textGen.GenerateTexture();
		}

		if (GUILayout.Button("SaveTexture")){
			Texture2D text = (Texture2D)textGen.gameObject.GetComponent<TextureDisplay>().textureRend.gameObject.GetComponent<Renderer>().sharedMaterial.mainTexture;
			text.Apply();
			byte[] bytes = text.EncodeToPNG();

			string textureName = "SavedTexture";
			DateTime tid = System.DateTime.Now;
			string[] month = new string[] {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};
			string timestamp = month[tid.Month] + "-"+ tid.DayOfWeek.ToString() + "-" + tid.Hour.ToString() + "-" + tid.Minute.ToString() +  "-" + tid.Second.ToString();
			/*
			char slash = '/';
			char colon = ':';
			string temp = ""; 
			foreach (char c in timestamp){

				if (c == slash){
					temp += colon;
				}
				else{
					temp += c;
				}

			}

			timestamp = temp;*/
			Debug.Log("timestamp: " + timestamp);
			File.WriteAllBytes("Assets/" + textureName + timestamp + ".png", bytes); 

		}





	


	} 
}
