using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(-1f, 1f)] [SerializeField] float scrollSpeed = 0.5f;

    private float offset;
    private Material mat;
    Camera cam;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        cam = Camera.main;
    }

    void Update()
    {
        offset += (Time.deltaTime * scrollSpeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        transform.position = cam.transform.position + new Vector3(0, 0, 10);
    }
}
