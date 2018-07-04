using UnityEngine;

namespace ColorSwitchGame
{
    /// <summary>
    /// This script defines a player, which 
    /// </summary>
    public class CSGPlayer:MonoBehaviour 
	{
		// Various variables we will access often during the game, so we cache them
		internal Transform thisTransform;
		static CSGGameController gameController;
		internal CSGShop shopObject;
		internal Rigidbody2D rigidBody;
		internal Transform cameraObject;

		[Tooltip("The index of the color of this object. It corresponds to the color list defined in the gamecontroller ( 0 is the first color, 1 is the seconds color, etc)")]
		public int colorIndex = 0;

		[Tooltip("The object that will be colored when this player touches a color ball")]
		public Transform coloredObject;

		[Tooltip("How high this player can jump")]
		public float jumpPower = 10;

		// The player is dead now. When dead, the player can't move.
		internal bool isDead = false;

		[Tooltip("The effect that is created at the location of this object when it is destroyed")]
		public Transform deathEffect;

		[Tooltip("Various sounds that play when the enemy touches the target, or when it gets hurt")]
		public AudioClip soundJump;
		public string soundSourceTag = "Sound";
		internal GameObject soundSource;


		// Use this for initialization
		void Start() 
		{
			thisTransform = transform;

			// Register the game controller for easier access
			if ( gameController == null )    
			    gameController = (CSGGameController) FindObjectOfType(typeof(CSGGameController));

			// Register the rigidbody for easier access
		    rigidBody = GetComponent<Rigidbody2D>();
		    if (rigidBody == null)
		        rigidBody = this.gameObject.AddComponent<Rigidbody2D>();
			    

			// Register the camera for easier access
			if ( Camera.main.transform )    cameraObject = Camera.main.transform;

			//Assign the sound source for easier access
			if ( GameObject.FindGameObjectWithTag(soundSourceTag) )    soundSource = GameObject.FindGameObjectWithTag(soundSourceTag);

			// Check if we have a shop in this level and get the current player sprite from it
			if ( shopObject == null && FindObjectOfType(typeof(CSGShop)) )    
			{
				shopObject = (CSGShop) FindObjectOfType(typeof(CSGShop));

				coloredObject.GetComponent<SpriteRenderer>().sprite = shopObject.playerBalls[PlayerPrefs.GetInt("PlayerIndex", 0)].icon;

			}

			// Set the current color of the object, based on the index
			SetColor(0);
		}

		/// <summary>
		/// Update this instance.
		/// </summary>
		void Update() 
		{
			// Set the local scale based on the movement speed of the player. This gives a bouncy effect to the player object, like a squishy ball.
			if ( coloredObject )    coloredObject.parent.localScale = new Vector2( 1 - rigidBody.velocity.y * 0.05f, 1 + rigidBody.velocity.y * 0.05f);
		}

		/// <summary>
		/// Update that happens after the regular Update() call
		/// </summary>
		void LateUpdate()
		{
			// Make the camera follow the position of the player
			if ( thisTransform.position.y > cameraObject.position.y )    cameraObject.position = new Vector3( cameraObject.position.x, thisTransform.position.y, cameraObject.position.z);
		}

		/// <summary>
		/// Jump up based on JumpPower, using rigidbody velocity
		/// </summary>
		void Jump()
		{
			// Add to the vertical velocity of the object
			rigidBody.velocity = Vector2.up * jumpPower;

			// If there is a sound source and audio to play, play the sound from the audio source
			if ( soundSource && soundJump )    soundSource.GetComponent<AudioSource>().PlayOneShot(soundJump);
		}

		/// <summary>
		/// Goes to the next color from the list of possible colors
		/// </summary>
		public void NextColor()
		{
			ChangeColor(1);
		}

		/// <summary>
		/// Changes the color based on the list of possible colors
		/// </summary>
		/// <param name="changeValue">Change value.</param>
		public void ChangeColor( int changeValue )
		{
			// Loop through the color list
			if ( colorIndex < gameController.colorList.Length - 1 )    colorIndex++;
			else    colorIndex = 0;
			
			// Set the color of the object based on the index
			SetColor(colorIndex);
		}

		/// <summary>
		/// Set one of the random colors from a list
		/// </summary>
		/// <param name="possibleValues">Possible values.</param>
		public void RandomColor( int[] possibleValues )
		{
			// Choose a random value from the list of possible color indexes
			int randomColorIndex = Mathf.FloorToInt(Random.Range(0, possibleValues.Length));

			// If the new color is the same as the old one, go to the next color in the list
			if ( colorIndex == possibleValues[randomColorIndex] )
			{
				if ( randomColorIndex < possibleValues.Length - 1 )    randomColorIndex++;
				else    randomColorIndex = 0;
			}

			SetColor(possibleValues[randomColorIndex]);
		}

        /// <summary>
        /// Sets the color of the object based on the index. This corresponds to the list of colors in the gamecontroller
        /// </summary>
        /// <param name="setValue">Set value.</param>




        public void SetColor( int setValue )
		{
			colorIndex = setValue;

            // Assign the color of this block from the list of colors in the gamecontroller
            if (setValue < gameController.colorList.Length)
                //coloredObject.GetComponent<SpriteRenderer>().color = gameController.colorList[colorIndex];//原始变色代码

                //Debug.Log(gameController.colorList[colorIndex]);//检查颜色代码
                //Debug.Log(coloredObject.GetComponent<SpriteRenderer>().sprite.name[0]);//检查文件名首位

            switch (gameController.colorList[colorIndex].ToString())//根据不同的颜色代码显示相应颜色的图片使结构支持显示层次复杂的图片-2018.5.25-周桂海
            {
                case "RGBA(0.914, 0.263, 0.208, 1.000)"://红
                    switch (coloredObject.GetComponent<SpriteRenderer>().sprite.name[0])//根据图片名前缀判定图片形状，选择相应形状的图片和颜色-2018.5.26-周桂海
                        {
                            case 'a':
                                coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("a_0");
                                break;
                            case 'b':
                                coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("b_0");
                                break;
                            case 'c':
                                coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("c_0");
                                break;
                            case 'd':
                                coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("d_0");
                                break;
                            case 'e':
                                coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("e_0");
                                break;
                        }
                    break;
                case "RGBA(0.204, 0.655, 0.325, 1.000)"://绿
                    switch (coloredObject.GetComponent<SpriteRenderer>().sprite.name[0])
                    {
                        case 'a':
                            coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("a_1");
                            break;
                        case 'b':
                            coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("b_1");
                            break;
                        case 'c':
                            coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("c_1");
                            break;
                        case 'd':
                            coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("d_1");
                            break;
                            case 'e':
                                coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("e_1");
                                break;
                        }
                    break;
                case "RGBA(0.259, 0.518, 0.953, 1.000)"://蓝
                    switch (coloredObject.GetComponent<SpriteRenderer>().sprite.name[0])
                    {
                        case 'a':
                            coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("a_2");
                            break;
                        case 'b':
                            coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("b_2");
                            break;
                        case 'c':
                            coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("c_2");
                            break;
                        case 'd':
                            coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("d_2");
                            break;
                            case 'e':
                                coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("e_2");
                                break;
                        }
                    break;
                case "RGBA(0.980, 0.733, 0.020, 1.000)"://黄
                    switch (coloredObject.GetComponent<SpriteRenderer>().sprite.name[0])
                    {
                        case 'a':
                            coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("a_3");
                            break;
                        case 'b':
                            coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("b_3");
                            break;
                        case 'c':
                            coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("c_3");
                            break;
                        case 'd':
                            coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("d_3");
                            break;
                        case 'e':
                            coloredObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("e_3");
                            break;
                        }
                    break;
            }
        }

        /// <summary>
        /// Kills the object, and creates a death effect
        /// </summary>
        public void Die()
		{
			if ( isDead == false )
			{
				isDead = true;

				GameObject.FindGameObjectWithTag("GameController").SendMessage("GameOver", 0.5f);

				// If there is a death effect, create it at the position of the player
				if( deathEffect )    Instantiate(deathEffect, transform.position, transform.rotation);

				// Remove the object from the game
				Destroy(gameObject);
			}
		}
	}
}
