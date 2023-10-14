namespace BAL.DTOs
{
    public class CarMakeListDTO
    {
        public List<CarModelListDTO> CarModels = new();
        public int MakeID { get; set; }
        public string? Name { get; set; }
        public string? ArabicName { get; set; }
        public string? ImagePath { get; set; }
    }
}
