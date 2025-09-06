using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;

namespace DungeonSlime;

public class Game1 : Core
{

    // Defines the slime animated sprite.
    private AnimatedSprite slime;
    
    // Defines the bat animated sprite.
    private AnimatedSprite bat;

    // Track the position of the slime.
    private Vector2 slimePosition;

    // Speed multiplier when moving.
    private const float MOVEMENT_SPEED = 5.0f;
    
    // Use a queue directly for input buffering.
    private Queue<Vector2> inputBuffer;
    private const int MAX_BUFFER_SIZE = 2;
    
    public Game1(): base("Dungeon Slime", 1280, 720, false)
    {
    }

    protected override void Initialize()
    {
        inputBuffer = new Queue<Vector2>(MAX_BUFFER_SIZE);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        var atlas =
            TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
        
        slime = atlas.CreateAnimatedSprite("slime-animation");
        slime.Scale = new Vector2(4.0f, 4.0f);
        
        bat = atlas.CreateAnimatedSprite("bat-animation");
        bat.Scale = new Vector2(4.0f, 4.0f);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
            ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        slime.Update(gameTime);
        bat.Update(gameTime);

        // Check for keyboard input and handle it.
        CheckKeyboardInput();

        // Check for gamepad input and handle it.
        CheckGamePadInput();
        
        base.Update(gameTime);
    }

    private void CheckKeyboardInput()
    {
        // If the space bar is held down increase the movement speed.
        float speed = MOVEMENT_SPEED;
        if (Input.Keyboard.IsKeyDown(Keys.Space))
        {
            speed *= 1.5f;
        }
        
        // If W or Up keys are down, move the slime up on the screen
        if (Input.Keyboard.IsKeyDown(Keys.W) ||
            Input.Keyboard.IsKeyDown(Keys.Up))
        {
            slimePosition.Y -= speed;
        }

        // If S or Down keys are down, move the slime down on the screen
        if (Input.Keyboard.IsKeyDown(Keys.S) ||
            Input.Keyboard.IsKeyDown(Keys.Down))
        {
            slimePosition.Y += speed;
        }
        
        // If A or Left keys are down, move the slime left on the screen
        if (Input.Keyboard.IsKeyDown(Keys.A) ||
            Input.Keyboard.IsKeyDown(Keys.Left))
        {
            slimePosition.X -= speed;
        }

        // If D or Right keys are down, move the slime right on the screen
        if (Input.Keyboard.IsKeyDown(Keys.D) ||
            Input.Keyboard.IsKeyDown(Keys.Right))
        {
            slimePosition.X += speed;
        }
    }

    private void CheckGamePadInput()
    {
        GamePadInfo gamePadOne = Input.GamePads[(int)PlayerIndex.One];

        // If the A button is held down, the movement speed increases by 1.5
        // and the gamepad vibrates as feedback to the player.
        float speed = MOVEMENT_SPEED;
        if (gamePadOne.IsButtonDown(Buttons.A))
        {
            speed *= 1.5f;
            gamePadOne.SetVibration(1.0f, TimeSpan.FromSeconds(1));
        }
        else
        {
            gamePadOne.StopVibration();
        }
        
        // Check thumbstick first since it has priority over which gamepad input
        // is movement.  It has priority since the thumbstick values provide a
        // more granular analog value that can be used for movement.
        if (gamePadOne.LeftThumbStick != Vector2.Zero)
        {
            slimePosition.X += gamePadOne.LeftThumbStick.X * speed;
            slimePosition.Y -= gamePadOne.LeftThumbStick.Y * speed;
        }
        else
        {
            // If DPadUp is down, move the slime up on the screen.
            if (gamePadOne.IsButtonDown(Buttons.DPadUp))
            {
                slimePosition.Y -= speed;
            }

            // If DPadDown is down, move the slime down on the screen.
            if (gamePadOne.IsButtonDown(Buttons.DPadDown))
            {
                slimePosition.Y += speed;
            }

            // If DPapLeft is down, move the slime left on the screen.
            if (gamePadOne.IsButtonDown(Buttons.DPadLeft))
            {
                slimePosition.X -= speed;
            }

            // If DPadRight is down, move the slime right on the screen.
            if (gamePadOne.IsButtonDown(Buttons.DPadRight))
            {
                slimePosition.X += speed;
            }
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        // The bounds of the icon within the texture.
        Rectangle iconSourceRect = new Rectangle(0, 0, 128, 128);
        
        // The bounds of the word mark within the texture.
        Rectangle wordmarkSourceRect = new Rectangle(150, 34, 458, 58);

        // Begin sprite batch to prepare for rendering.
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        // Draw the slime sprite.
        slime.Draw(SpriteBatch, slimePosition);
        
        // Draw the bat sprite 10px to the right of the slime.
        bat.Draw(SpriteBatch, new Vector2(slime.Width + 10, 0));
        
        // ALWAYS end the sprite batch when finished
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}