﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Adastra
{
	/// <summary>
	/// Emotiv FileSystem reader
	/// </summary>
	public class EmotivFileSystemDataReader : IRawDataReader
	{
		string filename;
		int counter = 0;
		System.IO.StreamReader file;
		IDigitalSignalProcessor dsp = null;

		public EmotivFileSystemDataReader(string filename, IDigitalSignalProcessor dsp)
		{
            Init(filename);

			this.dsp = dsp;
		}

        public EmotivFileSystemDataReader(string filename)
		{
            Init(filename);
		}

        private void Init(string filename)
        {
            this.filename = filename;

            file = new System.IO.StreamReader(filename);

            file.ReadLine();//skip one line
        }

		public event RawDataChangedEventHandler Values;

		public void Update()
		{
			if (file == null) return;

			double[] result = new double[14];

			string line = file.ReadLine();

			if (line != null)
			{
				string[] columns = line.Split(',');

				for (int i = 0; i < 14; i++)
					result[i] = double.Parse(columns[i + 2]);

				counter++;

				if (dsp != null)
					dsp.DoWork(ref result);

				Values(result);
			}
			else
			{
				file.Close();
				file = null;
			}
		}

		public double AdjustChannel(int number, double value)
		{
			//double[] channelAdjustments = { 1, 3, 5, 6, 7, 8, 9, 10, 11, 12, 12.5, 13.5, 15.5, 17.5 };
			//double[] channelAdjustments = { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27 };

			return (value + number);
		}
	}
}