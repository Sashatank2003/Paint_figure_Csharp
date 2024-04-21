using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Drawing.Imaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;
using SharpGL.SceneGraph;

namespace LR4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Color BackGroundColor = Color.Black;
        int count_l = 0;
        int count_t = 0;
        int count_r = 0;
        int count_p = 0;
        string TypeLine;
        bool scale = true;
        Color color = Color.White;
        string Pen;
        List<Rectangle> rectangles = new List<Rectangle>();
        void DrawLines()    //процедура рисования линии
        {
            for (int k = 0; k < count_l; k++)
            {
                OpenGL gl = openGLControl1.OpenGL;
                gl.Color(lines[k].color);
                gl.LineWidth(lines[k].linewidth);
                if (lines[k].TypeLine == "dottedline")
                {
                    gl.LineStipple(1, 0x00FF);
                    gl.Enable(OpenGL.GL_LINE_STIPPLE);
                }
                gl.Begin(OpenGL.GL_LINES);
                gl.Vertex(lines[k].x1, lines[k].y1);
                gl.Vertex(lines[k].x2, lines[k].y2);
                gl.End();
                gl.Flush();
                if (lines[k].TypeLine == "dottedline")
                {
                    gl.Disable(OpenGL.GL_LINE_STIPPLE);
                }
            }
        }

        void DrawTriangles()   //процедура рисования треугольника
        {
            for (int k = 0; k < count_t; k++)
            {
                OpenGL gl = openGLControl1.OpenGL;
                gl.Color(triangles[k].color);
                gl.Begin(OpenGL.GL_TRIANGLES);
                gl.Vertex(triangles[k].x1, triangles[k].y1);
                gl.Vertex(triangles[k].x2, triangles[k].y2);
                gl.Vertex(triangles[k].x3, triangles[k].y3);
                gl.End();
                gl.Flush();
            }
        }

        void DrawRect()    //процедура рисования прямоугольника
        {
            for (int k = 0; k < count_r; k++)
            {
                OpenGL gl = openGLControl1.OpenGL;
                gl.Color(quads[k].color);
                gl.Begin(OpenGL.GL_QUADS);
                gl.Vertex(quads[k].x1, quads[k].y1);
                gl.Vertex(quads[k].x2, quads[k].y2);
                gl.Vertex(quads[k].x3, quads[k].y3);
                gl.Vertex(quads[k].x4, quads[k].y4);
                gl.End();
                gl.Flush();
            }
        }

        void DrawPolygons() //процедура рисования многоугольника(5 уголв)
        {
            for (int k = 0; k < count_p; k++)
            {
                OpenGL gl = openGLControl1.OpenGL;
                gl.Color(polygons[k].color);
                gl.Begin(OpenGL.GL_POLYGON);
                gl.Vertex(polygons[k].x1, polygons[k].y1);
                gl.Vertex(polygons[k].x2, polygons[k].y2);
                gl.Vertex(polygons[k].x3, polygons[k].y3);
                gl.Vertex(polygons[k].x4, polygons[k].y4);
                gl.Vertex(polygons[k].x5, polygons[k].y5);
                gl.End();
                gl.Flush();
            }
        }
        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            OpenGL gl = openGLControl1.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -2.0f);
            if (scale)
            {
                gl.Scale((float)numericUpDown12.Value * 0.1, (float)numericUpDown12.Value * 0.1, (float)numericUpDown12.Value * 0.1);
            }
            else
            {
                gl.Scale((float)numericUpDown12.Value * (-0.1), (float)numericUpDown12.Value * (-0.1), (float)numericUpDown12.Value * (-0.1));
            }

            for (int k = 0; k < count_l; k++)
            {
                DrawLines();
            }

            for (int k = 0; k < count_t; k++)
            {
                DrawTriangles();
            }

            for (int k = 0; k < count_r; k++)
            {
                DrawRect();
            }

            for (int k = 0; k < count_p; k++)
            {
                DrawPolygons();
            }
     }

        class line
        {
            public float x1, x2, y1, y2, linewidth;
            public Color color;
            public string TypeLine;
            public line(Color _color, float _x1 = 0, float _x2 = 0, float _y1 = 0, float _y2 = 0, string _TypeLine = "", float _linewidth = 1)
            {
                x1 = _x1;
                x2 = _x2;
                y1 = _y1;
                y2 = _y2;
                color = _color;
                TypeLine = _TypeLine;
                linewidth = _linewidth;
            }
        }
        class quad
        {
            public float x1, x2, x3, x4, y1, y2, y3, y4;
            public Color color;
            public quad(Color _color, float _x1 = 0, float _x2 = 0, float _x3 = 0, float _x4 = 0, float _y1 = 0, float _y2 = 0, float _y3 = 0, float _y4 = 0)
            {
                x1 = _x1;
                x2 = _x2;
                x3 = _x3;
                x4 = _x4;
                y1 = _y1;
                y2 = _y2;
                y3 = _y3;
                y4 = _y4;
                color = _color;
            }
        }

        class triangle
        {
            public float x1, x2, x3, y1, y2, y3;
            public Color color;
            public triangle(Color _color, float _x1 = 0, float _x2 = 0, float _x3 = 0, float _y1 = 0, float _y2 = 0, float _y3 = 0)
            {
                x1 = _x1;
                x2 = _x2;
                x3 = _x3;
                y1 = _y1;
                y2 = _y2;
                y3 = _y3;
                color = _color;
            }
        }

        class polygon
        {
            public float x1, x2, x3, x4, x5, y1, y2, y3, y4, y5;
            public Color color;
            public polygon(Color _color, float _x1 = 0, float _x2 = 0, float _x3 = 0, float _x4 = 0, float _x5 = 0, float _y1 = 0, float _y2 = 0, float _y3 = 0, float _y4 = 0, float _y5 = 0)
            {
                x1 = _x1;
                x2 = _x2;
                x3 = _x3;
                x4 = _x4;
                x5 = _x5;
                y1 = _y1;
                y2 = _y2;
                y3 = _y3;
                y4 = _y4;
                y5 = _y5;
                color = _color;
            }
        }

        line[] lines = new line[10];
        triangle[] triangles = new triangle[10];
        quad[] quads = new quad[10];
        polygon[] polygons = new polygon[10];

        private void openGLControl1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void openGLControl1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void openGLControl1_MouseUp(object sender, MouseEventArgs e)
        {
        }


        private void button1_Click(object sender, EventArgs e) 
        {
            Pen = "triangle";
            triangles[count_t] = new triangle(color, float.Parse(numericUpDown2.Text), float.Parse(numericUpDown4.Text), float.Parse(numericUpDown6.Text), float.Parse(numericUpDown3.Text), float.Parse(numericUpDown5.Text), float.Parse(numericUpDown7.Text));
            count_t++;
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            Pen = "line";
            lines[count_l] = new line(color, float.Parse(numericUpDown2.Text), float.Parse(numericUpDown4.Text), float.Parse(numericUpDown3.Text), float.Parse(numericUpDown5.Text), TypeLine, (float)trackBar1.Value * 2);
            count_l++;
        }

        private void button3_Click(object sender, EventArgs e) 
        {
            TypeLine = "dottedline";
        }

        private void button4_Click(object sender, EventArgs e) 
        {
            Pen = "rectangle";
            quads[count_r] = new quad(color, float.Parse(numericUpDown2.Text), float.Parse(numericUpDown4.Text), float.Parse(numericUpDown6.Text), float.Parse(numericUpDown8.Text), float.Parse(numericUpDown3.Text), float.Parse(numericUpDown5.Text), float.Parse(numericUpDown7.Text), float.Parse(numericUpDown9.Text));
            count_r++;
        }

        private void button5_Click(object sender, EventArgs e) 
        {
            Pen = "polygon";
            polygons[count_p] = new polygon(color, float.Parse(numericUpDown2.Text), float.Parse(numericUpDown4.Text), float.Parse(numericUpDown6.Text),
               float.Parse(numericUpDown8.Text), float.Parse(numericUpDown10.Text), float.Parse(numericUpDown3.Text), float.Parse(numericUpDown5.Text),
               float.Parse(numericUpDown7.Text), float.Parse(numericUpDown9.Text), float.Parse(numericUpDown11.Text));
            count_p++;
        }

        private void button6_Click(object sender, EventArgs e) 
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                BackGroundColor = colorDialog1.Color;
                OpenGL gl = openGLControl1.OpenGL;
                gl.ClearColor(BackGroundColor.R, BackGroundColor.G, BackGroundColor.B, 0f);
            }
        }

        private void button7_Click(object sender, EventArgs e) 
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        private void button8_Click(object sender, EventArgs e) 
        {
            TypeLine = "solid";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void numericUpDown12_ValueChanged(object sender, EventArgs e)
        {

        }

        private void openGLControl1_Load(object sender, EventArgs e)
        {

        }
        
    }
}
