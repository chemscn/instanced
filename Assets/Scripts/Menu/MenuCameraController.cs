using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    public float cameraSpeed;
    public GameObject TitleEffectsContainer;
    private Vector2 motion;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        motion = new Vector2(1, 0);
        transform.Translate(motion * cameraSpeed * Time.deltaTime);

        TitleEffectsContainer.transform.Translate(motion * cameraSpeed * Time.deltaTime);

    }
}
