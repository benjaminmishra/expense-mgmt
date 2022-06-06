using System;
namespace Api.Models
{
	public class ExpenseDetailsDto
	{
		public int Id { get; set; }
		public string Currency { get; set; }
		public int CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime ModifiedOn { get; set; }
		public List<BillDto> Bills { get; set; }
	}

	public class BillDto
	{
		public int Id { get; set; }
		public string Serial { get; set; }
		public int Amount { get; set; }
		public DateTime UploadedOn { get; set; }
		public DateTime IncurredOn { get; set; }
		public string Reason { get; set; }
	}
}

