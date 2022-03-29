using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static PratikKargo.MainPage;

namespace PratikKargo
{
    class CityInfo
    {
        Random rand = new Random();
        public Point[] cities;
        public CityInfo(int numCities)
        {
            this.cities = new Point[numCities];
            GenerateCities(numCities);
        }

        public CityInfo()
        {

        }

        // Generates a given number of randomly placed cities
        private void GenerateCities(int num)
        {

         

            this.cities[0] = new Point((float)38.69301742336234, (float)35.549143170636405);
            this.cities[1] = new Point((float)38.69490153218463, (float)35.5430599207414);
            this.cities[2] = new Point((float)38.69591474353492, (float)35.539412116502035);
            this.cities[3] = new Point((float)38.698024856628976, (float)35.5322667117552);
            this.cities[4] = new Point((float)38.699088263038554, (float)35.52854380568901);

            
               


        }

        public void AddCities(Point[] c)
        {
            this.cities = c;
        }

        // Plots the cities on the form


        // Returns the distance between two cities
        public double Distance(Point one, Point two)
        {
            float x = (float)((two.X - one.X) *
                (two.X - one.X));

            float y = (float)((two.Y - one.Y) *
                (two.Y - one.Y));

            return (float)Math.Sqrt((double)x + (double)y);
        }
    }
}
