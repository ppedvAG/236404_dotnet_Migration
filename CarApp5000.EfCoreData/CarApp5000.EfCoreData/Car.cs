
namespace CarApp5000.EfCoreData
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public int KW { get; set; }
        public DateTime BuildDate { get; set; }
        public virtual Manufacturer? Manufacturer { get; set; }
    }
}
