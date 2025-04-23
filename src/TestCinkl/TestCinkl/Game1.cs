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
    private const float JumpVelocity = -6f;
    private bool _isOnGround;
    private List<Rectangle> _tiles;
    private Rectangle _tileSourceRect;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _playerPosition = new Vector2(100, 100);
        _velocity = Vector2.Zero;
        
        _tiles = new List<Rectangle>
        {
            new Rectangle(50, 200, 8, 8),
            new Rectangle(58, 200, 8, 8),
            new Rectangle(66, 200, 8, 8),
            new Rectangle(100, 300, 8, 8),
            new Rectangle(200, 250, 8, 8)
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

        if (keyboard.IsKeyDown(Keys.A))
            _velocity.X = -MoveSpeed;
        else if (keyboard.IsKeyDown(Keys.D))
            _velocity.X = MoveSpeed;
        else
            _velocity.X = 0;
        
        if (_isOnGround && (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Space)))
        {
            _velocity.Y = JumpVelocity;
            _isOnGround = false; // už není na zemi
        }
        
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Aplikuj gravitaci na vertikální složku rychlosti
        _velocity.Y += Gravity;

        // Aktualizuj pozici podle rychlosti
        _playerPosition += _velocity;
        
        // Rozměry hráče
        int playerWidth = 8;
        int playerHeight = 8;
        
        int screenWidth = _graphics.PreferredBackBufferWidth;
        int screenHeight = _graphics.PreferredBackBufferHeight;
        // TODO: Add your update logic here
        
        // Kolize s pravým a levým okrajem
        if (_playerPosition.X < 0)
        {
            _playerPosition.X = 0;
            _velocity.X = 0;
        }
        else if (_playerPosition.X + playerWidth > screenWidth)
        {
            _playerPosition.X = screenWidth - playerWidth;
            _velocity.X = 0;
        }
        
        // Kolize s okraji
        if (_playerPosition.X < 0)
        {
            _playerPosition.X = 0;
            _velocity.X = 0;
        }
        else if (_playerPosition.X + playerWidth > screenWidth)
        {
            _playerPosition.X = screenWidth - playerWidth;
            _velocity.X = 0;
        }

        // Kolize se spodní hranou (zemí)
        if (_playerPosition.Y + playerHeight >= screenHeight)
        {
            _playerPosition.Y = screenHeight - playerHeight;
            _velocity.Y = 0;
            _isOnGround = true;
        }
        else
        {
            _isOnGround = false;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        
        foreach (var tile in _tiles)
        {
            _spriteBatch.Draw(_spriteSheet, new Vector2(tile.X, tile.Y), _tileSourceRect, Color.White);
        }
        
        Rectangle sourceRectangle = new Rectangle(0, 0, 8, 8);
        _tileSourceRect = new Rectangle(8, 8, 8, 8);
        _spriteBatch.Draw(_spriteSheet, _playerPosition, sourceRectangle, Color.White);
        _spriteBatch.End();


        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}