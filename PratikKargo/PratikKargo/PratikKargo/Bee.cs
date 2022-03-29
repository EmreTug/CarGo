using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PratikKargo
{
    class Bee
    {
        public int status;
        public Point[] memoryPath;
        public double measureOfQuality;
        public int numberOfVisits;

        public Bee(int stat, Point[] path, double measure, int numVisits)
        {
            this.status = stat;
            this.memoryPath = path;
            this.measureOfQuality = measure;
            this.numberOfVisits = numVisits;
        }
    }
}
