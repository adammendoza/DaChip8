﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace DanTup.DaChip8
{
	public partial class Screen : Form
	{
		readonly Chip8 chip8;
		readonly Bitmap screen;
		readonly string ROM = "../../../ROMs/Chip-8 Pack/Chip-8 Games/Pong (1 player).ch8";

		// For timing..
		readonly Stopwatch stopWatch = Stopwatch.StartNew();
		readonly TimeSpan targetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60);
		TimeSpan lastTime;

		public Screen()
		{
			InitializeComponent();

			screen = new Bitmap(64, 32, PixelFormat.Format1bppIndexed);
			chip8 = new Chip8(screen);
			chip8.LoadProgram(File.ReadAllBytes(ROM));

			Application.Idle += IdleTick;
		}

		void IdleTick(object sender, EventArgs e)
		{
			var currentTime = stopWatch.Elapsed;
			var elapsedTime = currentTime - lastTime;

			while (elapsedTime >= targetElapsedTime)
			{
				Tick();
				elapsedTime -= targetElapsedTime;
				lastTime += targetElapsedTime;
			}

			Invalidate();
		}

		void Tick()
		{
			chip8.Tick();
		}
	}
}
