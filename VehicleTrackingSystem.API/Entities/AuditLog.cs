using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountManagement.API.Entities
{
    [Table("AuditLogs")]
	public class AuditLog
	{
		[Key, Identity]
		public int Id { get; set; }
		public string Message { get; set; }
		public string MessageTemplate { get; set; }
		public string Level { get; set; }
		public DateTime? TimeStamp { get; set; } = DateTime.Now;
		[StringLength(20)]
		public string Exception { get; set; }
		public string Properties { get; set; }
		public string LogEvent { get; set; }
		public string RequestBody { get; set; }
		public string ResponseBody { get; set; }
		public string RequestPath { get; set; }
		[StringLength(50)]
		public string MachineName { get; set; }
		[StringLength(50)]
		public string ClientIp { get; set; }
		[StringLength(10)]
		public string RequestMethod { get; set; }
		[StringLength(10)]
		public string ResponseCode { get; set; }
		[StringLength(150)]
		public string Description { get; set; }

		[MaxLength(20)]
		public string ClientId { get; set; }

		[MaxLength(20)]
		public string CorrelationId { get; set; }
	}
}
