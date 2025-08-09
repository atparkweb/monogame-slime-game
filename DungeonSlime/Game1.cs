using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

namespace DungeonSlime;

public class Game1 : Core
{
    private Texture2D _logo;
    
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
        _logo = Content.Load<Texture2D>("images/logo");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
            ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Begin sprite batch to prepare for rendering
        SpriteBatch.Begin();
        
        // Draw the logo texture
        SpriteBatch.Draw(
            _logo,              // texture
            new Vector2(        // position
                (Window.ClientBounds.Width * 0.5f) - (_logo.Width * 0.25f),
                (Window.ClientBounds.Height * 0.5f) - (_logo.Height * 0.25f)),
            null,               // source rect
            Color.White,        // color mask tint
            0.0f,               // rotation
            Vector2.Zero,       // origin
            0.5f,               // scale
            SpriteEffects.None, // effects
            0.0f                // layer depth
        );
        
        // ALWAYS end the sprite batch when finished
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}