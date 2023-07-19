using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApplication1.Controllers;

namespace WebApplication1.Models{

    [Table(name: "SwiftMessage")]
    public class SwiftMessage{
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        //Header
        [Required]
        public string Field1 { get; set; }
        //Type
        [Required]
        public string Field2 { get; set; }

        [ForeignKey("SwiftMessageContent")]
        [JsonIgnore]
        public int Field4Id { get; set; }
        public SwiftMessageContent? Field4 { get; set; }

        [ForeignKey("SwiftMessageChecksum")]
        [JsonIgnore]
        public int Field5Id { get; set; }
        public SwiftMessageChecksum Field5 { get; set; }
    }



    [Table(name: "SwiftMessageContent")]
    public class SwiftMessageContent{
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        // Transaction Reference Number
        public string Field20 { get; set; }
        // Related Reference
        public string Field21 { get; set; }
        //Narrative
        public string Field79 { get; set; }

        [ForeignKey("SwiftMessage")]
        [JsonIgnore]
        public int SwiftMessageId { get; set; }
        [JsonIgnore]
        public SwiftMessage SwiftMessage { get; set; }
    }



    [Table(name: "SwiftMessageChecksum")]
    public class SwiftMessageChecksum{
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        //Message Authentication Code
        [Required]
        public string MAC { get; set; }
        // Checksum
       [Required]
        public string CHK { get; set; }

        [ForeignKey("SwiftMessage")]
        [JsonIgnore]
        public int SwiftMessageId { get; set; }
        [JsonIgnore]
        public SwiftMessage SwiftMessage { get; set; }
    }
}

