using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Content;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;
using Duality;

namespace Tower_Defense_Project
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Game
    {
        List<Vector2> listT = new List<Vector2>(), listO = new List<Vector2>(), listL = new List<Vector2>();
        RectangleF oldBag = new RectangleF(50, 50, 25, 25);
        SpriteBatch spriteBatch;
        Texture2D tex;
        static ContentManager content;
        static GraphicsDeviceManager graphics;
        static KeyboardManager keyboardManager;
        static KeyboardState keyboard;
        static MouseManager mouseManager;
        static MouseState mouse;

        #region Properties
        public static ContentManager GameContent
        {
            get { return content; }
        }

        public static GraphicsDeviceManager Graphics
        {
            get { return graphics; }
        }

        public static KeyboardManager Keyboard
        {
            get { return keyboardManager; }
        }

        public static KeyboardState CurrentKeyboard
        {
            get { return keyboard; }
        }

        public static MouseManager Mouse
        {
            get { return mouseManager; }
        }

        public static MouseState CurrentMouse
        {
            get { return mouse; }
            set { mouse = value; }
        }
        #endregion

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            mouseManager = new MouseManager(this);
            keyboardManager = new KeyboardManager(this);
            content = Content;
            IsMouseVisible = true;
            //graphics.PreferredBackBufferHeight = 480;
            //graphics.PreferredBackBufferWidth = 800;
            /*graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 1440;
            graphics.IsFullScreen = true;
            IsMouseVisible = true;*/
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            ErrorHandler.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tex = Main.GameContent.Load<Texture2D>("Textures/help");

            listT.Add(Vector2.Zero);
            listT.Add(new Vector2(50, 25));
            listT.Add(new Vector2(75, 100));
            listT.Add(new Vector2(150, 100));

            listO.Add(Vector2.Zero);
            listO.Add(new Vector2(40, 25));
            listO.Add(new Vector2(65, 100));
            listO.Add(new Vector2(140, 100));

            listL.Add(Vector2.Zero);
            listL.Add(new Vector2(60, 25));
            listL.Add(new Vector2(85, 100));
            listL.Add(new Vector2(160, 100));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, graphics.GraphicsDevice.BlendStates.NonPremultiplied);

            Window.AllowUserResizing = false;

            spriteBatch.Draw(tex, oldBag, Color.Red);

            Console.WriteLine(Colliding(listT, oldBag));
            Console.WriteLine(Colliding(listO, oldBag));
            Console.WriteLine(Colliding(listL, oldBag));

            for (float i = 0; i < 1; i = i + .01f)
            {
                Vector2 t = Vector2.CatmullRom(listT[0], listT[1], listT[2], listT[3], i),
                    o = Vector2.CatmullRom(listO[0], listO[1], listO[2], listO[3], i),
                    l = Vector2.CatmullRom(listL[0], listL[1], listL[2], listL[3], i);
                RectangleF rect = new RectangleF(t.X, t.Y, 2, 2),
                    reco = new RectangleF(o.X, o.Y, 2, 2),
                    recl = new RectangleF(l.X, l.Y, 2, 2);
                spriteBatch.Draw(tex, rect, Color.Black);
                spriteBatch.Draw(tex, reco, Color.Black);
                spriteBatch.Draw(tex, recl, Color.Black);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool Colliding(List<Vector2> curve, RectangleF rect)
        {
            for (float i = 0; i < 1; i = i + .1f)
            {
                Vector2 t = Vector2.CatmullRom(curve[0], curve[1], curve[2], curve[3], i);

                if (rect.Contains(t)) return true;
            }
            return false;
        }
    }
}