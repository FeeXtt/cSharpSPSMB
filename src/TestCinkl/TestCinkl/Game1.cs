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

        // Kolize s horním a dolním okrajem
        if (_playerPosition.Y < 0)
        {
            _playerPosition.Y = 0;
            _velocity.Y = 0;
        }
        else if (_playerPosition.Y + playerHeight > screenHeight)
        {
            _playerPosition.Y = screenHeight - playerHeight;
            _velocity.Y = 0;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        Rectangle sourceRectangle = new Rectangle(0, 0, 8, 8);
        _spriteBatch.Draw(_spriteSheet, _playerPosition, sourceRectangle, Color.White);
        _spriteBatch.End();


        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}