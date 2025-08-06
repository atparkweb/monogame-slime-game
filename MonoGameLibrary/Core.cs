using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary;

public class Core : Game
{
    // Singleton
    private static Core _sInstance;

    public static Core Instance => _sInstance;
    
    public static GraphicsDeviceManager Graphics { get; private set; }
    
    public static new GraphicsDevice GraphicsDevice { get; private set; }
    
    public static SpriteBatch SpriteBatch { get; private set; }
    
    public static new ContentManager Content { get; private set; }

    public Core(string title, int width, int height, bool fullScreen)
    {
        // Ensure that multiple cores are not created.
        if (_sInstance != null)
        {
            throw new InvalidOperationException($"Only one instance of the {nameof(Core)} is allowed.");
        }
        
        // Store reference to engine for global member access.
        _sInstance = this;
        
        // Create a new graphics device manager.
        Graphics = new GraphicsDeviceManager(this);
        
        // Set the graphics defaults.
        Graphics.PreferredBackBufferWidth = width;
        Graphics.PreferredBackBufferHeight = height;
        Graphics.IsFullScreen = fullScreen;
        
        // Apply the graphic presentation changes.
        Graphics.ApplyChanges();
        
        // Set the window title.
        Window.Title = title;
        
        // Set the core's content manager to a reference of the base Game's
        // content manager
        Content = base.Content;
        
        // Set the root directory for content.
        Content.RootDirectory = "Content";
        
        // Mouse is visible by default
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
        
        // Set the core's graphics device to a reference of the base Game's
        // graphics device.
        GraphicsDevice = base.GraphicsDevice;
        
        // Create the sprite batch instance.
        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }
}