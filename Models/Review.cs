using System;

public class Review
{
    public int Id { get; set; }
    public int PhoneId { get; set; }
    public string Username { get; set; }
    public string Content { get; set; }
    public int Rating { get; set; }
    public DateTime DateCreated { get; set; }
}