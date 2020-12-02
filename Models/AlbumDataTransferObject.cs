using System;

namespace MyTunesAPI.Models
{
    public class AlbumDataTransferObject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Artist { get; set; }

        public int Price { get; set; }

        public string Genre { get; set; }

        public int Score { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}