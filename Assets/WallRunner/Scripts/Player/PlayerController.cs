using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace WallRunner
{
	public class PlayerController : MonoBehaviour
	{
		// Start is called before the first frame update
		private bool rotating = false;
		private bool powerActive = false;
		private float powerCooldown = 5.0f;
		private float maxCooldown = 25.0f;

		public Score score;
		[SerializeField] private InputActionReference rotate;
		[SerializeField] private InputActionReference power;
		[SerializeField] private Sprite inactive;
		[SerializeField] private Sprite active;
		[SerializeField] private Button powerButton;
		[SerializeField] private TextMeshProUGUI powerCooldownValue;
		[SerializeField] private ParticleSystem powerFX;
		[SerializeField] private ParticleSystem deathFX;
		[SerializeField] private ParticleSystem landFX;


		private new BoxCollider collider;

		private void Start()
		{
			// Initialized controls
			rotate.action.Enable();
			rotate.action.performed += OnRotate;
			
			power.action.Enable();
			power.action.performed += OnPower;
			

			collider = gameObject.GetComponent<BoxCollider>();
		}

		/// <summary> Reads the x value of a vector2 inputs to see if it was in the left or right direction </summary>
		private void OnRotate(InputAction.CallbackContext _context)
		{
			Vector2 input = _context.ReadValue<Vector2>();

			if(input.x < 0 && !rotating)
			{
				StartCoroutine(PlayerRotatorLeft_CR());
			}

			if(input.x > 0 && !rotating)
			{
				StartCoroutine(PlayerRotatorRight_CR());
			}
		}

		/// <summary> Links the controls from input action to the power activation coroutine </summary>
		private void OnPower(InputAction.CallbackContext _context)
		{
			OnPowerActivation();
		}
	
		/// <summary> Links the controls from input action to the power activation coroutine </summary>
		public void OnPowerActivation()
		{
			StartCoroutine(PowerActivation_CR());
		}

		
		private void OnDestroy()
		{
			rotate.action.performed -= OnRotate;
			power.action.performed -= OnPower;
		}

		
		// Update is called once per frame
		private void Update()
		{
			// Handles cooldown timer

			if(powerActive == false)
			{
				powerCooldown -= Time.deltaTime;
			}
			powerCooldown = Mathf.Clamp(powerCooldown, 0, maxCooldown);


			// Changes sprite for the power button and hides cooldown number when it reaches 0
			if(powerCooldown > 0)
			{
				powerButton.image.sprite = inactive;
				powerCooldownValue.text = powerCooldown.ToString("0");
			}
			else
			{
				powerButton.image.sprite = active;
				powerCooldownValue.text = " ";
			}

			if(Input.GetKeyDown(KeyCode.UpArrow) && !rotating)
			{
				StartCoroutine(PlayerFlip_CR());
			}
		}


		/// <summary> Handles the particle effect and booleans for the power being active </summary>
		public IEnumerator PowerActivation_CR()
		{
			
			if(powerCooldown <= 0 && !powerActive)
			{
				powerActive = true;
				
				ParticleSystem electricEffect = Instantiate(powerFX, gameObject.transform.position + (transform.up * 0.4f) , Quaternion.identity, transform);
			
				
				
				yield return new WaitForSeconds(electricEffect.duration);				
				powerCooldown = maxCooldown;
				powerActive = false;
				Destroy(electricEffect);
			}
		}

		/// <summary> Linking the rotate left input action to the coroutine </summary>
		private void OnRotateLeft(InputAction.CallbackContext _context)
		{
			if(!rotating)
			{
				StartCoroutine(PlayerRotatorLeft_CR());
			}
		}
		
		/// <summary> Linking the rotate right input action to the coroutine </summary>
		private void OnRotateRight(InputAction.CallbackContext _context)
		{
			if(!rotating)
			{
				StartCoroutine(PlayerRotatorRight_CR());
			}
		}

		private IEnumerator PlayerFlip_CR()
		{
			rotating = true;

			float t = 0f;
			float maxTime = 0.25f;
			Vector3 startPos = transform.position;
			Vector3 endPos = startPos + transform.up * 2.5f;
			endPos.z = transform.position.z;
			Quaternion startRot = gameObject.transform.rotation;
			Quaternion endRot = startRot * Quaternion.AngleAxis(180, Vector3.forward);


			while(t < maxTime * 1.1f)

			{
				gameObject.transform.rotation = Quaternion.Slerp(startRot, endRot, t / maxTime); 
				transform.position = Vector3.Lerp(startPos, endPos, t / maxTime);


				yield return null;

				t += Time.deltaTime;
			}

	
			rotating = false;
		}
		/// <summary> Moves and rotates the player in an clockwise direction </summary>
		private IEnumerator PlayerRotatorLeft_CR()
		{
			// Moves and rotates the player in a clockwise direction
			rotating = true;

			float t = 0f;
			float maxTime = 0.125f;
			Vector3 startPos = transform.position;
			Vector3 endPos = startPos + transform.right * -1.25f + transform.up * 1.25f;
			endPos.z = transform.position.z;
			Quaternion startRot = gameObject.transform.rotation;
			Quaternion endRot = startRot * Quaternion.AngleAxis(-90, Vector3.forward);


			while(t < maxTime * 1.1f)

			{
				gameObject.transform.rotation = Quaternion.Slerp(startRot, endRot, t / maxTime); 
				transform.position = Vector3.Lerp(startPos, endPos, t / maxTime);


				yield return null;

				t += Time.deltaTime;
			}

	
			rotating = false;
		}

		/// <summary> Moves and rotates the player in an anti-clockwise direction </summary>
		private IEnumerator PlayerRotatorRight_CR()
		{
			

			rotating = true;

			float t = 0f;
			float maxTime = 0.125f;
			Vector3 startPos = transform.position;
			Vector3 endPos = startPos + transform.right * 1.25f + transform.up * 1.25f;
			endPos.z = transform.position.z;
			Quaternion startRot = gameObject.transform.rotation;
			Quaternion endRot = startRot * Quaternion.AngleAxis(90, Vector3.forward);

			while(t < maxTime * 1.1f)
			{
				gameObject.transform.rotation = Quaternion.Slerp(startRot, endRot, t / maxTime);
				gameObject.transform.position = Vector3.Lerp(startPos, endPos, t / maxTime);

				yield return null;

				t += Time.deltaTime;
			}
		
			rotating = false;
		}
		
		
		
		/// <summary> Plays particle effect and changes scenes on player death</summary>
		private IEnumerator PlayerDeath_CR()
		{
		
			ParticleSystem death = Instantiate(deathFX, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 2f), Quaternion.identity);
			gameObject.transform.localScale = Vector3.zero;
			gameObject.transform.position = new Vector3(0, -50, -30);

			
			
			yield return new WaitForSeconds(death.duration);
			score.finalScore = score.GetScore();
			DontDestroyOnLoad(this);
			SceneManager.LoadScene("Death");
		}

		private void OnCollisionEnter(Collision _collision)
		{
			if(_collision.gameObject.CompareTag("Obstacle") && powerActive == false)
			{
				StartCoroutine(PlayerDeath_CR());
			}
		}
	}
}