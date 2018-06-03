using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SakurAni_Lib.Models.Database {

    public class DbBook {
        [Key]
        public int Isbn {get; set;}
        public string Title {get; set;}
        public double Price {get; set;}
        public string Picture {get; set;}
        public int SeriesNumber {get; set;}
        public string Currency {get; set;}
        public string Type {get; set;}
    }
}