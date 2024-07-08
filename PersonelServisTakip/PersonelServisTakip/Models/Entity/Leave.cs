namespace PersonelServisTakip.Models.Entity
{
	public class Leave
	{
		public int Id { get; set; }
		public int PersonelId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Reason { get; set; }
		public virtual Personel? Personel { get; set; }
	}
}