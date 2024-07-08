namespace PersonelServisTakip.Models.Entity
{
	public class PerformanceReview
	{
		public int Id { get; set; }
		public int PersonelId { get; set; }
		public DateTime ReviewDate { get; set; }
		public string Comments { get; set; }
		public int Rating { get; set; } // 1 to 5 scale
		public virtual Personel? Personel { get; set; }
	}
}