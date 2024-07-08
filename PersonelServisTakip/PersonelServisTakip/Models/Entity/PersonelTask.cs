namespace PersonelServisTakip.Models.Entity
{
	public class PersonelTask
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime AssignedDate { get; set; }
		public DateTime? DueDate { get; set; }
		public int PersonelId { get; set; }
		public virtual Personel? Personel { get; set; }
	}
}