namespace PersonelServisTakip.Models.Entity
{
	public class Department
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public ICollection<Personel>? Personels { get; } = new List<Personel>();
	}
}