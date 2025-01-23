namespace ToDoList.Server.Business
{
    public class ToDoTask
    {

        public ToDoTask() { }
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool Completed { get; set; }
    }
}
