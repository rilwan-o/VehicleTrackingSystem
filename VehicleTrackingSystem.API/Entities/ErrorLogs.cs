using MicroOrm.Dapper.Repositories.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleTrackingSystem.API.Entities
{
    [Table("ErrorLogs")]
    public class ErrorLogs
    {
        [Key, Identity]
        public int Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        [MaxLength(128)]
        public string Level { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; }
        [MaxLength(20)]
        public string ClientId { get; set; }

        [MaxLength(20)]
        public string CorrelationId { get; set; }
    }
}
