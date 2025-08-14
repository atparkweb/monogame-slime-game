using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace DungeonSlime;

public class Game1 : Core
{

    // Defines the slime sprite
    private Sprite slime;
    
    // Defines the bat sprite
    private Sprite bat;
    
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
        
        slime = atlas.CreateSprite("slime");
        slime.Scale = new Vector2(4.0f, 4.0f);
        slime.Color = Color.LimeGreen;
        
        bat = atlas.CreateSprite("bat");
        bat.Scale = new Vector2(4.0f, 4.0f);
        bat.Color = Color.Red;
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
        
        // The bounds of the icon within the texture.
        Rectangle iconSourceRect = new Rectangle(0, 0, 128, 128);
        
        // The bounds of the word mark within the texture.
        Rectangle wordmarkSourceRect = new Rectangle(150, 34, 458, 58);

        // Begin sprite batch to prepare for rendering.
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        // Draw the slime sprite.
        slime.Draw(SpriteBatch, Vector2.One);
        
        // Draw the bat sprite 10px to the right of the slime.
        bat.Draw(SpriteBatch, new Vector2(slime.Width + 10, 0));
        
        // ALWAYS end the sprite batch when finished
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}