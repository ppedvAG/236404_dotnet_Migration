using CarApp5000.EfCoreData;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace CarApp5000.UI.Desktop
{
    /// <summary>
    /// Interaction logic for CarView.xaml
    /// </summary>
    public partial class CarView : UserControl
    {
        string conString = "Server=(localdb)\\mssqllocaldb;Database=CarApp5000;Trusted_Connection=true;";
        EfContext con;
        ObservableCollection<Car> carList = new ObservableCollection<Car>();

        public CarView()
        {
            InitializeComponent();
            con = new EfContext(conString);
            myGrid.ItemsSource = carList;
        }
        private void DemoDatenErstellen(object sender, RoutedEventArgs e)
        {
            var con = new EfContext(conString);
            con.Database.EnsureCreated();

            var ddg = new DemoDataGenerator();
            con.AddRange(ddg.GenerateRealisticManufacturersAndCars());
            con.SaveChanges();
        }

        private void Laden(object sender, RoutedEventArgs e)
        {
            //myGrid.ItemsSource = con.Cars.ToList();
            //myGrid.ItemsSource = con.Cars.Where(x => x.KW > 10).OrderBy(x => x.BuildDate.Month).ToList();
            carList.Clear();

            var query = con.Cars.Where(x => x.KW > 10)
                                .OrderBy(x => x.BuildDate.Month)
                                .ToList();

            foreach (var car in query)
            {
                carList.Add(car);
            }
        }

        private void Speichern(object sender, RoutedEventArgs e)
        {
            con.SaveChanges();
        }

        private void Neu(object sender, RoutedEventArgs e)
        {
            var car = new Car() { Model = "Brumm Brumm", KW = 9000, BuildDate = DateTime.Now.AddDays(-20) };
            con.Cars.Add(car);
            carList.Add(car);
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (myGrid.SelectedItem is Car car)
            {
                var dlg =  MessageBox.Show($"Soll das Auto {car.Model} gelöscht werden?", "Löschen",MessageBoxButton.YesNo);

                if(dlg == MessageBoxResult.Yes)
                {
                    con.Cars.Remove(car);
                    carList.Remove(car);
                }
            }
        }
    }
}
