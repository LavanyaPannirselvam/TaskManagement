namespace TaskManagementApplication.Model
{
    public class Notification
    {
        private static int notificationID = 1;
        public Notification(string message) 
        {
            Id = notificationID++;
            Message = message;
        }
        public int Id { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return string.Format($"Id : {this.Id} \tMessage : {this.Message}");
        }
        public string ToStringWithoutId()
        {
            return this.Message;
        }
    }
}
