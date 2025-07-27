public class TaskItem
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int UserId { get; set; }
    public User User { get; set; }
}
