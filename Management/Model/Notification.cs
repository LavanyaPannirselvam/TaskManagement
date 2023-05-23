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
            return this.Message+"\n";
        }
    }
}
