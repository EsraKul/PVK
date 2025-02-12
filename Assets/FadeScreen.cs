using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    private float fadeDuration = 2;
    public Color fadeColor;
    private Renderer rend;
    public GameObject corner;
    public GameObject goBack;

    // från teleport

    public SteamVR_Action_Boolean m_TeleportAction;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private bool m_HasPosition = false;
    private bool m_IsTeleporting = false;
    private float m_FadeTime = 0.5f;
    public GameObject left;
    public GameObject right;
    private Vector3 oldPosition;

    // Start is called before the first frame update
    void Start()
    {
        corner = GameObject.FindWithTag("Corner");
        goBack = GameObject.FindWithTag("GoBack");
        left = GameObject.FindWithTag("left");
        right = GameObject.FindWithTag("right");
        goBack.SetActive(false);
        corner.SetActive(true);
        left.SetActive(false);
        right.SetActive(false);
        rend = GetComponent<Renderer>();

        if (fadeOnStart)
            FadeIn();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) // NEXT
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
          
        }

        if (Input.GetKeyDown(KeyCode.R)) // RESTART 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
  

        if (Input.GetKeyDown(KeyCode.B)) // BLACK
        {
            FadeOut();
            corner.SetActive(false);
            goBack.SetActive(true);
            BackToSquare(oldPosition);
            FadeIn();

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) //// LEFT
        {
            GetPosition(left);
            Debug.Log("Scene: " + SceneManager.GetActiveScene().name + "\n" + " Direction: Left");
            StartCoroutine(WaitBlack());
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) //// RIGHT
        {
            GetPosition(right);
            Debug.Log("Scene: " + SceneManager.GetActiveScene().name + "\n" + " Direction: Right");
            StartCoroutine(WaitBlack());
        }


    }

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;

        while (timer < fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut,timer/fadeDuration);
            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_Color", newColor2);
    }

    public IEnumerator WaitBlack()
    {
        float timer = 0;

        while (timer < 5)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        FadeOut();
        corner.SetActive(false);
        goBack.SetActive(true);
        BackToSquare(oldPosition);
        FadeIn();


    }

    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    private void GetPosition(GameObject dir)
    {
        // Get camerarig and head position
        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        // Figure out translation
        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        oldPosition = groundPosition;
        Vector3 translateVector = dir.transform.position - groundPosition;

        // Move
        StartCoroutine(MoveRig(cameraRig, translateVector));
    }

    private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {
        // Flag
        m_IsTeleporting = true;

        // Fade to black
        SteamVR_Fade.Start(Color.black, m_FadeTime, true);

        //Apply translation
        yield return new WaitForSeconds(m_FadeTime);
        cameraRig.position += translation;

        // Fade back to clear
        SteamVR_Fade.Start(Color.clear, m_FadeTime, true);

        //De-flag
        m_IsTeleporting = false;
    }

    // FOR GOING BACK TO SQUARE WITHOUT FADE
    private void BackToSquare(Vector3 dir)
    {
        // Get camerarig and head position
        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        // Figure out translation
        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        
        Vector3 translateVector = dir - groundPosition;

        cameraRig.position += translateVector;

        // Move
        //StartCoroutine(BackMoveRig(cameraRig, translateVector));

    }

}
