namespace DAL.Entities
{
    public class CustomMap
    {
        public string Id { get; set; }
        public int MapType { get; set; }
        public int CmapType { get; set; }
        public int FileFormat { get; set; }
        public string FilePath { get; set; }

        public string RequestId { get; set; }
        public Request Request { get; set; }
    }
}
