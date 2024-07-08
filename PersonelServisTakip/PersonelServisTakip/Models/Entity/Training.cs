namespace PersonelServisTakip.Models.Entity
{
	public class Training
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime TrainingDate { get; set; }
		public ICollection<Personel>? Attendees { get; } = new List<Personel>();
	}
}