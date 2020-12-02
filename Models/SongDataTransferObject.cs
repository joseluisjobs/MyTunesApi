namespace MyTunesAPI.Models
{
    public class SongDataTransferObject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Artist { get; set; }

        public int Price { get; set; }

        public int Duration { get; set; }

        public int Popularity { get; set; }

        public int AlbumId { get; set; }
    }
}