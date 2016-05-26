using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;

namespace Tower_Defense_Project
{
    struct Polygon
    {
        private Texture2D tex { get; set; }
        public List<Vector2[]> Sides { get; set; }
        public List<Vector2> Points { get; set; }

        public Polygon(List<Vector2> points) : this()
        {
            Points = points;
            Sides = new List<Vector2[]>();
            tex = Main.GameContent.Load<Texture2D>("Textures/help");
            CreateSides();
        }

        private void CreateSides()
        {
            foreach (Vector2 i in Points)
            {
                Vector2 shortestDistance = Vector2.Zero;
                float distance = float.MaxValue;

                for (int l = 0; l < Points.Count; l++)
                {
                    if (Points[l] != i && !SideExists(i, Points[l]))
                    {
                        if (Vector2.Distance(i, Points[l]) < distance)
                        {
                            shortestDistance = Points[l];
                            distance = Vector2.Distance(i, Points[l]);
                        }
                    }
                }
                Sides.Add(new Vector2[2] { i, shortestDistance });
            }
        }

        private bool SideExists(Vector2 a, Vector2 b)
        {
            foreach (Vector2[] i in Sides)
            {
                if (i.Contains(a) && i.Contains(b)) return true;
            }
            return false;
        }

        private void DrawLine(SpriteBatch spriteBatch, Vector2 begin, Vector2 end, int width = 1)
        {
            RectangleF r = new RectangleF(begin.X, begin.Y, (end - begin).Length() + width, width);
            Vector2 v = Vector2.Normalize(begin - end);
            float angle = (float)Math.Acos(Vector2.Dot(v, -Vector2.UnitX));
            if (begin.Y > end.Y) angle = MathUtil.TwoPi - angle;
            spriteBatch.Draw(tex, r, null, Color.Black, angle, Vector2.Zero, SpriteEffects.None, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Vector2[] i in Sides)
            {
                DrawLine(spriteBatch, i[0], i[1]);
            }
        }
    }
}
