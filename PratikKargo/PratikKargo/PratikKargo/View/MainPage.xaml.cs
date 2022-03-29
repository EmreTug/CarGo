using PratikKargo.Model;
using PratikKargo.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static PratikKargo.Model.DistanceModel;

namespace PratikKargo
{
    public partial class MainPage : ContentPage
    {
        public class Ant
        {//ant class only contains the index to the city--use city class to get the 2D location
            private int currentLocation;
            private int nextLocation;
            public int tour_number;
            public List<int> haveBeenList;//where ant has been (Tabu)
            public List<int> tourList;//total itinerary
            public double distanceTraveled;


            public Ant()
            {
                currentLocation = 0;
                nextLocation = 0;
                tour_number = 0;
                haveBeenList = new List<int>();
                tourList = new List<int>();
                distanceTraveled = 0;
            }
            //construct ants with starting position and number of cities for tabu & tour list
            public Ant(int startPos, int num_cities)
            {
                currentLocation = startPos;
                nextLocation = 0;
                tour_number = 0;
                haveBeenList = new List<int>();
                tourList = new List<int>();
                distanceTraveled = 0;
                for (int k = 0; k < num_cities; k++)
                {
                    haveBeenList.Add(0);//0 if ant hasnt been to city, 1 if it has
                    tourList.Add(0);
                }
            }
            //update total distance traveled
            public void update_total_distance(double distance)
            {
                distanceTraveled += distance;
            }


            //set ants current location
            public void set_current_location(int location)
            {
                currentLocation = location;
            }
            //set ants next location
            public void set_next_location(int next)
            {
                nextLocation = next;
            }
            //get ants current location
            public int get_current_location()
            {
                return currentLocation;
            }
            public double getDistance()
            {
                return distanceTraveled;
            }

            public void resetDistance()
            {
                distanceTraveled = 0;
            }

        }
        //city class
        public class City
        {
            Point location; //the city's location in x,y coordinate

            public City()
            {
                location = new Point(0, 0);
            }

            public City(Point p)
            {
                location = p;
            }
            public void set_location(int x, int y)
            {
                location.X = x;
                location.Y = y;
            }



            public Point getLocation()
            {
                return location;
            }
        }

        private List<Ant> ant_list;
        public static List<City> city_list;
        private List<City> rotation_city_list;

        public static List<int> best_tour_list;
        private double best_tour_length = -1;
        private double[,] distances;
        private double[,] pheromones;
        private int counter = 0;
        private int num_cities = 10;
        private int num_ants = 30;
        private int pherom_const = 100;
        private double ALPHA = 1.0;//weight of pheromone
        private double BETA = 1.0;//weight of distance
        private double RHO = .5;//decay rate
        private int iterations = 100;
        private int click_counter = 0;



        private Random rand_gen = new Random();
        public MainPage()
        {
            InitializeComponent();

            num_cities = 5;
            num_ants = 30;
            city_list = new List<City>();
            ant_list = new List<Ant>();
            best_tour_list = new List<int>();
            initialize_AntColony();


        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            initAnts();
            Calculate();
           // beecolony();

        }


        private void ant_colony_s_path_Load(object sender, EventArgs e)
        {
        


            num_cities = 5;
            num_ants = 30;
            city_list = new List<City>();
            ant_list = new List<Ant>();
            best_tour_list = new List<int>();
        }


        /******************************************************************
         * Draw_button_Click: initiate ant colony method
         ******************************************************************/
      


        /******************************************************************
         * DrawingPanel_MouseClick: click generates city
         ******************************************************************/


        /******************************************************************
         * random_cities_button_Click: user wants to get random cities
         ******************************************************************/
        private void random_cities_button_Click(object sender, EventArgs e)
        {
            initialize_AntColony();
        }

        /******************************************************************
         * define_cities_Button_Click: user wants to click/select cities
         ******************************************************************/
        private void define_cities_Button_Click(object sender, EventArgs e)
        {
            num_cities = 5;
          
        }


        /********************************************************************
         * initialize_AntColony: intialize values for ant colony
         ********************************************************************/
        private async void initialize_AntColony()
        {
            ant_list = new List<Ant>();
            best_tour_list = new List<int>();
            best_tour_length = -1;
            num_cities = 5;
            num_ants = 30;
            distances = new double[num_cities, num_cities];
            pheromones = new double[num_cities, num_cities];
           await initCities().ConfigureAwait(false);
            initAnts();
            initPherom();
        }
        /********************************************************************
         * initAnts: add ants to random starting position
         ********************************************************************/
        private void initAnts()
        {
            int rand_city = 0;
            ant_list.Clear();
            num_ants = 30;
            for (int i = 0; i < num_ants; i++)
            {
                rand_city = rand_gen.Next(0, num_cities);
                ant_list.Add(new Ant(rand_city, num_cities));//random start location
                ant_list[i].tourList[0] = ant_list[i].get_current_location();//set tour's first position as current location
                ant_list[i].haveBeenList[ant_list[i].get_current_location()] = 1;//set to 1 to designate that we went to this city
                ant_list[i].tour_number = 1;
            }
        }
        /********************************************************************
         * initCities: add cities with random staring positions
         *             //can be changed to allow user input
         ********************************************************************/
        private async Task initCities()
        {


            StaticClass.Instance.IsBusy = true;
            StaticClass.Instance.IsBusy1 = false;

            distances = new double[num_cities, num_cities];
            //initialize cities to random positions
           
                city_list = new List<City>();
            Task<Location> location = getLocation();
           // city_list.Add(new City(new Point(location.Result.Altitude.Value, location.Result.Longitude)));
                  
                     city_list.Add(new City(new Point(38.69301742336234, 35.549143170636405)));
                     city_list.Add(new City(new Point(38.69490153218463, 35.5430599207414)));
                     city_list.Add(new City(new Point(38.69591474353492, 35.539412116502035)));
                     city_list.Add(new City(new Point(38.698024856628976, 35.53226671175525)));
                     city_list.Add(new City(new Point(38.699088263038554, 35.52854380568901)));
            
                 
              






            //compute city distances
            //(n^2-n)/2 == number of connections btwn cities
            for (int i = 0; i < num_cities; i++)
                for (int k = 0; k < num_cities; k++)
                {
                    double x = Math.Pow((double)city_list[i].getLocation().X -
                        (double)city_list[k].getLocation().X, 2.0);
                    double y = Math.Pow((double)city_list[i].getLocation().Y -
                        (double)city_list[k].getLocation().Y, 2.0);
                    DistanceResponseModel model = await ApiServices.ServiceClientInstance.GetDistance(city_list[i].getLocation().X.ToString(), city_list[i].getLocation().Y.ToString(), city_list[k].getLocation().X.ToString(), city_list[k].getLocation().Y.ToString()).ConfigureAwait(false);
                    distances[i, k] = model.Rows.FirstOrDefault().Elements.FirstOrDefault().Distance.Value;
                }
            StaticClass.Instance.IsBusy = false;
            StaticClass.Instance.IsBusy1 = true;

        }
        /**************************************************************************
         * initPherom: initialize pheromone levels btwn cities to  a small constant
         ***************************************************************************/
        private void initPherom()
        {
            for (int from = 0; from < num_cities; from++)
            {
                for (int to = 0; to < num_cities; to++)
                {//initialize all pheromone btwn cities as a small constant
                    pheromones[from, to] = 1.0 / (double)num_cities;
                    pheromones[to, from] = 1.0 / (double)num_cities;
                }
            }
        }



    

        /***********************************************************************
         * backgroundWorker1_RunWorkerCompleted: draw solution based on ACO
         ***********************************************************************/



        /***********************************************************************
         * Calculate: main function that calculates ant movement, 
         *          evaporation and increment of pheromones
         ***********************************************************************/
        private void Calculate()
        {
            ALPHA =1;
            BETA =1;
            RHO =0.5;
            iterations = 100;
            for (int k = 0; k < iterations; k++)
            {
                for (int i = 0; i < num_cities; i++)//move ants till they reach the end
                    if (ants_stop()) //moves ants 1 step & check if all ants finished moving?
                    {
                        evaporatePheromones();
                        updatePheromones();
                        best_tour();//go through every ant and check if it has optimal solution
                        initAnts(); // reset ant position and tour
                    }
            }
        }


        /********************************************************************************************
         * goToNextCity: picks next city based on the attractiveness of the path(the pheromones)
         *              and its visibility (the distance)
         *********************************************************************************************/
        private void goToNextCity(Ant current_ant)
        {
            double sum_prob = 0;//denominator in probability function
            double move_prob = 0;//numerator in probability function
            int current_city = current_ant.get_current_location();
            for (int i = 0; i < num_cities; i++)//loop through all cities
            {
                if (current_ant.haveBeenList[i] == 0)
                {
                    sum_prob += Math.Pow(pheromones[current_city, i], ALPHA) *
                        Math.Pow(1.0 / distances[current_city, i], BETA);
                }
            }

            int destination_city = 0;
            double rand_move = 0;
            int count = 0;
            //loops until ant chooses a city
            while (count < 400)//400 is the threshold for loops
            {
                if (current_ant.haveBeenList[destination_city] == 0)//ant hasnt been to  this city
                {//calculate probability of movement
                    move_prob = (Math.Pow(pheromones[current_city, destination_city], ALPHA) *
                        Math.Pow(1.0 / distances[current_city, destination_city], BETA)) / sum_prob;
                    rand_move = rand_gen.NextDouble();
                    if (rand_move < move_prob) break;//break loop if ant moves to city
                }
                destination_city++;
                if (destination_city >= num_cities) destination_city = 0;//reset city count
                count++;
            }
            //update next location and tour itinerary
            current_ant.set_next_location(destination_city);//going to that city
            current_ant.haveBeenList[destination_city] = 1;//moved to that city
            current_ant.tourList[current_ant.tour_number] = destination_city;
            current_ant.tour_number++;
            //add to current distance
            current_ant.update_total_distance(distances[current_ant.get_current_location(), destination_city]);

            //if the ant reached the end, add up the distance for return path
            if (current_ant.tour_number == num_cities)
            {
                current_ant.update_total_distance(
                    distances[current_ant.tourList[num_cities - 1], current_ant.tourList[0]]);
            }

            current_ant.set_current_location(destination_city);//update current city to next city
        }


        /************************************************************************************
         * ants_stop: checks if all the ants have finished moving/reached final destination
         ************************************************************************************/
        private bool ants_stop()
        {
            int moved = 0;
            for (int i = 0; i < num_ants; i++)
            {
                if (ant_list[i].tour_number < num_cities)
                {//ants are still in moving
                    goToNextCity(ant_list[i]);
                    moved++;
                }
            }
            if (moved == 0)
            {
                return true;//ants have finished moving
            }
            else return false;
        }

        /************************************************************************************
         * evaporatePheromones: decreases current pheromones by a factor of (1.0-RHO) so
         *                   larger values of pheromones would decrease at a higher rate,
         *                   but this is good, since we want the ants to explore more
         ************************************************************************************/
        public void evaporatePheromones()
        {
            //handles both cases of [i,k] and [k,i] so dont need to code pheromones 2x
            for (int i = 0; i < num_cities; i++)
                for (int k = 0; k < num_cities; k++)
                {
                    pheromones[i, k] *= (1.0 - RHO);
                    //pheromone levels should always be at base levels
                    if (pheromones[i, k] < 1.0 / (double)num_cities)
                    {
                        pheromones[i, k] = 1.0 / (double)num_cities;
                    }
                }
        }



        /************************************************************************************
         * updatePheromones: update pheromones along all edges after each ant has completed
         *                  its tour
         ************************************************************************************/
        private void updatePheromones()
        {
            for (int i = 0; i < num_ants; i++)
            {
                for (int k = 0; k < num_cities; k++)
                {
                    int from = ant_list[i].tourList[k];//starting point of edge
                    //if city+1=num_cities, then city is last city and then destination is the starting city
                    int to = ant_list[i].tourList[((k + 1) % num_cities)];//endpoint of edge
                    pheromones[from, to] += (double)pherom_const / ant_list[i].getDistance();
                    pheromones[to, from] = pheromones[from, to];

                }
            }
        }
        /*****************************************************************************
         * best_tour: updates global tour length with shortest tour length
         *****************************************************************************/
        private void best_tour()
        {
            double best_local_tour = ant_list[0].getDistance();
            int save_index = 0;
            for (int i = 1; i < ant_list.Count; i++)//checks the best tour length among this iteration
            {
                if (ant_list[i].getDistance() < best_local_tour)
                {
                    best_local_tour = ant_list[i].getDistance();
                    save_index = i;
                }
            }
            //compare best local length with global length and update accordingly
            if (best_local_tour < best_tour_length || best_tour_length == -1)
            {
                best_tour_list = ant_list[save_index].tourList;
                best_tour_length = best_local_tour;
                lbl.Text = best_tour_length.ToString()+"    ";
                for (int i = 0; i < best_tour_list.Count; i++)
                {
                    lbl.Text += best_tour_list[i].ToString()+"-";
                }
            }
        }
        private async Task<Location> getLocation()
        {
            Location location = null;
            try
            {
                location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
            return location;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new MapPage());





        }
        //-----------------------------------------------------------------------------------------------------artifical bee colony
        public static List<int> siralama = new List<int>();
        public static Point[] points = new Point[5];

        private void beecolony()
        {
            List<City> c = new List<City>(5); // = new CityInfo(int.Parse(this.textBox1.Text));


            c.Add(new City(new Point(38.69301742336234, 35.549143170636405)));
            c.Add(new City(new Point(38.69490153218463, 35.5430599207414)));
            c.Add(new City(new Point(38.69591474353492, 35.539412116502035)));
            c.Add(new City(new Point(38.698024856628976, 35.53226671175525)));
            c.Add(new City(new Point(38.699088263038554, 35.52854380568901)));



            int maxNumVisits = c.Count() * 5;
            int bees = 200;


            Hive beehive = new Hive(bees, 20,
                150, 30,
              (int)Math.Pow(city_list.Count, 2), maxNumVisits, c);


            for (int i = 0; i < (int)Math.Pow(city_list.Count, 2); i++)
            {                
               points= beehive.Solve();
          
            }
            for (int i = 0; i <points.Length; i++)
            {
                for (int j = 0; j < c.Count; j++)
                {
                    if (c[j].getLocation()==points[i])
                    {
                        siralama.Add(j+1);
                    }
                }
            }
            
           
        }

    }



}
