using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace DungeonSlime;

public class Game1 : Core
{
    private TextureRegion slime;

    private TextureRegion bat;
    
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
        var atlasTexture = Content.Load<Texture2D>("images/atlas");

        var atlas =
            TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
        
        slime = atlas.GetRegion("slime");
        bat = atlas.GetRegion("bat");
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

        // Begin sprite batch to prepare for rendering
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        // Draw the slime texture region at a scale of 4.0
        slime.Draw(SpriteBatch, Vector2.Zero, Color.White, 0.0f, Vector2.One, 4.0f, SpriteEffects.None, 0.0f);
        
        // Draw the bat texture region 10px to the right of the slime at a scale of 4.0
        bat.Draw(SpriteBatch, new Vector2(slime.Width * 4.0f + 10, 0), Color.White, 0.0f, Vector2.One, 4.0f, SpriteEffects.None, 1.0f);
        
        // ALWAYS end the sprite batch when finished
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}