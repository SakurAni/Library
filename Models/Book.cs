
namespace SakurAni_Lib.Models {
    using System.Collections.Generic;
    public class Book {
        public int Isbn {get; set;}
        public string Title {get; set;}
        public double Price {get; set;}
        public IEnumerable<string> Author {get; set;}
        public string Picture {get; set;}
        public int SeriesNumber {get; set;}
        public string Currency {get; set;}
        public string Type {get; set;}
        public IEnumerable<string> Genres {get; set;}
    }
}