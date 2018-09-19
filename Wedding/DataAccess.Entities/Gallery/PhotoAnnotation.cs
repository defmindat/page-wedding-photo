namespace DataAccess.Entities.Gallery
{
    public class PhotoAnnotation
    {
        public int ID { get; set; }
        public string Tag { get; set; }
        public virtual Photo Photo { get; set; }
        public int PhotoID { get; set; }
    }
}
