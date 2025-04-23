using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestCinkl;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _spriteSheet;
    private Vector2 _playerPosition;
    private Vector2 _velocity;
    private const float Gravity = 0.3f;
    private const float MoveSpeed = 2.0f;
    private const float JumpVelocity = -10f;
    private bool _isOnGround;
    private List<Rectangle> _tiles;
    private Rectangle _tileSourceRect;
    private const int PlayerSize = 32;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        
    }
    private bool IsColliding(Rectangle a, Rectangle b)
    {
        return a.Intersects(b);
    }

    protected override void Initialize()
    {
        _playerPosition = new Vector2(100, 100);
        _velocity = Vector2.Zero;
        
        _tiles = new List<Rectangle>
        {
            new Rectangle(50, 300, 32, 32),
            new Rectangle(82, 300, 32, 32),
            new Rectangle(114, 300, 32, 32),
            new Rectangle(200, 250, 32, 32),
            new Rectangle(300, 350, 32, 32)
        };
        _tileSourceRect = new Rectangle(8, 0, 8, 8);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _spriteSheet = Content.Load<Texture2D>("spritesheet");

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
        Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

    KeyboardState keyboard = Keyboard.GetState();

    // Pohyb do stran
    if (keyboard.IsKeyDown(Keys.A))
        _velocity.X = -MoveSpeed;
    else if (keyboard.IsKeyDown(Keys.D))
        _velocity.X = MoveSpeed;
    else
        _velocity.X = 0;

    // Skok (s kontrolou, že je hráč na zemi)
    if (_isOnGround && (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Space)))
    {
        _velocity.Y = JumpVelocity;
        _isOnGround = false; // už není na zemi
    }

    // Aplikování gravitace na vertikální rychlost
    _velocity.Y += Gravity;

    // Aktualizace pozice hráče
    _playerPosition += _velocity;

    Rectangle playerRect = new Rectangle(
        (int)_playerPosition.X,
        (int)_playerPosition.Y,
        PlayerSize,
        PlayerSize
    );

    _isOnGround = false;

    // **Kontrola hranic okna** (proti levému, pravému a dolnímu okraji)
    int screenWidth = _graphics.PreferredBackBufferWidth;
    int screenHeight = _graphics.PreferredBackBufferHeight;

    // Levý okraj
    if (_playerPosition.X < 0)
    {
        _playerPosition.X = 0;
        _velocity.X = 0;
    }
    // Pravý okraj
    else if (_playerPosition.X + PlayerSize > screenWidth)
    {
        _playerPosition.X = screenWidth - PlayerSize;
        _velocity.X = 0;
    }

    // Dolní okraj (země)
    if (_playerPosition.Y + PlayerSize > screenHeight)
    {
        _playerPosition.Y = screenHeight - PlayerSize;
        _velocity.Y = 0;
        _isOnGround = true;
    }

    // Detekce kolizí s platformami
    foreach (var tile in _tiles)
    {
        if (IsColliding(playerRect, tile))
        {
            // Kolize ze spodu (přistání na platformě)
            if (_velocity.Y >= 0 && playerRect.Bottom > tile.Top && playerRect.Top < tile.Top)
            {
                // Pokud narazíme na platformu zespodu, přichytíme hráče na platformu
                _playerPosition.Y = tile.Top - playerRect.Height;
                _velocity.Y = 0;
                _isOnGround = true;
                break; // po kolizi se zastavíme
            }
            // Kolize zeshora (aby hráč neprošel platformou)
            else if (_velocity.Y < 0 && playerRect.Top < tile.Bottom && playerRect.Bottom > tile.Bottom)
            {
                _playerPosition.Y = tile.Bottom; // přichytí hráče na spodní část platformy
                _velocity.Y = 0; // zastaví vertikální pohyb
            }
        }
            base.Update(gameTime);
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        _tileSourceRect = new Rectangle(8, 8, 8, 8);
        foreach (var tile in _tiles)
        {
            _spriteBatch.Draw( _spriteSheet,
                new Rectangle(tile.X, tile.Y, 32, 32), // cílový obdélník
                _tileSourceRect, // zdrojový sprite (8x8)
                Color.White);
        }
        Rectangle playerSource = new Rectangle(0, 0, 8, 8); // sprite hráče v sheetu
        Rectangle playerDestination = new Rectangle(
            (int)_playerPosition.X,
            (int)_playerPosition.Y,
            PlayerSize,  // 32
            PlayerSize   // 32
        );
        
        
        _spriteBatch.Draw(_spriteSheet, playerDestination, playerSource, Color.White);
        _spriteBatch.End();


        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}