namespace TaskManagerAPI.models
{
    public class TaskModel
    {
        public int id { get; set; }
        public string? text { get; set; }
        public bool completed { get; set; }
        public DateTime? createdAt { get; set; }
        public string? creator { get; set; }
        public int creatorid { get; set; }
    }
}
