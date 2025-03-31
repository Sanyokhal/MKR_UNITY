using UnityEngine;

public class BallController : MonoBehaviour
{
   public float rayLength = 10f; 
    public float smoothSpeed = 5f; 
    public float kickForce = 20f; 
    private Vector3 aimDirection = Vector3.forward; 
    private Vector3 targetDirection = Vector3.forward; 
    private LineRenderer lineRenderer;
    private Rigidbody ballRigidbody;
    private bool notifiedAboutFall = false;
    private bool notifiedAboutGoal = false;

    private bool isKicking = false; 

    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    void Update()
    {
        if (!isKicking)
        {
            float horizontal = Input.GetAxis("Horizontal"); 
            float vertical = Input.GetAxis("Vertical"); 
            if (horizontal != 0 || vertical != 0)
            {
                targetDirection = new Vector3(horizontal, 0, vertical).normalized;
            }

            aimDirection = Vector3.Slerp(aimDirection, targetDirection, smoothSpeed * Time.deltaTime);
            lineRenderer.enabled = true; 
            lineRenderer.SetPosition(0, transform.position); 
            lineRenderer.SetPosition(1, transform.position + aimDirection * rayLength); 

            if (Input.GetKeyDown(KeyCode.Space))
            {
                KickBall();
            }
        }else{
            float yPosition = transform.position.y;
            if(yPosition < 0f){
                if(!notifiedAboutFall){
                    Debug.Log("Схоже м`яч випав за карту");
                    notifiedAboutFall = true;
                    Time.timeScale = 0;
                }
            }
        }
    }

    void KickBall()
    {
        isKicking = true;
        lineRenderer.enabled = false;
        ballRigidbody.linearVelocity = Vector3.zero;
        ballRigidbody.AddForce(aimDirection * kickForce, ForceMode.Impulse);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Net"))
        {
            if(!notifiedAboutGoal){
                Debug.Log("ГООООЛЛ!!!");
                notifiedAboutGoal = true;
            }
        }else if (collision.gameObject.CompareTag("Bar"))
        {
            Debug.Log("ПОПАВ В ШТАНГУ");
        }else if (collision.gameObject.CompareTag("GoalKeeper"))
        {
            Debug.Log("Воротар спіймав м'яч :(");
            Time.timeScale = 0;
        }
    }
}