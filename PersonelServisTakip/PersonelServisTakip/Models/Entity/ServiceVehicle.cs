using Microsoft.Extensions.Hosting;

namespace PersonelServisTakip.Models.Entity
{
	public class ServiceVehicle
	{
		public int Id { get; set; }
		public string VehicleNumber { get; set; }
		public string DriverName { get; set; }
		public DateTime ServiceDate { get; set; }
		public ICollection<Personel>? Personels { get; } = new List<Personel>();
	}
}