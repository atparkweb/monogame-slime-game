using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

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
    
    public Game1(): base("Dungeon Slime", 1280, 720, false)
    {
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
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
        // Get the state of the keyboard input.
        KeyboardState keyboardState = Keyboard.GetState();
        
        // If the space bar is held down increase the movement speed.
        float speed = MOVEMENT_SPEED;
        if (keyboardState.IsKeyDown(Keys.Space))
        {
            speed *= 1.5f;
        }
        
        // If W or Up keys are down, move the slime up on the screen
        if (keyboardState.IsKeyDown(Keys.W) ||
            keyboardState.IsKeyDown(Keys.Up))
        {
            slimePosition.Y -= speed;
        }

        // If S or Down keys are down, move the slime down on the screen
        if (keyboardState.IsKeyDown(Keys.S) ||
            keyboardState.IsKeyDown(Keys.Down))
        {
            slimePosition.Y += speed;
        }
        
        // If A or Left keys are down, move the slime left on the screen
        if (keyboardState.IsKeyDown(Keys.A) ||
            keyboardState.IsKeyDown(Keys.Left))
        {
            slimePosition.X -= speed;
        }

        // If D or Right keys are down, move the slime right on the screen
        if (keyboardState.IsKeyDown(Keys.D) ||
            keyboardState.IsKeyDown(Keys.Right))
        {
            slimePosition.X += speed;
        }
    }

    private void CheckGamePadInput()
    {
        GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

        // If the A button is held down, the movement speed increases by 1.5
        // and the gamepad vibrates as feedback to the player.
        float speed = MOVEMENT_SPEED;
        if (gamePadState.IsButtonDown(Buttons.A))
        {
            speed *= 1.5f;
            GamePad.SetVibration(PlayerIndex.One, 1.0f, 1.0f);
        }
        else
        {
            GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
        }
        
        // Check thumbstick first since it has priority over which gamepad input
        // is movement.  It has priority since the thumbstick values provide a
        // more granular analog value that can be used for movement.
        if (gamePadState.ThumbSticks.Left != Vector2.Zero)
        {
            slimePosition.X += gamePadState.ThumbSticks.Left.X * speed;
            slimePosition.Y -= gamePadState.ThumbSticks.Left.Y * speed;
        }
        else
        {
            // If DPadUp is down, move the slime up on the screen.
            if (gamePadState.IsButtonDown(Buttons.DPadUp))
            {
                slimePosition.Y -= speed;
            }

            // If DPadDown is down, move the slime down on the screen.
            if (gamePadState.IsButtonDown(Buttons.DPadDown))
            {
                slimePosition.Y += speed;
            }

            // If DPapLeft is down, move the slime left on the screen.
            if (gamePadState.IsButtonDown(Buttons.DPadLeft))
            {
                slimePosition.X -= speed;
            }

            // If DPadRight is down, move the slime right on the screen.
            if (gamePadState.IsButtonDown(Buttons.DPadRight))
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