﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Garage_Josefin
{
    class Bus : Vehicle
    {
        private double length;

        public double Length
        {
            get { return length; }
            set { length = value; }
        }

        public Bus(double length, string regNumb, string color, int wheelCount)
            : base(regNumb, color, wheelCount)
        {
            Length = length;
        }
        public override string StringifyOutput()
        {
            string vehicleInfo = $"{base.StringifyOutput()}, {Length} m long.";
            return vehicleInfo;
        }
    }
}
