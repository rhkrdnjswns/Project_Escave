using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
	public Rigidbody2D rig;
	public Animator anim;
	public BoxCollider2D standColl;
	public BoxCollider2D slideColl;
	public int booster;
	public float boosterSpeed;
	public float originSpeed;
	public SpriteRenderer sprite;

	public float hp;

	public int jumpCount = 0;
	private RaycastHit2D hitForJump;
	private int layerMask;
	public static Player instance;

	private bool isJump;
	private bool isSlide;

	private void Awake()
	{
		instance = this;
		rig = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		layerMask = 1 << LayerMask.NameToLayer("Land");
		sprite = GetComponent<SpriteRenderer>();
		//리지드바디, 애니메이터, 콜라이더, 레이어마스크 초기화
	}
	private void Start()
	{
		StartCoroutine(Co_Booster());
		StartCoroutine(Co_JumpCheck());
	}
    public void Jump()
	{
		if (jumpCount > 1 || isSlide) return;
		isJump = true;
		jumpCount++;
		if (jumpCount == 2)
        {
			anim.SetBool("DoubleJump", true);
		}
		rig.velocity = Vector2.up * 11;
	}

	private IEnumerator Co_JumpCheck()
	{
		while (true)
		{
			if(transform.position.y > -1)
            {
                while (true)
                {
					Debug.DrawRay(transform.position, -transform.up * 0.7f, Color.red);
					hitForJump = Physics2D.Raycast(transform.position, -transform.up, 0.7f, layerMask);
					if (hitForJump.collider != null)
					{
						jumpCount = 0;
						isJump = false;
						anim.SetBool("DoubleJump", false);
                        if (isSlide)
                        {
							anim.SetBool("Sliding", true);
							ColliderUtility(true);
						}
						break;
					}
					yield return null;
				}
            }
			yield return null;
		}
	}
	public void Sliding(bool isUp)
	{
		isSlide = true;
		if (booster == 1) return;
        if (isJump)
        {
            if (isUp)
            {
				isSlide = false;
				return;
			}
            else
            {
				return;
            }
		}
		if (!isUp)
		{
			anim.SetBool("Sliding", true);
			ColliderUtility(true);
		}
		else
		{
			anim.SetBool("Sliding", false);
			ColliderUtility(false);
			isSlide = false;
			print("포인터 업");
		}
	}
	private void ColliderUtility(bool isSlide)
    {
		standColl.enabled = !isSlide;
		slideColl.enabled = isSlide;
    }
    private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Booster")
		{
			booster = 1;
			InGameManager.instance.BoosterUtility(boosterSpeed, true);
			coll.gameObject.SetActive(false);
			//스크롤 속도 증가
		}
		if (coll.tag == "Obstacle")
		{
			switch (booster)
			{
				case 0:
					InGameManager.instance.GameOver();
					break;
				case 1:
					coll.GetComponent<ObstacleUtility>().isDestroy = true;
					break;
				case 2:
					Debug.Log("부스터 종료 후 무적 시간");
					break;
			}
		}
		if (coll.tag == "MapChanger")
		{ 
			InGameManager.instance.ChangeMap();
		}
	}

	private IEnumerator Co_Booster()
	{
		while (true)
		{
			yield return new WaitUntil(() => booster == 1);
			anim.SetBool("Booster", true);
			yield return new WaitForSeconds(3.5f);
			InGameManager.instance.BoosterUtility(originSpeed, false);
			anim.SetBool("Booster", false);
			booster = 2;
            for (int i = 0; i < 4; i++)
            {
				sprite.DOColor(new Color(0.3f, 0.3f, 0.3f), 0.2f);
				yield return new WaitForSeconds(0.2f);
				sprite.DOColor(Color.white, 0.2f);
				yield return new WaitForSeconds(0.2f);
			}
			booster = 0;
		}
	}
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
			Jump();
        }
    }
}
