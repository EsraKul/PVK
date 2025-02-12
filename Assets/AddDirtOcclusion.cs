using UnityEngine;
using System.Collections;

public class AddDirtOcclusion : MonoBehaviour {

	public Texture2D dirtTexture;

	public float occlusionBrightThreshold = 0.5f;

	// Use this for initialization
	void Start () {



		Renderer rend = GetComponent<Renderer>();
		Material mat = rend.material;
		Texture2D occlusionMap = (Texture2D)rend.sharedMaterial.GetTexture("_OcclusionMap");
		Texture2D dirtOcclusioned = new Texture2D(dirtTexture.width, dirtTexture.height);


		for (int x = 0; x < occlusionMap.width; x++){

			for (int y = 0; y < occlusionMap.height; y++){

				if (occlusionMap.GetPixel(x,y).grayscale > occlusionBrightThreshold){
					
					dirtOcclusioned.SetPixel(x,y, Color.clear);

				}

				else {
					dirtOcclusioned.SetPixel(x,y, dirtTexture.GetPixel(x,y));
				}

			}

		}
		//dirtOcclusioned.alphaIsTransparency = true;
		dirtOcclusioned.Apply();

		//mat.mainTexture = dirtOcclusioned;
		rend.material = mat;
		rend.material.SetTexture("_DetailMask", dirtOcclusioned);
		rend.material.SetTexture("_DetailAlbedoMap", dirtOcclusioned);
		rend.material.EnableKeyword("_DETAIL_MULX2");



	}
	

}
