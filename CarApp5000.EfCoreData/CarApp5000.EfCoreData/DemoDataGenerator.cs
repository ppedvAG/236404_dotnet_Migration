
namespace CarApp5000.EfCoreData
{
    public class DemoDataGenerator
    {
        public List<Manufacturer> GenerateRealisticManufacturersAndCars()
        {
            var manufacturers = new List<Manufacturer>
            {
                new Manufacturer {  Name = "Toyota", City = "Toyota City" },
                new Manufacturer {  Name = "Ford", City = "Dearborn" },
                new Manufacturer {  Name = "Volkswagen", City = "Wolfsburg" },
                new Manufacturer {  Name = "Honda", City = "Tokyo" },
                new Manufacturer {  Name = "BMW", City = "Munich" }
            };

            var cars = new List<Car>
            {
                new Car { Model = "Corolla", KW = 97, BuildDate = new DateTime(2020, 1, 1), Manufacturer = manufacturers[0] },
                new Car { Model = "F-150", KW = 250, BuildDate = new DateTime(2021, 5, 15), Manufacturer = manufacturers[1] },
                new Car { Model = "Golf", KW = 110, BuildDate = new DateTime(2019, 3, 10), Manufacturer = manufacturers[2] },
                new Car { Model = "Civic", KW = 104, BuildDate = new DateTime(2022, 7, 20), Manufacturer = manufacturers[3] },
                new Car { Model = "3 Series", KW = 135, BuildDate = new DateTime(2018, 2, 28), Manufacturer = manufacturers[4] }
            };

            // Assigning cars to their respective manufacturers
            foreach (var car in cars)
            {
                car.Manufacturer?.Cars.Add(car);
            }

            return manufacturers;
        }
    }

}
