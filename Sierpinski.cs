using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Sierpinski
{
	/// <summary>
	/// Draw a Sierpinski triangle
	/// </summary>
	public class Sierpinski : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private bool paintOk;
		private const int RESIZE_DONE = 0x232;

		public Sierpinski()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// Rest of constructor
			//
			paintOk = true;
			this.Resize += new EventHandler(OnResize);
			this.Paint += new PaintEventHandler(OnPaint);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Sierpinski));
			// 
			// Sierpinski
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.DarkSlateBlue;
			this.ClientSize = new System.Drawing.Size(520, 462);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Sierpinski";
			this.Text = "Sierpinski Triangle";

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Sierpinski());
		}

		private void OnPaint(object sender, System.Windows.Forms.PaintEventArgs pe)
		{
			if(! paintOk)
				return;

			// Figure out the first points for the triangle
			Point p1 = new Point( (this.Width / 2), 1 );
			Point p2 = new Point( 1, (this.Height - 40) );
			Point p3 = new Point( this.Width - 10, (this.Height - 40) );

			// And draw it!
			int depth = 10;
			Pen drawPen = new System.Drawing.Pen(Color.AliceBlue, 1);
			DrawSierpinski(p1, p2, p3, depth, pe.Graphics, drawPen);
		}

		// We have to catch the "resize done" (also "move done")
		// event so that we don't repaint the triangle at every step
		// of a resize.
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			if((m.Msg == RESIZE_DONE) && (! paintOk))
			{
				this.Invalidate();
				paintOk = true;
			}
			base.WndProc(ref m);
		}

		private void DrawSierpinski(Point p1, Point p2, Point p3, int depth, Graphics g, Pen drawPen)
		{
			// Base case
			if(depth == 0)
				return;

			// Draw the lines around the edges of the triangle
			g.DrawLine(drawPen, p1, p2);
			g.DrawLine(drawPen, p1, p3);
			g.DrawLine(drawPen, p2, p3);

			// Figure out the midpoints
			Point p1p2 = new Point(
				(p1.X + p2.X) / 2,
				(p1.Y + p2.Y) / 2);
			Point p1p3 = new Point(
				(p1.X + p3.X) / 2,
				(p1.Y + p3.Y) / 2);
			Point p2p3 = new Point(
				(p2.X + p3.X) / 2,
				(p2.Y + p3.Y) / 2);

			// Draw Sierpinki triangles inside the new points
			DrawSierpinski(p1, p1p2, p1p3, depth - 1, g, drawPen);
			DrawSierpinski(p1p2, p2, p2p3, depth - 1, g, drawPen);
			DrawSierpinski(p1p3, p2p3, p3, depth - 1, g, drawPen);
		}

		private void OnResize(Object sender, EventArgs e)
		{
			paintOk = false;
		}
	}
}


