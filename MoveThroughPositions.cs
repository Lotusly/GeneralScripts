using System.Collections;
using UnityEngine;

public class MoveThroughPositions : MonoBehaviour {

    public int index = 0;

    [SerializeField] private Transform[] PositionList;
    [SerializeField] private float Error;
    [SerializeField] private float Speed;
    private int listSize;
    private bool canMove = true;
	private Animator anim;

    void Start() {
        listSize = PositionList.Length;
        transform.LookAt(PositionList[index]);
		anim = gameObject.GetComponent<Animator>();
		anim.SetBool("IsWalking", false);
		canMove = false;
        if (gameObject.tag != "Enemy")
        {
            StartCoroutine(delayWalk());
        }
    }

	private IEnumerator delayWalk()
	{
		yield return new WaitForSecondsRealtime(Random.value);
		canMove = true;
		anim.SetBool("IsWalking",true);
	}

    public void StartMoving() {
        canMove = true;
        anim.SetBool("IsWalking", true);
    }

	public void StopMoving() {
		canMove = false;
        anim.SetBool("IsWalking", false);
    }

    public void SetSpeed(float x)
    {
        Speed = x;
        anim.speed = (float)0.5;
    }

    // Update is called once per frame
    void Update() {
        if (!canMove) return;
        
        if (listSize > 0 && Vector3.Distance(transform.position, PositionList[index].position) < Error){
            if (gameObject.tag == "Enemy" && index == 1)
            {
                CanvasCameraController.instance.ConfigureResetLevelCanvas("Dr. Xernon's robot slipped into the crowd and got away.\nTry again.", 2);
                CanvasCameraController.instance.SwitchCanvas("resetlevel");
            }
            index = (index + 1) % listSize;
            
        }
        transform.LookAt(PositionList[index]);
        transform.Translate(transform.forward * Speed * Time.deltaTime, Space.World);
    }

}