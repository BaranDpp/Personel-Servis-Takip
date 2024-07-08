namespace PersonelServisTakip.Models.Entity
{
    public class Personel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? Photo { get; set; }
        public int ServiceVehicleId { get; set; } // Required foreign key property
        public virtual ServiceVehicle? ServiceVehicle { get; set; } = null!; // Required reference navigation to principal

        // Yeni eklenen alanlar
        public int DepartmentId { get; set; } // Foreign key for Department

        public virtual Department? Department { get; set; } // Navigation property for Department
        public IFormFile PhotoFile { get; set; }
    }
}