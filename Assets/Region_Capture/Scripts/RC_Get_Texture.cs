using UnityEngine;
using System.Collections;

public class RC_Get_Texture : MonoBehaviour {

	public Camera RenderCamera;
	[Space(20)]
	public bool FreezeEnable = false;
    private bool SkinnedMesh;

    void Start () 
	{
		if (RenderCamera) StartCoroutine(WaitForTexture());
		if (FreezeEnable && RenderCamera) RenderCamera.enabled = false;
        if (GetComponent<SkinnedMeshRenderer>()) SkinnedMesh = true;
    }


	private IEnumerator WaitForTexture() 
	{
		while (!RenderCamera.targetTexture)
		{
            if (!RenderCamera.gameObject.activeInHierarchy) break;
			Debug.Log("Wait for RenderTexture");
			yield return new WaitForSeconds(0.01f);
			yield return new WaitForEndOfFrame();;
		}

        if (RenderCamera.targetTexture)
        {
            if (SkinnedMesh) GetComponent<SkinnedMeshRenderer>().material.SetTexture("_MainTex", RenderCamera.targetTexture);
            else GetComponent<MeshRenderer>().material.SetTexture("_MainTex", RenderCamera.targetTexture);
        }
	}
		
	void onGUI () 
	{
		if (RenderCamera)
		{
            if (FreezeEnable) RenderCamera.enabled = false;
			else RenderCamera.enabled = true;
		}
	}
}