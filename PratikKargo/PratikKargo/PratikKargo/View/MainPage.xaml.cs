using PratikKargo.Model;
using PratikKargo.Services;
using PratikKargo.View;
using PratikKargo.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public class City
        {
            Point location;
            public int kargoId { get; set; }
            public City()
            {
                location = new Point(0, 0);
                kargoId = -1;
            }

            public City(Point p, int id)
            {
                location = p;
                kargoId = id;
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
        public static List<City> city_list = new List<City>();
        public static List<int> best_tour_list;
        private double best_tour_length = -1;
        private double[,] distances;
        private double[,] pheromones;
        private int num_cities = 10;
        private int num_ants = 30;
        private int pherom_const = 100;
        private double ALPHA = 1.0;//weight of pheromone
        private double BETA = 1.0;//weight of distance
        private double RHO = .5;//decay rate
        private int iterations = 100;



        //***********************************************************************************************************************



        //***********************************************************************************************************************
        private Random rand_gen = new Random();
        public MainPage()
        {
            InitializeComponent();



        }


        protected override async void OnAppearing()
        {
            if (StaticClass.AllCargo.Count < 1 && StaticClass.Completed.Count < 1)
            {
                StaticClass.Instance.IsBusy = true;
                StaticClass.Instance.IsBusy1 = false;

                List<CargoDistance> distancess;

                FirebaseHelper firebaseHelper = new FirebaseHelper();
                var allCargo = await firebaseHelper.GetAllCargo().ConfigureAwait(false);
                distancess = new List<CargoDistance>();
                Random r = new Random();
                int index = r.Next(0, allCargo.Count);
                for (int i = 0; i < allCargo.Count; i++)

                {
                    if (i == index)
                    {
                        continue;
                    }

                    distancess.Add(new CargoDistance
                    {
                        adress = allCargo[i].Adress,
                        namesurname = allCargo[i].NameSurname,
                        number = allCargo[i].PhoneNumber,
                        Distance = "500m",
                        KargoId = allCargo[i].KargoId,
                        X = allCargo[i].X,
                        Y = allCargo[i].Y,
                    });
                }
                distancess.Add(new CargoDistance
                {
                    adress = allCargo[index].Adress,
                    number = allCargo[index].PhoneNumber,
                    namesurname = allCargo[index].NameSurname,
                    KargoId = allCargo[index].KargoId,
                    Distance = "0m",
                    X = allCargo[index].X,
                    Y = allCargo[index].Y,
                });
                var veri = distancess.Select(n => new double[] { double.Parse(n.X, System.Globalization.CultureInfo.InvariantCulture), double.Parse(n.Y, System.Globalization.CultureInfo.InvariantCulture) }) // her bir satır için double dizi dön
                .ToArray();

                ObservableCollection<CargoMain> tempmodel = new ObservableCollection<CargoMain>();

                var kumeler = KOrtalamalar(veri, 3);
                for (int i = 0; i < distancess.Count; i++)
                {
                    if (kumeler[i] == 1)
                    {
                        tempmodel.Add(new CargoMain { address = distancess[i].adress, distance = distancess[i].Distance, NameSurname = distancess[i].namesurname, phoneNumber = distancess[i].number, X = distancess[i].X, Y = distancess[i].Y, kargoId = distancess[i].KargoId });
                        city_list.Add(new City(new Point(Double.Parse(distancess[i].X, System.Globalization.CultureInfo.InvariantCulture), Double.Parse(distancess[i].Y, System.Globalization.CultureInfo.InvariantCulture)), distancess[i].KargoId));

                    }
                }

                //distancess = distancess.OrderBy(a => a.Distance).Take(5).ToList();
                //foreach (var item in distancess)
                //{
                //    tempmodel.Add(new CargoMain { address = item.adress, distance = item.Distance, NameSurname = item.namesurname, phoneNumber = item.number, X = item.X, Y = item.Y, kargoId = item.KargoId });
                //    city_list.Add(new City(new Point(Double.Parse(item.X, System.Globalization.CultureInfo.InvariantCulture), Double.Parse(item.Y, System.Globalization.CultureInfo.InvariantCulture)), item.KargoId));
                //}

                ant_list = new List<Ant>();
                best_tour_list = new List<int>();
                await initialize_AntColony().ConfigureAwait(false);
                num_cities = city_list.Count;
                num_ants = 30;
                for (int i = 0; i < best_tour_list.Count; i++)
                {
                    int nokta = best_tour_list[i];
                    int kargoId = city_list[nokta].kargoId;
                    var temp = tempmodel.FirstOrDefault(a => a.kargoId == kargoId);
                    StaticClass.AllCargo.Add(temp);
                }

                StaticClass.Instance.IsBusy = false;
                StaticClass.Instance.IsBusy1 = true;
                beecolony();            }



        }
        //***********************************************************************************************************************************
        static IList<int> KOrtalamalar(double[][] veri, int kumeSayisi)
        {
            var random = new Random(5555);
            // Her satırı rastgele bir kümeye ata
            var sonucKumesi = Enumerable
                                    .Range(0, veri.Length)
                                    .Select(index => (AtananKume: random.Next(0, kumeSayisi),
                                                  Degerler: veri[index]))
                                    .ToList();

            var boyutSayisi = veri[0].Length;
            var limit = 10000;
            var guncellendiMi = true;
            while (--limit > 0)
            {
                // kümelerin merkez noktalarını hesapla
                var merkezNoktalar = Enumerable.Range(0, kumeSayisi)
                                                .AsParallel()
                                                .Select(kumeNumarasi =>
                                                (
                                                kume: kumeNumarasi,
                                                merkezNokta: Enumerable.Range(0, boyutSayisi)
                                                                                    .Select(eksen => sonucKumesi.Where(s => s.AtananKume == kumeNumarasi)
                                                                                    .Average(s => s.Degerler[eksen]))
                                                                                    .ToArray())
                                                        ).ToArray();
                // Sonuç kümesini merkeze en yakın ile güncelle
                guncellendiMi = false;
                //for (int i = 0; i < sonucKumesi.Count; i++)
                Parallel.For(0, sonucKumesi.Count, i =>
                {
                    var satir = sonucKumesi[i];
                    var eskiAtananKume = satir.AtananKume;


                    var yeniAtananKume = merkezNoktalar.Select(n => (KumeNumarasi: n.kume,
                                                                    Uzaklik: UzaklikHesapla(satir.Degerler, n.merkezNokta)))
                                         .OrderBy(x => x.Uzaklik)
                                         .First()
                                         .KumeNumarasi;

                    if (yeniAtananKume != eskiAtananKume)
                    {
                        sonucKumesi[i] = (AtananKume: yeniAtananKume, Degerler: satir.Degerler);
                        guncellendiMi = true;
                    }
                });

                if (!guncellendiMi)
                {
                    break;
                }
            } // while

            return sonucKumesi.Select(k => k.AtananKume).ToArray();
        }

        static double UzaklikHesapla(double[] birinciNokta, double[] ikinciNokta)
        {
            var kareliUzaklik = birinciNokta
                                    .Zip(ikinciNokta,
                                        (n1, n2) => Math.Pow(n1 - n2, 2)).Sum();
            return Math.Sqrt(kareliUzaklik);
        }



        /********************************************************************
         * initialize_AntColony: intialize values for ant colony
         ********************************************************************/
        private async Task initialize_AntColony()
        {

            ant_list = new List<Ant>();
            best_tour_list = new List<int>();
            best_tour_length = -1;
            num_cities = city_list.Count;
            num_ants = 30;
            distances = new double[num_cities, num_cities];
            pheromones = new double[num_cities, num_cities];
            await initCities().ConfigureAwait(false);
            await initAnts().ConfigureAwait(false);
            await initPherom().ConfigureAwait(false);
            //   await initAnts().ConfigureAwait(false);
            await Calculate().ConfigureAwait(false);
        }
        /********************************************************************
         * initAnts: Karıncalar rastgele başlangıç pozisyonlarına eklenir
         ********************************************************************/
        private async Task initAnts()
        {
            int rand_city = 0;
            ant_list.Clear();
            num_ants = 30;
            for (int i = 0; i < num_ants; i++)
            {
                rand_city = rand_gen.Next(0, num_cities);
                ant_list.Add(new Ant(rand_city, num_cities));//rastgele başlangıç ​​konumu
                ant_list[i].tourList[0] = ant_list[i].get_current_location();//geçerli konum olarak turun ilk konumunu ayarla
                ant_list[i].haveBeenList[ant_list[i].get_current_location()] = 1;//bu şehre gittiğimizi belirtiyoruz
                ant_list[i].tour_number = 1;
            }
        }


        /********************************************************************
         * initCities: noktalar ekleniyor
         ********************************************************************/
        private async Task initCities()
        {

            distances = new double[num_cities, num_cities];
            // Task<Location> location = getLocation();



            //noktalar arası uzaklıklar google distance matrix api kullanarak alınıyor
            for (int i = 0; i < num_cities; i++)
                for (int k = 0; k < num_cities; k++)
                {
                    //double x = Math.Pow((double)city_list[i].getLocation().X -
                    //    (double)city_list[k].getLocation().X, 2.0);
                    //double y = Math.Pow((double)city_list[i].getLocation().Y -
                    //    (double)city_list[k].getLocation().Y, 2.0);
                    DistanceResponseModel model = await ApiServices.ServiceClientInstance.GetDistance(city_list[i].getLocation().X.ToString(),
                        city_list[i].getLocation().Y.ToString(), city_list[k].getLocation().X.ToString(), city_list[k].getLocation().Y.ToString()).ConfigureAwait(false);
                    distances[i, k] = model.Rows.FirstOrDefault().Elements.FirstOrDefault().Distance.Value;
                }
            StaticClass.Instance.IsBusy = false;
            StaticClass.Instance.IsBusy1 = true;

        }


        /**************************************************************************
         * initPherom: noktalar arasındaki feromon seviyelerini küçük bir sabite başlıyor
         ***************************************************************************/
        private async Task initPherom()
        {
            for (int from = 0; from < num_cities; from++)
            {
                for (int to = 0; to < num_cities; to++)
                {
                    pheromones[from, to] = 1.0 / (double)num_cities;
                    pheromones[to, from] = 1.0 / (double)num_cities;
                }
            }
        }




        /***********************************************************************
         * Calculate: karınca hareketini, feromonların buharlaşmasını ve artışını hesaplayan ana fonksiyon
         ***********************************************************************/
        private async Task Calculate()
        {
            ALPHA = 1;
            BETA = 1;
            RHO = 0.5;
            iterations = 100;
            for (int k = 0; k < iterations; k++)
            {
                for (int i = 0; i < num_cities; i++)//karıncalar noktaların hepsini dolanana kadar devam ediyor
                    if (ants_stop()) //karıncalar turu tamamlayana hareket ediyor ve kontrol ediliyor
                    {
                        evaporatePheromones();
                        updatePheromones();
                        best_tour();//en optimum çözüme sahip olup olmadığı kontrol ediliyor
                        await initAnts(); // karınca pozisyonları ve turları sıfırlanıyor
                    }
            }

        }


        /********************************************************************************************
         * goToNextCity: yolun feromon seviyesi ve mesafeye göre bir sonraki şehri seç.
         *********************************************************************************************/
        private void goToNextCity(Ant current_ant)
        {
            double sum_prob = 0;//olasılık fonksiyonunda payda
            double move_prob = 0;
            int current_city = current_ant.get_current_location();
            for (int i = 0; i < num_cities; i++)
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
            //karınca bir şehir seçene kadar döngü devam ediyor 400 eşik değeri 
            while (count < 400)
            {
                if (current_ant.haveBeenList[destination_city] == 0)//karınca şehre gelmediyse hareket olasılığını hesapla
                {
                    move_prob = (Math.Pow(pheromones[current_city, destination_city], ALPHA) *
                        Math.Pow(1.0 / distances[current_city, destination_city], BETA)) / sum_prob;
                    rand_move = rand_gen.NextDouble();
                    if (rand_move < move_prob) break;
                }
                destination_city++;
                if (destination_city >= num_cities) destination_city = 0;//şehir sayısını geçerse sıfırla
                count++;
            }
            //seçilen şehre göre bilgileri güncelle
            current_ant.set_next_location(destination_city);
            current_ant.haveBeenList[destination_city] = 1;
            current_ant.tourList[current_ant.tour_number] = destination_city;
            current_ant.tour_number++;

            current_ant.update_total_distance(distances[current_ant.get_current_location(), destination_city]);

            //karınca bütün noktaları gezdi ise geri dönüş uzaklıgını da ekle 
            if (current_ant.tour_number == num_cities)
            {
                current_ant.update_total_distance(
                    distances[current_ant.tourList[num_cities - 1], current_ant.tourList[0]]);
            }

            current_ant.set_current_location(destination_city);
        }


        /************************************************************************************
         * ants_stop: tüm karıncalar hedefe ulaştı mı kontrol et
         ************************************************************************************/
        private bool ants_stop()
        {
            int moved = 0;
            for (int i = 0; i < num_ants; i++)
            {
                if (ant_list[i].tour_number < num_cities)
                {//hangi karınca turu bitirmediyse bitirmesini sağla
                    goToNextCity(ant_list[i]);
                    moved++;
                }
            }
            if (moved == 0)
            {
                return true;
            }
            else return false;
        }

        /************************************************************************************
         * evaporatePheromones: feromonları fenomon bozulma hızına(rho) göre azalt
         ************************************************************************************/
        public void evaporatePheromones()
        {

            for (int i = 0; i < num_cities; i++)
                for (int k = 0; k < num_cities; k++)
                {
                    pheromones[i, k] *= (1.0 - RHO);
                    //fenomon başlangıç seviyesinin altına inemez
                    if (pheromones[i, k] < 1.0 / (double)num_cities)
                    {
                        pheromones[i, k] = 1.0 / (double)num_cities;
                    }
                }
        }



        /************************************************************************************
         * updatePheromones: karıncalar turu tamamladıkca noktaların fenomonlarını güncelle
         ************************************************************************************/
        private void updatePheromones()
        {
            for (int i = 0; i < num_ants; i++)
            {
                for (int k = 0; k < num_cities; k++)
                {
                    int from = ant_list[i].tourList[k];
                    int to = ant_list[i].tourList[((k + 1) % num_cities)];//k+1 number nokta sayısına eşitse yeni hedef başlangıc noktası oldugu için başlangıca olan fenomon güncellenir
                    pheromones[from, to] += (double)pherom_const / ant_list[i].getDistance();
                    pheromones[to, from] = pheromones[from, to];

                }
            }
        }
        /*****************************************************************************
         * best_tour: global tur uzunluğunu en kısa tur uzunluğu ile güncelle
         *****************************************************************************/
        private void best_tour()
        {
            double best_local_tour = ant_list[0].getDistance();
            int save_index = 0;
            for (int i = 1; i < ant_list.Count; i++)
            {
                if (ant_list[i].getDistance() < best_local_tour)
                {
                    best_local_tour = ant_list[i].getDistance();
                    save_index = i;
                }
            }
            //en iyi yerel uzunlugu globalle kıyasla
            if (best_local_tour < best_tour_length || best_tour_length == -1)
            {
                best_tour_list = ant_list[save_index].tourList;
                best_tour_length = best_local_tour;
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


            c.Add(new City(new Point(38, 35),1));
            c.Add(new City(new Point(343, 335),2));
            c.Add(new City(new Point(438, 315),3));
            c.Add(new City(new Point(328, 356),4));
            c.Add(new City(new Point(380, 935),5));
          



            int maxNumVisits = c.Count() * 5;
            int bees = 200;


            Hive beehive = new Hive(bees, 20,
                150, 30,
              (int)Math.Pow(c.Count, 2), maxNumVisits, c);


            for (int i = 0; i < (int)Math.Pow(c.Count, 2); i++)
            {
                points = beehive.Solve();

            }
            for (int i = 0; i < points.Length; i++)
            {
                for (int j = 0; j < c.Count; j++)
                {
                    if (c[j].getLocation() == points[i])
                    {
                        siralama.Add(j + 1);
                    }
                }
            }


        }

    }



}
