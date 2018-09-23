namespace DataAccess.Entities.PinnedPhotos
{
    public class PhotoAnnotation
    {
        public int ID { get; set; }
        public string Tag { get; set; }
        public virtual PinnedPhoto PinnedPhoto { get; set; }
        public int PinnedPhotoID { get; set; }
    }
}
