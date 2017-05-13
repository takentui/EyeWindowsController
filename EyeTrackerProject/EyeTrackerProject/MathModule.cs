using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeTrackerProject
{
    class MathModule
    {
        public List<int> x_left;
        public List<int> x_right;
        public List<int> y_left;
        public List<int> y_right;

        public float left_corner_x, left_corner_y;
        public float right_corner_x, right_corner_y;

        public List<float> x_mouse;
        public List<float> y_mouse;

        public float width_ratio;
        public float height_ratio;
        public MathModule()
        {
            x_left = new List<int>();
            x_right = new List<int>();
            y_left = new List<int>();
            y_right = new List<int>();
            x_mouse = new List<float>();
            y_mouse = new List<float>();
        }

        public MathModule(float w, float h):this()
        {
            width_ratio = w;
            height_ratio = h;
        }
        public void addPoint(int x_l, int y_l, int x_r, int y_r)
        {
            if (x_left.Count() > 40)
            {
                //x_left.RemoveAt(39);
                //y_left.RemoveAt(39);
            }
            x_left.Add(x_l);
            y_left.Add(y_l);

            if (x_right.Count() > 40)
            {
                //x_right.RemoveAt(39);
                //y_right.RemoveAt(39);
            }
            x_right.Add(x_r);
            y_right.Add(y_r);
            ApproximateCoordinates();
        }

        public void ApproximateCoordinates()
        {
            float x, y;
            if (x_mouse.Count() > 40)
            {
                //x_mouse.RemoveAt(39);
                //y_left.RemoveAt(39);
            }
            x = x_left.Last() * width_ratio * left_corner_x;
            x += x_right.Last() * width_ratio * right_corner_x;
            x_mouse.Add(x / 2);

            y = y_left.Last() * left_corner_y;
            y += y_right.Last() * right_corner_y;
            y_mouse.Add( height_ratio - (y / 2));
        }

        public void setLeftCorner(float x, float y)
        { 
            left_corner_x = x;
            left_corner_y = y  * 5;
        }

        public void setRightCorner(float x, float y)
        {
            right_corner_x = x;
            right_corner_y = y * 5;
        }

        public int getX()
        {
            if (x_mouse.Count() > 0)
            {
                return (int) x_mouse.Last();
            }
            return 0;
        }

        public int getY()
        {
            if (x_mouse.Count() > 0)
            {
                return (int) y_mouse.Last();
            }
            return 0;
        }
    }
}
