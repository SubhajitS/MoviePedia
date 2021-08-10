using System;
using System.Collections.Generic;

namespace Entities
{
    public class Movie
    {
        public string Language { get; set; }
        public string Location { get; set; }
        public string  Plot { get; set; }
        public string Poster { get; set; }
        public IEnumerable<string> SoundEffects { get; set; }
        public IEnumerable<string> Stills { get; set; }
        public string Title { get; set; }
        public string ListingType { get; set; }
        public decimal Rating { get; set; }
        public string ID { get; set; }
    }
}
