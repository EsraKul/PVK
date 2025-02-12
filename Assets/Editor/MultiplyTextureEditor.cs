using UnityEngine;
using System.Collections;
using UnityEditor; 

[CustomEditor (typeof (MultiplyTexture))]  
public class MultiplyTextureEditor : Editor {

	public override void OnInspectorGUI(){

		MultiplyTexture multiplyScript = (MultiplyTexture)target;


		if (DrawDefaultInspector ()){			//if value changed

		//	if (textGen.autoUpdate){
		//		textGen.GenerateTexture();
		//	}
		}


		if (GUILayout.Button("Multiply")){
			multiplyScript.Multiply();
		}

		if (GUILayout.Button("LinearBurn")){
			multiplyScript.LinearBurn();
		}

		if (GUILayout.Button("ThresholdDirt")){
			multiplyScript.ThresholdDirt();
		}

		if (GUILayout.Button("Grunge")){
			multiplyScript.GrungeTexture();
		}
		if (GUILayout.Button("Perlin")){
			multiplyScript.ApplyPerlin();
		}
	} 



}
