namespace LAB7
{
    public class CommentAttribute : Attribute
    {
        public string Comment { get; set; }
        public CommentAttribute(string comment)
        {
            Comment = comment;
        }
    }
}
