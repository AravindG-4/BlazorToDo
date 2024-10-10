namespace MongoAuth.Client
{
    public class ToDoHelper
    {
        public string Title { get; set; }
        public bool IsDone { get; set; }

        public ToDoHelper(string Title)
        {
            this.Title = Title;
            this.IsDone = false;
        }

    }
}
